\# Contributing



Thank you for considering contributing to ModernWinFormsUI.



ModernWinFormsUI is a lightweight UI component library for .NET 8 WinForms applications. The goal is to provide modern, consistent and designer-friendly controls without breaking native WinForms behavior.



\## How to Contribute



You can contribute by:



\- Reporting bugs

\- Suggesting new components

\- Improving documentation

\- Fixing visual inconsistencies

\- Adding examples to the demo application



\## Development Setup



1\. Clone the repository.

2\. Open `ModernWinFormsUI.sln` in Visual Studio.

3\. Set `ModernWinFormsUI.Demo` as the startup project.

4\. Build the solution.

5\. Run the demo application.



\## Component Guidelines



When adding or updating a component:



\- Keep the API simple and predictable.

\- Support Visual Studio Designer usage.

\- Use tokens from the `Tokens` folder.

\- Avoid hardcoded random colors, fonts or spacing.

\- Include default, hover, focus, disabled and error states when relevant.

\- Preserve native WinForms behavior where possible.

\- Avoid unnecessary dependencies.



\## Naming



All public controls should use the `Mw` prefix.



Examples:



\- `MwButton`

\- `MwCard`

\- `MwTextBox`

\- `MwBadge`

\- `MwAlert`



\## Pull Request Checklist



Before opening a pull request, please check:



\- The solution builds successfully.

\- The demo application runs.

\- The component works in Visual Studio Designer.

\- Public properties are grouped under the `ModernWinFormsUI` category when relevant.

\- The README or docs are updated if needed.



\## License



By contributing, you agree that your contributions will be licensed under the MIT License.

