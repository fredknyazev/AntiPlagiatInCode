using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorrowingSearch
{
    class StepMethod
    {
        public Statement Analise (string firstProg, string secondProg,int n)
        {
            Lexer lex = new Lexer(new TokenType());
            string firstProgNormal = TextNormalizer.Normalize(firstProg);
            string secondProgNormal = TextNormalizer.Normalize(secondProg);
            List<Token> firstProgToken = lex.TokenRepresent(firstProgNormal);
            List<Token> secondProgToken = lex.TokenRepresent(secondProgNormal);
            Statement state = new Statement();
            state.ShingleMethod = Shingle(n,firstProgToken, secondProgToken);
            state.LevenshteynMethod = Levenshteun(firstProgToken, secondProgToken);
            return state;
        }


        private double Shingle(int n, List<Token> firstProgToken, List<Token> secondProgToken)
        {
            List<int> firstShingles = getNgrams(firstProgToken,n);
            List<int> secondShingles = getNgrams(secondProgToken, n);
            return 1.0 * CountOfIntersect(firstShingles,secondShingles) / CountOfUnion(firstShingles,secondShingles);
        }

        private List<int> getNgrams(List<Token> firstProgToken,int n)
        {
            List<int> Ngrams = new List<int>();
            for(int i=0;i+n-1<firstProgToken.Count();i++)
            {
                int gram = 0;
                for(int  j=0;j<n;j++)
                {
                    gram += firstProgToken[i + j].getHash() * (int)Math.Pow(31, n - j - 1);
                }
                Ngrams.Add(gram);
            }
            return Ngrams;
        }
        private int CountOfIntersect(List<int> _first, List<int> _second)
        {
            List<int> first = new List<int>(_first);
            List<int> second = new List<int>(_second);
            int count=0;
            foreach (var f in first)
            {
                if(second.Contains(f))
                {
                    count++;
                    second.Remove(f);
                }
            }
            return count;
        }
        private int CountOfUnion(List<int> _first, List<int> _second)
        {
            List<int> first = new List<int>(_first);
            List<int> second = new List<int>(_second);
            foreach (var f in first)
            {
                if (second.Contains(f))
                {
                    second.Remove(f);
                }
            }
            return first.Count()+second.Count();
        }
        private double Levenshteun(List<Token> firstProgToken, List<Token> secondProgToken)
        {
            int LD = levenshteinDistance(tokensToString(firstProgToken), tokensToString(secondProgToken));
            return 1.0-1.0*LD/Math.Max(firstProgToken.Count(),secondProgToken.Count());
        }
        private int levenshteinDistance(string s,string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];
            // Step 1
            if (n == 0)
                return m;
            if (m == 0)
                return n;
            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++) { }
            for (int j = 0; j <= m; d[0, j] = j++) { }
            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }
        private string tokensToString(List<Token> input)
        {
            StringBuilder output = new StringBuilder();
            foreach(var t in input)
            {
                output.Append(t.Data);
            }
            return output.ToString();
        }
    }
    struct Statement
    {
        public double ShingleMethod { get; set; }
        public double LevenshteynMethod { get; set; }
    }
}
