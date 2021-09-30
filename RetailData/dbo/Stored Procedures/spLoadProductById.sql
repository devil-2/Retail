CREATE PROCEDURE [dbo].[spLoadProductById]
	@Id int 
AS
	begin
	
	set nocount on;

	SELECT Id, ProductName, [Description], RetailPrice, QuantityInStock, Tax
	from [dbo].[Product] 
	where Id = @Id;
end
