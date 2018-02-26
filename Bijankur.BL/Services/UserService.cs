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
using System.IdentityModel.Tokens.Jwt;
using Bijankur.BL.Security;

namespace Bijankur.BL.Services
{
    public interface IUserService
    {
        ResponseViewModel<UserLoginResponseViewModel> Authenticate(UserLoginRequestViewModel inputModel);
        ResponseViewModel<UserRegisterResponseViewModel> CreateUser(UserRegisterRequestViewModel inputModel);
        ResponseViewModel<UserUpdateResponseViewModel> UpdateUser(UserUpdateRequestViewModel inputModel);
        ResponseViewModel<List<UserRegisterResponseViewModel>> GetAll();
        ResponseViewModel<UserRegisterResponseViewModel> GetById(long userId);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IErrorMessageService _errorMessageService;
        private readonly ISecurityHelper _securityHelper;
        private readonly ILogger _loggerService;
        public UserService(IUserRepository userRepository,
                           IErrorMessageService errorMessageService,
                           ISecurityHelper securityHelper,
                           ILoggerFactory loggerService)
        {
            _userRepository = userRepository;
            _errorMessageService = errorMessageService;
            _securityHelper = securityHelper;
            _loggerService = loggerService.CreateLogger<UserService>();
        }

        public ResponseViewModel<UserLoginResponseViewModel> Authenticate(UserLoginRequestViewModel inputModel)
        {
            ResponseViewModel<UserLoginResponseViewModel> response = new ResponseViewModel<UserLoginResponseViewModel>();
            List<Error> _lstError = new List<Error>();
            UserLoginResponseViewModel _objUserLoginResponseViewModel = new UserLoginResponseViewModel();
            try
            {
                var finduser = _userRepository.FindByEmail(inputModel.email);
                if (finduser != null)
                {
                    if (!_securityHelper.VerifyPasswordHash(inputModel.password, finduser.PasswordHash, finduser.PasswordSalt))
                    {
                        var errorMessage = _errorMessageService.GetErrorMessagesData("115");
                        var objError = new Error { Code = "115", Message = errorMessage };
                        _lstError.Add(objError);
                    }
                    else
                    {
                        string accessToken = _securityHelper.GetAccessToken(inputModel.email);
                        _objUserLoginResponseViewModel.email = finduser.Email;
                        _objUserLoginResponseViewModel.firstname = finduser.FirstName;
                        _objUserLoginResponseViewModel.lastname = finduser.LastName;
                        _objUserLoginResponseViewModel.userid = finduser.UserId;
                        _objUserLoginResponseViewModel.token = accessToken;
                    }
                }
                else
                {
                    var errorMessage = _errorMessageService.GetErrorMessagesData("115");
                    var objError = new Error { Code = "115", Message = errorMessage };
                    _lstError.Add(objError);
                }

                if (_lstError.Count == 0)
                {
                    response.Status = true;
                    response.Message = "User authenticated sucessfully";
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Result = _objUserLoginResponseViewModel;
                }
                else
                {
                    response.Status = false;
                    response.Errors = _lstError;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                }

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    _loggerService.LogError(1, "## [UserService][Authenticate] innerexception :" + ex.InnerException.ToString());

                    if (ex.InnerException.Message != null)
                    {
                        _loggerService.LogError(1, "## [UserService][Authenticate] innerexception message :" + ex.InnerException.Message.ToString());
                    }
                }
                else
                {
                    _loggerService.LogError(1, "## [UserService][Authenticate] exception :" + ex.Message.ToString());
                }

                response.Status = false;
                response.Message = _errorMessageService.GetErrorMessagesData("501");
                response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            return response;
        }
               
