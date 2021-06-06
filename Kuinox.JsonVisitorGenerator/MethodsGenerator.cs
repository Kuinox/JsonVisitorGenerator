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

        void AppendArrayProcessing( JObject arrayDef, string className, bool isReading )
        {
            int recurseCount = 0;
            string DoAppendArrayRead( JObject property, int recurseCount )
            {
                JObject arrayTypeDef = property["items"] as JObject;
                string csType = JSTypeToCS( property["items"] as JObject, null );
                string listName = "array" + recurseCount;
                if( isReading )
                {
                    _s.AppendLine( $"List<{csType}> {listName} = new List<{csType}>();" );
                }
                _s.AppendLine( "reader.Read();" );
                _s.AppendLine( "reader.Read();" );
                _s.AppendLine( $@"while( reader.TokenType != JsonTokenType.EndArray )
{{" );
                if( IsBaseType( arrayTypeDef ) )
                {
                    if( isReading )
                    {
                        _s.AppendLine( $"{listName}.Add({GetBaseTypeReaderExpression( arrayTypeDef )});" );
                    }
                    _s.AppendLine( "reader.Read();" );
                    // When we visit, because it's a single value, it will be skipped
                }
                else if( IsArray( arrayTypeDef ) )
                {
                    //TODO: test this.
                    string newListName = DoAppendArrayRead( arrayTypeDef, recurseCount + 1 );
                    if( isReading )
                    {
                        _s.AppendLine( $"{listName}.Add({newListName})" );
                    }
                }
                else
                {
                    if( isReading )
                    {
                        _s.Append( $"{listName}.Add(" );
                    }
                    _s.Append( GetMethodThatProcess( property["items"] as JObject, isReading ? "Read" : "Visit" ) );
                    if( isReading )
                    {
                        _s.Append( ')' );
                    }
                    _s.AppendLine( ";" );
                }
                _s.Append( '}' );
                return listName;
            }
            string variableName = DoAppendArrayRead( arrayDef, recurseCount );
            _s.AppendLine( $"return {variableName};" );
        }

        void GenerateVisitAndReadFromProperty( JObject definition, string defName, bool isMainMethod )
        {
            string className = CamelToPascal( defName );
            AppendVisitorDeclaration( definition, className, isMainMethod );
            AppendReadDeclaration( definition, className, isMainMethod );
        }

        private void AppendVisitorDeclaration( JObject property, string thingBeingRead, bool isMainReadMethod )
        {
            _s.Append( $@"
        /// <summary>
        /// {property["description"]}
        /// </summary>
        {(isMainReadMethod ? "public" : "protected")} virtual void Visit{(isMainReadMethod ? "" : thingBeingRead)}(Utf8JsonReader reader)
        {{
" );
            if( IsBaseType( property ) )
            {
                _s.AppendLine( "reader.Read();" );
            }
            else if( IsArray( property ) )
            {
                AppendArrayVisit( property, thingBeingRead );
            }
            else
            {
                _s.AppendLine( $"Visit{thingBeingRead}(reader);" );
            }
        }

        /// <param name="thingBeingRead">Will be used to name the read method.</param>
        void AppendReadDeclaration( JObject property, string thingBeingRead, bool isMainReadMethod )
        {
            string csType = JSTypeToCS( property, thingBeingRead, true );
            _s.Append(
        $@"        }}
        /// <summary>
        /// {property["description"]}
        /// </summary>
        /// <returns>The parsed value.</returns>
        {(isMainReadMethod ? "public" : "protected")} {csType} Read{(isMainReadMethod ? "" : thingBeingRead)}(Utf8JsonReader reader)
        {{
" );
            if( IsBaseType( property ) )
            {
                _s.AppendLine( $"return {GetBaseTypeReaderExpression( property )};" );
            }
            else if( IsArray( property ) )
            {
                AppendArrayRead( property, thingBeingRead );
            }
            else
            {
                //TODO: add option ignore unknown property.
                //TODO: add checks on property token type.
                _s.Append( @$"
                reader.Read(); // Open Object.
                reader.Read(); // first property.
                while(reader.TokenType != JsonTokenType.EndArray)
                {{
                    string property = reader.GetString();
                    switch(property)
                    {{
                        //TODO.
                        default:
                            throw new InvalidDataException(""Unknown property"");
                    }}
                }}
" );
            }
            _s.AppendLine( "}" );
        }
    }
}
