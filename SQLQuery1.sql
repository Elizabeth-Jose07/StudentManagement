use studentmanagement
go

--creating tables


--students table
create table students(studentid int not null primary key,studentname nvarchar(50),dob date);


--course table 
create table course(cid int not null primary key,cname nvarchar(50),cDuration nvarchar(50),fees money,[course level]
nvarchar(50),placement bit,[seats available] int ,ctype nvarchar(50))


ALTER TABLE course
ADD CONSTRAINT [seats available]
  CHECK ([seats available]>0);


--enroll table
create table Enroll(studentid nvarchar(10) references students(studentid),cid nvarchar (10) references course(cid),DateOfEnroll date,
primary key (studentid,cid));



--to insert students to data base

create proc procinsertstudents(@stid nvarchar(10),@stname nvarchar(30),@stdob date) as
begin
insert into students values(@stid,@stname,@stdob)
end


--to get student by id
create proc  procGetStudentByID(@stid nvarchar (10))
as
begin 
select * from students where studentid= @stid
end


--to get course by id
create proc procGetCourseByID(@cid nvarchar(10))
as
begin
select * from course where cid= @cid;
end

--to insert course into database

create  proc insertCourse(@cid nvarchar(10),@cname nvarchar(50),@cdur nvarchar(50),@fees money,@ctype nvarchar(50),@plc nvarchar(10),@seats int,@clevel nvarchar(50))
  as
  begin
  insert into course values(@cid,@cname,@cdur,@fees,@ctype,@plc,@seats,@clevel)
  end
  select * from course


--to insert enrollments into database
CREATE proc procenroll(@stid nvarchar(10), @cid nvarchar(10),@doe datetime) as
 begin
 insert into enroll values(@stid,@cid,@doe)
 end  
  
--to get enrollments from database
 create proc GetEnrollments
 as
 begin
 select s.studentid,s.studentname,c.cname,e.DateOfEnroll
 from students s inner join enroll e
 on s.studentid=e.studentid
 join course c on
 c.cid=e.cid;
 end

 --trigger on enroll to decrease the seats availble in course

 CREATE TRIGGER trig_onenroll ON enroll
AFTER INSERT
AS
begin 
update course 
-- gets the column information from the first table
SET [seats available] = [seats available]-1  
where course.cid=(select i.cid from inserted i)
end
  

  