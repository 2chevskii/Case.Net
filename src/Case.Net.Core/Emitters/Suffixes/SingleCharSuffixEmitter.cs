using System;
using System.Collections.Generic;

namespace Case.Net.Emitters.Suffixes
{
    public sealed class SingleCharSuffixEmitter : ISuffixEmitter
    {
        private readonly string _strSuffixChar;

        public char SuffixChar { get; }
        public bool CheckValueEnd { get; }

        public SingleCharSuffixEmitter(char suffixChar, bool checkValueEnd = true)
        {
            SuffixChar     = suffixChar;
            CheckValueEnd  = checkValueEnd;
            _strSuffixChar = suffixChar.ToString();
        }

        public bool EmitSuffix(IReadOnlyList<string> words, out ReadOnlySpan<char> suffixBuffer)
        {
            if ( CheckValueEnd )
            {
                var lastWord = words[words.Count - 1];
                var lastChar = lastWord[lastWord.Length - 1];

                if ( lastChar == SuffixChar )
                {
                    suffixBuffer = ReadOnlySpan<char>.Empty;

                    return false;
                }
            }

            suffixBuffer = _strSuffixChar.AsSpan();

            return true;
        }
    }
}
