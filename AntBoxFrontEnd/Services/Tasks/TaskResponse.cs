using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using AntBoxFrontEnd.Services.Address;
using AntBoxFrontEnd.Services.Customer;
using AntBoxFrontEnd.Services.AntBoxes;
using System.Globalization;

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

        [JsonProperty("folio")]
        public string Folio { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("service_time")]
        public bool service_time { get; set; }

        [JsonProperty("complete_after")]
        public long completeAfter { get; set; }

        [JsonProperty("complete_before")]
        public long completeBefore { get; set; }

        [JsonProperty("address")]
        public AddressResponse Address { get; set; }

        [JsonProperty("customer")]
        public CustomerResponse Customer { get; set; }

        [JsonProperty("antboxs")]
        public List<AntBoxResponse> Antboxs { get; set; }

        public String completeAfterDate {
            get {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                epoch = epoch.AddMilliseconds(completeAfter);
                return epoch.ToString("dd/MM/yyyy hh:mm tt", new CultureInfo("es-MX"));
            }
        }

        public bool verifday
        {
            get
            {
                DateTime tomorrow = DateTime.Today.AddDays(1);
                if (Status != "completed")
                {
                    return true;
                } else {
                    return false;
                }
            }
        }

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

    public class ListCustomerTask
    {
        public ListCustomerTask()
        {
            Tasks = new List<TaskResponse>();
        }

        [JsonProperty("tasks")]
        public List<TaskResponse> Tasks { get; set; }
    }
}