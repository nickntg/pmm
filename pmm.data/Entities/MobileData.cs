using System;

namespace pmm.data.Entities
{
    public class MobileData
    {
        public virtual string Id { get; set; }
        public virtual byte[] ImageContent { get; set; }
        public virtual double BatteryLevel { get; set; }
        public virtual DateTimeOffset RecordedAt { get; set; }
        public virtual string DeviceName { get; set; }
        public virtual DateTimeOffset CreatedAt { get; set; }
        public virtual bool Processed { get; set; }
    }
}
