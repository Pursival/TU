drop table if exists SystemUsers;
create table SystemUsers( 
	userID integer primary key auto_increment,
	userName varchar(100),
    userPassword varchar(100),
    userType varchar(100)
    );