using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
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
            _s.Append( $"public class {baseClassName}Visitor{{" );
            //TODO: if subobject get the same name, we will generate uncompillable code.
            //We should check if a subobject got the same name, if it's the case, don't generate and error.
            AppendObjectDef( baseClassName, _json, true );
            _s.Append( '}' );

            if( hasNamespace )
            {
                _s.Append( '}' );
            }
            return _s.ToString();
        }

        void AppendObjectDef( string typeName, JObject schema, bool rootObject )
        {
            string accessLevel = rootObject ? SyntaxFacts.GetText( _info.DeclaredAccessibility ) : "public";
            // nested object won't be more exposed than the root object.
            List<(string propertyName, string propertyType)> properties = GenerateVisitorPart( typeName, schema );

            GenerateObjectFromDefs( typeName, schema, accessLevel, properties );
        }

        private void GenerateObjectFromDefs( string typeName, JObject schema, string accessLevel, List<(string propertyName, string propertyType)> properties )
        {
            _s.Append(
$@"/// <summary>
///{schema["title"]?.ToString().Replace( "\n", "<br>" )}<br>
///{schema["description"]?.ToString().Replace( "\n", "<br>" )}
/// </summary>
{accessLevel} class {typeName} {{" );

            _s.Append( "public " );
            _s.Append( typeName );
            _s.Append( '(' );
            for( int i = 0; i < properties.Count; i++ )
            {
                _s.Append( properties[i].propertyType );
                _s.Append( ' ' );
                _s.Append( PascalToCamel( StringToCName( properties[i].propertyName ) ) );
                if( i + 1 < properties.Count ) _s.Append( ',' );
            }
            _s.Append( "){" );
            foreach( (string propertyName, _) in properties )
            {
                _s.Append( "this." );
                _s.Append( CamelToPascal( StringToCName( propertyName ) ) );
                _s.Append( '=' );
                _s.Append( PascalToCamel( StringToCName( propertyName ) ) );
                _s.Append( ';' );
            }
            _s.Append( '}' );
            foreach( (string propertyName, string propertyType) in properties )
            {
                _s.Append( propertyType );
                _s.Append( ' ' );
                _s.Append( CamelToPascal( StringToCName( propertyName ) ) );
                _s.Append( "{get;}" );
            }
            JObject? defs = ((JObject?)schema["definitions"]);
            if( defs is not null )
            {
                IEnumerable<JProperty> defProps = defs.Properties();
                foreach( JProperty prop in defProps )
                {
                    AppendObjectDef( StringToCName( "#/definitions/" + prop.Name ), prop.Value as JObject, false );
                }
            }
            _s.Append( '}' );
        }

        private List<(string propertyName, string propertyType)> GenerateVisitorPart( string typeName, JObject schema )
        {
            List<(string propertyName, string propertyType)> properties = new();
            AppendVisitAndRead( schema, typeName, false );//generate visit/read of the object itself.
            IEnumerable<JProperty> props = ((JObject)schema["properties"]).Properties();
            foreach( var prop in props ) //and it's properties.
            {
                properties.Add( (prop.Name, JSTypeToCS( prop.Value as JObject, prop.Name )) );
                AppendVisitAndRead( prop.Value as JObject, prop.Name, false );
            }
            void AppendVisitAndRead( JObject propertyDef, string typeName, bool isMainMethod )
            {
                string className = CamelToPascal( typeName );
                AppendVisitOrRead( propertyDef, className, isMainMethod, false );
                AppendVisitOrRead( propertyDef, className, isMainMethod, true );
            }
            return properties;
        }
    }
}
