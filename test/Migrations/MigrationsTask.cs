﻿namespace test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    public partial class MigrationTask : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Task",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Price = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropTable("dbo.Phones");
        }
    }
}
