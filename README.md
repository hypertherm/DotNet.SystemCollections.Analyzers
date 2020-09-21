# About the project

[![License: MIT](https://img.shields.io/github/license/hypertherm/DotNet.SystemCollections.Analyzers?color=brightgreen)](https://opensource.org/licenses/MIT)
![Contributions welcome](https://img.shields.io/badge/contributions-welcome-brightgreen.svg)
![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square)

[![Azure DevOps Build Status](https://dev.azure.com/hypertherm/Robotmaster/_apis/build/status/CI-CD/hypertherm.DotNet.SystemCollections.Analyzers?branchName=refs%2Fpull%2F52%2Fmerge)](https://dev.azure.com/hypertherm/Robotmaster/_build/latest?definitionId=18&branchName=refs%2Fpull%2F52%2Fmerge)

## Basic Overview

This repository is dedicated to common performance and code quality analyzers to follow the best practices surrounding the use of data structures and their APIs located under System.Collections & System.Linq.

## __Disclaimer__

This project was developed as an internal hackathon project as a proof of concept and depicts the idea. It’s still a prototype and using it could give you false positives. Moreover, the ruleset in the library hasn't been finalized and is subject to change. We're still working on these ideas to make sure they are accurate and reflect the best development practices in .NET. We welcome and encourage contributions on those ideas. For those interested in helping us that way, please read the contribution guidelines for the project.

Nonetheless, it could provide useful insights to the developers. You can either consult the project’s roadmap or subscribe to the project’s notifications to know when the project will move outside of its prototype phase.

## Table of Content

* [DotNet.SystemCollections.Analyzers](#dotnetsystemcollectionsanalyzers)
  
  * [What is Roslyn](#what-is-roslyn)
  
  * [Library Introduction](#library-introduction)
  
  * [Why should you use it in your .NET projects](#why-should-you-use-it-in-your-net-projects)
  
  * [Goals](#goals)

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

There are many code analysis libraries for .NET, but they do not focus on how we should use data structures such as an `List<T>`. Using a framework like .NET makes it easier for software developers to create an impact on their business and solve problems for their customers. The problem is that even though you can use a list everywhere just because you're used to its APIs doesn't mean you should. Each data structure is built to serve a different purpose and knowing when to use each one within an algorithm could make a considerable difference in performance in the hot paths.

### __Goals__

The purpose of this library is to empower the software developers using .NET and enable them to leverage the best practices which sometimes could make a difference from quality or performance point of view. The goal is to pinpoint the issues concerning the usage of types found in System.Collections such as `IEnumerable<T>` or extension methods found in System.Linq. This tool is here to help developers make better performance decisions through a set of micro-optimizations.


## __Ruleset__

### __Code Analysis Examples__

* Suggesting, for instance, using an `HashSet<T>` whenever a developer is doing a heavy Contains check on their `List<T>`.

* Suggesting using a for-loop when iterating on a collection that allows random access such as `IList<T>`.

* Suggesting using a separate method to yield values lazily instead of taking the time to create a temporary list.

### __More details__

See the [ruleset](ruleset.md) file for details.

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

Each project in Visual Studio has a "treat warnings as errors" option. If you wish to treat the warnings given by the analyzers as errors:

1. Right-click on your project, select "Properties".

2. Click "Build".

3. Update your warning settings in Visual Studio by either
   1. Switching "Treat warnings as errors" from "Specific warnings" or "None" to "All"

   2. Add specific warnings from the library as errors.

## __Contributing__

Please note that we have a code of conduct. Please follow it in all your interactions with this project.

See the [contributing](CONTRIBUTING.md) file for more details.

## __Release Notes__

See the [release-notes](release-notes.md) file for details.

## __Roadmap__

Version 1.0 - Finalization of the ruleset covered by the library

Version 2.0 - Automatic code suggestions for best practices

## __License__

This project is licensed under the MIT License - see the [license](LICENSE.md) file for details

## __About Robotmaster__

Hypertherm's Robotic Software team develops Robotmaster, the world’s leading CAD/CAM robot programming software, used by some of the largest multinationals such as Boeing and Mercedes and the smallest industrial manufacturing shops performing short customized runs. To know more about Robotmaster, please visit our [website](https://www.robotmaster.com/en/).
