drop table if exists Devices;
create table Devices(
deviceId int primary key auto_increment,
deviceType varchar(100),
deviceBrand varchar(100),
deviceLocation varchar(100)
); 