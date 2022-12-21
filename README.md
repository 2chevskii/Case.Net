<!-- markdownlint-disable-next-line -->
![logo](logo.svg)

String manipulation library built with intention to make naming conventions conversion easy

## Motivation

Suddenly, suffering from urgent need of easy and performant way to convert strings from arbitrary naming convention
to `camelCase`, I went out searching around NuGet and found out that there are not many options available in .NET (actually one),
which would be public, and not part of some parent project.
I see that as a huge overlook, considering amount of such packages for other languages (JS, for example), so I decided
it would be quite useful to write something which will be reliable and extensible, so here it is.

> Note that library is still in development and is lacking some planned functionality (see [TODO](#todo)) or *may* contain bugs, feel free to report them in the issues section <!-- [issues](issues url here) -->

## Features

- Built-in support for all common naming conventions:
  - `camelCase`
  - `PascalCase`
  - `snake_case`
  - `kebab-case`
  - See [supported conventions](#currently-supported-conventions-as-target) for full list
- Easily extensible:
  - Modular design - combine different types of parsers, word emitters, concatenators, etc. to build the converter you need
  - Modules are dead simple to implement
- Performant:
  - Default modules' implementation aims to be as much GC-friendly as possible
  - No `Regex` usage, tokenization done by simple condition-based lexers choosing leftmost overlapped sequence (smallest possible token size is chosen)
- Zero dependencies:
  - Keeping your application size as low as possible by avoiding third party library bloat
- Portable:
  - Targets NetStandard2.0, allowing references from .NET Core 2.0 or greater and .NET Framework 4.6.1 or greater
  - Targets NetStandard2.1, allowing references from .NET Core 3.0 or greater

### Warning

Default `Case.NET.Parsing.Parser` implementation *is not* thread-safe in `netstandard2.0` build while *it is* in `netstandard2.1`.
Thus it's recommended to have a `Parser` instance for each thread, if your project is targeting .NET Framework, or .NET Core 2.0-2.2

For the same reason `Parser.Universal` is a *property*, returning new instance every call in `netstandard2.0`, while it is a *static field* in `netstandard2.1`

## Usage

> NuGet package is listed under name of `CaseDotNet` since `Case.NET` was reserved

### Installation

`Install-Package CaseDotNet -Version 0.3.0`

### Include namespace

```cs
using Case.NET
```

### Using built-in converters

Class `Case.NET.CaseConverter` contains static fields with built-in converters under corresponding names:

```cs
static void Main(string[] args) {
  CasedString converted = CaseConverter.CamelCase.ConvertCase("not_camel_case");

  Console.WriteLine((string)converted); // notCamelCase
}
```

## Currently supported conventions (as target)

- `camelCase`
- `PascalCase`
- `snake_case`
- `CONSTANT_CASE`
- `kebab-case`
- `Train-Case`

## TODO

### Target conventions to support

- [ ] `dot.case`
- [ ] `Namespace.Case`
- [ ] `no case`
- [ ] `path/case` (With optional backslash `\` as delimiter)
- [ ] `Sentence case`
- [ ] `Title Case`
- [ ] Swap case (Change case of every character) `Swap Case` -> `sWAP cASE`
- [ ] `FeNcE CaSe`
- [ ] `SPonGE CasE`
- [ ] `reVERse spONge cASe`
- [ ] `RanDOM CAse`

### Misc

- Improve unit test coverage
- Better build/release flow on CI
- Docs

Build versioning:

- Regular (non tag) builds:
  - Appveyor version: {Case.Net.Core version}-{branch name}+{build number}
  - Project version: {Project version}-{branch name}+{build number}
- Tag builds:
  - Appveyor version: {Tag version}+{build number}
  - Released project version: {Tag version}+{build number}
  - Other project versions: {do not build}
