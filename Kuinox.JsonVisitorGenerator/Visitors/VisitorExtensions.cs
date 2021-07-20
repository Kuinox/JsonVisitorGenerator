using System.Text;
using System.Text.Json;

namespace Kuinox.JsonVisitorGenerator
{
    public static class VisitorExtensions
    {
        public static void Visit( this SchemaVisitorBase @this, string schema )
        {
            Utf8JsonReader reader = new( Encoding.UTF8.GetBytes( schema ) );
            reader.Read();
            @this.Visit( ref reader );
        }
    }
}
