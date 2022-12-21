namespace Case.Net.Extensions
{
    public static class CharExtensions
    {
        public static bool IsLetter(this char self)
        {
            return char.IsLetter( self );
        }

        public static bool IsLetterOrDigit(this char self)
        {
            return char.IsLetterOrDigit( self );
        }

        public static bool IsDigit(this char self)
        {
            return char.IsDigit( self );
        }

        public static bool IsUnderscore(this char self)
        {
            return self == '_';
        }

        public static bool IsDash(this char self)
        {
            return self == '-';
        }

        public static bool IsLower(this char self)
        {
            return char.IsLower( self );
        }

        public static bool IsUpper(this char self)
        {
            return char.IsUpper( self );
        }
    }
}
