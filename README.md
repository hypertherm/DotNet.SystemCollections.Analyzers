# About the project

[![License: MIT](https://img.shields.io/github/license/hypertherm/DotNet.SystemCollections.Analyzers?color=brightgreen)](https://opensource.org/licenses/MIT)
![Contributions welcome](https://img.shields.io/badge/contributions-welcome-orange.svg)
[![GitHub Issues](https://img.shields.io/github/issues/hypertherm/DotNet.SystemCollections.Analyzers.svg)](https://github.com/hypertherm/DotNet.SystemCollections.Analyzers/issues)

## Basic Overview

This repository contains implementations for common performance and code quality best practices surronding the use of data structures and their APIs located under System.Collections & System.Linq.

## Table of Content

## What is DotNet.SystemCollections.Analyzers

An analyzer library for C# and VB.NET empowered by Roslyn to help .NET developers to harness the full capabilities and performance of the CLR. The set of analyzers will analyze your code for quality, performance and maintainability.

## What Is Roslyn

[From Microsoft Docs](https://docs.microsoft.com/en-us/visualstudio/extensibility/dotnet-compiler-platform-roslyn-extensibility?view=vs-2019)
> The core mission of the .NET Compiler Platform ("Roslyn") is opening up the C# and Visual Basic compilers and allowing tools and developers to share in the rich information compilers have about programs. Code analysis tools improve code quality, and code generators aid in application construction. As tools get smarter, they need access to more and more of the deep code knowledge that only compilers possess.

## Goals

The goal of this library is to analyze the code and pinpoint both quality and performance pitfalls concerning the usage of types found in System.Collections such `IEnumerable<T>` or extensions methods found in System.Linq. This tool is here to help developers make better performance decisions through a set of micro-optimizations. The tool will not try to rewrite your LINQ expressions, but it’ll be able of

- Suggesting, for instance, using an `HashSet<T>` whenever a developer is doing a heavy Contains check on their `List<T>`.

- Suggesting using a for-loop when iterating on a collection that allows random access such as `IList<T>`.

- Suggesting using a separate method to yield values lazily instead of taking the time to create a temporary list.

## Documentation

You can find more about the current and planned list of rules here: (insert link to file inside the repository)
Selecting your data structure: (Link to file)

## Installation

Requirements to use:

- Recommended version of Visual Studio: Visual Studio 2015
- Minimal NET Framework supported: 4.6+
- Minimal .NET Core : 1.0

Requirements to build the solution:

- Recommended version of Visual Studio: Visual Studio 2017 version 15.3
- Minimal NET Framework supported: 4.6+
- Minimal .NET Core : 1.0

Installing the package from the Package Manager: `Install-Package DotNet.SystemCollections.Analyzers`

Each project in Visual Studio has a "treat warnings as errors" option. Go through each of your projects and change that setting:
1.Right-click on your project, select "Properties".
2.Click "Build".
3.Switch "Treat warnings as errors" from "All" to "Specific warnings" or "None".

## Release Notes

## Roadmap

## Contributing

- Filing a bug report
- Adding documentation on a new/existing rule
- Requesting a new code analyzer
- Requesting a new case scenario in an existing code analyzer
- Requesting a code fix provider
- Adding test coverage on the analyzers and code fix providers
- Adding benchmarking coverage on code analyzers

### Why do we use StyleCop.Analyzers



## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## About Robotmaster

Hypertherm's Robotic Software team develops Robotmaster, the world’s leading CAD/CAM robot programming software, used by some of the largest multinationals such as Boeing and Mercedes and the smallest industrial manufacturing shops performing short customized runs. To know more about Robotmaster, you can read more on our [website](https://www.robotmaster.com/en/).
