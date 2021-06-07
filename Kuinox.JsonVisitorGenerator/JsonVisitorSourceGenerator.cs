using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.CodeAnalysis.Text;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Kuinox.JsonVisitorGenerator
{
    [Generator]
    public class JsonVisitorSourceGenerator : ISourceGenerator
    {
        public void Initialize( GeneratorInitializationContext context )
        {
            context.RegisterForSyntaxNotifications( () => new SyntaxReceiver() );
        }

        public void Execute( GeneratorExecutionContext context )
        {
            SyntaxReceiver? syntaxReceiver = (SyntaxReceiver?)context.SyntaxReceiver;
            if( syntaxReceiver?.CandidateClasses is null ) throw new NullReferenceException();
            INamedTypeSymbol? attributeType = context.Compilation.GetTypeByMetadataName( "Kuinox.JsonVisitorGenerator.JsonVisitorAttribute" );
            foreach( ClassDeclarationSyntax candidate in syntaxReceiver.CandidateClasses )
            {
                SemanticModel model = context.Compilation.GetSemanticModel( candidate.SyntaxTree );
                INamedTypeSymbol? symbolInfo = model.GetDeclaredSymbol( candidate );
                if( symbolInfo is null ) continue;
                foreach( AttributeData item in symbolInfo.GetAttributes() )
                {
                    if( item.AttributeClass is null ) continue;
                    if( item.AttributeClass.Equals( attributeType, SymbolEqualityComparer.Default ) )
                    {
                        TypedConstant arg = item.ConstructorArguments.Single();
                        string path = (string)arg.Value!;

                        AdditionalText? file = context.AdditionalFiles.Where( s => StringComparer.Ordinal.Equals( s.Path, path ) ).SingleOrDefault();
                        if( file is null )
                        {
                            //TODO: emit proper error that the file couldn't be found.
                            throw new FileNotFoundException();
                        }
                        SourceText text = file.GetText( context.CancellationToken )!;
                        SourceGenerator sg = new( context, candidate, symbolInfo, text, true );
                        string generatedSource = sg.GenerateSource();
                    }
                }
            }
        }

        class SyntaxReceiver : ISyntaxReceiver
        {
            public List<ClassDeclarationSyntax>? CandidateClasses { get; private set; }
            public void OnVisitSyntaxNode( SyntaxNode syntaxNode )
            {
                if( syntaxNode is ClassDeclarationSyntax baseTypeSyntax )
                {
                    if( baseTypeSyntax.AttributeLists.Count == 0 ) return;
                    CandidateClasses ??= new();
                    CandidateClasses.Add( baseTypeSyntax );
                }
            }
        }
    }
}
