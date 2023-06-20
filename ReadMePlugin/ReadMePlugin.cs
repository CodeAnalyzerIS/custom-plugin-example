using CodeAnalyzerTool.API;
using CodeAnalyzerTool.API.ConfigModel;

namespace ReadMePlugin;

public class ReadMePlugin : IPlugin
{
    public string PluginName => "Readme";
    public Task<IEnumerable<RuleViolation>> Analyze(PluginConfig pluginConfig, string? pluginsPath)
    {

        var cd = Directory.GetCurrentDirectory();
        Console.WriteLine("ReadMePlugin: Current Directory = " + cd);
        var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "README.md", SearchOption.TopDirectoryOnly);
        if (files.Length > 0) return Task.FromResult(new List<RuleViolation>().AsEnumerable());

        var rule = new Rule(
            ruleName: "ReadMeMissing",
            title: "Readme Missing Rule",
            description: "This rule enforces that the repository root contains a README.md file",
            category: "Documentation",
            pluginName: PluginName,
            targetLanguage: "NA",
            isEnabledByDefault: true,
            defaultSeverity: Severity.Warning
        );
        
        var location = new Location(
            path: cd,
            startLine: 0,
            endLine: 0,
            fileExtension: ".md"
        );

        var severity = pluginConfig.Rules.First().Severity;
        
        var result = new RuleViolation(
            rule: rule,
            message: "Repository does not contain a README.md file in the root folder!",
            location: location,
            severity: severity
        );
        
        var analysisResults = new List<RuleViolation> { result };
        return Task.FromResult(analysisResults.AsEnumerable());
    }
}