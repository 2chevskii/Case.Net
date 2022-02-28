using static Case.NET.CaseConverter;

namespace Case.NET.Extensions
{
    public static class StringExtensions
    {
        public static CasedString ToCamelCase(this string self) => CamelCase.ConvertCase(self);

        public static CasedString ToPascalCase(this string self) => PascalCase.ConvertCase(self);

        public static CasedString ToSnakeCase(this string self) => SnakeCase.ConvertCase(self);

        public static CasedString ToConstantCase(this string self) =>
            ConstantCase.ConvertCase(self);

        public static CasedString ToKebabCase(this string self) => KebabCase.ConvertCase(self);

        public static CasedString ToTrainCase(this string self) => TrainCase.ConvertCase(self);
    }
}