        public ResponseViewModel<UserRegisterResponseViewModel> CreateUser(UserRegisterRequestViewModel inputModel)
        {
            ResponseViewModel<UserRegisterResponseViewModel> response = new ResponseViewModel<UserRegisterResponseViewModel>();
            List<Error> lstError = new List<Error>();
            UserRegisterResponseViewModel objUserResponseViewModel = new UserRegisterResponseViewModel();
            try
            {
                _loggerService.LogInformation(1, "## [UserService][CreateUser]- Start CreateUser user method.");

                var finduser = _userRepository.FindByEmail(inputModel.email);
                if (finduser == null)
                {
                    byte[] passwordHash, passwordSalt;
                    _securityHelper.CreatePasswordHash(inputModel.password, out passwordHash, out passwordSalt);
                    
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
                        CreatedBy = inputModel.createdby,
                        UpdatedBy = inputModel.createdby,
                        CreatedOn = DateTime.UtcNow,
                        UpdatedOn = DateTime.UtcNow
                    };

                    _loggerService.LogInformation(1, "## [UserService][CreateUser]- Create User Model Email : " + objuser.Email + " , FirstName  : " + objuser.FirstName + " , LastName : " + objuser.LastName);
                   
                    var userId = _userRepository.Add(objuser);
                    if (userId > 0)
                    {
                        string birthDate = Convert.ToString(inputModel.birthdate);

                        objUserResponseViewModel = new UserRegisterResponseViewModel()
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

                        _loggerService.LogInformation(1, "## [UserService][CreateUser]- User created sucessfully : " + objUserResponseViewModel.email);
                    }
                    else
                    {
                        _loggerService.LogInformation(1, "## [UserService][CreateUser]- User not created in userRepository for email id : " + inputModel.email);
                        var errorMessage = _errorMessageService.GetErrorMessagesData("2020");                       
                        var objError = new Error { Code = "2020", Message = errorMessage };
                        lstError.Add(objError);
                    }
                }
                else
                {
                    _loggerService.LogInformation(1, "## [UserService]-[CreateUser]- User already exists ");
                    var errorMessage = _errorMessageService.GetErrorMessagesData("2020");
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
                    _loggerService.LogError(1, "## [UserService][CreateUser] innerexception :" + ex.InnerException.ToString());

                    if (ex.InnerException.Message != null)
                    {
                        _loggerService.LogError(1, "## [UserService][CreateUser] innerexception message :" + ex.InnerException.Message.ToString());
                    }
                }
                else
                {
                    _loggerService.LogError(1, "## [UserService][CreateUser] exception :" + ex.Message.ToString());
                }

                response.Status = false;
                response.Message = _errorMessageService.GetErrorMessagesData("501");
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                return response;
            }
            return response;
        }

