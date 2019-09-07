using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace AzureServiceFabric.TechTalk.Ingest.Models
{
    public class Message
    {
        [Required]
        public string From { get; set; }

        [Required]
        public string To { get; set; }

        [Required]
        public string Body { get; set; }

        public Guid TransactionId { get; set; }
    }
}
