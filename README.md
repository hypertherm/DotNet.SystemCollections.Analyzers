# About the project

[![License: MIT](https://img.shields.io/github/license/hypertherm/DotNet.SystemCollections.Analyzers?color=brightgreen)](https://opensource.org/licenses/MIT)
![Contributions welcome](https://img.shields.io/badge/contributions-welcome-brightgreen.svg)
![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square)

Codacy C# Grade, Code Climate Technical Debt analysis

Azure DevOps (build status), NuGet Downloads, Visual Studio Marketplace Rating

## Basic Overview

This repository contains implementations for common performance and code quality best practices surronding the use of data structures and their APIs located under System.Collections & System.Linq.

## Table of Content

* [DotNet.SystemCollections.Analyzers](#dotnetsystemcollectionsanalyzers)
  
  * [What is Roslyn](#what-is-roslyn)
  
  * [Library Introduction](#library-introduction)
  
  * [Why should you use it in your .NET projects](#why-should-you-use-it-in-your-net-projects)
  
  * [Goals](#goals)
  
  * [Analyzers in action](#analyzers-in-action)
  
  * [Prototype Disclaimer](#disclaimer)

* [Ruleset](#ruleset)

* [Installation](#installation)

* [Contributing](#contributing)
  
  * [How to contribute](#how-to-contribute)
  
  * [Why do we use StyleCop.Analyzers](#why-do-we-use-stylecopanalyzers)

* [Release Notes](#release-notes)

* [Roadmap](#roadmap)

* [License](#license)

* [About Robotmaster](#about-robotmaster)

## DotNet.SystemCollections.Analyzers

### What is Roslyn

[From Microsoft Docs](https://docs.microsoft.com/en-us/visualstudio/extensibility/dotnet-compiler-platform-roslyn-extensibility?view=vs-2019)
> The core mission of the .NET Compiler Platform ("Roslyn") is opening up the C# and Visual Basic compilers and allowing tools and developers to share in the rich information compilers have about programs. Code analysis tools improve code quality, and code generators aid in application construction. As tools get smarter, they need access to more and more of the deep code knowledge that only compilers possess.

### Library Introduction

An analyzer library for `C#` and `VB.NET` empowered by Roslyn to help .NET developers to harness the full capabilities and performance of the CLR. The set of analyzers will analyze your code for quality, performance and maintainability.

### Why should you use it in your .NET projects

### Goals

The goal of this library is to analyze the code and pinpoint both quality and performance pitfalls concerning the usage of types found in System.Collections such `IEnumerable<T>` or extensions methods found in System.Linq. This tool is here to help developers make better performance decisions through a set of micro-optimizations.

### Analyzers in action

The tool will not try to rewrite your LINQ expressions, but it’ll be able of

* Suggesting, for instance, using an `HashSet<T>` whenever a developer is doing a heavy Contains check on their `List<T>`.

* Suggesting using a for-loop when iterating on a collection that allows random access such as `IList<T>`.

* Suggesting using a separate method to yield values lazily instead of taking the time to create a temporary list.

### Disclaimer

This project was developed as an internal hackathon project. It’s still a prototype so using it could give you false positives. Moreover, the ruleset in the library hasn't been finalized and is subject to change. We're still working on these ideas to make sure they reflect the best development practices in .NET and welcome and encourage contributions on those ideas. For those interested in helping us that way, please read the contribution guidelines for the project.

 Nonetheless, it can provide useful insights to the things we should avoid doing as developers. You can either consult the project’s roadmap to know when the project will move outside of its prototype phase or subscribe on the project’s notifications to made aware of releases and project updates.

## Ruleset

See the [ruleset.md](ruleset.md) file for details.

## Installation

Requirements to use:

* Recommended version of Visual Studio: Visual Studio 2015
* Minimal NET Framework supported: 4.6+
* Minimal .NET Core : 1.0

Requirements to build the solution:

* Recommended version of Visual Studio: Visual Studio 2017 version 15.3
* Minimal NET Framework supported: 4.6+
* Minimal .NET Core : 1.0

## Contributing

### How to contribute

* Filing a bug report
* Adding documentation on a new/existing rule
* Requesting a new code analyzer
* Requesting a new case scenario in an existing code analyzer
* Requesting a code fix provider
* Adding test coverage on the analyzers and code fix providers
* Adding benchmarking coverage on code analyzers

### Why do we use StyleCop.Analyzers

Installing the package from the Package Manager: `Install-Package DotNet.SystemCollections.Analyzers`

Each project in Visual Studio has a "treat warnings as errors" option. Go through each of your projects and change that setting:
1.Right-click on your project, select "Properties".
2.Click "Build".
3.Switch "Treat warnings as errors" from "All" to "Specific warnings" or "None".

## Release Notes

See the [release-notes.md](release-notes.md) file for details.

## Roadmap

Version 1.0 - Finalization of the ruleset covered by the library

Version 2.0 - Automatic code suggestions for best practices

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## About Robotmaster

Hypertherm's Robotic Software team develops Robotmaster, the world’s leading CAD/CAM robot programming software, used by some of the largest multinationals such as Boeing and Mercedes and the smallest industrial manufacturing shops performing short customized runs. To know more about Robotmaster, you can read more on our [website](https://www.robotmaster.com/en/).
