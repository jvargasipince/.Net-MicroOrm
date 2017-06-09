create table company (
id int identity(1,1) primary key,
name nvarchar(100) not null,
address nvarchar(100) null,
phone nvarchar(10) null,
status bit default 1
)
go
