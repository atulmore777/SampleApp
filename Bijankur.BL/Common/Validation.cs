using BJK.BL.ViewModels.RequestViewModel;
using BJK.DAL;
using BJK.DAL.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BJK.BL.Common
{
    public class Validation
    {
        public class BirthDateValidate : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value != null)
                {
                    bool result = false;

                    var formats = new[] { "yyyy-MM-ddThh:mm:ssZ", "yyyy-MM-dd'T'HH:mm:ss'Z'" };
                    string strDate = Convert.ToString(value);
                    DateTime dt;
                    if (DateTime.TryParseExact(strDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out dt))
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }

                    if (result)
                    {
                        DateTime _birthJoin = Convert.ToDateTime(value);
                        if (_birthJoin > DateTime.Now)
                        {
                           return new ValidationResult("106");
                        }
                    }
                    else
                    {
                                           
                        return new ValidationResult("112");
                    }
                }
                return ValidationResult.Success;
            }
        }
        public class DateValidate : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                bool result = true;

                if (value != null)
                {
                    var formats = new[] {  "yyyy-MM-ddThh:mm:ssZ","yyyy-MM-dd'T'HH:mm:ss'Z'"};
                    string strDate = Convert.ToString(value);
                    DateTime dt;
                    if (DateTime.TryParseExact(strDate, formats,CultureInfo.InvariantCulture,DateTimeStyles.RoundtripKind,out dt))
                    {
                       result = true;                                                
                    }
                    else
                    {
                        result = false;
                    }
                }
                return result;
            }
        }

        public class UserStatusValidate : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                bool result = false;

                if (value != null && value != "")
                {
                    var builder = new ConfigurationBuilder()
                                 .AddJsonFile("appsettings.json")
                                 .AddEnvironmentVariables();

                    var configuration = builder.Build();

                    var userTypeString = configuration["User:Status"];
                    string[] lstUserType = userTypeString.Split(',');
                    for (int i = 0; i < lstUserType.Length; i = i + 1)
                    {
                        if(lstUserType[i].ToLower() == value.ToString().ToLower())
                        {
                            result = true;
                            break;
                        }
                    }                   
                }
                return result;
            }
        }

        public class UserTypeValidate : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                bool result = false;

                if (value != null && value != "")
                {
                    var builder = new ConfigurationBuilder()
                                 .AddJsonFile("appsettings.json")
                                 .AddEnvironmentVariables();

                    var configuration = builder.Build();

                    var userTypeString = configuration["User:Type"];
                    string[] lstUserType = userTypeString.Split(',');
                    for (int i = 0; i < lstUserType.Length; i = i + 1)
                    {
                        if (lstUserType[i].ToLower() == value.ToString().ToLower())
                        {
                            result = true;
                            break;
                        }
                    }
                }
                return result;
            }
        }

        public class UserIdValidate : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                bool result = false;
                try
                {
                    long userId = Convert.ToInt64(value);
                    DataLayerContext dlContext = new DataLayerContext();
                    UserRepository userRepository = new UserRepository(dlContext);
                    var userData = userRepository.Find(userId);
                    if (userData != null && userData.UserId > 0)
                    {
                        result = true;
                    }
                }
                catch(Exception ex)
                {
                    result = false;
                }             
                return result;
            }
        }

        public class RoleIdValidate : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                bool result = true;
                try
                {
                    if (value != null && value != "")
                    {
                        result = false;

                        int roleId = Convert.ToInt32(value);
                        DataLayerContext dlContext = new DataLayerContext();
                        RoleRepository roleRepository = new RoleRepository(dlContext);
                        var roleData = roleRepository.Find(roleId);
                        if (roleData != null && roleData.RoleId > 0)
                        {
                            result = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    result = false;
                }
                return result;
            }
        }

        public class RoleNameForUpdateValidation : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value != null && value != "")
                {
                    var model = (RoleUpdateRequestViewModel)validationContext.ObjectInstance;

                    if (!string.IsNullOrEmpty(model.rolename))
                    {
                        int roleId = model.roleid;
                        string roleName = Convert.ToString(model.rolename);
                        try
                        {
                            DataLayerContext dlContext = new DataLayerContext();
                            RoleRepository roleRepository = new RoleRepository(dlContext);
                            bool actualresult = roleRepository.ValidateRoleName(roleId, roleName);
                            if (actualresult)
                            {
                                return ValidationResult.Success;
                            }
                            else
                            {
                                return new ValidationResult("130");
                            }
                        }
                        catch (Exception ex)
                        {
                           // return new ValidationResult("2020$$areaname");
                        }
                    }
                }
                return ValidationResult.Success;
            }
        }

        public class PermissionIdValidate : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                bool result = true;
                try
                {
                    if (value != null && value != "")
                    {
                        result = false;

                        int roleId = Convert.ToInt32(value);
                        DataLayerContext dlContext = new DataLayerContext();
                        PermissionRepository permissionRepository = new PermissionRepository(dlContext);
                        var permissionData = permissionRepository.Find(roleId);
                        if (permissionData != null && permissionData.PermissionId > 0)
                        {
                            result = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    result = false;
                }
                return result;
            }
        }

    }
}
