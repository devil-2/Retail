﻿CREATE PROCEDURE [dbo].[spSaleInsert]
	@Id int output,
	@CashierId nvarchar(128),
	@SaleDate datetime2,
	@SubTotal money,
	@Tax money,
	@Total money
AS
Begin
	set nocount on;
	insert into dbo.Sale(CashierId, SaleDate, SubTotal, Tax, Total) values
	(@CashierId,@SaleDate,@SubTotal,@Tax,@Total);

	select @Id = @@IDENTITY;
End

