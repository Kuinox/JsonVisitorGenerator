using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Kuinox.JsonVisitorGenerator
{
    public partial class SchemaVisitor
    {
        public class SchemaDefinition
        {
            readonly Dictionary<string, SchemaDefinition> _childs = new();
            readonly SchemaVisitor _visitor;

            public SchemaDefinition( SchemaVisitor visitor, SchemaDefinition[] parents, string name, ChildKind childKind )
            {
                _visitor = visitor;
                Parents = parents;
                Name = name;
                ChildKind = childKind;
            }

            public SchemaDefinition[] Parents { get; }
            public IReadOnlyDictionary<string, SchemaDefinition> Childs => _childs;
            string? _simpleType;
            public string? SimpleType
            {
                get => _simpleType;
                set
                {
                    if( _simpleType != null ) throw new InvalidOperationException( "SimpleType was already set. Cannot override." );
                    if( value == null ) throw new NullReferenceException( "Cannot set SimpleType to null." );
                    _simpleType = value;
                }
            }

            string? _ref;
            public string? Ref
            {
                get => _ref;
                set
                {
                    if( _ref != null ) throw new InvalidOperationException( "Ref was already set. Cannot override." );
                    if( value == null ) throw new NullReferenceException( "Cannot set Ref to null." );
                    _ref = value;
                }
            }
            public string Name { get; }
            public ChildKind ChildKind { get; }
            private string? _value;
            public string? Value
            {
                get => _value;
                set
                {
                    if( value == null ) throw new NullReferenceException( "Cannot set value to null." );
                    if( _value != null ) throw new InvalidOperationException( "Can set the value only once." );
                    _value = value;
                }
            }
            IEnumerable<string> DefPath => Parents.Append( this ).Select( s => s.Name );
            public string Namespace => string.Join( ".", Parents.Append( this ).Select( s => s.SafeName + "Definition" ) );
            public string FullNameWithNamespace => Namespace + "." + SafeName;
            private string? _safeName;

            public string? SafeName
            {
                get => _safeName ?? throw new NullReferenceException();
                set
                {
                    if( _safeName != null ) throw new InvalidOperationException( "Cannot set this value twice." );
                    _safeName = value;
                }
            }
            public TypeKind Type { get; set; }

            public bool HaveAdditionoalProperties { get; set; } = true;

            public SchemaDefinition CreateChild( string name, ChildKind childKind )
            {
                Debug.Assert( childKind != ChildKind.Root );
                SchemaDefinition def = new( _visitor, Parents.Append( this ).ToArray(), name, childKind );
                _childs[name] = def;
                return def;
            }
            public void SetChildsSafeName()
            {

                foreach( KeyValuePair<string, string> item in GetFriendlyName( _childs.Keys ) )
                {
                    if( _childs.ContainsKey( item.Key ) ) //We inject unrelated name as constraint, we dont want these there.
                    {
                        _childs[item.Key].SafeName = item.Value;
                    }
                }
                foreach( SchemaDefinition item in _childs.Values ) // this must be executed later because childs name depends on parent name.
                {
                    item.SetChildsSafeName();
                }
            }

            PocoType ToPocoType()
            {
                return new PocoType()
                {
                    TypeKind = Type switch
                    {
                        TypeKind.Constant => JsonVisitorGenerator.TypeKind.BaseType,
                        TypeKind.Reference => JsonVisitorGenerator.TypeKind.Reference,
                        TypeKind.EmptyObject => JsonVisitorGenerator.TypeKind.BaseType,
                        TypeKind.Constraint => throw new InvalidOperationException(),
                        TypeKind.Simple => JsonVisitorGenerator.TypeKind.BaseType,
                        _ => throw new InvalidOperationException( "Unknown type kind." )
                    }
                };
            }
            public PocoModel ToPocoModel()
            {
                TypeReference reference = new( Parents.Append( this ).Select( s => s.Name ) );
                PocoModel model = new( reference );
                model.Properties = _childs
                    .Where( s => s.Value.ChildKind == ChildKind.Property )
                    .ToDictionary(
                        s => s.Key,
                        s => s.Value.ToPocoType()
                    );
                return model;
            }

            SchemaDefinition GetChildByPath( string[] path )
            {
                if( path.Length == 0 ) return this;
                return _childs[path.First()].GetChildByPath( path.Skip( 1 ).ToArray() );
            }

           

            public void AppendClassDefinition( StringBuilder sb )
            {
                sb.Append( "namespace " );
                sb.Append( Namespace );
                sb.Append( "{public class " );
                sb.Append( SafeName );
                sb.Append( '{' );
                foreach( SchemaDefinition def in Childs.Values.Where( s => s.ChildKind == ChildKind.Property ) )
                {
                    def.AppendProperty( sb );
                }
                if( HaveAdditionoalProperties )
                {

                    SchemaDefinition? additionalProperty = Childs.Values.LastOrDefault( s => s.ChildKind == ChildKind.AdditionalProperty );

                    sb.Append( "public " );
                    if( additionalProperty != null )
                    {
                        sb.Append( additionalProperty.GetCSType() );
                    }
                    else
                    {
                        sb.Append( "System.Collections.Generic.Dictionary<string,object> " );
                    }
                    sb.Append( "AdditionalProperties {get; set;}" );

                }
                sb.Append( "}}" );

            }

            string GetCSType()
            {
                if( ChildKind == ChildKind.AdditionalProperty )
                {
                    return $"System.Collections.Generic.Dictionary<string, {GetCSTypeInternal()}>";
                }
                return GetCSTypeInternal();

                string GetCSTypeInternal()
                {
                    if( Name == "commonTaskPropertySets" )
                    {
                        //this is a good test case. 
                    }
                    if( Type == TypeKind.EmptyObject ) return "object";
                    if( Type == TypeKind.Reference )
                    {
                        if( Ref == null ) throw new InvalidOperationException( "Logic error. Ref type, but no ref registered." );
                        SchemaDefinition referedDef = _visitor.Definitions[Ref];
                        return referedDef.FullNameWithNamespace;
                    }
                    return SimpleType switch
                    {
                        "array" => (Childs.SingleOrDefault().Value?.GetCSType() ?? "object") + "[]",
                        "boolean" => "bool",
                        "integer" => "long",
                        "number" => "double",
                        "object" => FullNameWithNamespace,
                        "string" => "string",
                        null => "TODO_NOTSIMPLE",
                        _ => "TODO_UNKNOWN_CASE"
                    };
                }

            }
            void AppendProperty( StringBuilder sb )
            {
                sb.Append( @$"
/// <summary>
/// In JSON, this property is named ""{Name}""
/// </summary>
public " );
                sb.Append( GetCSType() );
                sb.Append( ' ' );
                sb.Append( SafeName );
                sb.Append( " {get; set;}" );
            }
            public void Display()
            {
                Console.WriteLine( "->" + Namespace.PadRight( 50 ) + " " + Type.ToString().PadRight( 20 ) + " " + ChildKind );
                foreach( var child in _childs )
                {
                    child.Value.Display();
                }
            }


        }
    }
}
