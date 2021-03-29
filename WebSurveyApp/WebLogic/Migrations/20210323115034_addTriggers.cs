using Microsoft.EntityFrameworkCore.Migrations;

namespace WebLogic.Migrations
{
    public partial class addTriggers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
  CREATE TRIGGER Surveys_Insert_Update_Delete
  ON Surveys
  AFTER INSERT, UPDATE, DELETE
  AS
  BEGIN
    WITH Inserted_Deleted AS
	(
	 SELECT Id FROM Inserted
	 UNION
	 SELECT Id FROM Deleted
	)
    UPDATE Surveys
    SET Modified = GETDATE()
    WHERE Id = (SELECT Id FROM Inserted_Deleted)
  END
  GO

  CREATE TRIGGER Questions_Insert_Update_Delete
  ON Questions
  AFTER INSERT, UPDATE, DELETE
  AS
  BEGIN
	WITH Inserted_Deleted AS
	(
	 SELECT SurveyID FROM Inserted
   	 UNION
 	 SELECT SurveyID FROM Deleted
	 )
	 UPDATE Surveys
	 SET Modified = GETDATE()
	 WHERE Id = (SELECT SurveyId FROM Inserted_Deleted)
  END
  GO

  CREATE TRIGGER Options_Insert_Update_Delete
  ON Options
  AFTER INSERT, UPDATE, DELETE
  AS
  BEGIN
  WITH Inserted_Deleted AS
  (
	SELECT QuestionId FROM Inserted
	UNION
	SELECT QuestionId FROM Deleted
  )
	UPDATE Surveys
	SET Modified = GETDATE()
	WHERE Id = (SELECT SurveyID FROM Inserted_Deleted as ins_del
	JOIN Questions as q
    ON ins_del.QuestionId = q.Id
    GROUP BY q.SurveyId)
  END
  GO
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"  DROP TRIGGER Surveys_Insert_Update_Delete
  DROP TRIGGER Questions_Insert_Update_Delete
  DROP TRIGGER Options_Insert_Update_Delete
  GO
");
        }
    }
}
