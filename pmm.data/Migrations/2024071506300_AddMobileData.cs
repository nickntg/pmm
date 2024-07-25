using FluentMigrator;

namespace AccountsApi.Core.DataAccess.Migrations
{
	[Migration(1)]
	public class _2024071506300_AddMobileData : AutoReversingMigration
	{
		public override void Up()
		{
            IfDatabase("PostgreSQL")
                .Create.Table("mobile_data")
                .WithColumn("id").AsString(32).NotNullable()
                .WithColumn("device_name").AsString(250).NotNullable()
                .WithColumn("battery_level").AsDouble().NotNullable()
                .WithColumn("processed").AsBoolean().NotNullable()
                .WithColumn("image_content").AsBinary().NotNullable()
                .WithColumn("recorded_at").AsCustom("timestamp with time zone").Nullable()
                .WithColumn("created_at").AsCustom("timestamp with time zone").NotNullable();

            IfDatabase("PostgreSQL")
                .Create.PrimaryKey("pk_mobiledata").OnTable("mobile_data").Column("id");
            IfDatabase("PostgreSQL")
                .Create.Index("idx_mobiledata_created_at").OnTable("mobile_data").OnColumn("created_at");
            IfDatabase("PostgreSQL")
                .Create.Index("idx_mobiledata_recorded_at").OnTable("mobile_data").OnColumn("recorded_at");
            IfDatabase("PostgreSQL")
                .Create.Index("idx_mobiledata_device_name").OnTable("mobile_data").OnColumn("device_name");
            IfDatabase("PostgreSQL")
                .Create.Index("idx_mobiledata_processed").OnTable("mobile_data").OnColumn("processed");
        }
	}
}