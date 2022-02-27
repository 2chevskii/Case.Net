using System;

namespace Case.NET.Extensions
{
    public static class ConvertedStringExtensions
    {
        public static ConvertedString ToCamelCase(this ConvertedString self)
        {
            return CaseConverter.CamelCase.ConvertCase(self.Tokens); // reuse tokens
        }

        public static ConvertedString ToPascalCase(this ConvertedString self)
        {
            return CaseConverter.PascalCase.ConvertCase(self.Tokens);
        }

        public static ConvertedString ToSnakeCase(this ConvertedString self)
        {
            throw new NotImplementedException();
        }

        public static ConvertedString ToAllUpperCase(this ConvertedString self)
        {
            throw new NotImplementedException();
        }

        public static ConvertedString ToKebabCase(this ConvertedString self)
        {
            throw new NotImplementedException();
        }

        public static ConvertedString ToTrainCase(this ConvertedString self)
        {
            throw new NotImplementedException();
        }
    }
}
