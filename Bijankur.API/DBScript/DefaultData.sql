
INSERT INTO Roles (RoleName, RoleDesc, IsSystem,CreatedOn,CreatedBy,UpdatedOn,UpdatedBy,IsDeleted)
VALUES ('Admin', 'Admin Description', 1,GETDATE(),'-',GETDATE(),'-',0);

INSERT INTO Roles (RoleName, RoleDesc, IsSystem,CreatedOn,CreatedBy,UpdatedOn,UpdatedBy,IsDeleted)
VALUES ('Driver', 'Driver Description', 1,GETDATE(),'-',GETDATE(),'-',0);

INSERT INTO ApplicationInfo (Apptoken, AppName, selfregisterdefaultRoleid)
VALUES ('0d971cb11fe24bb2994738d42f8e3573', 'Mobile', 2);

INSERT INTO ApplicationInfo (Apptoken, AppName, selfregisterdefaultRoleid)
VALUES ('72f303a7-f1f0-45a0-ad2b-e6db29328b1a', 'Web', 2);


/* Added Error Message */


 INSERT INTO ErrorMessage (ErrorMessageId, ErrorCode,Module, Message ,ErrorLanguage,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy)
VALUES (1, '101','User','Required first name','en',getdate(),getdate(),'admin','admin');

 INSERT INTO ErrorMessage (ErrorMessageId, ErrorCode,Module, Message ,ErrorLanguage,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy)
VALUES (2, '102','User','Required last name','en',getdate(),getdate(),'admin','admin');

 INSERT INTO ErrorMessage (ErrorMessageId, ErrorCode,Module, Message ,ErrorLanguage,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy)
VALUES (3, '103','User','Required email','en',getdate(),getdate(),'admin','admin');

 INSERT INTO ErrorMessage (ErrorMessageId, ErrorCode,Module, Message ,ErrorLanguage,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy)
VALUES (4, '104','User','Required contact number','en',getdate(),getdate(),'admin','admin');

 INSERT INTO ErrorMessage (ErrorMessageId, ErrorCode,Module, Message ,ErrorLanguage,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy)
VALUES (5, '105','User','Required birth date','en',getdate(),getdate(),'admin','admin');

 INSERT INTO ErrorMessage (ErrorMessageId, ErrorCode,Module, Message ,ErrorLanguage,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy)
VALUES (6, '106','User','Birth date can not be greater than current date','en',getdate(),getdate(),'admin','admin');

INSERT INTO ErrorMessage (ErrorMessageId, ErrorCode,Module, Message ,ErrorLanguage,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy)
VALUES (7, '107','User','Invalid email','en',getdate(),getdate(),'admin','admin');

INSERT INTO ErrorMessage (ErrorMessageId, ErrorCode,Module, Message ,ErrorLanguage,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy)
VALUES (8, '108','User','Invalid user status','en',getdate(),getdate(),'admin','admin');

INSERT INTO ErrorMessage (ErrorMessageId, ErrorCode,Module, Message ,ErrorLanguage,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy)
VALUES (9, '109','User','Invalid user type','en',getdate(),getdate(),'admin','admin');

INSERT INTO ErrorMessage (ErrorMessageId, ErrorCode,Module, Message ,ErrorLanguage,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy)
VALUES (11, '111','User','Required password','en',getdate(),getdate(),'admin','admin');

INSERT INTO ErrorMessage (ErrorMessageId, ErrorCode,Module, Message ,ErrorLanguage,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy)
VALUES (12, '112','User','Invalid birth date','en',getdate(),getdate(),'admin','admin');

INSERT INTO ErrorMessage (ErrorMessageId, ErrorCode,Module, Message ,ErrorLanguage,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy)
VALUES (13, '113','User','Required username','en',getdate(),getdate(),'admin','admin');

INSERT INTO ErrorMessage (ErrorMessageId, ErrorCode,Module, Message ,ErrorLanguage,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy)
VALUES (14, '114','User','Required password','en',getdate(),getdate(),'admin','admin');

INSERT INTO ErrorMessage (ErrorMessageId, ErrorCode,Module, Message ,ErrorLanguage,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy)
VALUES (15, '115','User','Invalid login attempt','en',getdate(),getdate(),'admin','admin');

INSERT INTO ErrorMessage (ErrorMessageId, ErrorCode,Module, Message ,ErrorLanguage,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy)
VALUES (16, '116','User','Users found','en',getdate(),getdate(),'admin','admin');

INSERT INTO ErrorMessage (ErrorMessageId, ErrorCode,Module, Message ,ErrorLanguage,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy)
VALUES (17, '117','User','Users not found','en',getdate(),getdate(),'admin','admin');


INSERT INTO ErrorMessage (ErrorMessageId, ErrorCode,Module, Message ,ErrorLanguage,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy)
VALUES (18, '118','User','Userid not found','en',getdate(),getdate(),'admin','admin');

INSERT INTO ErrorMessage (ErrorMessageId, ErrorCode,Module, Message ,ErrorLanguage,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy)
VALUES (19, '119','User','Required userid ','en',getdate(),getdate(),'admin','admin');

INSERT INTO ErrorMessage (ErrorMessageId, ErrorCode,Module, Message ,ErrorLanguage,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy)
VALUES (20, '120','User','User not updated ','en',getdate(),getdate(),'admin','admin');

INSERT INTO ErrorMessage (ErrorMessageId, ErrorCode,Module, Message ,ErrorLanguage,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy)
VALUES (21, '121','Role','Role already exists','en',getdate(),getdate(),'admin','admin');

INSERT INTO ErrorMessage (ErrorMessageId, ErrorCode,Module, Message ,ErrorLanguage,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy)
VALUES (22, '122','Role','Role not created','en',getdate(),getdate(),'admin','admin');

