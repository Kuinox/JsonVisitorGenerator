using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Kuinox.JsonVisitorGenerator
{
    /// <summary>
    /// Takes <see cref="PocoModel"/> as input.
    /// Validate that the models given can be compiled.
    /// Translate the unsafe strings to safe strings.
    /// Generate POCOs
    /// Generate visitors.
    /// </summary>
    class PocoModelResolver
    {
        /// <summary>
        /// Explicitly not a tree.
        /// </summary>
        readonly IReadOnlyList<PocoModel> _models;
        IReadOnlyList<PocoModel> _modelsWithCorrectPropertyNames;
        public PocoModelResolver( IEnumerable<PocoModel> models )
        {
            _models = models.ToList();
        }

        class Node
        {
            public Dictionary<string, Node> Childrens = new();
        }

        void Validate()
        {
            Node root = new();
            foreach( PocoModel model in _models )
            {
                Node current = root;
                for( int i = 0; i < model.Path.Count; i++ )
                {
                    Node newNode = new();
                    if( model.Path.Count - 1 == i ) // last one.
                    {
                        if( newNode.Childrens.ContainsKey( model.Path[i] ) )
                        {
                            throw new InvalidDataException( $"The following types {string.Join( ".", model.Path )} have been declared multiples times, or is conflicting with another type." );
                        }
                    }
                }
            }


            var references = _models
                .SelectMany( model => model.Properties.Select( prop => (model, prop) ) )
                .SelectMany( s => s.prop.Value.TypeReferences.Select( type => (s.model, s.prop, type) ) );


            HashSet<TypeReference> definitions = new( _models.Select( s => s.Name ) );

            foreach( var item in references )
            {
                item.
            }

            var unknownReference = references.Where( p => !definitions.Contains( p.type ) ).ToArray();
            if( unknownReference.Any() )
            {
                throw new InvalidDataException( $"The following references does not match any model definition: \n{string.Join( "\n", unknownReference.Select( s => s.type ) )}." );
            }
        }

        static PocoModel SanitizeProperties( PocoModel pocoModel )
        {
            GetFriendlyName( pocoModel.Properties.Keys, pocoModel.Name );
        }

        static Dictionary<string, string> GetFriendlyName( ICollection<string> names, string parentNodeName )
        {
            Dictionary<string, string> correctNames = new();
            correctNames[parentNodeName] = parentNodeName;
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
    }
}
