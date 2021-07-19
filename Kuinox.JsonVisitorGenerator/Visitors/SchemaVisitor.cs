using Kuinox.JsonVisitorGenerator.Visitors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text.Json;

namespace Kuinox.JsonVisitorGenerator
{
    public partial class SchemaVisitor : SchemaWithPathVisitor
    {
        readonly Dictionary<string, SchemaDefinition> _defsDic = new();
        readonly Stack<SchemaDefinition> _defsStack = new();
        readonly HashSet<string> _referencedSchema = new();

        public IReadOnlyDictionary<string, SchemaDefinition> Definitions => _defsDic;
        SchemaDefinition CurrentDef => _defsStack.Peek();

        ChildKind _childKind = ChildKind.Invalid;

        public override void Visit( ref Utf8JsonReader reader )
        {
            string currPath = CurrentPath;
            SchemaDefinition def;
            if( _defsStack.Count == 0 )
            {
                def = new SchemaDefinition( Array.Empty<SchemaDefinition>(), "#", ChildKind.Root );
            }
            else
            {
                ChildKind childKind = _childKind;
                if( childKind == ChildKind.Invalid ) throw new InvalidOperationException( "Child kind was not set." );
                _childKind = ChildKind.Invalid;
                def = CurrentDef.CreateChild( CurrentSchemaName, childKind );
            }
            _defsDic.Add( currPath, def );
            _defsStack.Push( def );
            base.Visit( ref reader );
            _defsStack.Pop();
        }
        public void Compute( string rootSchemaName )
        {
            SchemaDefinition def = _defsDic["#"];
            def.SafeName = rootSchemaName;
            def.SetChildsSafeName();
        }

        public void Display()
        {
            _defsDic["#"].Display();
        }

        protected override void VisitTypeField0( ref Utf8JsonReader reader )
        {
            // This case is when the type may be multiple simples types.
            // This is the same thing than an union type.
            CurrentDef.Type |= TypeKind.Union;
            base.VisitTypeField0( ref reader );
        }

        protected override void VisitTypeField1( ref Utf8JsonReader reader )
        {
            CurrentDef.Type |= TypeKind.Simple;
            CurrentDef.SimpleType = reader.GetString();
            reader.Read();
        }

        protected override void VisitAnyOf( ref Utf8JsonReader reader )
        {
            CurrentDef.Type |= TypeKind.Union;
            base.VisitAnyOf( ref reader );
        }

        protected override void VisitAnyOfEntry( ref Utf8JsonReader reader )
        {
            _childKind = ChildKind.SubType;
            base.VisitAnyOfEntry( ref reader );
        }

        protected override void VisitAllOf( ref Utf8JsonReader reader )
        {
            CurrentDef.Type |= TypeKind.Union;
            base.VisitAllOf( ref reader );
        }

        protected override void VisitAllOfEntry( ref Utf8JsonReader reader )
        {
            _childKind = ChildKind.SubType;
            base.VisitAllOfEntry( ref reader );
        }

        protected override void VisitOneOfEntry( ref Utf8JsonReader reader )
        {
            CurrentDef.Type |= TypeKind.Union;
            base.VisitOneOfEntry( ref reader );
        }

        protected override void VisitRef( ref Utf8JsonReader reader )
        {
            string @ref = reader.GetString() ?? throw new InvalidDataException();
            CurrentDef.Type |= TypeKind.Reference;
            CurrentDef.Ref = @ref;
            _referencedSchema.Add( @ref );
            base.VisitRef( ref reader );
        }

        protected override void VisitEnum( ref Utf8JsonReader reader )
        {
            CurrentDef.Type |= TypeKind.Union;
            base.VisitEnum( ref reader );
        }
        protected override void VisitDefault( ref Utf8JsonReader reader )
        {
            CurrentDef.Type |= TypeKind.Constraint;
            base.VisitDefault( ref reader );
        }

        protected override void VisitDefinitionsKey( ref Utf8JsonReader reader )
        {
            _childKind = ChildKind.Definition;
            base.VisitDefinitionsKey( ref reader );
        }

        protected override void VisitPropertiesKey( ref Utf8JsonReader reader )
        {
            _childKind = ChildKind.Property;
            base.VisitPropertiesKey( ref reader );
        }

        protected override void VisitItems( ref Utf8JsonReader reader )
        {
            _childKind = ChildKind.SubType;
            base.VisitItems( ref reader );
        }

        protected override void VisitAdditionalProperties( ref Utf8JsonReader reader )
        {
            _childKind = ChildKind.AdditionalProperty;
            base.VisitAdditionalProperties( ref reader );
        }

        protected override void VisitDependencyField1Entry( ref Utf8JsonReader reader )
        {
            _childKind = ChildKind.Dependency;
            base.VisitDependencyField1Entry( ref reader );
        }

        protected override void VisitDependenciesField0( ref Utf8JsonReader reader )
        {
            _childKind = ChildKind.Dependency;
            base.VisitDependenciesField0( ref reader );
        }

        protected override void VisitExclusiveMaximum( ref Utf8JsonReader reader )
        {
            CurrentDef.Type |= TypeKind.Constraint;
            base.VisitExclusiveMaximum( ref reader );
        }

        protected override void VisitExclusiveMinimum( ref Utf8JsonReader reader )
        {
            CurrentDef.Type |= TypeKind.Constraint;
            base.VisitExclusiveMinimum( ref reader );
        }
    }
}
