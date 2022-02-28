using static Case.NET.CaseConverter;

namespace Case.NET.Extensions
{
    public static class ConvertedStringExtensions
    {
        public static CasedString ToCamelCase(this CasedString self, bool forceParse =  false)
        {
            if (forceParse)
            {
                return CamelCase.ConvertCase(self.OriginalValue);
            }

            return CamelCase.ConvertCase(self.Tokens, self.OriginalValue);
        }

        public static CasedString ToPascalCase(this CasedString self, bool forceParse = false)
        {
            if (forceParse)
            {
                return PascalCase.ConvertCase(self.OriginalValue);
            }

            return PascalCase.ConvertCase(self.Tokens, self.OriginalValue);
        }

        public static CasedString ToSnakeCase(this CasedString self, bool forceParse = false)
        {
            if (forceParse)
            {
                return SnakeCase.ConvertCase(self.OriginalValue);
            }

            return SnakeCase.ConvertCase(self.Tokens, self.OriginalValue);
        }

        public static CasedString ToConstantCase(this CasedString self, bool forceParse = false)
        {
            if (forceParse)
            {
                return ConstantCase.ConvertCase(self.OriginalValue);
            }

            return ConstantCase.ConvertCase(self.Tokens, self.OriginalValue);
        }

        public static CasedString ToKebabCase(this CasedString self, bool forceParse = false)
        {
            if (forceParse)
            {
                return KebabCase.ConvertCase(self.OriginalValue);
            }

            return KebabCase.ConvertCase(self.Tokens, self.OriginalValue);
        }

        public static CasedString ToTrainCase(this CasedString self, bool forceParse = false)
        {
            if (forceParse)
            {
                return TrainCase.ConvertCase(self.OriginalValue);
            }

            return TrainCase.ConvertCase(self.Tokens, self.OriginalValue);
        }
    }
}
