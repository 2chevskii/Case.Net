## Case conventions

- `PascalCase`
  - Words concatenated with empty string
  - Every word starts with a capital letter
  - No special characters are allowed
- `camelCase`
  - Words concatenated with empty string
  - First word starts with a lower case letter
  - Every subsequent word starts with a capital letter
  - No special characters are allowed
- `snake_case`
  - Words concatenated with a single underscore (`_`) character
  - Every letter is in lower case
  - No special characters are allowed
- `kebab-case`
  - Words concatenated with a single dash (`-`) character
  - Every letter is in lower case
  - No special characters are allowed
- `Train-Case` (`Header-Case`)
  - Words concatenated with a single dash (`-`) character
  - Every word starts with a capital letter
  - No special characters are allowed
- `CONSTANT_CASE` (`ALL_UPPER_CASE`)
  - Words concatenated with a single underscore (`_`) character
  - Every letter is in upper case
  - No special characters are allowed
- `Capital Case`
  - Words concatenated with a single whitespace (` `) character
  - Every word starts with a capital letter
  - No special characters are allowed
- `dot.case`
  - Words concatenated with a single dot (`.`) character
  - Every letter is in lower case
  - No special characters are allowed
- `no case`
  - Words concatenated with a single whitespace (` `) character
  - Every letter is in lower case
  - No special characters are allowed
- `path/case`
  - Words concatenated with a single forward slash (`/`) character
  - Every letter is in lower case
  - No special characters are allowed
  - Variations:
    - Preserve word casing
    - Backward slash concatenator
- `Sentence case`
  - Words concatenated with a single whitespace (` `) character
  - First word starts with a capital letter
  - Subsequent words start with a lower case letter
  - No special characters are allowed
  - Variations:
    - Add a full stop at the end


## How it works

1. Parse the biggest prefix using `IPrefixParser`
2. Strip the prefix from the input string
3. Parse the biggest suffix using `ISuffixParser`
4. Strip the suffix from the input string
5. Break the input string into words starting from the `0` index (or `0` + `prefix length`)
