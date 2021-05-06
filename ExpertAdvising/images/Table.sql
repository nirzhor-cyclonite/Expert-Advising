create database ExpertAdvising

use ExpertAdvising

--UserInfo Entity
create table UserInfo(
	UserId varchar(300) primary key not null,
	FirstName varchar(300) not null,
	LastName varchar(300) not null,
	ProfileImage varchar(1500),
	Password varchar(500) not null,
	WalletStatus decimal(20,3) default 0,
	Interest varchar(7500),
	ActiveStatus varchar(100) default 'ACTIVE'
)

--Expert Entity
create table Expert(
	ExpertId varchar(300) primary key not null,
	FirstName varchar(300) not null,
	LastName varchar(300) not null,
	ProfileImage varchar(1500),
	Password varchar(500) not null,
	WalletStatus decimal(20,3) default 0,
	ExpertiseSector varchar(500) not null,
	BankAccount varchar(500),
	ActiveStatus varchar(100) default 'ACTIVE'
)

--Expertise Entity
create table Expertise(
	ExpertId varchar(300) foreign key references Expert(ExpertId) unique,
	Education varchar(7500) not null,
	WorkingArea varchar(7500) not null,
	Expertise varchar(7500),
	Fee decimal(10,2) not null,
	Rating decimal(5,2) not null,
	Bio varchar(7500)
)

--Session Entity
create table Session(
	SessionId int primary key identity(1,1),
	UserId varchar(300) foreign key references UserInfo(UserId),
	ExpertId varchar(300) foreign key references Expert(ExpertId),
	CompletionStatus varchar(200),
	Topic varchar(500),
)

--SessionNote Entity
create table SessionNote(
	SessionId int foreign key references Session(SessionId) unique,
	ExpertNote varchar(7500),
	UserNote varchar(7500),
)	

--Review Entity
create table Review(
	SessionId int foreign key references Session(SessionId) unique,
	Rating int,
	Review varchar(1000)
)




--Content Entity
create table Content(
	ContentId int primary key identity(1,1),
	ExpertId varchar(300) foreign key references Expert(ExpertId),
	Topic varchar(1000),
	Item varchar(1500),
	ContentType varchar(100),
	UploadDate datetime
)

--Calendar Entity
create table Calendar(
	ExpertId varchar(300) foreign key references Expert(ExpertId),
	StartingFrom datetime,
	EndingAt datetime
)

--SessionDetails Entity
create table SessionDetails(
	SessionId int foreign key references Session(SessionId) unique,
	DurationInMinutes decimal(20,2),
	Cost decimal(20,2),
	StartTime datetime,
	EndTime datetime,
	Brief_Description varchar(7500)
)

--CancelledSession Entity	 
create table CancelledSession(
	SessionId int foreign key references Session(SessionId) unique,
	CancelledFrom varchar(300),
	UserType varchar(100)
)


