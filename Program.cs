/*  
 * tranche.NET - a DSL for modeling structured finance products.
 * Copyright (C) 2012 Timothy Goric
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AbstractSyntaxTree;
using LexicalAnalysis;
using SemanticAnalysis;
using SyntaxAnalysis;
using ILGen;

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

        private static void Compile (IList<string> args)
        {
            var scan = new Scanner();
            scan.SetSource(GetSource(args), 0);

            var parser = new Parser(scan);
            if (!parser.Parse())
            {
                Console.WriteLine("Parsing failed!");
                return;
            }

            var root = parser.SyntaxTreeRoot;
            Console.WriteLine(root.Print(0));

            RunSemanticPasses(root);
            GenerateIntermediateCode(args[0], root);
            
            Console.Read();
        }

        private static string GetSource(IEnumerable<string> args)
        {
            //combine multiple source files
            var source = new StringBuilder();

            foreach (var file in args)
                source.Append(File.ReadAllText(file));
            return source.ToString();
        }

        private static void RunSemanticPasses(Node root)
        {
            var mgr = new ScopeManager();

            var first = new FirstPass(root, mgr);
            var second = new SecondPass(root, mgr);
            first.Run();
            second.Run();
        }

        private static void GenerateIntermediateCode(string sourceFile, Node root)
        {
            var asmName = sourceFile.Substring(sourceFile.LastIndexOf("\\", StringComparison.Ordinal) + 1).Replace(".tn", "");
            var cg = new CodeGenerator(asmName);
            cg.Generate(root);
            cg.WriteAssembly();
        }

        private static void ShowUsage ()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("  tc.exe sourceFile [sourceFile [...]]");
        }
    }
}
