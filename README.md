# Fusionary DotNet

Common DotNet libraries

## Publish All Packages

(run in solution directory)
```shell
dotnet pack fusionary.sln --configuration Release
dotnet nuget push "**/*.nupkg" --skip-duplicate
```

## Publish a Single Package

(run in project directory)
```shell
dotnet pack --configuration Release
dotnet nuget push "**/*.nupkg" --skip-duplicate
```