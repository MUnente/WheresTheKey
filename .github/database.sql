create database db_wheresthekey;
use db_wheresthekey;

create table role (
	id int identity(1,1) primary key,
	description varchar(30) not null
);

insert into role (description) values
('Administrador'),
('Servidor');

create table personStatus (
	id int identity(1, 1) primary key,
	description varchar(30) not null
);

insert into personStatus (description) values
('Pendente'),
('Recusado'),
('Aprovado'),
('Bloqueado');

create table person (
	id varchar(12) primary key,
	name varchar(50) not null,
	password varchar(20) not null,
	roleId int not null,
	statusId int not null,
	foreign key (roleId) references role (id),
	foreign key (statusId) references personStatus (id)
);

create table placeType (
	id int identity(1,1) primary key,
	description varchar(30) not null
);

insert into placeType (description) values
('Laboratório de Informática'),
('Laboratório de Química'),
('Laboratório de Robótica'),
('Laboratório de Física'),
('Sala de aula'),
('Auditório');

create table place (
	id int identity(1,1) primary key,
	placeNumber int null,
	placeTypeId int not null,
	foreign key (placeTypeId) references placeType (id)
);

create table reservationStatus (
	id int identity(1,1) primary key,
	description varchar(30) not null
);

insert into reservationStatus (description) values
('Pendente'),
('Recusado'),
('Aprovado');

create table reservation (
	id int identity(1,1) primary key,
	placeId int not null,
	personId varchar(12) not null,
	reservationStatus int not null,
	foreign key (placeId) references place (id),
	foreign key (personId) references person (id)
);





