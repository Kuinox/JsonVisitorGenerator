using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kuinox.JsonVisitorGenerator
{
    public class PocoModel
    {
        public PocoModel( string name, string[] pocoPath )
        {
            Name = name;
            Path = pocoPath;
        }

        /// <summary>
        /// Local name, not full path one.
        /// </summary>
        public string Name { get; }
        public IList<string> Path { get; }

        /// <summary>
        /// Key => the name of the property.
        /// Value => the type of the property.
        /// </summary>
        public Dictionary<string, PocoType> Properties { get; set; }

        /// <summary>
        /// Get all the types defined below 
        /// </summary>
        public ICollection<PocoType> Types { get; }

    }

    public struct TypeReference : IEquatable<TypeReference>
    {
        public IReadOnlyList<string> DefinitionPath { get; }

        public TypeReference( IEnumerable<string> definitionPath )
        {
            DefinitionPath = definitionPath.ToArray();
        }

        public bool Equals( TypeReference other )
            => other.DefinitionPath.SequenceEqual( other.DefinitionPath );

        public override bool Equals( object obj )
            => obj is TypeReference reference && Equals( reference );

        public override string ToString()
            => string.Join( "/", DefinitionPath );

        public override int GetHashCode() => DefinitionPath.GetHashCode();
    }

    public enum TypeKind
    {
        Unknown,
        BaseType,
        Reference,
        UnionType
    }

    public class PocoType
    {
        public TypeKind TypeKind { get; set; }
        public IReadOnlyList<TypeReference> TypeReferences { get; set; }
    }
}
