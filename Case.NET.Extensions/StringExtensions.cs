using System;

namespace Case.NET.Extensions
{
    public static class StringExtensions
    {
        public static CasedString ToCamelCase(this string self)
        {
            return CaseConverter.CamelCase.ConvertCase(self);
        }

        public static CasedString ToPascalCase(this string self)
        {
            return CaseConverter.PascalCase.ConvertCase(self);
        }

        public static CasedString ToSnakeCase(this string self)
        {
            throw new NotImplementedException();
        }

        public static CasedString ToAllUpperCase(this string self)
        {
            throw new NotImplementedException();
        }

        public static CasedString ToKebabCase(this string self)
        {
            throw new NotImplementedException();
        }

        public static CasedString ToTrainCase(this string self)
        {
            throw new NotImplementedException();
        }
    }
}
