using System;

namespace Case.NET.Extensions
{
    public static class ConvertedStringExtensions
    {
        public static CasedString ToCamelCase(this CasedString self)
        {
            //return CaseConverter.CamelCase.ConvertCase(self.Tokens); // reuse tokens

            throw new NotImplementedException();
        }

        public static CasedString ToPascalCase(this CasedString self)
        {
            //return CaseConverter.PascalCase.ConvertCase(self.Tokens);

            throw new NotImplementedException();
        }

        public static CasedString ToSnakeCase(this CasedString self)
        {
            throw new NotImplementedException();
        }

        public static CasedString ToAllUpperCase(this CasedString self)
        {
            throw new NotImplementedException();
        }

        public static CasedString ToKebabCase(this CasedString self)
        {
            throw new NotImplementedException();
        }

        public static CasedString ToTrainCase(this CasedString self)
        {
            throw new NotImplementedException();
        }
    }
}
