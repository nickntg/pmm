using System;

namespace pmm.server.Models
{
    public class MobileDataDto
    {
        public byte[] ImageContent { get; set; }
        public double BatteryLevel { get; set; }
        public string DeviceName { get; set; }
        public DateTimeOffset RecordedAt { get; set; }
    }
}
