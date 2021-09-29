CREATE PROCEDURE [dbo].[spLoadProducts]
	
AS
begin
	
	set nocount on;

	SELECT Id, ProductName, [Description], RetailPrice, QuantityInStock
	from [dbo].[Product]
	order by ProductName
end
