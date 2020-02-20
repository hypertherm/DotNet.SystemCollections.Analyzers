# About the project

[![License: MIT](https://img.shields.io/github/license/hypertherm/DotNet.SystemCollections.Analyzers?color=brightgreen)](https://opensource.org/licenses/MIT)
![Contributions welcome](https://img.shields.io/badge/contributions-welcome-brightgreen.svg)
![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square)

TODO: Wait until project public for Codacy C# Grade

TODO Wait until release pipeline is ready Azure DevOps (build status), NuGet Downloads, Visual Studio Marketplace Rating

## Basic Overview

This repository contains implementations for common performance and code quality best practices surronding the use of data structures and their APIs located under System.Collections & System.Linq.

## Table of Content

* [DotNet.SystemCollections.Analyzers](#dotnetsystemcollectionsanalyzers)
  
  * [What is Roslyn](#what-is-roslyn)
  
  * [Library Introduction](#library-introduction)
  
  * [Why should you use it in your .NET projects](#why-should-you-use-it-in-your-net-projects)
  
  * [Goals](#goals)

  * [Prototype Disclaimer](#disclaimer)

* [Ruleset](#ruleset)

  * [Code Analysis Examples](#code-analysis-examples)

  * [More Details](#more-details)

* [Installation](#installation)
  
  * [Usage](#usage)
  
  * [Development](#development)

  * [Treating warnings as compiling errors](#treating-warnings-as-compiling-errors)

* [Contributing](#contributing)
  
* [Release Notes](#release-notes)

* [Roadmap](#roadmap)

* [License](#license)

* [About Robotmaster](#about-robotmaster)

## __DotNet.SystemCollections.Analyzers__

### __What is Roslyn__

[From Microsoft Docs](https://docs.microsoft.com/en-us/visualstudio/extensibility/dotnet-compiler-platform-roslyn-extensibility?view=vs-2019)
> The core mission of the .NET Compiler Platform ("Roslyn") is opening up the C# and Visual Basic compilers and allowing tools and developers to share in the rich information compilers have about programs. Code analysis tools improve code quality, and code generators aid in application construction. As tools get smarter, they need access to more and more of the deep code knowledge that only compilers possess.

### __Library Introduction__

An analyzer library for `C#` and `VB.NET` empowered by Roslyn to help .NET developers to harness the full capabilities and performance of the CLR. The set of analyzers will analyze your code for quality, performance and maintainability.

### __Why should you use it in your .NET projects__

There are many code analysis libraries for .NET, but they do not focused on how we should use data structures such as an `List<T>`. Using a framework like .NET makes it easier for software developers to create an impact on their business and solve problems for their customers. The problem is that even though you can use a list everywhere just because you're used to its APIs doesn't mean you should. Each data structure serves a different purpose and knowing when to use within an algorithm makes the difference between something that is slow between something that is fast.

The purpose of the library is to empower the software developers using __.NET is a fantastic platform for application development__ and leveraging best practices can make the difference between a slow and fast application.

### __Goals__

The goal of this library is to analyze the code and pinpoint both quality and performance best-practices concerning the usage of types found in System.Collections such `IEnumerable<T>` or extensions methods found in System.Linq. This tool is here to help developers make better performance decisions through a set of micro-optimizations.

### __Disclaimer__

This project was developed as an internal hackathon project. It’s still a prototype so using it could give you false positives. Moreover, the ruleset in the library hasn't been finalized and is subject to change. We're still working on these ideas to make sure they reflect the best development practices in .NET and welcome and encourage contributions on those ideas. For those interested in helping us that way, please read the contribution guidelines for the project.

 Nonetheless, it can provide useful insights to the things we should avoid doing as developers. You can either consult the project’s roadmap to know when the project will move outside of its prototype phase or subscribe on the project’s notifications to made aware of releases and project updates.

## __Ruleset__

### __Code Analysis Examples__

* Suggesting, for instance, using an `HashSet<T>` whenever a developer is doing a heavy Contains check on their `List<T>`.

* Suggesting using a for-loop when iterating on a collection that allows random access such as `IList<T>`.

* Suggesting using a separate method to yield values lazily instead of taking the time to create a temporary list.

### __More details__

See the [ruleset.md](ruleset.md) file for details.

## __Installation__

### __Usage__

Requirements to use:

* Recommended version of Visual Studio: Visual Studio 2015
* Minimal NET Framework supported: 4.6+
* Minimal .NET Core : 1.0

You can download the NuGet Package from [here](#missing-link-from-nuget-org)

### __Development__

Requirements to build the solution:

* Recommended version of Visual Studio: Visual Studio 2017 version 15.3
* Minimal NET Framework supported: 4.6+
* Minimal .NET Core : 1.0

Installing the package from the Package Manager: `Install-Package DotNet.SystemCollections.Analyzers`

### __Treating warnings as compiling errors__

Each project in Visual Studio has a "treat warnings as errors" option. Go through each of your projects and change that setting:

1. Right-click on your project, select "Properties".

2. Click "Build".

3. Switch "Treat warnings as errors" from "All" to "Specific warnings" or "None".

## __Contributing__

Please note we have a code of conduct, follow it in all your interactions with the project.

See the [CONTRIBUTING.md](contributing.md) file for more details.

## __Release Notes__

See the [release-notes.md](release-notes.md) file for details.

## __Roadmap__

Version 1.0 - Finalization of the ruleset covered by the library

Version 2.0 - Automatic code suggestions for best practices

## __License__

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## __About Robotmaster__

Hypertherm's Robotic Software team develops Robotmaster, the world’s leading CAD/CAM robot programming software, used by some of the largest multinationals such as Boeing and Mercedes and the smallest industrial manufacturing shops performing short customized runs. To know more about Robotmaster, you can read more on our [website](https://www.robotmaster.com/en/).
