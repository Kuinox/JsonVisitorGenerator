using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Kuinox.JsonVisitorGenerator.Visitors
{
    public class SchemaWithPathVisitor : SchemaVisitorBase
    {
        readonly Stack<string> _currentSchemaPath;
        public SchemaWithPathVisitor() => _currentSchemaPath = new( new[] { "#" } );
        protected string CurrentPath => string.Join( "/", _currentSchemaPath.Reverse() );
        protected string CurrentSchemaName => _currentSchemaPath.Peek();

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

        protected override void VisitAdditionalProperties( ref Utf8JsonReader reader )
        {
            _currentSchemaPath.Push( "additionalProperties" );
            base.VisitAdditionalProperties( ref reader );
            _currentSchemaPath.Pop();
        }

        int _allOfCounter = 0;
        protected override void VisitAllOf( ref Utf8JsonReader reader )
        {
            _allOfCounter = 0;
            _currentSchemaPath.Push( "allOf" );
            base.VisitAllOf( ref reader );
            _currentSchemaPath.Pop();
        }
        protected override void VisitAllOfEntry( ref Utf8JsonReader reader )
        {
            _currentSchemaPath.Push( _allOfCounter++.ToString() );
            base.VisitAllOfEntry( ref reader );
            _currentSchemaPath.Pop();
        }

        protected override void VisitAnyOf( ref Utf8JsonReader reader )
        {
            _allOfCounter = 0;
            _currentSchemaPath.Push( "anyOf" );
            base.VisitAnyOf( ref reader );
            _currentSchemaPath.Pop();
        }

        protected override void VisitAnyOfEntry( ref Utf8JsonReader reader )
        {
            _currentSchemaPath.Push( _allOfCounter++.ToString() );
            base.VisitAnyOfEntry( ref reader );
            _currentSchemaPath.Pop();
        }

        protected override void VisitDependencies( ref Utf8JsonReader reader )
        {
            _currentSchemaPath.Push( "dependencies" );
            base.VisitDependencies( ref reader );
            _currentSchemaPath.Pop();
        }
    }
}
