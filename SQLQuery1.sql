use studentmanagement
go

drop table course

drop table degree

drop table diploma


;


alter table [course].[degree]
ADD [placement] BOOLEAN;

--CREATE TABLE Course.Degree(cid nvarchar(10) not null primary key,

--create proc insertDegreeCourse(@cid nvarchar(10),@cname nvarchar(50),@cdur nvarchar(50),@fees money,@clevel nvarchar(50),@plc nvarchar(10),@seats int, @active nvarchar(10))
      --as
      --begin
      --insert into course.Degree values(@cid,@cname,@cdur,@fees,@clevel,@plc,@seats,@active)
      --end

--create proc insertdiplomacourse(@cid nvarchar(10),@cname nvarchar(50),@cdur nvarchar(50),@fees money,@ctype nvarchar(50),@plc nvarchar(10),@seats int, @active nvarchar(10))
--      as
--      begin
--      insert into course values(@cid,@cname,@cdur,@fees,@ctype,@plc,@seats,@active)
--      end

  --Alter  proc insertCourse(@cid nvarchar(10),@cname nvarchar(50),@cdur nvarchar(50),@fees money,@ctype nvarchar(50),@plc nvarchar(10),@seats int,@clevel nvarchar(50))
  --as
  --begin
  --insert into course values(@cid,@cname,@cdur,@fees,@ctype,@plc,@seats,@clevel)
  --end
  select * from course
  

  UPDATE course SET ctype='Bachelors' WHERE cid='12';

  
  update course set placement=1 where cid= '100'

  delete from course
  select placement from course

  select * from course
  update course set cDuration ='2 months' where cid='100';

-- create proc  procGetStudentByID(@stid nvarchar (10))
--as
--begin 
--select * from students where studentid= @stid
--end

--create proc procGetCourseByID(@cid nvarchar(10))
--as
--begin
--select * from course where cid= @cid;
--end


--alter proc procGetCourseByID(@cid nvarchar(10))
--as
--begin
--select cid,cname from course where cid= @cid;
--end

--alter proc procenroll(@stid nvarchar(10), @cid nvarchar(10),@doe datetime) as
-- begin
-- insert into enroll values(@stid,@cid,@doe)
-- end

 select * from course

 Insert into enroll(studentid,cid,DateOfEnroll)
 values(13,100,'2011-09-08')

 Insert into enroll(studentid,cid,doe)
 values(13,200,'2011-09-08')


 select * from Enroll
 alter table enroll add
 primary key(studentid,cid)

 drop table enroll

 create table Enroll(studentid nvarchar(10) references students(studentid),cid nvarchar (10) references course(cid),DateOfEnroll date, primary key (studentid,cid));

 select s.studentid,s.studentname,c.cname,e.DateOfEnroll
 from students s inner join enroll e
 on s.studentid=e.studentid
 join course c on
 c.cid=e.cid;

 --create proc GetEnrollments
 --as
 --begin
 --select s.studentid,s.studentname,c.cname,e.DateOfEnroll
 --from students s inner join enroll e
 --on s.studentid=e.studentid
 --join course c on
 --c.cid=e.cid;
 --end