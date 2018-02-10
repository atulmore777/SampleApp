using Bijankur.DAL;
using Bijankur.DAL.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bijankur.BL.Common
{
    public class Validation
    {
        public static IConfiguration Configuration { get; set; }
        public class BirthDateValidate : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value != null)
                {
                    DateTime _birthJoin = Convert.ToDateTime(value);
                    if (_birthJoin > DateTime.Now)
                    {
                        DataLayerContext dlContext = new DataLayerContext();
                        ErrorMessageRepository errorMessageRepository = new ErrorMessageRepository(dlContext);
                        var message = errorMessageRepository.GetByCode("106");
                        return new ValidationResult(message);
                    }
                }
                return ValidationResult.Success;
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
    }
}
