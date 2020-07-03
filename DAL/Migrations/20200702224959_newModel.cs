using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class newModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"
IF OBJECT_ID('PROC_ECHEANCE_RELANCE') IS NOT NULL
DROP PROCEDURE PROC_ECHEANCE_RELANCE
go
CREATE PROCEDURE PROC_ECHEANCE_RELANCE
	@dateDebut varChar(10)
	 ,
	@dateFin varChar(10),
	@DB varchar(25)
AS
Declare
@SCHEMA varchar(3)='DBO',
@SQL varchar(max);
set @SQL =
 'SELECT DOSSIER.DOSSIER_NUMERO DossierNumero,LIBELLE_ENGAGEMENT LibelleEngagement,
	    ETCIV_MATRICULE EtcivMatricule,ETCIV_NOMREDUIT EtcivNomreduit,
	    ETCIV_TELEPHONE EtcivTelephone,ETCIV_ADRESS_GEOG1 EtcivAdressGeog1,
	    ETCIV_NUMCPT_CONTRIB EtcivNumcptContrib, ECHAPP_NUMERO EchappNumero,
	    ECHAPP_DATE EchappDate,ECHAPP_MONT_CAPITAL EchappMontCapital,
	    ECHAPP_MONT_TAXE EchappMontTaxe,ECHAPP_MONT_ECH EchappMontEch,
	    ECHAPP_DATE EchappDateTombEche
FROM
(
    SELECT D.DOSSIER_NUMERO,T.LIBELLE_ENGAGEMENT,
		 D.ETCIV_MATRICULE,E.ETCIV_NOMREDUIT,
		 ETCIV_TELEPHONE,E.ETCIV_ADRESS_GEOG1,
		 ETCIV_NUMCPT_CONTRIB
    FROM  ['+@DB+'].['+@SCHEMA+'].DOSSIER_CLIENT D
    JOIN ['+@DB+'].['+@SCHEMA+'].ETAT_CIVIL E ON (D.ETCIV_MATRICULE=E.ETCIV_MATRICULE)
    JOIN ['+@DB+'].['+@SCHEMA+'].TYPE_ENGAGEMENT T ON(D.CODE_TYPE_ENGAGEMENT=T.CODE_TYPE_ENGAGEMENT)
    WHERE D.DOSSIER_ETAT IN (''ML'',''IR'',''MP'')
    AND (T.TYPENG_TYPDOSS =''30'' OR T.TYPENG_TYPDOSS =''02'')   AND D.PERIODIC_CODE <> ''00''
) DOSSIER
  JOIN
	    (SELECT DOSSIER_NUMERO,LS_TA_ECHAPP_NUMERO ECHAPP_NUMERO ,
		  	  LS_TA_DATE_ECHEANCE ECHAPP_DATE ,LS_TA_MT_CAPITAL ECHAPP_MONT_CAPITAL,
		  	  LS_TA_MT_TAXE ECHAPP_MONT_TAXE,	  LS_TA_MT_ECHEANCE+LS_TA_MT_TAXE ECHAPP_MONT_ECH,
		  	  LS_TA_DATE_ECHEANCE ECHAPP_DATE_TOMB_ECHE
		FROM ['+@DB+'].['+@SCHEMA+'].LEASE_TAB_AMORTISSEMENT P
	     WHERE
		      LS_TA_ECHAPP_NUMERO NOT IN (SELECT CONVERT(CHAR(3),ECHAPP_NUMERO)
			 FROM ['+@DB+'].['+@SCHEMA+'].ECHEAN_APPEL E
			 WHERE E.DOSSIER_NUMERO=P.DOSSIER_NUMERO
			  and ECHAPP_DATE between '''+@dateDebut+''' and '''+@dateFin+ '''
		       )
		      AND DOSSIER_NUMERO IN (
		      					   SELECT DOSSIER_NUMERO
								   FROM ['+@DB+'].['+@SCHEMA+'].PRET_COMPLEMENT_DOSSIER
		      					   WHERE CMP_SUSPENSION_TRAIT<>1
		      			       )
		  UNION
		  SELECT DOSSIER_NUMERO,CONVERT(CHAR(3),ECHAPP_NUMERO),
		  	  ECHAPP_DATE,ECHAPP_MONT_CAPITAL,
		  	  ECHAPP_MONT_TAXE,ECHAPP_MONT_ECH,
		  	  ECHAPP_DATE ECHAPP_DATE_TOMB_ECHE
		   FROM	  ['+@DB+'].['+@SCHEMA+'].PRET_TABLEAU_AMORTISSEMENTS P
		   WHERE ECHAPP_NUMERO NOT IN (
		  							 SELECT ECHAPP_NUMERO
									 FROM ['+@DB+'].['+@SCHEMA+'].ECHEAN_APPEL E WHERE E.DOSSIER_NUMERO=P.DOSSIER_NUMERO
									  and ECHAPP_DATE between '''+@dateDebut+''' and '''+@dateFin+'''
		    					  )
		  	   AND DOSSIER_NUMERO IN (SELECT DOSSIER_NUMERO
								 FROM
								 ['+@DB+'].['+@SCHEMA+'].PRET_COMPLEMENT_DOSSIER
								   where CMP_SUSPENSION_TRAIT<>1 )
) ECHEANCES
ON DOSSIER.DOSSIER_NUMERO =ECHEANCES.DOSSIER_NUMERO
WHERE ECHAPP_DATE BETWEEN '''+@dateDebut+''' AND '''+@dateFin+'''
AND
ETCIV_MATRICULE COLLATE French_CI_AS IN (SELECT MATRICULE FROM CONSENTEMENTS)
';
Execute (@SQL)
";

            migrationBuilder.Sql(sp);

            migrationBuilder.CreateTable(
                name: "Consentements",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Matricule = table.Column<string>(nullable: true),
                    Nom = table.Column<string>(nullable: true),
                    Tel = table.Column<string>(nullable: true),
                    Mail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consentements", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Echeances",
                columns: table => new
                {
                    DossierNumero = table.Column<string>(nullable: false),
                    LibelleEngagement = table.Column<string>(nullable: true),
                    EtcivMatricule = table.Column<string>(nullable: true),
                    EtcivNomreduit = table.Column<string>(nullable: true),
                    EtcivTelephone = table.Column<string>(nullable: false),
                    EtcivAdressGeog1 = table.Column<string>(nullable: true),
                    EtcivNumcptContrib = table.Column<string>(nullable: true),
                    EchappNumero = table.Column<string>(nullable: true),
                    EchappDate = table.Column<DateTime>(nullable: false),
                    EchappMontCapital = table.Column<decimal>(nullable: false),
                    EchappMontTaxe = table.Column<decimal>(nullable: false),
                    EchappMontEch = table.Column<decimal>(nullable: false),
                    EchappDateTombEche = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Echeances", x => x.DossierNumero);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateSend = table.Column<DateTime>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    Canal = table.Column<string>(nullable: true),

                    ConsentementID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Notification_Consentements_ConsentementID",
                        column: x => x.ConsentementID,
                        principalTable: "Consentements",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notification_ConsentementID",
                table: "Notification",
                column: "ConsentementID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Echeances");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Consentements");
        }
    }
}