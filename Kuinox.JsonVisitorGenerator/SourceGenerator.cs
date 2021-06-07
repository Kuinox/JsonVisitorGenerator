using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Kuinox.JsonVisitorGenerator
{
    public partial class SourceGenerator
    {
        readonly JObject _json;
        readonly GeneratorExecutionContext _context;
        readonly ClassDeclarationSyntax _candidate;
        readonly INamedTypeSymbol _info;
        readonly SourceText _text;
        readonly bool _generateNestedClass;
        readonly StringBuilder _s = new();
        public SourceGenerator(
            GeneratorExecutionContext context,
            ClassDeclarationSyntax candidate,
            INamedTypeSymbol symbolInfo,
            SourceText text,
            bool generateNestedClass )
        {
            string jsonStr = text.ToString();
            _json = JObject.Parse( jsonStr );
            _context = context;
            _candidate = candidate;
            _info = symbolInfo;
            _text = text;
            _generateNestedClass = generateNestedClass;
        }

        public void AppendUsings()
        {
            _s.AppendLine(
$@"using System.Text.Json;
using System.Collections.Generic;
using System.IO;
using System;" );
        }

        public string GenerateSource()
        {
            string baseClassName = _info.BaseType.Name; // TODO: emit error.
            bool hasNamespace = string.IsNullOrEmpty( baseClassName );
            AppendUsings();
            if( hasNamespace )
            {
                _s.Append( $"namespace {_info.ContainingNamespace.Name}\n{{" );
            }
            //TODO: if subobject get the same name, we will generate uncompillable code.
            //We should check if a subobject got the same name, if it's the case, don't generate and error.
            AppendObjectDef( baseClassName, _json, true );

            if( hasNamespace )
            {
                _s.Append( '}' );
            }
            return _s.ToString();
        }

        void AppendObjectDef( string schemaName, JObject schemaDefinition, bool rootObject )
        {
            string accessLevel = rootObject ? SyntaxFacts.GetText( _info.DeclaredAccessibility ) : "public";
            // nested object won't be more exposed than the root object.

            _s.Append( @$"    /// <summary>" );
            _s.Append( schemaDefinition["title"] );
            _s.Append( "<br>" );
            _s.Append( schemaDefinition["description"] );
            _s.Append( "</summary>\n" );
            _s.Append( accessLevel );
            _s.Append( " class " );
            _s.Append( schemaName );
            _s.Append( '{' );
            AppendMethodsAndSubObjects( schemaDefinition, schemaName );
            _s.Append( '}' );
        }
    }
}
