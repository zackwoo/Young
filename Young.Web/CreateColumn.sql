if not exists(select * from syscolumns where id=object_id('t_test') and name='name') 
begin
--�����
ALTER TABLE t_test ADD [name]  nvarchar(50) 
END
ELSE BEGIN
--�޸���
alter table t_test alter column [name] varchar(4000) 
end
