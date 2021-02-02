use WFA3DotNet

select * from Employeetab

exec sp_columns EmployeeTab

ALTER TABLE EmployeeTab
alter COLUMN salary decimal(7,3);

alter proc sp_InsertRow
@empname varchar(20),
@esal float,
@deptno int
as
begin
insert into Employeetab 
values(@empname,@esal,@deptno)
end

exec sp_InsertRow 'Ramya Behara',4444.44,13

 create proc sp_UpdateEmp
 @empid int,
 @empname varchar(20),
 @esal float,
 @deptNo int 

 as
 begin
 update EmployeeTab set EmpName=@empname,
 salary=@esal,deptno=@deptno where EmpId=@empid
 end

 exec sp_UpdateEmp 18,'Ekta',3333.33,11

 create proc sp_SelectWithId
 @empid int
 as
 begin
 select e1.EmpId,e1.EmpName,e1.salary,d1.deptName
 from EmployeeTab e1
 join DeptTab d1
 on e1.deptno=d1.deptid
 where empid=@empid
 end

 exec sp_SelectWithId 3


 create proc sp_DeleteWithId
 @empid int
 as
 begin
 delete from employeetab where empid=@empid
 end

 exec sp_DeleteWithId 23