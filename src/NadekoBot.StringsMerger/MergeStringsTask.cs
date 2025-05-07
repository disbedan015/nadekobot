using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Core;

public class MergeStringsTask : Task
{
    // all input JSON paths
    [Required]
    public ITaskItem[] InputResponses { get; set; }

    [Required]
    public ITaskItem[] InputCommands { get; set; }

    [Required]
    public ITaskItem[] InputNames { get; set; }


    // where to write the merged file
    [Required]
    public string OutputDir { get; set; }

    // Now matching .yml instead of .json
    private readonly Regex _yamlRegex = new(@"res(?:\.(?<lang>.+))?\.yml$", RegexOptions.IgnoreCase);

    public override bool Execute()
    {
        try
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .IgnoreUnmatchedProperties()
                .Build();

            var serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull)
                .Build();

            // lang → merged dictionary
            var mergedByLang = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);
            var processedFileCount = 0;

            foreach (var item in InputResponses)
            {
                var filePath = item.ItemSpec;
                var fileName = Path.GetFileName(filePath);
                var match = _yamlRegex.Match(fileName);

                if (!match.Success)
                {
                    Log.LogMessage(MessageImportance.Low,
                        $"Skipping file '{fileName}' as it doesn't match res.<lang>.yml pattern.");
                    continue;
                }

                var lang = match.Groups["lang"].Value;

                try
                {
                    var text = File.ReadAllText(filePath);
                    var incoming = deserializer.Deserialize<Dictionary<string, string>>(text)
                                   ?? new Dictionary<string, string>();

                    if (!mergedByLang.TryGetValue(lang, out var existing))
                    {
                        // First file for this lang
                        mergedByLang[lang] = new Dictionary<string, string>(incoming, StringComparer.OrdinalIgnoreCase);
                    }
                    else
                    {
                        // Merge: union of keys, incoming wins on conflicts
                        foreach (var kv in incoming)
                            existing[kv.Key] = kv.Value;
                    }

                    processedFileCount++;
                }
                catch (YamlException ye)
                {
                    Log.LogError($"YAML parsing error in '{filePath}': {ye.Message}");
                }
                catch (Exception ex)
                {
                    Log.LogError($"Error processing '{filePath}': {ex.Message}");
                }
            }

            if (processedFileCount == 0)
            {
                Log.LogError("No valid YAML files were processed.");
                return false;
            }

            // Write merged YAML out
            var outResDir = Path.Combine(OutputDir, "res");
            Directory.CreateDirectory(outResDir);

            foreach (var kvp in mergedByLang)
            {
                var lang = kvp.Key;
                var data = kvp.Value;
                var outputPath = Path.Combine(outResDir,
                    string.IsNullOrWhiteSpace(lang) ? "responses/responses.yml" : $"responses/responses.{lang}.yml");

                try
                {
                    var yaml = serializer.Serialize(data);
                    File.WriteAllText(outputPath, yaml);
                    Log.LogMessage(MessageImportance.High,
                        $"Merged {data.Count} entries for '{lang}' → {outputPath}");
                }
                catch (Exception ex)
                {
                    Log.LogError($"Failed to write '{outputPath}': {ex.Message}");
                    return false;
                }
            }

            Log.LogMessage(MessageImportance.High,
                $"Successfully processed {processedFileCount} files into {mergedByLang.Count} language YAMLs.");
            return true;
        }
        catch (Exception ex)
        {
            Log.LogError($"Task failed: {ex.Message}");
            return false;
        }
    }
}