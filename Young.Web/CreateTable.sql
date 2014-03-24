if(not Exists(Select * From SysObjects Where xtype='U' And Name='t_test')) 
begin
CREATE TABLE t_test
( ID int NOT NULL IDENTITY(1,1) PRIMARY KEY) 
end