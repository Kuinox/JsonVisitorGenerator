using Kuinox.JsonVisitorGenerator;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.Json;
#nullable enable

namespace ConsoleApp2
{
    class Program
    {
        public static void Main()
        {
            var reader = new Utf8JsonReader(
                File.ReadAllBytes( @"C:\dev\Kuinox.JsonVisitorGenerator\Tests\Kuinox.JsonVisitorGenerator.Tests\schema.json" ) );
            SchemaVisitor visitor = new();
            reader.Read();
            visitor.Visit(ref reader );
        }
    }
}
