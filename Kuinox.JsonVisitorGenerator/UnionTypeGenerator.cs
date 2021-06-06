using CK.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kuinox.JsonVisitorGenerator
{
    partial class SourceGenerator
    {
        readonly HashSet<string> _set = new();

        void AppendMultiTypeDeclaration( string typeName, string[] types )
        {
            if( !_set.Add( typeName ) ) return;
            if( types.Length == 0 ) throw new ArgumentOutOfRangeException( nameof( types ) );
            string[] camelCaseTypes = types.Select( s => PascalToCamel( s ) ).ToArray();
            _s.AppendLine( $@"
public sealed class {typeName}
{{
    public {typeName}(" );
            for( int i = 0; i < types.Length; i++ )
            {
                _s.Append( typeName[i] );
                _s.Append( ' ' );
                _s.Append( camelCaseTypes[i] );
                if( i != types.Length - 1 ) _s.AppendLine( "," );
            }
            _s.AppendLine( ")\n{" ); //constructor
            for( int i = 0; i < types.Length; i++ )
            {
                _s.Append( typeName[i] );
                _s.Append( " = " );
                _s.Append( camelCaseTypes[i] );
                _s.AppendLine( ";" );
            }
            _s.AppendLine( "}" );
            //fields
            for( int i = 0; i < types.Length; i++ )
            {
                _s.Append( "public " );
                _s.Append( typeName );
                _s.Append( "? " );
                _s.Append( typeName );
                _s.AppendLine( " { get; }" );
            }

            _s.Append( '}' );
        }
    }
}
