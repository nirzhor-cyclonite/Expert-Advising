create database ExpertAdvisingTrial

use ExpertAdvisingTrial

--UserInfo Entity
create table UserInfo(
	Id int identity(1,1) unique,
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
	Id int identity(1,1) unique,
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
	--Expertise varchar(7500),
	ExpertiseSectors varchar(7500),
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
	Rating decimal(3,2),
	--Review varchar(1000)
	UserReview varchar(1000)
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
	Id int identity(1,1) unique,
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

select * from UserInfo
select * from Expert
select * from Session
select * from SessionDetails
select * from SessionNote
select * from Review
select * from CancelledSession
select * from Calendar
select * from Content
select * from Expertise

select ExpertId from Expert where ExpertId='mithila_sharmin@gmail.com'

alter table Review alter column Rating decimal(3,2)


select count(SessionId) from Session where UserId='maliha@gmail.com' 
select count(SessionId) from Session where UserId='nirzhor@gmail.com'
select count(SessionId) from Session where UserId='sujoy@gmail.com'
select count(SessionId) from Session where UserId='nashfin@gmail.com'
select count(SessionId) from Session where UserId='anita@gmail.com'


select count(SessionId) from Session where UserId='maliha@gmail.com' and CompletionStatus='UPCOMING'
select count(SessionId) from Session where UserId='maliha@gmail.com' and CompletionStatus='COMPLETED'


