using System.Collections.Generic;

using Case.NET.Parsing.Tokens;

namespace Case.NET.Emit.Concat
{
    public class SingleCharWordConcatenator : IWordConcatenator
    {
        public static readonly SingleCharWordConcatenator Dash =
            new SingleCharWordConcatenator('-');
        public static readonly SingleCharWordConcatenator Underscore =
            new SingleCharWordConcatenator('.');

        public readonly char ConcatChar;
        public readonly bool InsertIfPresent;

        protected readonly string concatCharAsString;

        public SingleCharWordConcatenator(char concatChar, bool insertIfPresent = false)
        {
            ConcatChar = concatChar;
            InsertIfPresent = insertIfPresent;

            concatCharAsString = concatChar.ToString();
        }

        public string GetConcatenation(IList<WordToken> tokens, int index)
        {
            if (InsertIfPresent)
            {
                // skip all checks, just return the concatenation value
                return concatCharAsString;
            }

            WordToken currentToken = tokens[index];

            char curTokenLastChar = currentToken.Value[currentToken.Length-1];

            if (curTokenLastChar == ConcatChar)
            {
                return string.Empty;
            }

            // by design, ICaseConverter should not pass here list with capacity less than 2
            // nor should it pass the last token, so we do not check list bounds
            WordToken nextToken = tokens[index + 1];

            char nextTokenFirstChar = nextToken.Value[0];

            if (nextTokenFirstChar == ConcatChar)
            {
                return string.Empty;
            }

            return concatCharAsString;
        }
    }
}
