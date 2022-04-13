create database WebHocTiengAnh
Go
use WebHocTiengAnh
Go;
CREATE TABLE UserAccount (
    UserID int NOT NULL AUTO_INCREMENT,
    Email varchar(255),
    FullName varchar(255),
	UserPassword varchar(255),
	IsDeleted bit,
	Management bit,
	PRIMARY KEY (UserID),
	Primary Key(Email)
);
Go;
create proc spGetUserByEmail @Email nvarchar(50)
as
begin try
	select UserID, Email, FullName, UserPassword, IsDeleted, Management from UserAccount where @Email = UserAccount.Email
end try

begin catch
end catch
go

create proc spGetAllUserAccount
As
Begin select UserID, Email, FullName from UserAccount
End
Go

exec spGetUserByEmail @Email = '345@gmail.com'
go

ALTER TABLE UserAccount
ADD CONSTRAINT UQ_UserAccount UNIQUE(Email);
go;
create type DataUserType as table(
	[UserID] int NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[FullName] [varchar](255) NOT NULL,
	[UserPassword] [varchar] (255) NOT NULL,
	[Management] [bit] NULL,
	[IsDeleted] [bit] NULL
)
Go;

create proc tblKhach_insert
@DataUserType DataUserType READONLY,
@ResCode VARCHAR(10) output,
@ResDes VARCHAR(10)	output
as begin tran
begin try
	insert into UserAccount([UserID],[Email],[FullName], [UserPassword], [IsDeleted], [Management])
	select * from @DataUserType

	set @ResCode = '00'
	set @ResDes = 'success'
	commit tran
	end try
begin catch
end catch
go;

create proc spUserInsert 
@Email varchar(255),
@FullName varchar(255),
@Password varchar(255),
@IsDeleted bit,
@Management bit

as begin 
	insert into UserAccount values(
	@Email, @FullName, @Password, @IsDeleted, @Management )
end
Go

create proc spUserUpdate
@Email varchar(255),
@FullName varchar(255),
@Password varchar(255),
@IsDeleted bit,
@Management bit
as begin tran
begin try
	update UserAccount
	Set
		FullName = @FullName,
		UserPassword = @Password,
		IsDeleted = @IsDeleted,
		Management = @Management
	where @Email = Email
	commit tran
	end try
begin catch
end catch
go
create proc spDeleteUser
@Email varchar(255)
as begin tran
begin try
	update UserAccount
	Set
		IsDeleted = '1'
	where @Email = Email
	commit tran
	end try
begin catch
end catch
Go

create proc spPhanQuyen
@Email varchar(255)
as begin tran
begin try
	update UserAccount
	Set
		UserAccount.Management = 1
	where @Email = Email
	commit tran
	end try
begin catch
end catch
Go


exec spPhanQuyen 'Hieu@gmail.com'
go;
Select * from UserAccount




