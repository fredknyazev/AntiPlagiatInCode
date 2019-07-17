using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BorrowingSearch
{
    class Lexer
    {
        private TokenType _type;
        public Lexer(TokenType type)
        {
            _type = type;
        }
        public List<Token> TokenRepresent(string prog)
        {
            prog = TokenizeStringValues(prog);
            Regex reg = new Regex(_type.Keyword);
            prog = reg.Replace(prog, "K");
            reg = new Regex(_type.Cycle);
            prog = reg.Replace(prog, "C");
            reg = new Regex(_type.VariableType);
            prog = reg.Replace(prog, "V");
            reg = new Regex(_type.Operator3);
            prog = reg.Replace(prog, "O");
            reg = new Regex(_type.Operator2);
            prog = reg.Replace(prog, "O");
            reg = new Regex(_type.Operator1);
            prog = reg.Replace(prog, "O");
            reg = new Regex(_type.Number);
            prog = reg.Replace(prog, "N");
            reg = new Regex(_type.Identificator);
            prog = reg.Replace(prog, "I");
            List<Token> output = new List<Token>();
            foreach (var v in prog)
            {
                if("KCVONI".Contains(v))
                {
                    output.Add(new Token(v));
                }
            }
            return output;
        }
        private string TokenizeStringValues (string input)
        {
            int quotesOpen = -1;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '\"' && input[i + 1] != '\'')
                {
                    if (quotesOpen == -1)
                        quotesOpen = i;
                    else
                    {
                        input = input.Remove(quotesOpen, i - quotesOpen + 1);
                        input = input.Insert(quotesOpen, "S");
                        i = quotesOpen + 1;
                        quotesOpen = -1;
                    }
                }
            }
            foreach (var i in input)
                continue;
            return input;
        }
    }
    class Token
    {
        public char Data { get; set; }
        public Token (char data)
        {
            Data = data;
        }

        internal int getHash()
        {
            return (int)Math.Pow(Data, 5) - 13;
        }
    }
    class TokenType
    {
        public string Keyword = @"\b(abstract|as|base|break|byte|case|catch|checked|class|const|continue|default|delegate|else|enum|event|explicit|extern|false|finally|fixed|goto|if|implicit|in|interface|internal|is|lock|namespace|new|null|operator|out|override|params|private|protected|public|readonly|ref|return|sealed|sizeof|stackalloc|static|struct|switch|this|throw|true|try|typeof|unchecked|unsafe|using|using static|virtual|void|volatile|add|alias|ascending|async|await|by|descending|dynamic|equals|from|get|global|group|into|join|let|nameof|on|orderby|partial|remove|select|set|value|var|when|where|yield)\b";
        public string Cycle = @"\b(for|do|while|foreach)\b";
        public string VariableType = @"\b(bool|char|decimal|double|float|int|long|object|sbyte|short|string|uint|ulong|ushort|var)\b";
        public string Operator1 = @"[\+]|[\-]|[\*]|[\/]|[=]|[%]|[>]|[<]|[!]|[~]|[&]|[\|]|[\^]";
        public string Operator2 = @"\^=|\|=|%=|&=|/=|\*=|\-=|\+=|>>|<<|\|\||&&|<=|>=|!=|={2}|\-{2}|\+{2}";
        public string Operator3 = @">>=|<<=";
        public string Number = @"\d+";
        public string Identificator = @"[a-z|.]+";
    }
}
