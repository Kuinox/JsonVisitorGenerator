using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kuinox.JsonVisitorGenerator
{
    partial class SourceGenerator
    {
        public void AppendType( string typeName, JObject properties )
        {
            _s.AppendLine( @$"public sealed class {typeName}
{{
    {typeName}(" );
            var arr = ((IEnumerable<KeyValuePair<string, JToken?>>)properties).ToArray();
            for( int i = 0; i < arr.Length; i++ )
            {
                _s.Append( CamelToPascal( arr[i].Key ) );
                _s.Append( ' ' );
                _s.Append( arr[i].Key );
                if( i < arr.Length - 1 ) _s.AppendLine( "," );
            }
            _s.AppendLine( ")" );
            _s.AppendLine( "{" );

            //TODO
            _s.AppendLine( @"}
    }" );

        }
    }
}
