# Case.NET

String manipulation library for string casing manipulation <!-- What the fuck is this? -->

## Motivation

Suddenly, suffering from urgent need of easy and performant way to convert strings from arbitrary naming convention
to `camelCase`, I went out searching around NuGet and found out that there are not many options available in .NET (actually one),
which would be public, and not part of some parent project.
I see that as a huge overlook, considering amount of such packages for other languages (JS, for example), so I decided
it would be quite useful to write something which will be reliable and extensible, so here it is.

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

> TODO

## Currently supported conventions (as target)

- `camelCase`
- `PascalCase`
- `snake_case`
- `CONSTANT_CASE`
- `kebab-case`
- `Train-Case`

## TODO

### Target conventions to support

- `Capital Case`
- `dot.case`
- `Namespace.Case`
- `no case`
- `path/case` (With optional backslash `\` as delimiter)
- `Sentence case`
- `Title Case`
- Swap case (Change case of every character) `Swap Case` -> `sWAP cASE`
- `FeNcE CaSe`
- `SPonGE CasE`
- `reVERse spONge cASe`
- `RanDOM CAse`
