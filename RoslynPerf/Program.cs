using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;
using static System.Console;

namespace RoslynPerf
{
    class Program
    {
        static void Main(string[] args)
        {
            var rawCode = @"
namespace RoslynPerf
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
";
            var code = CSharpSyntaxTree.ParseText(rawCode);
            var corLib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
            var compilation = CSharpCompilation.Create("TestCompilation", new[] { code }, new[] { corLib });
            var semanticModel = compilation.GetSemanticModel(code);
            var root = code.GetRoot();
            SyntaxNode programClass = root.DescendantNodes().OfType<ClassDeclarationSyntax>().First();

            bool check;
            var stopWatch = new Stopwatch();
            var times = 10 * 1000 * 1000;

            stopWatch.Start();
            for (int i = 0; i < times; i++)
                check = programClass.IsKind(SyntaxKind.ClassDeclaration);
            stopWatch.Stop();
            WriteLine($"IsKind Match: {stopWatch.Elapsed.TotalMilliseconds :n}");
            stopWatch.Reset();

            stopWatch.Start();
            for (int i = 0; i < times; i++)
                check = programClass.IsKind(SyntaxKind.StructDeclaration);
            stopWatch.Stop();
            WriteLine($"IsKind No Match: {stopWatch.Elapsed.TotalMilliseconds :n}");
            stopWatch.Reset();

            stopWatch.Start();
            for (int i = 0; i < times; i++)
                check = programClass.RawKind.Equals((int)SyntaxKind.ClassDeclaration);
            stopWatch.Stop();
            WriteLine($"RawKind.Equals: {stopWatch.Elapsed.TotalMilliseconds :n}");
            stopWatch.Reset();

            stopWatch.Start();
            for (int i = 0; i < times; i++)
                check = programClass is ClassDeclarationSyntax;
            stopWatch.Stop();
            WriteLine($"is Class: {stopWatch.Elapsed.TotalMilliseconds :n}");
            stopWatch.Reset();

            stopWatch.Start();
            for (int i = 0; i < times; i++)
                check = programClass is StructDeclarationSyntax;
            stopWatch.Stop();
            WriteLine($"is Struct (no match): {stopWatch.Elapsed.TotalMilliseconds :n}");
            stopWatch.Reset();

            stopWatch.Start();
            for (int i = 0; i < times; i++)
                check = programClass is IAliasSymbol;
            stopWatch.Stop();
            WriteLine($"is interface (no match): {stopWatch.Elapsed.TotalMilliseconds :n}");
            stopWatch.Reset();

            SyntaxNode node;
            stopWatch.Start();
            for (int i = 0; i < times; i++)
                node = programClass as ClassDeclarationSyntax;
            stopWatch.Stop();
            WriteLine($"as Class: {stopWatch.Elapsed.TotalMilliseconds :n}");
            stopWatch.Reset();

            stopWatch.Start();
            for (int i = 0; i < times; i++)
                node = programClass as StructDeclarationSyntax;
            stopWatch.Stop();
            WriteLine($"as Struct (no match): {stopWatch.Elapsed.TotalMilliseconds :n}");
            stopWatch.Reset();

            object o;
            stopWatch.Start();
            for (int i = 0; i < times; i++)
                o = programClass as IAliasSymbol;
            stopWatch.Stop();
            WriteLine($"as interface (no match): {stopWatch.Elapsed.TotalMilliseconds :n}");
            stopWatch.Reset();

            stopWatch.Start();
            for (int i = 0; i < times; i++)
            {
                if (programClass.IsKind(SyntaxKind.ClassDeclaration))
                node = (ClassDeclarationSyntax)programClass;
            }
            stopWatch.Stop();
            WriteLine($"if then direct cast: {stopWatch.Elapsed.TotalMilliseconds :n}");
            stopWatch.Reset();
        }
    }
}