using Case.Net.Common.Entities;

namespace Case.Net.Common.Conventions;

/// <summary>
/// Contains methods used to parse and convert strings from/to a certain naming style
/// </summary>
public interface INamingConvention
{
    /// <summary>
    /// Specifies the name of the current <see cref="INamingConvention"/>
    ///      <example>
    ///          <code>camelCase</code>
    ///          <code>PascalCase</code>
    ///      </example>
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Attempts to convert given <see cref="CasedString"/> into current naming style
    /// </summary>
    /// <param name="input">Source object</param>
    /// <param name="output">Output object</param>
    /// <returns>
    /// <see langword="true"/> on success, <see langword="false"/> on failure
    /// </returns>
    bool TryConvert(CasedString input, out CasedString output);

    /// <summary>
    /// Converts given <see cref="CasedString"/> into current naming style
    /// </summary>
    /// <param name="input">Source object</param>
    /// <returns>Output object</returns>
    CasedString Convert(CasedString input);

    /// <summary>
    /// Parses given <paramref name="input"/> into a <see cref="CasedString"/>
    /// </summary>
    /// <param name="input">Input sequence</param>
    /// <returns>Output object</returns>
    CasedString Parse(ReadOnlySpan<char> input);

    /// <summary>
    /// Attempts to parse given <paramref name="input"/> into a <see cref="CasedString"/>
    /// </summary>
    /// <param name="input">Input sequence</param>
    /// <param name="output">Output object</param>
    /// <returns>If the parsing was successful</returns>
    bool TryParse(ReadOnlySpan<char> input, out CasedString output);
}
