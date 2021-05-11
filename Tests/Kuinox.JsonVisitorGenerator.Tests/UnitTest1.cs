using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;
using NUnit.Framework;
using System.IO;
using System.Threading.Tasks;

namespace Kuinox.JsonVisitorGenerator.Tests
{
    using GeneratorTest = CSharpSourceGeneratorTest<JsonVisitorSourceGenerator, NUnitVerifier>;
    public class Tests
    {
        [Test]
        public async Task Test1()
        {
            string source = @"
using Kuinox.JsonVisitorGenerator;
[JsonVisitor(""schema.json"")]
public class TestVisitor : JsonVisitor
{
}
";
            await new GeneratorTest
            {
                ReferenceAssemblies = ReferenceAssemblies.Net.Net50,
                TestState =
                {
                    Sources = {source, await File.ReadAllTextAsync(@"C:\dev\Kuinox.JsonVisitorGenerator\Kuinox.JsonVisitorGenerator\JsonVisitorAttribute.cs" ) },
                    AdditionalFiles =
                    {
                        { ("schema.json", await File.ReadAllTextAsync("schema.json")) }
                    }
                }
            }.RunAsync();
        }
    }
}
