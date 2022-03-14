namespace Case.NET
{
    public enum KnownCaseKind
    {
        /// <summary>
        /// No naming convention was determined
        /// </summary>
        UNKNOWN,
        /// <summary>
        /// Source string resembles <see langword="PascalCase"/> naming convention
        /// </summary>
        PASCAL,
        /// <summary>
        /// Source string resembles <see langword="camelCase"/> naming convention
        /// </summary>
        CAMEL,
        /// <summary>
        /// Source string resembles <see langword="snake_case"/> naming convention
        /// </summary>
        SNAKE,
        /// <summary>
        /// Source string resembles <see langword="CONSTANT_CASE"/> naming convention
        /// </summary>
        CONSTANT,
        /// <summary>
        /// Source string resembles <see langword="kebab-case"/> naming convention
        /// </summary>
        KEBAB,
        /// <summary>
        /// Source string resembles <see langword="Train-Case"/> naming convention
        /// </summary>
        TRAIN,
        /// <summary>
        /// Source string resembles <see langword="dot.case"/> naming convention
        /// </summary>
        DOT,
        /// <summary>
        /// Source string resembles <see langword="Namespace.Case"/> naming convention
        /// </summary>
        NAMESPACE,
        /// <summary>
        /// Source string resembles <see langword="no case"/> naming convention
        /// </summary>
        NO,
        /// <summary>
        /// Source string resembles <see langword="path/case"/> naming convention
        /// </summary>
        PATH_FORWARD,
        /// <summary>
        /// Source string resembles <see langword="path\case"/> naming convention
        /// </summary>
        PATH_BACKWARD,
        /// <summary>
        /// Source string resembles <see langword="Sentence case"/> naming convention
        /// </summary>
        SENTENCE,
        /// <summary>
        /// Source string resembles <see langword="FeNcE CaSe"/> naming convention
        /// </summary>
        FENCE,
        /// <summary>
        /// Source string resembles <see langword="SPonGE CasE"/> naming convention
        /// </summary>
        SPONGE,
        /// <summary>
        /// Source string resembles <see langword="reVERse spONge cASe"/> naming convention
        /// </summary>
        SPONGE_REVERSE,
        /// <summary>
        /// Source string resembles <see langword="Title Case"/> naming convention
        /// </summary>
        TITLE
    }
}
