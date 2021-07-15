using Kuinox.JsonVisitorGenerator;
using Microsoft.CodeAnalysis.CSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
#nullable enable

namespace ConsoleApp2
{
    class Program
    {
        public static void Main()
        {
            var schema = File.ReadAllBytes( @"D:\Kuinox\Downloads\schema(1)" );
            SchemaVisitor visitor = new();
            Utf8JsonReader reader = new( schema );
            reader.Read();
            visitor.Visit( ref reader );
            Node node = new( Array.Empty<Node>(), "#" );
            foreach( var def in visitor.Definitions )
            {
                Console.WriteLine( def.Key );
            }
            Node[] nodes = Array.Empty<Node>();
            foreach( var def in visitor.Definitions )
            {
                node.Insert( nodes, def.Key.Split( '/' ) );
            }
            node.SetChildsSafeName();
            node.Display();
        }

        class Node
        {
            private readonly Dictionary<string, Node> _childs = new();
            private string? _safeName;

            public Node[] Parents { get; }
            public string SchemaName { get; }

            public Node( Node[] parents, string name )
            {
                Parents = parents;
                SchemaName = name;
            }

            public string? SafeName
            {
                get => _safeName; set
                {
                    if( _safeName != null ) throw new InvalidOperationException( "Cannot set this value twice." );
                    _safeName = value;
                }
            }

            public IReadOnlyDictionary<string, Node> Childs => _childs;
            public void Insert( IEnumerable<Node> parents, IEnumerable<string> currentPath )
            {
                string key = currentPath.First();
                Node[] newParents = parents.Append( this ).ToArray();
                Node? value;
                if( !_childs.TryGetValue( key, out value ) ) // Ensure the node exists.
                {
                    value = new Node( newParents, key );
                    _childs[key] = value;
                }
                if( currentPath.Count() > 1 )
                {
                    value.Insert( newParents, currentPath.Skip( 1 ) );
                }
            }

            public void SetChildsSafeName()
            {
                foreach( Node item in _childs.Values )
                {
                    item.SetChildsSafeName();
                }
                foreach( KeyValuePair<string, string> item in GetFriendlyName( _childs.Keys.ToHashSet() ) )
                {
                    _childs[item.Key].SafeName = item.Value;
                }
            }

            static Dictionary<string, string> GetFriendlyName( ISet<string> names )
            {
                Dictionary<string, string> correctNames = new();
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

            public void Display( int depth = 0 )
            {
                Console.WriteLine( this );
                foreach( var item in _childs )
                {
                    item.Value.Display( depth + 1 );
                }
            }

            public string Namespace => string.Join( '.', Parents.Append( this ).Select( s => s.SafeName ) );

            public override string ToString() => Namespace;
        }
    }
}
