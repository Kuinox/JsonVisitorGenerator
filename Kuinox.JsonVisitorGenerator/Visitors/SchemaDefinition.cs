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
            public SchemaDefinition( SchemaDefinition[] parents, string name, ChildKind childKind )
            {
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

            public SchemaDefinition CreateChild( string name, ChildKind childKind )
            {
                Debug.Assert( childKind != ChildKind.Root );
                SchemaDefinition def = new( Parents.Append( this ).ToArray(), name, childKind );
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

            SchemaDefinition GetChildByPath( string[] path )
            {
                if( path.Length == 0 ) return this;
                return _childs[path.First()].GetChildByPath( path.Skip( 1 ).ToArray() );
            }

            Dictionary<string, string> GetFriendlyName( Dictionary<string, SchemaDefinition>.KeyCollection names )
            {
                Dictionary<string, string> correctNames = new();
                correctNames[Name] = SafeName;
                HashSet<string> correctButNotPascal = new();
                HashSet<string> containInvalidChars = new();
                foreach( string name in names )
                {
                    if( !SyntaxFacts.IsValidIdentifier( name ) )
                    {
                        containInvalidChars.Add( name );
                        continue;
                    }
                    if( char.IsUpper( name[0] ) )
                    {
                        correctNames.Add( name, name );
                    }
                    else
                    {
                        correctButNotPascal.Add( name );
                    }
                }
                foreach( string path in correctButNotPascal )
                {
                    string name = Path.GetFileName( path );
                    string pascalified = char.ToUpperInvariant( name[0] ) + name.Remove( 0, 1 );
                    if( correctNames.ContainsValue( pascalified ) )
                    {
                        correctNames.Add( path, name ); //Original name is not pascal, but conflict with a pascal name.
                    }
                    else
                    {
                        correctNames.Add( path, pascalified );
                    }
                }
                int anonymousCounter = 0;
                string GetAnonymousName()
                {
                    while( anonymousCounter >= 0 )
                    {
                        string typeName = "AnonymousType" + anonymousCounter;
                        bool contains = correctNames.Values.Contains( typeName );
                        anonymousCounter++;
                        if( !contains ) return typeName;
                    }
                    throw new InvalidOperationException( "Can you stop doing stupid stuff ?" );
                }
                foreach( string name in containInvalidChars )
                {
                    string replacedName = Regex.Replace( name, "[^a-zA-Z0-9]", "" );
                    if( string.IsNullOrEmpty( replacedName ) )
                    {
                        replacedName = GetAnonymousName();
                    }
                    else if( char.IsDigit( replacedName[0] ) )
                    {
                        replacedName = "Field" + replacedName;
                    }

                    string pascalified = char.ToUpperInvariant( replacedName[0] ) + replacedName.Remove( 0, 1 );
                    string originalString = pascalified;
                    int i = 0;
                    while( correctNames.ContainsValue( pascalified ) )
                    {
                        pascalified = originalString + i;
                        if( i == int.MaxValue ) throw new InvalidOperationException( "Stop doing BS and fix your json schema." );
                    }
                    correctNames.Add( name, pascalified );
                }
                return correctNames;
            }

            public void AppendClassDefinition( StringBuilder sb, SchemaVisitor visitor )
            {
                sb.Append( "namespace " );
                sb.Append( Namespace );
                sb.Append( "{public class " );
                sb.Append( SafeName );
                sb.Append( '{' );
                foreach( SchemaDefinition def in Childs.Values.Where( s => s.ChildKind == ChildKind.Property ) )
                {
                    def.AppendProperty( sb, visitor );
                }
                sb.Append( "}}" );

            }

            string GetCSType( SchemaVisitor visitor )
            {
                if( Type == TypeKind.EmptyObject ) return "object";
                if( Type == TypeKind.Reference )
                {
                    if( Ref == null ) throw new InvalidOperationException( "Logic error. Ref type, but no ref registered." );
                    SchemaDefinition referedDef = visitor.Definitions[Ref];
                    return referedDef.FullNameWithNamespace;
                }
                return SimpleType switch
                {
                    "array" => "TODO_ARRAY",
                    "boolean" => "bool",
                    "integer" => "long",
                    "number" => "double",
                    "object" => "TODO_OBJECT",
                    "string" => "string",
                    null => "TODO_NOTSIMPLE",
                    _ => "TODO_UNKNOWN_CASE"
                };
            }
            void AppendProperty( StringBuilder sb, SchemaVisitor visitor )
            {
                sb.Append( @$"
/// <summary>
/// In JSON, this property is named ""{Name}""
/// </summary>
public " );
                sb.Append( GetCSType( visitor ) );
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

            public string Name { get; }
            public ChildKind ChildKind { get; }

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
        }
    }
}
