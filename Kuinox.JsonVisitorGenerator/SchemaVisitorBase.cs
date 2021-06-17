using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Kuinox.JsonVisitorGenerator
{
    public class SchemaVisitorBase
    {
        public virtual void Visit( ref Utf8JsonReader reader )
        {
            reader.Read();//Should be: start obj.
            while( reader.TokenType != JsonTokenType.EndObject )
            {
                string? propName = reader.GetString();
                reader.Read();
                switch( propName )
                {
                    case "$id":
                    case "id":
                        VisitId( ref reader );
                        break;
                    case "$schema":
                        VisitSchema( ref reader );
                        break;
                    case "title":
                        VisitTitle( ref reader );
                        break;
                    case "description":
                        VisitDescription( ref reader );
                        break;
                    case "$defs":
                    case "definitions":
                        VisitDefinitions( ref reader );
                        break;
                    case "properties":
                        VisitProperties( ref reader );
                        break;
                    case "type":
                        VisitType( ref reader );
                        break;
                    case "pattern":
                        VisitPattern( ref reader );
                        break;
                    case "items":
                        VisitItems( ref reader );
                        break;
                    case "$ref":
                        VisitRef( ref reader );
                        break;
                    case "required":
                        VisitRequired( ref reader );
                        break;
                    case "format":
                        VisitFormat( ref reader );
                        break;
                    case "additionalProperties":
                        VisitAdditionalProperties( ref reader );
                        break;
                    case null:
                    default:
                        throw new InvalidDataException();
                }
            }
            reader.Read(); //end obj
        }

        protected virtual void VisitId( ref Utf8JsonReader reader )
        {
            reader.Read();
        }
        protected virtual void VisitSchema( ref Utf8JsonReader reader )
        {
            reader.Read();
        }
        protected virtual void VisitDescription( ref Utf8JsonReader reader )
        {
            reader.Read();
        }
        protected virtual void VisitTitle( ref Utf8JsonReader reader )
        {
            reader.Read();
        }
        protected virtual void VisitDefinitions( ref Utf8JsonReader reader )
        {
            reader.Read(); // read start obj.
            while( reader.TokenType != JsonTokenType.EndObject )
            {
                VisitDefinitionsEntry( ref reader );
            }
            reader.Read(); // read end obj.
        }

        protected virtual void VisitDefinitionsEntry( ref Utf8JsonReader reader )
        {
            VisitDefinitionsKey( ref reader );
            VisitDefinitionsValue( ref reader );
        }

        protected virtual void VisitDefinitionsKey( ref Utf8JsonReader reader )
        {
            reader.Read();
        }
        protected virtual void VisitDefinitionsValue( ref Utf8JsonReader reader )
        {
            Visit( ref reader );
        }

        protected virtual void VisitProperties( ref Utf8JsonReader reader )
        {
            reader.Read(); // read start obj.
            while( reader.TokenType != JsonTokenType.EndObject )
            {
                VisitPropertiesEntry( ref reader );
            }
            reader.Read(); // read end obj.
        }

        protected virtual void VisitPropertiesEntry( ref Utf8JsonReader reader )
        {
            VisitDefinitionsKey( ref reader );
            VisitPropertiesValue( ref reader );
        }

        protected virtual void VisitPropertiesKey( ref Utf8JsonReader reader )
        {
            reader.Read();
        }

        protected virtual void VisitPropertiesValue( ref Utf8JsonReader reader )
        {
            Visit( ref reader );
        }
        protected virtual void VisitType( ref Utf8JsonReader reader )
        {
            if( reader.TokenType == JsonTokenType.StartArray )
            {
                reader.Skip();
                reader.Read();
            }
            else if( reader.TokenType == JsonTokenType.String )
            {
                reader.Read();
            }
            else
            {
                throw new InvalidDataException();
            }
        }

        protected virtual void VisitPattern( ref Utf8JsonReader reader )
        {
            reader.Read(); //Read the pattern
        }

        protected virtual void VisitItems( ref Utf8JsonReader reader )
        {
            if( reader.TokenType == JsonTokenType.StartObject )
            {
                Visit( ref reader );
            }
            else if( reader.TokenType == JsonTokenType.StartArray )
            {
                reader.Read();
                while( reader.TokenType != JsonTokenType.EndArray )
                {
                    Visit( ref reader );
                }
            }
            else
            {
                throw new InvalidDataException();
            }
        }

        protected virtual void VisitRef( ref Utf8JsonReader reader )
        {
            reader.Read(); //Read "$ref".
        }

        protected virtual void VisitFormat( ref Utf8JsonReader reader )
        {
            reader.Read(); //Read "format".
        }

        protected virtual void VisitRequired( ref Utf8JsonReader reader )
        {
            reader.Skip();
            reader.Read();
        }

        protected virtual void VisitAdditionalProperties( ref Utf8JsonReader reader )
        {
            if( reader.TokenType == JsonTokenType.StartObject )
            {
                Visit( ref reader );
            }
            else if( reader.TokenType == JsonTokenType.True || reader.TokenType == JsonTokenType.False )
            {
                reader.Read();
            }
            else
            {
                throw new InvalidDataException();
            }
        }
    }
}
