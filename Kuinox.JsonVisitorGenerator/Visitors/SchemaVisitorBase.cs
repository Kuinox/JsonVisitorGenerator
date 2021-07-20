using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Kuinox.JsonVisitorGenerator
{
    public class SchemaVisitorBase
    {
        public virtual void Visit( ref Utf8JsonReader reader )
        {
            if( reader.TokenType != JsonTokenType.StartObject ) throw new InvalidDataException( "Invalid json schema." );
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
                    case "minItems":
                        VisitMinItems( ref reader );
                        break;
                    case "minimum":
                        VisitMinimum( ref reader );
                        break;
                    case "exclusiveMinimum":
                        VisitExclusiveMinimum( ref reader );
                        break;
                    case "exclusiveMaximum":
                        VisitExclusiveMaximum( ref reader );
                        break;
                    case "allOf":
                        VisitAllOf( ref reader );
                        break;
                    case "anyOf":
                        VisitAnyOf( ref reader );
                        break;
                    case "oneOf":
                        VisitOneOf( ref reader );
                        break;
                    case "default":
                        VisitDefault( ref reader );
                        break;
                    case "enum":
                        VisitEnum( ref reader );
                        break;
                    case "uniqueItems":
                        VisitUniqueItems( ref reader );
                        break;
                    case "dependencies":
                        VisitDependencies( ref reader );
                        break;
                    case null:
                    default:
                        reader.Skip();
                        reader.Read();
                        break;
                }
            }
            reader.Read(); //end obj
        }

        protected virtual void VisitDependencies( ref Utf8JsonReader reader )
        {
            if( reader.TokenType == JsonTokenType.StartObject )
            {
                VisitDependenciesField0( ref reader );
            }
            else if( reader.TokenType == JsonTokenType.StartArray )
            {
                VisitDependenciesField1( ref reader );
            }
            else
            {
                throw new InvalidDataException();
            }
        }

        protected virtual void VisitDependenciesField0( ref Utf8JsonReader reader )
        {
            Visit( ref reader );
        }

        protected virtual void VisitDependenciesField1( ref Utf8JsonReader reader )
        {
            reader.Read();
            while( reader.TokenType != JsonTokenType.EndArray )
            {
                VisitDependencyField1Entry( ref reader );
            }
        }

        protected virtual void VisitDependencyField1Entry( ref Utf8JsonReader reader )
        {
            reader.Read();
        }

        protected virtual void VisitAnyOf( ref Utf8JsonReader reader )
        {
            reader.Read();//begin array.
            while( reader.TokenType != JsonTokenType.EndArray )
            {
                VisitAnyOfEntry( ref reader );
            }
            reader.Read(); // read end array.
        }

        private void VisitOneOf( ref Utf8JsonReader reader )
        {
            reader.Read();//begin array.
            while( reader.TokenType != JsonTokenType.EndArray )
            {
                VisitOneOfEntry( ref reader );
            }
            reader.Read(); // read end array.
        }
        protected virtual void VisitOneOfEntry( ref Utf8JsonReader reader )
            => Visit( ref reader );

        protected virtual void VisitAnyOfEntry( ref Utf8JsonReader reader )
            => Visit( ref reader );

        protected virtual void VisitUniqueItems( ref Utf8JsonReader reader )
            => reader.Read();

        protected virtual void VisitEnum( ref Utf8JsonReader reader )
        {
            reader.Read(); //start array
            while( reader.TokenType != JsonTokenType.EndArray )
            {
                VisitEnumEntry( ref reader );
            }
            reader.Read(); //end array
        }

        protected virtual void VisitEnumEntry( ref Utf8JsonReader reader )
        {
            reader.Skip();
            reader.Read();
        }

        protected virtual void VisitDefault( ref Utf8JsonReader reader )
        {
            reader.Skip();
            reader.Read();
        }

        protected virtual void VisitAllOf( ref Utf8JsonReader reader )
        {
            reader.Read();//begin array.
            while( reader.TokenType != JsonTokenType.EndArray )
            {
                VisitAllOfEntry( ref reader );
            }
            reader.Read(); // read end array.
        }

        protected virtual void VisitAllOfEntry( ref Utf8JsonReader reader )
            => Visit( ref reader );

        protected virtual void VisitExclusiveMinimum( ref Utf8JsonReader reader )
        {
            reader.Skip();
            reader.Read();
        }
        protected virtual void VisitExclusiveMaximum( ref Utf8JsonReader reader )
        {
            reader.Skip();
            reader.Read();
        }

        protected virtual void VisitMinimum( ref Utf8JsonReader reader )
            => reader.Read();

        protected virtual void VisitMinItems( ref Utf8JsonReader reader )
            => reader.Read();

        protected virtual void VisitId( ref Utf8JsonReader reader )
            => reader.Read();

        protected virtual void VisitSchema( ref Utf8JsonReader reader )
            => reader.Read();

        protected virtual void VisitDescription( ref Utf8JsonReader reader )
            => reader.Read();

        protected virtual void VisitTitle( ref Utf8JsonReader reader )
            => reader.Read();

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
            VisitPropertiesKey( ref reader );
            VisitPropertiesValue( ref reader );
        }

        protected virtual void VisitPropertiesKey( ref Utf8JsonReader reader )
        {
            reader.Read();
        }

        protected virtual void VisitPropertiesValue( ref Utf8JsonReader reader )
            => Visit( ref reader );
        protected virtual void VisitType( ref Utf8JsonReader reader )
        {
            if( reader.TokenType == JsonTokenType.StartArray )
            {
                VisitTypeField0( ref reader );
            }
            else if( reader.TokenType == JsonTokenType.String )
            {
                VisitTypeField1( ref reader );
            }
            else
            {
                throw new InvalidDataException();
            }
        }

        protected virtual void VisitTypeField0( ref Utf8JsonReader reader )
        {
            reader.Skip();
            reader.Read();
        }

        protected virtual void VisitTypeField1( ref Utf8JsonReader reader )
            => reader.Read();

        protected virtual void VisitPattern( ref Utf8JsonReader reader )
            => reader.Read();

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
            => reader.Read(); //Read "$ref".

        protected virtual void VisitFormat( ref Utf8JsonReader reader )
            => reader.Read(); //Read "format".

        protected virtual void VisitRequired( ref Utf8JsonReader reader )
        {
            reader.Skip();
            reader.Read();
        }

        protected virtual void VisitAdditionalProperties( ref Utf8JsonReader reader )
        {
            if( reader.TokenType == JsonTokenType.StartObject )
            {
                VisitAdditionalPropertiesField0(ref reader );
            }
            else if( reader.TokenType == JsonTokenType.True || reader.TokenType == JsonTokenType.False )
            {
                VisitAdditionalPropertiesField1( ref reader );
            }
            else
            {
                throw new InvalidDataException();
            }
        }

        protected virtual void VisitAdditionalPropertiesField0( ref Utf8JsonReader reader )
        {
            Visit( ref reader );
        }

        protected virtual void VisitAdditionalPropertiesField1( ref Utf8JsonReader reader )
        {
            reader.Read();
        }
    }
}
