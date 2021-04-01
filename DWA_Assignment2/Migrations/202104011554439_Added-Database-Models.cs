namespace DWA_Assignment2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDatabaseModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        MeetId = c.Int(),
                        AgeRange = c.String(),
                        Gender = c.String(),
                        Distance = c.String(),
                        Stroke = c.String(),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.Meets", t => t.MeetId)
                .Index(t => t.MeetId);
            
            CreateTable(
                "dbo.Lanes",
                c => new
                    {
                        LaneId = c.Int(nullable: false, identity: true),
                        EventId = c.Int(),
                        LaneNumber = c.Int(nullable: false),
                        SwimmerTime = c.Time(nullable: false, precision: 7),
                        Heat = c.String(),
                        Swimmer_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.LaneId)
                .ForeignKey("dbo.AspNetUsers", t => t.Swimmer_Id)
                .ForeignKey("dbo.Events", t => t.EventId)
                .Index(t => t.EventId)
                .Index(t => t.Swimmer_Id);
            
            CreateTable(
                "dbo.FamilyGroups",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        GroupName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        Parent_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.GroupId)
                .ForeignKey("dbo.AspNetUsers", t => t.Parent_Id)
                .Index(t => t.Parent_Id);
            
            CreateTable(
                "dbo.Meets",
                c => new
                    {
                        MeetId = c.Int(nullable: false, identity: true),
                        MeetName = c.String(maxLength: 20),
                        Date = c.DateTime(nullable: false),
                        Venue = c.String(),
                        PoolLength = c.String(),
                    })
                .PrimaryKey(t => t.MeetId);
            
            AddColumn("dbo.AspNetUsers", "FamilyGroup_GroupId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "FamilyGroup_GroupId");
            AddForeignKey("dbo.AspNetUsers", "FamilyGroup_GroupId", "dbo.FamilyGroups", "GroupId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "MeetId", "dbo.Meets");
            DropForeignKey("dbo.AspNetUsers", "FamilyGroup_GroupId", "dbo.FamilyGroups");
            DropForeignKey("dbo.FamilyGroups", "Parent_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Lanes", "EventId", "dbo.Events");
            DropForeignKey("dbo.Lanes", "Swimmer_Id", "dbo.AspNetUsers");
            DropIndex("dbo.FamilyGroups", new[] { "Parent_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "FamilyGroup_GroupId" });
            DropIndex("dbo.Lanes", new[] { "Swimmer_Id" });
            DropIndex("dbo.Lanes", new[] { "EventId" });
            DropIndex("dbo.Events", new[] { "MeetId" });
            DropColumn("dbo.AspNetUsers", "FamilyGroup_GroupId");
            DropTable("dbo.Meets");
            DropTable("dbo.FamilyGroups");
            DropTable("dbo.Lanes");
            DropTable("dbo.Events");
        }
    }
}
