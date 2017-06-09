create table invoiceDetail (
id int identity(1,1) primary key,
idInvoice int foreign key references invoice(id),
productname nvarchar(50),
quantity int,
unitprice money,
subtotal money,
datecreate datetime default getdate(),
status bit default 1
)
go
