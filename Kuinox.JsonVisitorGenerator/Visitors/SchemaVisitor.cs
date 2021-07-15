using Kuinox.JsonVisitorGenerator.Visitors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;

namespace Kuinox.JsonVisitorGenerator
{
    public class SchemaVisitor : SchemaWithPathVisitor
    {
        public class SchemaDefinition
        {
            TypeType _type;

            public SchemaDefinition(SchemaDefinition? parent, string schemaName )
            {
                Parent = parent;
                Name = schemaName;
                Path = path;
            }

            public SchemaDefinition? Parent { get; }
            public string Name { get; }
            public string Path { get; }
            public TypeType Type
            {
                get => _type; set
                {
                    if( _type != TypeType.Unknown ) throw new InvalidDataException( "Type has already been set." );
                    _type = value;
                }
            }
        }

        public enum TypeType
        {
            Unknown,
            Simple,
            Union,
            Reference
        }


        readonly Dictionary<string, SchemaDefinition> _defsDic = new();
        readonly Stack<SchemaDefinition> _defsStack = new();
        readonly HashSet<string> _referencedSchema = new();

        public IReadOnlyDictionary<string, SchemaDefinition> Definitions => _defsDic;
        SchemaDefinition CurrentDef => _defsStack.Peek();

        public override void Visit( ref Utf8JsonReader reader )
        {
            string currPath = CurrentPath;
            SchemaDefinition def = new( currPath );
            _defsDic.Add( currPath, def );
            _defsStack.Push( def );
            base.Visit( ref reader );
            _defsStack.Pop();
        }

        protected override void VisitTypeAnyOfField0( ref Utf8JsonReader reader )
        {
            // This case is when the type may be multiple simples types.
            // This is the same thing than an union type.
            // So I register as an union type.
            CurrentDef.Type = TypeType.Union;
            reader.Read();
            while( reader.TokenType != JsonTokenType.EndArray )
            {
                 string type = reader.GetString();

            }
        }

        protected override void VisitRef( ref Utf8JsonReader reader )
        {
            _referencedSchema.Add( reader.GetString() ?? throw new InvalidDataException() );
            base.VisitRef( ref reader );
        }
    }
}
