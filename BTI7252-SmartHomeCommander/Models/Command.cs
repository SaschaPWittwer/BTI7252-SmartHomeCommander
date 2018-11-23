using System;

namespace BTI7252_SmartHomeCommander.Models
{
    public class Command
    {
        public string Sender { get; set; }
        public string Payload { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Sender) && !string.IsNullOrWhiteSpace(Payload);
        }
    }
}