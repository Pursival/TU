insert into devices(deviceType, deviceBrand, deviceLocation)
values ("TV","Samsung","LivingRoom"),("Speaker","JBL","LivingRoom")("Light","Veco","Closet"),("Light","Veco","Bathroom");
select * from devices;

insert into systemusers(userName, userPassword, userType)
values("Ivo", "123456", "Admin"),("Teo", "123", "StandartUser"),("Kiro", "1234", "StandartUser");
select * from systemusers;
use homeSequritySystem;
insert into useractivity(deviceID, userID, stateOfDevice, dateOfUse)
values(1, 1, "off", '2022-09-12 5:28:0'), (1, 1, "on", "2022-09-12  5:28:30"),(1, 1, "off", '2022-09-12  5:28:45');
select * from useractivity;