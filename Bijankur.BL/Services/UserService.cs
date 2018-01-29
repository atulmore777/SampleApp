using Bijankur.BL.ViewModels.RequestViewModel;
using Bijankur.BL.ViewModels.ResponseViewModel;
using Bijankur.DAL.Models;
using Bijankur.DAL.Repository;
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
        private IUserRepository userRepository;
        private readonly IErrorMessageService errorMessageService;
        public UserService(IUserRepository userRepository, IErrorMessageService errorMessageService)
        {
            this.userRepository = userRepository;
            this.errorMessageService = errorMessageService;
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
                    objUserResponseViewModel = new UserResponseViewModel()
                    {
                        userid = userId,
                        address = finduser.Address,
                        birthdate = Convert.ToDateTime(finduser.BirthDate),
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

                var finduser = userRepository.FindByEmail(inputModel.email);
                if (finduser != null)
                {
                    Users objuser = new Users()
                    {
                        Address = inputModel.address,
                        BirthDate = inputModel.birthdate,
                        ContactNumber = inputModel.contactnumber,
                        Email = inputModel.email,
                        FirstName = inputModel.firstname,
                        LastName = inputModel.lastname,
                        Status = inputModel.status,
                        UserType = inputModel.usertype,
                        CreatedBy = "admin",
                        UpdatedBy = "admin",
                        CreatedOn = DateTime.UtcNow,
                        UpdatedOn = DateTime.UtcNow
                    };

                    var userId = userRepository.Add(objuser);
                    if (userId > 0)
                    {
                        objUserResponseViewModel = new UserResponseViewModel()
                        {
                            userid = userId,
                            address = inputModel.address,
                            birthdate = inputModel.birthdate,
                            contactnumber = inputModel.contactnumber,
                            email = inputModel.email,
                            firstname = inputModel.firstname,
                            lastname = inputModel.lastname,
                            status = inputModel.status,
                            usertype = inputModel.usertype
                        };
                    }
                    else
                    {
                        var errorMessage = errorMessageService.GetErrorMessagesData("2020");                       
                        var objError = new Error { Code = "2020", Message = errorMessage };
                        lstError.Add(objError);
                    }
                }
                else
                {
                    //loggerService.LogInformation(1, "[User]-[CreateUser]- User Already exists ");
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
                //loggerService.LogError(1, " [User]-[CreateUser] User Exception:" + ex.StackTrace);
                response.Status = false;
                response.Message = errorMessageService.GetErrorMessagesData("2007");
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                return response;
            }
            return response;
        }


    }
}
