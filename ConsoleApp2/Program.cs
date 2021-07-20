using Kuinox.JsonVisitorGenerator;
using Microsoft.CodeAnalysis.CSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
#nullable enable

namespace ConsoleApp2
{
    class Program
    {
        public static void Main()
        {
            //var schema = File.ReadAllBytes( @"D:\Kuinox\Downloads\schema(1)" );
            var schema = File.ReadAllBytes( @"C:\dev\Kuinox.JsonVisitorGenerator\Tests\Kuinox.JsonVisitorGenerator.Tests\schema.json" );
            SchemaVisitor visitor = new();
            Utf8JsonReader reader = new( schema );
            reader.Read();
            visitor.Visit( ref reader );
            visitor.Compute( "Schema" );
            visitor.Display();
            StringBuilder sb = new();
            visitor.Definitions["#"].AppendClassDefinition( sb );
            foreach( var item in visitor.Definitions )
            {
                if( item.Value.ChildKind != SchemaVisitor.ChildKind.Definition ) continue;
                item.Value.AppendClassDefinition( sb );
            }
            Console.WriteLine( sb );
        }
    }
}
