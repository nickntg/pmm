using FluentNHibernate.Mapping;
using pmm.data.Entities;
using pmm.data.Mappings.Custom;

namespace pmm.data.Mappings
{
    public class MobileDataMap : ClassMap<MobileData>
    {
        public MobileDataMap()
        {
            Table("mobile_data");
            Id(x => x.Id).GeneratedBy.UuidHex("N").Column("id");
            Map(x => x.DeviceName).Column("device_name");
            Map(x => x.BatteryLevel).Column("battery_level");
            Map(x => x.ImageContent).Column("image_content");
            Map(x => x.Processed).Column("processed");
            Map(x => x.RecordedAt).CustomType<PostgresqlTimestamptz>().Column("recorded_at");
            Map(x => x.CreatedAt).CustomType<PostgresqlTimestamptz>().Column("created_at");
        }
    }
}
