CREATE PROCEDURE [dbo].[spLoadSaleId]
	@CashierId nvarchar(128),
	@SaleDate datetime2
AS
begin
	SELECT Id 
	from [dbo].Sale
	where CashierId = @CashierId and SaleDate = @SaleDate;
end
