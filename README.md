# consuldotnext [![Circle CI](https://circleci.com/gh/rtezli/consuldotnext.svg?style=svg)](https://circleci.com/gh/rtezli/consuldotnext)

Consul client for DNX

## Consul

Consul is a tool for service discovery and configuration. Consul is distributed, highly available, and extremely scalable.

## DNX

The .NET Execution Environment (DNX) is a software development kit (SDK) and runtime environment that has everything you need to build and run .NET applications for Windows, Mac and Linux

## Getting started

You need to have `npm` installed. To restore packages type

    npm install

Use `grunt` to recompile if source files have been changed. Type :

    npm install grunt
    npm install grunt-cli

To install `DNX` follow [this](http://docs.asp.net/en/latest/getting-started/index.html) guide.

Navigate to the source folder (where the `project.json` is located) and restore Nuget packages by :

    dnu restore

You should now be good to go.
