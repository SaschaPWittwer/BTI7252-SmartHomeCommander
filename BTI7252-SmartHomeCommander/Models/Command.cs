using System;

namespace BTI7252_SmartHomeCommander.Models
{
    public class Command
    {
        public Guid Receiver { get; set; }
        public string Payload { get; set; }

        public bool IsValid()
        {
            return !Receiver.Equals(Guid.Empty) && !string.IsNullOrWhiteSpace(Payload);
        }
    }
}