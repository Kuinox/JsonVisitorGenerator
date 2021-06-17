using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;

namespace Kuinox.JsonVisitorGenerator
{
    public class SchemaVisitor : SchemaVisitorBase
    {
        public class SchemaDefinition
        {
            readonly HashSet<string> _properties = new();
            public SchemaDefinition( string path ) => Path = path;
            public void AddProperty( string property )
            {
                if( !_properties.Add( property ) ) throw new InvalidDataException( "Property declared more than once." );
            }
            public string Path { get; }
            public IEnumerable<string> Properties => _properties;
        }

        readonly Stack<string> _currentSchemaPath;
        readonly Dictionary<string, SchemaDefinition> _objDefs = new();
        readonly HashSet<string> _referencedSchema = new();
        public SchemaVisitor() => _currentSchemaPath = new( new[] { "#" } );

        string CurrentPath => string.Join( "/", _currentSchemaPath.Reverse() );

        public IEnumerable<KeyValuePair<string, SchemaDefinition>> TreeShakedDefinitions
            => _objDefs.Where( s => _referencedSchema.Contains( s.Key ) );

        public override void Visit( ref Utf8JsonReader reader )
        {
            string currPath = CurrentPath;
            _objDefs.Add( currPath, new SchemaDefinition( currPath ) );
        }

        protected override void VisitDefinitions( ref Utf8JsonReader reader )
        {
            _currentSchemaPath.Push( "definitions" );
            base.VisitDefinitions( ref reader );
            _currentSchemaPath.Pop();
        }
        protected override void VisitDefinitionsKey( ref Utf8JsonReader reader )
        {
            _currentSchemaPath.Push( reader.GetString() ?? throw new InvalidDataException() );
            base.VisitDefinitionsKey( ref reader );
        }

        protected override void VisitDefinitionsValue( ref Utf8JsonReader reader )
        {
            base.VisitDefinitionsValue( ref reader );
            _currentSchemaPath.Pop();
        }

        protected override void VisitPropertiesKey( ref Utf8JsonReader reader )
        {
            _currentSchemaPath.Push( reader.GetString() ?? throw new InvalidDataException() );
            base.VisitPropertiesKey( ref reader );
        }
        protected override void VisitProperties( ref Utf8JsonReader reader )
        {
            _currentSchemaPath.Push( "properties" );
            base.VisitProperties( ref reader );
            _currentSchemaPath.Pop();
        }

        protected override void VisitPropertiesValue( ref Utf8JsonReader reader )
        {
            base.VisitPropertiesValue( ref reader );
            _currentSchemaPath.Pop();
        }

        protected override void VisitItems( ref Utf8JsonReader reader )
        {
            _currentSchemaPath.Push( "items" );
            base.VisitItems( ref reader );
            _currentSchemaPath.Pop();
        }
        protected override void VisitRef( ref Utf8JsonReader reader )
        {
            _referencedSchema.Add( reader.GetString() ?? throw new InvalidDataException() );
            base.VisitRef( ref reader );
        }

        protected override void VisitAdditionalProperties( ref Utf8JsonReader reader )
        {
            _currentSchemaPath.Push( "additionalProperties" );
            base.VisitAdditionalProperties( ref reader );
            _currentSchemaPath.Pop();
        }
    }
}
