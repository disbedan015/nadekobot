using System;
using System.IO;
using Microsoft.Build.Framework;
using Newtonsoft.Json.Linq;

public class MergeYmlTask : Microsoft.Build.Utilities.Task
{
    // all input JSON paths
    [Required]
    public ITaskItem[] InputFiles { get; set; }

    // where to write the merged file
    [Required]
    public string OutputFile { get; set; }

    public override bool Execute()
    {
        try
        {
            var merged = new JObject();

            foreach (var item in InputFiles)
            {
                var text = File.ReadAllText(item.ItemSpec);
                var obj  = JObject.Parse(text);
                merged.Merge(obj, new JsonMergeSettings {
                    MergeArrayHandling = MergeArrayHandling.Union
                });
            }

            Directory.CreateDirectory(Path.GetDirectoryName(OutputFile));
            File.WriteAllText(OutputFile, merged.ToString());
            Log.LogMessage(MessageImportance.High,
                $"Merged {InputFiles.Length} JSON files into {OutputFile}");
            return true;
        }
        catch (Exception ex)
        {
            Log.LogErrorFromException(ex, showStackTrace: true);
            return false;
        }
    }
}