        public ResponseViewModel<UserUpdateResponseViewModel> UpdateUser(UserUpdateRequestViewModel inputModel)
        {
            ResponseViewModel<UserUpdateResponseViewModel> response = new ResponseViewModel<UserUpdateResponseViewModel>();
            List<Error> lstError = new List<Error>();
            UserUpdateResponseViewModel objUserResponseViewModel = new UserUpdateResponseViewModel();
            try
            {
                _loggerService.LogInformation(1, "## [UserService][UpdateUser]- Start calling UpdateUser method for userId : " + inputModel.userid);

                byte[] passwordHash, passwordSalt;
                _securityHelper.CreatePasswordHash(inputModel.password, out passwordHash, out passwordSalt);

                Users objuser = new Users()
                {
                    UserId = inputModel.userid,
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

                _loggerService.LogInformation(1, "## [UserService][UpdateUser]- Update user Email : " + objuser.Email + " , FirstName  : " + objuser.FirstName + " , LastName : " + objuser.LastName);

                bool result = _userRepository.Update(objuser);
                if (result)
                {
                    string birthDate = Convert.ToString(inputModel.birthdate);

                    objUserResponseViewModel = new UserUpdateResponseViewModel()
                    {
                        userid = inputModel.userid,
                        address = inputModel.address,
                        birthdate = string.Format("{0:yyyy-MM-ddTHH:mm:ssZ}", birthDate),
                        contactnumber = inputModel.contactnumber,
                        email = inputModel.email,
                        firstname = inputModel.firstname,
                        lastname = inputModel.lastname,
                        status = inputModel.status,
                        usertype = inputModel.usertype
                    };

                    _loggerService.LogInformation(1, "## [UserService][UpdateUser]- User updated sucessfully : " + objUserResponseViewModel.email);
                }
                else
                {
                    _loggerService.LogInformation(1, "## [UserService][UpdateUser]- User not updated in userRepository for email id : " + inputModel.email);
                    var errorMessage = _errorMessageService.GetErrorMessagesData("120");
                    var objError = new Error { Code = "120", Message = errorMessage };
                    lstError.Add(objError);
                }


                if (lstError.Count == 0)
                {
                    response.Status = true;
                    response.Message = "User updated sucessfully";
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
                    _loggerService.LogError(1, "## [UserService][UpdateUser] innerexception :" + ex.InnerException.ToString());

                    if (ex.InnerException.Message != null)
                    {
                        _loggerService.LogError(1, "## [UserService][UpdateUser] innerexception message :" + ex.InnerException.Message.ToString());
                    }
                }
                else
                {
                    _loggerService.LogError(1, "## [UserService][UpdateUser] exception :" + ex.Message.ToString());
                }

                response.Status = false;
                response.Message = _errorMessageService.GetErrorMessagesData("501");
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                return response;
            }
            return response;
        }

        public ResponseViewModel<List<UserRegisterResponseViewModel>> GetAll()
        {
            ResponseViewModel<List<UserRegisterResponseViewModel>> response = new ResponseViewModel<List<UserRegisterResponseViewModel>>();
            List<UserRegisterResponseViewModel> _lstUserResponse = new List<UserRegisterResponseViewModel>();
            try
            {
                var _lstUser = _userRepository.GetAll().ToList();


                for(int i=0; i<_lstUser.Count; i++)
                {
                    string birthDate = string.Empty;
                    if (_lstUser[i].BirthDate != null)
                    {
                        birthDate = string.Format("{0:yyyy-MM-ddTHH:mm:ssZ}", _lstUser[i].BirthDate);
                    }

                    UserRegisterResponseViewModel _objUserResponse = new UserRegisterResponseViewModel()
                    {
                        userid = _lstUser[i].UserId,
                        address = _lstUser[i].Address,
                        birthdate = birthDate,
                        contactnumber = _lstUser[i].ContactNumber,
                       email = _lstUser[i].Email,
                       firstname = _lstUser[i].FirstName,
                       lastname = _lstUser[i].LastName,
                       status = _lstUser[i].Status,
                       usertype = _lstUser[i].UserType
                    };

                    _lstUserResponse.Add(_objUserResponse);
                }

                if(_lstUserResponse.Count > 0)
                {
                    response.Status = true;
                    response.Message = _errorMessageService.GetErrorMessagesData("116");
                    response.Result = _lstUserResponse;
                }
                else
                {
                    response.Status = false;
                    response.Message = _errorMessageService.GetErrorMessagesData("117");
                    response.Result = _lstUserResponse;
                }

            }
            catch(Exception ex)
            {
                if (ex.InnerException != null)
                {
                    _loggerService.LogError(1, "## [UserService][GetAll] innerexception :" + ex.InnerException.ToString());

                    if (ex.InnerException.Message != null)
                    {
                        _loggerService.LogError(1, "## [UserService][GetAll] innerexception message :" + ex.InnerException.Message.ToString());
                    }
                }
                else
                {
                    _loggerService.LogError(1, "## [UserService][GetAll] exception :" + ex.Message.ToString());
                }

                response.Status = false;
                response.Message = _errorMessageService.GetErrorMessagesData("501");
                response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            return response;
        }

        public ResponseViewModel<UserRegisterResponseViewModel> GetById(long userId)
        {

            ResponseViewModel<UserRegisterResponseViewModel> response = new ResponseViewModel<UserRegisterResponseViewModel>();
            List<Error> lstError = new List<Error>();
            UserRegisterResponseViewModel objUserResponseViewModel = new UserRegisterResponseViewModel();
            try
            {
                var finduser = _userRepository.Find(userId);
                if (finduser != null)
                {
                    string birthDate = string.Empty;
                    if (finduser.BirthDate != null)
                    {
                        birthDate = string.Format("{0:yyyy-MM-ddTHH:mm:ssZ}", finduser.BirthDate);
                    }
                  
                    objUserResponseViewModel = new UserRegisterResponseViewModel()
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
                    var errorMessage = _errorMessageService.GetErrorMessagesData("2020");
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
                    _loggerService.LogError(1, "## [UserService][GetById] innerexception :" + ex.InnerException.ToString());

                    if (ex.InnerException.Message != null)
                    {
                        _loggerService.LogError(1, "## [UserService][GetById] innerexception message :" + ex.InnerException.Message.ToString());
                    }
                }
                else
                {
                    _loggerService.LogError(1, "## [UserService][GetById] exception :" + ex.Message.ToString());
                }

                response.Status = false;
                response.Message = _errorMessageService.GetErrorMessagesData("501");
                response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            return response;
        }
    }
}
