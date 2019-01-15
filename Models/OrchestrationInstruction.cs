using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Red_Folder.ActivityTracker.Models
{
    public class OrchestrationInstruction
    {
        [JsonProperty("startFrom")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrchestrationStep StartFrom { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("weekNumber")]
        public int WeekNumber { get; set; }

        [JsonProperty("filename")]
        public string Filename { get; set; }

        public bool PerformGetWeek
        {
            get
            {
                return StartFrom == OrchestrationStep.FromStart;
            }
        }

        public bool PerformRetrieval
        {
            get
            {
                return (PerformGetWeek || StartFrom == OrchestrationStep.FromRetrieval);
            }
        }

        public bool PerformScreenCapture
        {
            get
            {
                return (PerformRetrieval || StartFrom == OrchestrationStep.FromScreenCapture);
            }
        }
    }
}
