drop table if exists UserPrivilige;
create table UserPrivilige( 
UserPriviligeID integer primary key auto_increment,
constraint	foreign key (UserName) references SystemUsers(UserName),
constraint  foreign key (DeviceLocation) references UserActivity(DeviceLocation)
);trqq se deklarirat kato promenlivi purvo i nai veroqtno samo primari key moje 