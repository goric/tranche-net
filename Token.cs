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
using SyntaxAnalysis;

using QUT.Gppg;

namespace tc
{
    internal class Token
    {
        public string Value { get; set; }
        public LexLocation Source { get; set; }
        public Tokens TokenType { get; set; }

        public Token (Tokens type)
        {
            TokenType = type;
        }
        public Token (Tokens type, string value)
        {
            TokenType = type;
            Value = value;
        }
        public Token (Tokens type, string value, int line, int column, string file = "")
        {
            TokenType = type;
            Value = value;
            Source = new LexLocation(line, column, 0, 0);
        }
    }
}
