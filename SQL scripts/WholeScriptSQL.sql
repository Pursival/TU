create database HomeSequritySystem;
use homeSequritySystem;

drop table if exists SystemUsers;
create table SystemUsers( 
	userID integer primary key auto_increment,
	userName varchar(100) not null,
    userPassword varchar(100) not null,
    userType varchar(100) not null
    );
    
drop table if exists Devices;
create table Devices(
	deviceId int primary key auto_increment,
	deviceType varchar(100) not null,
	deviceBrand varchar(100) not null,
	deviceLocation varchar(100) not null
); 

drop table if exists UserActivity;
create table UserActivity( 
	activityID integer primary key auto_increment,
	deviceID int not null,
    userID int not null,
    stateOfDevice varchar(5) not null,
    dateOfUse datetime not null,
    foreign key(deviceID) references Devices(deviceId),
    foreign key(userID) references SystemUsers(userID)
    );
    
    insert into devices(deviceType, deviceBrand, deviceLocation)
values ("TV","Samsung","LivingRoom"),("Speaker","JBL","LivingRoom"),("Light","Veco","Closet"),("Light","Veco","Bathroom"),("Light","Veco","Bedroom"),("Light","Veco","Terrace"),("Light","Veco","Kitchen"),("Light","Veco","LivingRoom");
select * from devices;

insert into systemusers(userName, userPassword, userType)
values("Ivo", "123456", "Admin"),("Teo", "123", "StandartUser"),("Kiro", "1234", "StandartUser");
select * from systemusers;
use homeSequritySystem;
insert into useractivity(deviceID, userID, stateOfDevice, dateOfUse)
values(1, 1, "off", '2022-09-12 5:28:0'), (1, 1, "on", "2022-09-12  5:28:30"),(1, 1, "off", '2022-09-12  5:28:45');
select * from useractivity;