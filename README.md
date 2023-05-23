# Fusionary DotNet

Common DotNet libraries

## Publish All Packages

1. Push Branch
2. Add Tag (if you don't add a tag, this will build preview packages)
3. Build Packages

(run in solution directory)
```shell
dotnet pack fusionary.sln --configuration Release
dotnet nuget push "**/*.nupkg" --skip-duplicate
```

## Publish a Single Package

(run in project directory)
```shell
dotnet pack *.csproj --configuration Release
dotnet nuget push "**/*.nupkg" --skip-duplicate
```

(run this to cleanup left overs)
```shell
rm -rf ./**/*.nupkg
```
