CREATE PROCEDURE [dbo].[spSaleDetailInsert]
	@SaleId int,
	@ProductId int,
	@Quantity int,
	@PurchasePrice money,
	@Tax money
AS
Begin
	set nocount on;
	insert into dbo.SaleDetail(SaleId, ProductId, Quantity, PurchasePrice, Tax) values
	(@SaleId, @ProductId, @Quantity, @PurchasePrice, @Tax);
End