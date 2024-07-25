using System;

namespace pmm.core.Models
{
    public class MobileDataDto
    {
        public byte[] ImageContent { get; set; }
        public double BatteryLevel { get; set; }
        public string DeviceName { get; set; }
        public DateTimeOffset RecordedAt { get; set; }
    }
}
