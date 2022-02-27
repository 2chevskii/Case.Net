# Case.NET

String manipulation library for string casing manipulation

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
  - `ALL_UPPER_CASE`
  - `kebab-case`
  - `Train-Case`
- Easily extensible:
  - Modular design - combine different types of parsers, word emitters, concatenators, etc. to build the converter you need
  - Modules are dead simple to implement
- Performant:
  - Default modules' implementation aims to be as much GC-friendly as possible
  - No `Regex` usage, tokenization done by simple condition-based lexers choosing leftmost overlapped sequence (smallest possible token size is chosen)

## Usage

> TODO

## TODO

> TODO
