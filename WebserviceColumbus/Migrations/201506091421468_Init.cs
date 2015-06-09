namespace WebserviceColumbus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Coordinates",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.LocationDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                        PlaceID = c.String(nullable: false),
                        CoordinatesID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Coordinates", t => t.CoordinatesID, cascadeDelete: true)
                .Index(t => t.CoordinatesID);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Note = c.String(),
                        LocationDetailsID = c.Int(nullable: false),
                        Travel_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.LocationDetails", t => t.LocationDetailsID, cascadeDelete: true)
                .ForeignKey("dbo.Travels", t => t.Travel_ID)
                .Index(t => t.LocationDetailsID)
                .Index(t => t.Travel_ID);
            
            CreateTable(
                "dbo.Travels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Paragraphs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Header = c.String(),
                        Text = c.String(),
                        PhotoID = c.Int(nullable: false),
                        FullWidth = c.Boolean(nullable: false),
                        Width = c.Int(nullable: false),
                        Height = c.Int(nullable: false),
                        Travelogue_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Travelogues", t => t.Travelogue_ID)
                .Index(t => t.Travelogue_ID);
            
            CreateTable(
                "dbo.Travelogues",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TravelID = c.Int(nullable: false),
                        Title = c.String(),
                        Published = c.Boolean(nullable: false),
                        PublishedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RatingValue = c.Double(nullable: false),
                        Travelogue_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Travelogues", t => t.Travelogue_ID)
                .Index(t => t.Travelogue_ID);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Caption = c.String(),
                        URL = c.String(),
                        LocationID = c.Int(nullable: false),
                        TravelID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
        }
        
        public override void Down()
        {
        }
    }
}
