using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;
using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kuinox.JsonVisitorGenerator.Tests
{
    public class VisitorTests
    {
        [Test]
        public async Task additional_property_detected()
        {
            string source = @"
{
  ""type"": ""object"",
  ""additionalProperties"": false
}
";
            SchemaVisitor visitor = new();
            visitor.Visit( source );
            visitor.Definitions["#"].HaveAdditionoalProperties.Should().BeFalse();
        }

        [Test]
        public async Task additional_property_type_is_defined()
        {
            string source = @"
{
            ""type"": ""object"",
            ""additionalProperties"": { ""$ref"": ""#"" }
}";
            SchemaVisitor visitor = new();
            visitor.Visit( source );
            var root = visitor.Definitions["#"];
            root.Childs.Should().HaveCount( 1 );
            var child = root.Childs.Single().Value;
            child.Name.Should().Be( "additionalProperties" );
            child.ChildKind.Should().Be( SchemaVisitor.ChildKind.AdditionalProperty );
            child.Type.Should().Be( SchemaVisitor.TypeKind.Reference );
        }

        [Test]
        public async Task additional_property_show_up_as_dictionary()
        {
            string source = @"
{
            ""type"": ""object"",
            ""properties"": {
                ""foo"": {
                    ""type"": ""string""
               }
            },
            ""additionalProperties"": { ""$ref"": ""#"" }
}";
            SchemaVisitor visitor = new();
            visitor.Visit( source );
            visitor.Compute( "Schema" );
            var root = visitor.Definitions["#"];
            var sb = new StringBuilder();
            root.AppendClassDefinition( sb );
            string genClass = sb.ToString();
            genClass.Replace(" ", "").Should().Contain( "Dictionary<string,Schema" );
        }
    }
}
