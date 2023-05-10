# Custom Plugin Example

As is explained in the Code Analyzer Tool (C.A.T.) repository: it is possible to extend the functionality of the analysis by creating your own plugins. More information can be found in the C.A.T. [documentation](https://github.com/CodeAnalyzerIS/code-analyzer-tool/wiki).  
In this repository you can find an example of a custom Plugin which can be added to the Code Analyzer Tool.  

## How to create your own custom plugin?

To create your own custom plugin, you have add the following nuget package to your project:  https://www.nuget.org/packages/CodeAnalyzerTool.API  

This nuget package contains an API with a interface called `IPlugin` which should be implemented in your own custom Plugin `Class`.  
The interface contains only a single method, `Analyze` in which you will return the results of your analysis in the form of `Task<IEnumerable<AnalysisResult>>`.  

To enable and configure the plugin you must add the necessary information to the `plugins` list inside the config file. More information about configuration can be found [here](https://github.com/CodeAnalyzerIS/code-analyzer-tool/wiki/Configuration).

## How to use/install the custom plugin

The first important step is to edit the `.csproj` of the project to allow for the dependencies to be generated as `.dll`'s as well.  
This can be accomplished by doing the following:  
Add the following line inside the `PropertyGroup` element.  
```
<EnableDynamicLoading>true</EnableDynamicLoading>
```
The PropertyGroup could look something like this:  
```
<PropertyGroup>
    <TargetFramework>net7.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <EnableDynamicLoading>true</EnableDynamicLoading>
</PropertyGroup>
```
If you don't want the project to create unnessecary dependencies (which are already implemented in the source code), 
you can check the source code dependencies and add the following line to the ones that are already available and you don't want to be generated again:  
```
<ExcludeAssets>runtime</ExcludeAssets>
```
Your package reference could look like this:  
```
<PackageReference Include="CodeAnalyzerTool.RoslynPlugin.API" Version="0.0.5" >
    <ExcludeAssets>runtime</ExcludeAssets>
</PackageReference>
```
  
After you have done these configurations, you can build the project but make sure the `.csproj` changes were saved.  

Once the files were generated, go to the directory where they were generated and copy them all.  
Then paste the files in the plugin folder which is specified by the configuration file (more information on that can be found here: https://github.com/CodeAnalyzerIS/code-analyzer-tool/wiki/Configuration)  

Our example for the directory structure was:  
rootOfRepository/CAT/Roslyn/rules/LicenseCheck/
  
IMPORTANT:  
Make sure to use the 'rules' folder in the Roslyn plugin folder and then use the name specified in the rule as `DiagnosticId` as foldername for the rule files.  

If you run the tool now, the custom rule should be implemented by the tool.
