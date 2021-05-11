using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Kuinox.JsonVisitorGenerator
{
    public class SourceGenerator
    {
        readonly JObject _json;
        readonly GeneratorExecutionContext _context;
        readonly ClassDeclarationSyntax _candidate;
        readonly INamedTypeSymbol _info;
        readonly SourceText _text;
        readonly StringBuilder _s = new();
        public SourceGenerator( GeneratorExecutionContext context, ClassDeclarationSyntax candidate, INamedTypeSymbol symbolInfo, SourceText text )
        {
            string jsonStr = text.ToString();
            _json = JObject.Parse( jsonStr );
            _context = context;
            _candidate = candidate;
            _info = symbolInfo;
            _text = text;
        }

        public string GenerateSource()
        {
            string baseClassName = _info.BaseType.Name; // TODO: emit error.
            bool hasNamespace = string.IsNullOrEmpty( baseClassName );
            _s.AppendLine(
$@"using System.Text.Json;
using System.Collections.Generic;" );
            if( hasNamespace )
            {
                _s.Append( $"namespace {_info.ContainingNamespace.Name}\n{{" );
            }

            _s.Append( @$"    /// <summary>
                /// {_json["title"]}
                /// {_json["description"]}
                /// </summary>
                {SyntaxFacts.GetText( _info.DeclaredAccessibility )} class {baseClassName}
                {{
            " );

            GenerateProperties();

            if( hasNamespace )
            {
                _s.Append( '}' );
            }
            _s.Append( '}' );
            return _s.ToString();
        }

        static string GetBaseTypeReader( JObject property )
            => (property["type"]?.ToString()) switch
            {
                "boolean" => "reader.GetBoolean()",
                "string" => "reader.GetString()",
                "uri" => "new Uri(reader.GetString())",
                "number" => "reader.GetDouble()",
                _ => throw new InvalidOperationException( "Unknown base type or not a valid json type." )
            };

        static bool IsBaseType( JObject property )
            => property["type"]?.ToString() is "boolean" or "string" or "uri" or "number";
        static bool IsArray( JObject property )
            => property["type"]?.ToString() is "array";

        static string CamelToPascal( string name ) => name.Substring( 0, 1 ).ToUpper() + name.Substring( 1 );

        void AppendArrayVisit( JObject property )
        {
            JObject arrayTypeDef = property["items"] as JObject;
            string csType = JSTypeToCS( property["items"] as JObject );

            _s.Append( $@"reader.Read(); //OpenArray
reader.Read(); //First element

while( reader.TokenType != JsonTokenType.EndArray )
{{
" );
            if( IsBaseType( arrayTypeDef ) )
            {
                _s.Append( "reader.Read();" );
            }
            else if( IsArray( arrayTypeDef ) )
            {
                throw new NotImplementedException();
            }
            else
            {
                _s.AppendLine( $"Visit{csType}(reader);" );
            }
            _s.AppendLine( "}" );

        }

        void AppendArrayRead( JObject property )
        {
            JObject arrayTypeDef = property["items"] as JObject;
            string csType = JSTypeToCS( property["items"] as JObject );

            _s.Append( $@"List<{csType}> arr = new List<{csType}>();
reader.Read(); //OpenArray
reader.Read(); //First element

while( reader.TokenType != JsonTokenType.EndArray )
{{
arr.Add(" );
            if( IsBaseType( arrayTypeDef ) )
            {
                _s.Append( GetBaseTypeReader( arrayTypeDef ) );
            }
            else if( IsArray( arrayTypeDef ) )
            {
                throw new NotImplementedException();
            }
            else
            {
                _s.Append( $"Read{csType}()" );
            }
            _s.Append( @");
    reader.Read();
}
return arr;
" );

        }

        void GenerateProperty( JProperty propertyObj )
        {
            if( propertyObj.Name.StartsWith( "$" ) ) return;
            JObject property = (JObject)propertyObj.Value;
            string className = CamelToPascal( propertyObj.Name );
            AppendVisitorDeclaration( property, className, csType );
            AppendReadDeclaration( property, csType, className );
        }

        private void AppendVisitorDeclaration( JObject property, string className, string csType )
        {
            _s.Append( $@"
        /// <summary>
        /// {property["description"]}
        /// </summary>
        protected virtual void Visit{className}(Utf8JsonReader reader)
        {{
" );
            if( IsBaseType( property ) )
            {
                _s.AppendLine( "reader.Read();" );
            }
            else if( IsArray( property ) )
            {
                AppendArrayVisit( property );
            }
            else
            {
                _s.AppendLine( $"Visit{csType}(reader);" );
            }
        }

        void AppendReadDeclaration( JObject property, string csType, string className )
        {
            _s.Append(
    $@"        }}

        /// <summary>
        /// {property["description"]}
        /// </summary>
        /// <returns>The parsed value.</returns>
        protected {csType} Read{className}(Utf8JsonReader reader)
        {{
" );
            if( IsBaseType( property ) )
            {
                _s.AppendLine( $"return {GetBaseTypeReader( property )};" );
            }
            else if( IsArray( property ) )
            {
                AppendArrayRead( property );
            }
            else
            {
                _s.AppendLine( $"Read{csType}(reader);" );
            }
            _s.AppendLine( "}" );
        }

        string JSTypeToCS( JObject jObject, bool useNamespace = true )
        {
            string typeStr = (jObject["type"] ?? jObject["$ref"]).ToString();
            switch( typeStr )
            {
                case "uri":
                    return "Uri";
                case "boolean":
                    return "bool";
                case "string":
                    return "string";
                case "array":
                    return $"IEnumerable<{JSTypeToCS( jObject["items"] as JObject )}>";
                case "object":
                    return "TODO";
                default:
                    if( typeStr[0] == '#' )
                    {
                        string typeName = Path.GetFileName( typeStr );
                        if( useNamespace ) typeName = _info.BaseType.Name + "." + typeName;
                        return typeName;
                    }
                    throw new NotImplementedException();
            };
        }
        void GenerateProperties()
        {
            IEnumerable<JProperty> props = ((JObject)_json["properties"]).Properties();
            foreach( var prop in props )
            {
                GenerateProperty( prop );
            }
        }
    }
}
