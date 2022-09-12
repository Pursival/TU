create database HomeSequritySystem;
use homeSequritySystem;

drop table if exists UserActivity;
create table UserActivity( 
	activityID integer primary key auto_increment,
	deviceID int,
    userID int,
    stateOfDevice varchar(5),
    dateOfUse datetime,
    foreign key(deviceID) references Devices(deviceId),
    foreign key(userID) references SystemUsers(userID)
    );