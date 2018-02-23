using Bijankur.BL.Common;
using Bijankur.BL.ViewModels.RequestViewModel;
using Bijankur.BL.ViewModels.ResponseViewModel;
using Bijankur.DAL.Models;
using Bijankur.DAL.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Bijankur.BL.Services
{
    public interface IUserService
    {
        ResponseViewModel<UserResponseViewModel> CreateUser(UserRequestViewModel inputModel);
        ResponseViewModel<UserResponseViewModel> GetUserData(long userId);        
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IErrorMessageService errorMessageService;
        private readonly ILogger loggerService;
        public UserService(IUserRepository userRepository,
                           IErrorMessageService errorMessageService, 
                           ILoggerFactory loggerService)
        {
            this.userRepository = userRepository;
            this.errorMessageService = errorMessageService;
            this.loggerService = loggerService.CreateLogger<UserService>();
        }

        public ResponseViewModel<UserResponseViewModel> GetUserData(long userId)
        {
            
            ResponseViewModel<UserResponseViewModel> response = new ResponseViewModel<UserResponseViewModel>();
            List<Error> lstError = new List<Error>();
            UserResponseViewModel objUserResponseViewModel = new UserResponseViewModel();
            try
            {
                var finduser = userRepository.Find(userId);
                if (finduser != null)
                {
                    string birthDate = string.Empty;
                    if(finduser.BirthDate != null)
                    {
                        birthDate = Convert.ToString(finduser.BirthDate);
                    }                     

                    objUserResponseViewModel = new UserResponseViewModel()
                    {
                        userid = userId,
                        address = finduser.Address,
                        birthdate = birthDate,
                        contactnumber = finduser.ContactNumber,
                        email = finduser.Email,
                        firstname = finduser.FirstName,
                        lastname = finduser.LastName,
                        status = finduser.Status,
                        usertype = finduser.UserType
                    };
                }
                else
                {
                    var errorMessage = errorMessageService.GetErrorMessagesData("2020");
                    errorMessage = errorMessage.Replace("$$InputData$$", "User");
                    var objError = new Error { Code = "2020", Message = errorMessage };
                    lstError.Add(objError);
                }

                if (lstError.Count == 0)
                {
                    response.Status = true;
                    response.Message = "User get sucessfully";
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Result = objUserResponseViewModel;
                }
                else
                {
                    response.Status = false;
                    response.Errors = lstError;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    loggerService.LogError(1, "## [UserService][GetUserData] innerexception :" + ex.InnerException.ToString());

                    if (ex.InnerException.Message != null)
                    {
                        loggerService.LogError(1, "## [UserService][GetUserData] innerexception message :" + ex.InnerException.Message.ToString());
                    }
                }
                else
                {
                    loggerService.LogError(1, "## [UserService][GetUserData] exception :" + ex.Message.ToString());
                }

                response.Status = false;
                response.Message = errorMessageService.GetErrorMessagesData("501");
                response.StatusCode = (int)HttpStatusCode.BadRequest;              
            }
            return response;
        }

        public ResponseViewModel<UserResponseViewModel> CreateUser(UserRequestViewModel inputModel)
        {
            ResponseViewModel<UserResponseViewModel> response = new ResponseViewModel<UserResponseViewModel>();
            List<Error> lstError = new List<Error>();
            UserResponseViewModel objUserResponseViewModel = new UserResponseViewModel();
            try
            {
                loggerService.LogInformation(1, "## [UserService][CreateUser]- Start calling create user method.");

                var finduser = userRepository.FindByEmail(inputModel.email);
                if (finduser == null)
                {
                    byte[] passwordHash, passwordSalt;
                    Helper.CreatePasswordHash(inputModel.password, out passwordHash, out passwordSalt);
                    
                    Users objuser = new Users()
                    {
                        Address = inputModel.address,
                        BirthDate = DateTimeOffset.Parse(inputModel.birthdate).UtcDateTime,
                        ContactNumber = inputModel.contactnumber,
                        Email = inputModel.email,
                        FirstName = inputModel.firstname,
                        LastName = inputModel.lastname,
                        Status = inputModel.status,
                        UserType = inputModel.usertype,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,                        
                        CreatedBy = "admin",
                        UpdatedBy = "admin",
                        CreatedOn = DateTime.UtcNow,
                        UpdatedOn = DateTime.UtcNow
                    };

                    loggerService.LogInformation(1, "## [UserService][CreateUser]- Create User Model Email : " + objuser.Email + " , FirstName  : " + objuser.FirstName + " , LastName : " + objuser.LastName);
                   
                    var userId = userRepository.Add(objuser);
                    if (userId > 0)
                    {
                        string birthDate = Convert.ToString(inputModel.birthdate);

                        objUserResponseViewModel = new UserResponseViewModel()
                        {
                            userid = userId,
                            address = inputModel.address,
                            birthdate = string.Format("{0:yyyy-MM-ddTHH:mm:ssZ}", birthDate), 
                            contactnumber = inputModel.contactnumber,
                            email = inputModel.email,
                            firstname = inputModel.firstname,
                            lastname = inputModel.lastname,
                            status = inputModel.status,
                            usertype = inputModel.usertype
                        };

                        loggerService.LogInformation(1, "## [UserService][CreateUser]- User created sucessfully : " + objUserResponseViewModel.email);
                    }
                    else
                    {
                        loggerService.LogInformation(1, "## [UserService][CreateUser]- User not created in userRepository for email id : " + inputModel.email);
                        var errorMessage = errorMessageService.GetErrorMessagesData("2020");                       
                        var objError = new Error { Code = "2020", Message = errorMessage };
                        lstError.Add(objError);
                    }
                }
                else
                {
                    loggerService.LogInformation(1, "## [UserService]-[CreateUser]- User already exists ");
                    var errorMessage = errorMessageService.GetErrorMessagesData("2020");
                    errorMessage = errorMessage.Replace("$$InputData$$", "User");
                    var objError = new Error { Code = "2020", Message = errorMessage };
                    lstError.Add(objError);
                }

                if (lstError.Count == 0)
                {
                    response.Status = true;
                    response.Message = "User Created Sucessfully";
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Result = objUserResponseViewModel;
                }
                else
                {
                    response.Status = false;
                    response.Errors = lstError;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                }

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    loggerService.LogError(1, "## [UserService][CreateUser] innerexception :" + ex.InnerException.ToString());

                    if (ex.InnerException.Message != null)
                    {
                        loggerService.LogError(1, "## [UserService][CreateUser] innerexception message :" + ex.InnerException.Message.ToString());
                    }
                }
                else
                {
                    loggerService.LogError(1, "## [UserService][CreateUser] exception :" + ex.Message.ToString());
                }

                response.Status = false;
                response.Message = errorMessageService.GetErrorMessagesData("501");
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                return response;
            }
            return response;
        }


    }
}