INSERT INTO ErrorMessage (ErrorMessageId, ErrorCode,Module, Message ,ErrorLanguage,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy)
VALUES (23, '123','Role','Roles found','en',getdate(),getdate(),'admin','admin');

INSERT INTO ErrorMessage (ErrorMessageId, ErrorCode,Module, Message ,ErrorLanguage,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy)
VALUES (24, '124','Role','Roles not found','en',getdate(),getdate(),'admin','admin');

INSERT INTO ErrorMessage (ErrorMessageId, ErrorCode,Module, Message ,ErrorLanguage,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy)
VALUES (25, '125','Role','Role not found','en',getdate(),getdate(),'admin','admin');



INSERT INTO Permission (CreatedOn, PermissionCode ,Name,PermissionName,UpdatedOn)
VALUES (getdate(),'Area:Create','Area Create', 'Area Create',getdate());

INSERT INTO RolePermission (RoleId, PermissionID ,CreatedOn,CreatedBy,UpdatedOn,UpdatedBy,IsDeleted)
VALUES (1,1,getdate(),'admin',getdate(),'admin',null);

/* User */

insert into Users(FirstName,LastName,Email,ContactNumber,Address,BirthDate,
CreatedOn,CreatedBy,UpdatedOn,UpdatedBy,LastLoggedIn,Status,UserType,Aspnetuserid)
values('admin','demo','admin@demo.com','0123456789','NA',getdate(),getdate(),null,getdate(),null,getdate(),'NA','NA','1d4bbd2d-9c76-489d-bb20-b23e03f1ce50')

insert into UserRole(RoleId,CreatedOn,UpdatedOn,ASPNETUserId)
values(1,null,null,'1d4bbd2d-9c76-489d-bb20-b23e03f1ce50')




/* Adding Driver Data */



insert into Email(Body,Subject,Type,CreatedOn,UpdatedOn)
values('<!DOCTYPE HTML PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>ERROR - Exection</title>
</head>
<body>

    <table border="1">
        <tbody>
            <tr>
                <td>Error date:</td>
                <td>%%ErrorDate%%</td>
            </tr>
            
            <tr>
                <td>UserName:</td>
                <td>%%UserName%%</td>
            </tr>
            <!--<tr>
                <td>Error Path:</td>
                <td>%%ErrorPath%%</td>
            </tr>-->
            <tr>
                <td>App Name:</td>
                <td>%%AppName%%</td>
            </tr>
            <tr>
                <td>Module Name:</td>
                <td>%%ModuleName%%</td>
            </tr>
            <tr>
                <td>Method Name:</td>
                <td>%%MethodName%%</td>
            </tr>
            <tr>
                <td>Exception Message:</td>
                <td>%%Message%%</td>
            </tr>
            <tr>
                <td>Inner Exception Details:</td>
                <td>%%StackTrace%%</td>
            </tr>
            <tr>
                <td>Extra Information:</td>
                <td>%%ExtraInfo%%</td>
            </tr>
        </tbody>
    </table>

</body>
</html>
','Exception' ,'Exception' ,getdate(),getdate())


insert into Email(Body,Subject,Type,CreatedOn,UpdatedOn)
values('<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title></title>
</head>
<body>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td align="center" valign="top">
                <table width="614" border="0" cellspacing="0" cellpadding="5" bgcolor="#1B75BC">
                    <tr>
                        <td>
                            <table width="614" border="0" cellspacing="0" cellpadding="0" bgcolor="#ffffff">
                                <tr>
                                    <td>
                                        <table width="590" border="0" cellspacing="17" cellpadding="0">
                                            <tr>
                                                <td>
                                                    <table width="580px" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td>
                                                                <table width="580px" border="0" cellspacing="0" cellpadding="0">
                                                                 
                                                                    <tr>
                                                                        <td>
                                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                <tr>
                                                                                    <td valign="top">
                                                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                            <tr>
                                                                                                <td style="font-family: Helvetica; font-size: 14px; line-height: 19px; text-align: left;
                                                                                        color: #666;">
                                                                                                    <br />
                                                                                                    <div style="font-family: Helvetica; font-size: 12px; line-height: 15px; color: #5a5a5a;
                                                                                            font-weight: normal; text-align: left;">
                                                                                                        Dear <b>%%USERNAME%%</b>,
                                                                                                    </div>
                                                                                                    <p style="font-size: 12px;">
                                                                                                       Welcome to skipcart
																									   Your registration has been submitted. You will be notified upon accepted                                                                                                
                                                                                                       <br />
                                                                                                        We look forward to a long and successful association.
                                                                                                    </p>
                                                                                                    <p>Your login id is: <b>%%LOGINID%%</b></p>
                                                                                                    <p>Your login password is: <b>%%USERPASS%%</b></p>
                                                                                               

                                                                                                    <p style="font-size: 12px;">
                                                                                                        Thank you,
                                                                                                    </p>
                                                                                                    <p style="font-size: 12px;">
                                                                                                        Skip Cart Team
                                                                                                    </p>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-family: Arial; font-size: 10px; line-height: 12px; text-align: center;color: white;padding-bottom: 18px;">
                            <br />
                            Copyright &copy; 2017 <span style="color: white">skipcart.com All Rights Reserved</span>
                        </td>
                    </tr>
                </table>
</body>
</html>','Registration' ,'Registration' ,getdate(),getdate())


insert into Email(Body,Subject,Type,CreatedOn,UpdatedOn)
values('<!DOCTYPE HTML PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>ERROR - Exection</title>
</head>
<body>

   SMS CODE : <b>%%SMSCODE%%</b>
   

</body>
</html>','SMSCode' ,'SMSCode' ,getdate(),getdate())
