CREATE PROCEDURE [dbo].[spLoadUser]
	@Id nvarchar(128)
AS
begin
	set nocount on;
	Select Id, FirstName, LastName, EmailAddress, CreatedDate
	from [dbo].[User]
	where Id = @Id;
end;