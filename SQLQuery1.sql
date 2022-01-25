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