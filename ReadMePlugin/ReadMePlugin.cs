using CAT_API;
using CAT_API.ConfigModel;

namespace ReadMePlugin;

public class ReadMePlugin : IPlugin
{
    public Task<IEnumerable<AnalysisResult>> Analyze(PluginConfig pluginConfig, string pluginsPath)
    {

        var cd = Directory.GetCurrentDirectory();
        Console.WriteLine("ReadMePlugin: Current Directory = " + cd);
        var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "README.md", SearchOption.TopDirectoryOnly);
        if (files.Length > 0) return Task.FromResult(new List<AnalysisResult>().AsEnumerable());

        var rule = new Rule(
            id: "ReadMeMissing",
            title: "Readme Missing Rule",
            description: "This rule enforces that the repository root contains a README.md file",
            category: "Documentation",
            isEnabledByDefault: true,
            defaultSeverity: Severity.Warning
        );
        
        var location = new Location(
            path: cd,
            startLine: 0,
            endLine: 0,
            fileExtension: ".md"
        );

        var severity = pluginConfig.Rules.First().Severity; // todo
        
        var result = new AnalysisResult(
            rule: rule,
            pluginId: "ReadMe",
            message: "Repository does not contain a README.md file in the root folder!",
            targetLanguage: "NA",
            location: location,
            severity: severity
        );
        
        var analysisResults = new List<AnalysisResult> { result };
        return Task.FromResult(analysisResults.AsEnumerable());
    }
}