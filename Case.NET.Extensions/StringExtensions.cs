using System;

namespace Case.NET.Extensions
{
    public static class StringExtensions
    {
        public static ConvertedString ToCamelCase(this string self)
        {
            return CaseConverter.CamelCase.ConvertCase(self);
        }

        public static ConvertedString ToPascalCase(this string self)
        {
            return CaseConverter.PascalCase.ConvertCase(self);
        }

        public static ConvertedString ToSnakeCase(this string self)
        {
            throw new NotImplementedException();
        }

        public static ConvertedString ToAllUpperCase(this string self)
        {
            throw new NotImplementedException();
        }

        public static ConvertedString ToKebabCase(this string self)
        {
            throw new NotImplementedException();
        }

        public static ConvertedString ToTrainCase(this string self)
        {
            throw new NotImplementedException();
        }
    }
}
