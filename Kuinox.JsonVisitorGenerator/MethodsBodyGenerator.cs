using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kuinox.JsonVisitorGenerator
{
    partial class SourceGenerator
    {
        void AppendArrayProcessingImplementation( JObject arrayDef, bool isReading )
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
    }
}
