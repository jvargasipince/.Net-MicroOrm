create table invoice (
id int identity(1,1) primary key,
nroinvoice char(5) not null,
companyid int foreign key references company(id),
customer nvarchar(100) not null,
ammount money not null,
nroproducts int not null,
datecreate datetime default getdate(),
status bit default 1
)
go
