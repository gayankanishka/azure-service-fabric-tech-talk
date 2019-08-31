using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AzureServiceFabric.TechTalk.Main.Models
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
