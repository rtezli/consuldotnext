# consuldotnext [![Build Status](https://travis-ci.org/rtezli/consuldotnext.svg?branch=master)](https://travis-ci.org/rtezli/consuldotnext)

Consul Catalog SDK for .NET Core

## Consul

Consul is a tool for service discovery and configuration. Consul is distributed, highly available, and extremely scalable.

## DNX

The .NET Execution Environment (DNX) is a software development kit (SDK) and runtime environment that has everything you need to build and run .NET applications for Windows, Mac and Linux

## consuldotnext

consuldotnext does not implement the whole Consul functionality. Instead we focus on what is essential for querying a Consul agent from code i.e. performing service discovery in ASP.NET 6 referring to the [Consul catalog API](https://www.consul.io/docs/agent/http/catalog.html).
When using consuldotnext you will still need to have a Consul agent running.

## Getting started

You need to have `npm` installed. To restore packages type

    npm install

Use `grunt` to recompile if source files have been changed. Type :

    npm install grunt
    npm install grunt-cli

To install `DNX` follow [this](http://docs.asp.net/en/latest/getting-started/index.html) guide.

Navigate to the source folder (where the `project.json` is located) and restore Nuget packages by :

    grunt restore

You should now be good to go.

## Develop

Select and editor of your choice. Open a command line in the root folder and type :

    grunt watch

This will watch for changes and rebuild the application

## Build

You can trigger the build manually by :

    grunt build

## Tests

### Unit Tests

You can trigger testing manually by :

    grunt unit-test

### Integration Tests

To run the integration tests you need to have Consul installed. There are binaries for the most operating system which can be downloaded [here](https://www.consul.io/downloads.html)

To run the tests just call

    grunt int-test
