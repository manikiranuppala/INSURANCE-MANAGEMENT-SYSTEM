create database iCareTPADB;
 use iCareTPADB;

create table Insurers(
	InsureId int primary key identity(1000,1),
	InsurerName varchar(30) not null,
	RegistrationNo int unique,
	HeadOffice varchar(80) not null
	);

create table Hospitals(
	HospitalId int primary key identity(2000,1), 
	HospitalName varchar(20) not null,
	Address varchar(30) not null,
	City varchar(20) not null,
	State varchar(20) not null,
	Pincode int not null,
	InsurerId int not null references Insurers(InsureId)
	);

create table Users(
	UserId int primary key identity(3000,1),
	FirstName varchar(20) not null,
	LastName varchar(20) not null,
	EmailId varchar(20) not null unique,
	Password varchar(20) not null,
);

alter table Insurers Alter column RegistrationNo int not null;

select * from Users;
delete Users where FirstName='Ashim';

Insert into Users (FirstName,LastName,EmailId,Password) values('Imraan','Ahamed','imraan29@gmail.com','imraan123');
Insert into Users (FirstName,LastName,EmailId,Password) values('Farveen','Banu','Farveen@gmail.com','farveen123');
Insert into Users (FirstName,LastName,EmailId,Password) values('Raj','Kumar','raj@gmail.com','raj123');
Insert into Users (FirstName,LastName,EmailId,Password) values('Ramesh','Ram','Ramesh@gmail.com','Ramesh123');
Insert into Users (FirstName,LastName,EmailId,Password) values('Shameera','Banu','Shameer@gmail.com','sameera123');

Insert into Insurers (InsurerName,RegistrationNo,HeadOffice) values('HDFC',1234,'Chennai');
Insert into Insurers (InsurerName,RegistrationNo,HeadOffice) values('AXIS',1235,'Banglore');
Insert into Insurers (InsurerName,RegistrationNo,HeadOffice) values('ICICI',1236,'Mumbai');
Insert into Insurers (InsurerName,RegistrationNo,HeadOffice) values('Kotak',1237,'Delhi');
Insert into Insurers (InsurerName,RegistrationNo,HeadOffice) values('State Bank Of India',1238,'Chennai');

Insert into Hospitals (HospitalName,Address,City,State,Pincode,InsurerId) values('Global Hospital','Chengalpatu','Chengalpatu','Tamil Nadu',600042,1000);
Insert into Hospitals (HospitalName,Address,City,State,Pincode,InsurerId) values('Appolo Hospital','waynland','waynland','Banglore',500042,1001);
Insert into Hospitals (HospitalName,Address,City,State,Pincode,InsurerId) values('ABC Hospital','Sanjay Gandhi','Sanjay Gandhi','Mumbai',400042,1002);

CREATE PROCEDURE stpFindHospitalByPincode 
@pincode int
as
begin
select * from Hospitals where Pincode=@pincode
end;

