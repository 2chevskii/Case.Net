using System;
using System.Collections.Generic;

namespace Case.Net.Parsing
{
    public sealed class DelegateParser : IWordParser
    {
        public delegate bool Parser(ReadOnlySpan<char> input, out IReadOnlyList<WordPosition> words);

        private readonly Parser _parser;

        public DelegateParser(Parser parser) { _parser = parser; }

        public bool TryParse(ReadOnlySpan<char> input, out IReadOnlyList<WordPosition> words)
        {
            return _parser( input, out words );
        }
    }
}
