using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

using LexicalAnalysis;
using SyntaxAnalysis;

namespace tc
{
    class Program
    {
        static void Main (string[] args)
        {
            if (args.Length < 1)
            {
                ShowUsage();
                return;
            }
            Compile(args);
        }

        private static void Compile (string[] args)
        {
            //combine multiple source files
            StringBuilder source = new StringBuilder();

            foreach (var file in args)
                source.Append(File.ReadAllText(file));

            var scan = new Scanner();
            scan.SetSource(source.ToString(), 0);

            var parser = new Parser(scan);
            if (!parser.Parse())
            {
                Console.WriteLine("Parsing failed!");
                return;
            }

            var root = parser.SyntaxTreeRoot;
            Console.WriteLine(root.Print(0));
            Console.Read();
        }

        private static void ShowUsage ()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("  tc.exe sourceFile [sourceFile [...]]");
        }
    }
}
