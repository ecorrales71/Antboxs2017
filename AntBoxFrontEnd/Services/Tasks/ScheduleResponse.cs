using Newtonsoft.Json;
using System.Collections.Generic;

namespace AntBoxFrontEnd.Services.Tasks
{
    public class ScheduleResponse
    {
        [JsonProperty("schedules")]
        public List<Schedules> Schedules { get; set; }

    }
}