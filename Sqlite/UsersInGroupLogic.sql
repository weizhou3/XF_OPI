--Find Users in group logic
select id from Groups where GroupName = 'Admin';
select UserId from GroupMembers where UserId is not null and GroupId = 3;
select * from Users where id in (2,5,7) order by LastName

--Delete Users logic
delete from GroupMembers where UserId in (8,9)
delete from Users where id in (8,9)
