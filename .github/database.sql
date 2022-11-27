create database Wheresthekey;
use Wheresthekey;

create table role (
	id int identity(1,1) primary key,
	description varchar(30) not null
);

insert into role (description) values
('Administrator'),
('Employee');

create table personStatus (
	id int identity(1, 1) primary key,
	description varchar(30) not null
);

insert into personStatus (description) values
('Pending'),
('Refused'),
('Approved'),
('Blocked');

create table person (
	id varchar(12) primary key,
	name varchar(50) not null,
	password varbinary(1024) not null,
	passwordSalt varbinary(1024) not null,
	rolePersonId int not null,
	accountStatusId int not null,
	foreign key (rolePersonId) references role (id),
	foreign key (accountStatusId) references personStatus (id)
);

create table place (
	id int identity(1,1) primary key,
	description varchar(50) not null
);

create table reservationStatus (
	id int identity(1,1) primary key,
	description varchar(30) not null
);

insert into reservationStatus (description) values
('Pending'),
('Refused'),
('Approved');

create table reservation (
	id int identity(1,1) primary key,
	placeId int not null,
	personId varchar(12) not null,
	startDate Datetime not null,
	endDate Datetime not null,
	reservationStatus int not null,
	foreign key (placeId) references place (id),
	foreign key (personId) references person (id)
);

