/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
insert into company (name, address, phone)
values 
('Kaizen Force', 'Av. Principal 123', '123-4567'),
('Microsoft', 'Av. Camino Real 456', '987-6543'),
('Apple', 'Av. Benavides 789', '864-2097'),
('Facebook', 'Av. Pardo 852', '531-0856')
go

insert into invoice (nroinvoice, companyid, customer, ammount, nroproducts)
values 
('I0001', 1, 'Jorge Vargas', 250.00, 2),
('I0002', 2, 'Bill Gates', 500.00, 1),
('I0003', 3, 'Steve Jobs', 750.00, 2),
('I0004', 4, 'Mark Zuckerberg', 1000.00, 3)
go

insert into invoiceDetail (nroinvoiceidInvoice, productname, quantity, unitprice, subtotal)
values 
(1, 'Mouse Gamer', 1, 130.00, 130.00),
(1, 'KeyBoard Gamer', 1, 120.00, 120.00),
(2, 'Hard Drive Portable', 1, 500.00, 500.00),
(3, 'Ipad', 1, 450.00, 450.00),
(3, 'Head Phones', 1, 300.00, 300.00),
(4, 'Karpesky Antivirus', 2, 200.00, 400.00),
(4, 'Monitor LG HD', 1, 600.00, 600.00)
go