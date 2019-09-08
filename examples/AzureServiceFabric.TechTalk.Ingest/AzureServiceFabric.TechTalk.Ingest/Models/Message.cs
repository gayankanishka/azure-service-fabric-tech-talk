using System;
using System.ComponentModel.DataAnnotations;

namespace AzureServiceFabric.TechTalk.Ingest.Models
{
    /// <summary>
    /// Message Contract
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Twilio outbound number
        /// </summary>
        [Required]
        public string From { get; set; }

        /// <summary>
        /// Receivers phone number
        /// </summary>
        [Required]
        public string To { get; set; }

        /// <summary>
        /// Message body
        /// </summary>
        [Required]
        public string Body { get; set; }

        /// <summary>
        /// Unique ID to track the message
        /// </summary>
        public Guid TransactionId { get; set; }
    }
}
