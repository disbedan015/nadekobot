using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace NadekoBot.StringsMerger;

public class MergeResponseStringsTask : Task
{
    [Required]
    public ITaskItem[] InputFiles { get; set; }

    [Required]
    public string OutputDir { get; set; }

    private readonly Regex _jsonRegex = new(@"res\.(?<lang>.+)\.json$", RegexOptions.IgnoreCase);

    public override bool Execute()
    {
        try
        {
            var mergedByLang = new Dictionary<string, JObject>();
            var processedFileCount = 0;

            foreach (var item in InputFiles)
            {
                var filePath = item.ItemSpec;
                var fileName = Path.GetFileName(filePath);
                var match = _jsonRegex.Match(fileName);

                if (!match.Success)
                {
                    Log.LogMessage(MessageImportance.Low, $"Skipping file '{fileName}' as it doesn't match the expected language pattern.");
                    continue;
                }

                var lang = match.Groups["lang"].Value;
                
                try
                {
                    var text = File.ReadAllText(filePath);
                    var obj = JObject.Parse(text);

                    if (!mergedByLang.TryGetValue(lang, out var existingObj))
                    {
                        mergedByLang[lang] = obj;
                    }
                    else
                    {
                        existingObj.Merge(obj, new JsonMergeSettings
                        {
                            MergeArrayHandling = MergeArrayHandling.Union
                        });
                    }
                    processedFileCount++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}' for language '{lang}': {ex.Message}");
                }
            }

            if (mergedByLang.Count == 0 && InputFiles.Length > 0)
            {
                Console.WriteLine("No valid language files were processed.");
                throw new Exception("No valid language files were processed.");
            }
            
            if (mergedByLang.Count == 0 && InputFiles.Length == 0)
            {
                Console.WriteLine("No input files provided.");
                throw new Exception("No input files provided.");
            }


            foreach (var kvp in mergedByLang)
            {
                var lang = kvp.Key;
                var merged = kvp.Value;
                var outputFilePath = Path.Combine(OutputDir, "res", $"res.{lang}.json");

                try
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(outputFilePath));
                    File.WriteAllText(outputFilePath, merged.ToString());
                    Console.WriteLine($"Merged strings for language '{lang}' into '{outputFilePath}'");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to write merged file for language '{lang}' to '{outputFilePath}': {ex.Message}");
                    throw new Exception($"Failed to write merged file for language '{lang}' to '{outputFilePath}': {ex.Message}");
                }
            }

            Console.WriteLine($"Successfully processed {processedFileCount} files into {mergedByLang.Count} language-specific merged files in '{Path.Combine(OutputDir, "res")}'.");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to process files: {ex.Message}");
            throw new Exception($"Failed to process files: {ex.Message}");
        }
    }
}