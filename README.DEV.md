# Enum to class generator

### Version management

Add git tag with new version number:

`git tag v1.0.3`

If you are building packages on github, then push the tag:
`git push --tags`
The `Release.yml` workflow will build, test and publish package to nuget.

Build using release configuration:

`dotnet build -c Release`

The build command will also generate nuget package with the version eqal to the tag.

### Github secrets
Be sure to set the required github secrets

``NUGET_API_KEY`` - Your nuget api key (generated on you nuget.org account)
``NUGET_SOURCE`` - nuget packages index path. e.g. `https://api.nuget.org/v3/index.json`