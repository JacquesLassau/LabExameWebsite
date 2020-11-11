namespace LabExameWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConstrucaoDMLInicialLabExame : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TBAGENDAMENTO",
                c => new
                    {
                        COCODCON = c.Int(nullable: false, identity: true),
                        COIDPACCON = c.Int(nullable: false),
                        COIDEXCON = c.Int(nullable: false),
                        CODTHRCON = c.DateTime(nullable: false),
                        COPROTCON = c.String(),
                    })
                .PrimaryKey(t => t.COCODCON)
                .ForeignKey("dbo.TBEXAME", t => t.COIDEXCON, cascadeDelete: true)
                .ForeignKey("dbo.TBPACIENTE", t => t.COIDPACCON, cascadeDelete: true)
                .Index(t => t.COIDPACCON)
                .Index(t => t.COIDEXCON);
            
            CreateTable(
                "dbo.TBEXAME",
                c => new
                    {
                        EXCODEXA = c.Int(nullable: false, identity: true),
                        EXNOMEEXA = c.String(nullable: false, maxLength: 100),
                        EXOBSEXA = c.String(nullable: false, maxLength: 1000),
                        EXIDTPEXA = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EXCODEXA)
                .ForeignKey("dbo.TBTIPOEXAME", t => t.EXIDTPEXA, cascadeDelete: true)
                .Index(t => t.EXIDTPEXA);
            
            CreateTable(
                "dbo.TBTIPOEXAME",
                c => new
                    {
                        TPCODEXA = c.Int(nullable: false, identity: true),
                        TPNOMEEXA = c.String(nullable: false, maxLength: 100),
                        TPDESCEXA = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.TPCODEXA);
            
            CreateTable(
                "dbo.TBPACIENTE",
                c => new
                    {
                        PACODPAC = c.Int(nullable: false, identity: true),
                        PANOMEPAC = c.String(nullable: false, maxLength: 100),
                        PACPFPAC = c.String(nullable: false, maxLength: 14),
                        PANASCPAC = c.DateTime(nullable: false),
                        PASEXOPAC = c.String(nullable: false, maxLength: 10),
                        PATELPAC = c.String(nullable: false, maxLength: 14),
                        PAEMAILPAC = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.PACODPAC);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBAGENDAMENTO", "COIDPACCON", "dbo.TBPACIENTE");
            DropForeignKey("dbo.TBAGENDAMENTO", "COIDEXCON", "dbo.TBEXAME");
            DropForeignKey("dbo.TBEXAME", "EXIDTPEXA", "dbo.TBTIPOEXAME");
            DropIndex("dbo.TBEXAME", new[] { "EXIDTPEXA" });
            DropIndex("dbo.TBAGENDAMENTO", new[] { "COIDEXCON" });
            DropIndex("dbo.TBAGENDAMENTO", new[] { "COIDPACCON" });
            DropTable("dbo.TBPACIENTE");
            DropTable("dbo.TBTIPOEXAME");
            DropTable("dbo.TBEXAME");
            DropTable("dbo.TBAGENDAMENTO");
        }
    }
}
