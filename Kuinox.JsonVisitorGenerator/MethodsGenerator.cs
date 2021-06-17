using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kuinox.JsonVisitorGenerator
{
    partial class SourceGenerator
    {
        /// <param name="typeName">Will be used to name the read method.</param>
        void AppendVisitOrRead( JObject propertyDef, string typeName, bool isMainReadMethod, bool isReading )
        {
            string csType = JSTypeToCS( propertyDef, typeName );
            _s.Append( "/// <summary>" );
            _s.Append( propertyDef["description"] );
            _s.Append( "</summary>\n" );
            _s.Append( isMainReadMethod ? "public " : "protected " );
            _s.Append( isReading ? csType : "void" );
            _s.Append( ' ' );
            _s.Append( isReading ? "Read" : "Visit" );
            _s.Append( isMainReadMethod ? "" : CamelToPascal( StringToCName( typeName ) ) );
            _s.Append( "(ref Utf8JsonReader reader){" );
            if( IsBaseType( propertyDef ) )
            {
                if( isReading )
                {
                    _s.AppendLine( $"var ret = {GetBaseTypeReaderExpression( propertyDef )};" );
                }
                _s.AppendLine( "reader.Read();" );
                if( isReading )
                {
                    _s.AppendLine( $"return ret;" );
                }
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
