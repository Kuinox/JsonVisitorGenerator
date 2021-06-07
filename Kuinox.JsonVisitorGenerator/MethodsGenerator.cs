using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kuinox.JsonVisitorGenerator
{
    partial class SourceGenerator
    {
        void AppendMethodsAndSubObjects( JObject schema, string defName )
        {
            GenerateVisitAndReadFromProperty( schema, defName, true );//generate visit/read of the object itself.
            IEnumerable<JProperty> props = ((JObject)schema["properties"]).Properties();
            foreach( var prop in props ) //and it's properties.
            {
                GenerateVisitAndReadFromProperty( prop.Value as JObject, prop.Name, false );
            }
            JObject? defs = ((JObject?)schema["definitions"]);
            if( defs is null ) return;
            IEnumerable<JProperty> defProps = defs.Properties();
            foreach( JProperty prop in defProps )
            {
                AppendObjectDef( prop.Name, prop.Value as JObject, false );
            }
        }



        void GenerateVisitAndReadFromProperty( JObject propertyDef, string defName, bool isMainMethod )
        {
            string className = CamelToPascal( defName );
            AppendMethod( propertyDef, className, isMainMethod, false );
            AppendMethod( propertyDef, className, isMainMethod, true );
        }

        /// <param name="thingBeingRead">Will be used to name the read method.</param>
        void AppendMethod( JObject propertyDef, string thingBeingRead, bool isMainReadMethod, bool isReading )
        {
            string csType = JSTypeToCS( propertyDef, thingBeingRead, true );
            _s.Append( "/// <summary>" );
            _s.Append( propertyDef["description"] );
            _s.Append( "</summary>\n" );
            _s.Append( isMainReadMethod ? "public " : "protected " );
            _s.Append( isReading ? csType : "void" );
            _s.Append( ' ' );
            _s.Append( isReading ? "Read" : "Visit" );
            _s.Append( isMainReadMethod ? "" : thingBeingRead );
            _s.Append( "(Utf8JsonReader reader){" );
            if( IsBaseType( propertyDef ) )
            {
                if( isReading )
                {
                    _s.AppendLine( $"return {GetBaseTypeReaderExpression( propertyDef )};" );
                }
                _s.AppendLine( "reader.Read();" );
            }
            else if( IsArray( propertyDef ) )
            {
                AppendArrayProcessingImplementation( propertyDef, isReading );
            }
            else
            {
                //TODO: add option ignore unknown property.
                //TODO: add checks on property token type.
                _s.Append( @$"
                reader.Read(); // Open Object.
                reader.Read(); // first property.
                while(reader.TokenType != JsonTokenType.EndObject)
                {{
                    string property = reader.GetString();
                    switch(property)
                    {{
                        //TODO.
                        default:
                            throw new InvalidDataException(""Unknown property"");
                    }}
                }}
                throw new NotImplementedException();
" );
            }
            _s.AppendLine( "}" );
        }
    }
}
