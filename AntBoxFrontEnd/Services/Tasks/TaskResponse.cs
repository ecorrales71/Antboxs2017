using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using AntBoxFrontEnd.Services.Address;
using AntBoxFrontEnd.Services.Customer;

namespace AntBoxFrontEnd.Services.Tasks
{
    public class TaskResponse : Response
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("service_time")]
        public bool service_time { get; set; }

        [JsonProperty("completeAfter")]
        public long completeAfter { get; set; }

        [JsonProperty("completeBefore")]
        public long completeBefore { get; set; }

        [JsonProperty("address")]
        public AddressResponse Address { get; set; }

        [JsonProperty("customer")]
        public CustomerResponse Customer { get; set; }

    }

    public class ListTask
    {
        public ListTask()
        {
            Tasks = new List<TaskResponse>();
        }

        [JsonProperty("tasks")]
        public List<TaskResponse> Tasks { get; set; }
    }
}