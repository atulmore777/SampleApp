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
    public interface IRoleService
    {
        ResponseViewModel<RoleCreateResponseViewModel> CreateRole(RoleCreateRequestViewModel inputModel);

        ResponseViewModel<List<RoleResponseViewModel>> GetAll();

        ResponseViewModel<RoleResponseViewModel> GetById(int userId);
    }
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IErrorMessageService _errorMessageService;
        private readonly ISecurityHelper _securityHelper;
        private readonly ILogger _loggerService;
        public RoleService(IRoleRepository roleRepository,
                           IErrorMessageService errorMessageService,
                           ISecurityHelper securityHelper,
                           ILoggerFactory loggerService)
        {
            _roleRepository = roleRepository;
            _errorMessageService = errorMessageService;
            _securityHelper = securityHelper;
            _loggerService = loggerService.CreateLogger<UserService>();
        }

        public ResponseViewModel<RoleCreateResponseViewModel> CreateRole(RoleCreateRequestViewModel inputModel)
        {
            ResponseViewModel<RoleCreateResponseViewModel> response = new ResponseViewModel<RoleCreateResponseViewModel>();
            List<Error> lstError = new List<Error>();
            RoleCreateResponseViewModel objRoleCreateResponseViewModel = new RoleCreateResponseViewModel();
            try
            {
                _loggerService.LogInformation(1, "## [RoleService][CreateRole]- Start calling CreateRole method.");

                var findRole = _roleRepository.FindByName(inputModel.rolename);
                if (findRole == null)
                {
                    Roles objRoles = new Roles()
                    {
                        RoleName = inputModel.rolename,
                        RoleDescription = inputModel.roledescription,
                        IsDeleted = false,
                        CreatedBy = inputModel.createdby,
                        UpdatedBy = inputModel.createdby,
                        CreatedOn = DateTime.UtcNow,
                        UpdatedOn = DateTime.UtcNow
                    };

                    _loggerService.LogInformation(1, "## [UserService][CreateRole]- CreateRole Model is RoleName : " + objRoles.RoleName);

                    var roleId = _roleRepository.Add(objRoles);
                    if (roleId > 0)
                    {
                        objRoleCreateResponseViewModel.roleid = roleId;
                        objRoleCreateResponseViewModel.rolename = objRoles.RoleName;
                        objRoleCreateResponseViewModel.roledescription = objRoles.RoleDescription;
                        _loggerService.LogInformation(1, "## [UserService][CreateRole]- Role created sucessfully : " + objRoleCreateResponseViewModel.rolename);
                    }
                    else
                    {
                        _loggerService.LogInformation(1, "## [UserService][CreateRole]- Role not created : " + objRoleCreateResponseViewModel.rolename);
                        var errorMessage = _errorMessageService.GetErrorMessagesData("122");
                        var objError = new Error { Code = "122", Message = errorMessage };
                        lstError.Add(objError);
                    }
                }
                else
                {
                    _loggerService.LogInformation(1, "## [UserService]-[CreateRole]- Role already exists ");
                    var errorMessage = _errorMessageService.GetErrorMessagesData("121");
                    var objError = new Error { Code = "121", Message = errorMessage };
                    lstError.Add(objError);
                }

                if (lstError.Count == 0)
                {
                    response.Status = true;
                    response.Message = "Role created sucessfully";
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Result = objRoleCreateResponseViewModel;
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
                    _loggerService.LogError(1, "## [RoleService][CreateRole] innerexception :" + ex.InnerException.ToString());

                    if (ex.InnerException.Message != null)
                    {
                        _loggerService.LogError(1, "## [RoleService][CreateRole] innerexception message :" + ex.InnerException.Message.ToString());
                    }
                }
                else
                {
                    _loggerService.LogError(1, "## [RoleService][CreateRole] exception :" + ex.Message.ToString());
                }

                response.Status = false;
                response.Message = _errorMessageService.GetErrorMessagesData("501");
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                return response;
            }
            return response;
        }

        public ResponseViewModel<List<RoleResponseViewModel>> GetAll()
        {
            ResponseViewModel<List<RoleResponseViewModel>> response = new ResponseViewModel<List<RoleResponseViewModel>>();
            List<RoleResponseViewModel> _lstRoleResponse = new List<RoleResponseViewModel>();
            try
            {
                var _lstRole = _roleRepository.GetAll().ToList();


                for (int i = 0; i < _lstRole.Count; i++)
                {

                    RoleResponseViewModel _objRoleResponse = new RoleResponseViewModel()
                    {
                        roleid = _lstRole[i].RoleId,
                        roledescription = _lstRole[i].RoleDescription,
                        rolename = _lstRole[i].RoleName,
                        IsDeleted = _lstRole[i].IsDeleted
                    };

                    _lstRoleResponse.Add(_objRoleResponse);
                }

                if (_lstRoleResponse.Count > 0)
                {
                    response.Status = true;
                    response.Message = _errorMessageService.GetErrorMessagesData("123");
                    response.Result = _lstRoleResponse;
                }
                else
                {
                    response.Status = false;
                    response.Message = _errorMessageService.GetErrorMessagesData("124");
                    response.Result = _lstRoleResponse;
                }

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    _loggerService.LogError(1, "## [RoleService][GetAll] innerexception :" + ex.InnerException.ToString());

                    if (ex.InnerException.Message != null)
                    {
                        _loggerService.LogError(1, "## [RoleService][GetAll] innerexception message :" + ex.InnerException.Message.ToString());
                    }
                }
                else
                {
                    _loggerService.LogError(1, "## [RoleService][GetAll] exception :" + ex.Message.ToString());
                }

                response.Status = false;
                response.Message = _errorMessageService.GetErrorMessagesData("501");
                response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            return response;
        }

        public ResponseViewModel<RoleResponseViewModel> GetById(int roleId)
        {

            ResponseViewModel<RoleResponseViewModel> response = new ResponseViewModel<RoleResponseViewModel>();
            List<Error> lstError = new List<Error>();
            RoleResponseViewModel objRoleResponseViewModel = new RoleResponseViewModel();
            try
            {
                var findRole = _roleRepository.Find(roleId);
                if (findRole != null)
                {
                     objRoleResponseViewModel.roleid = findRole.RoleId;
                    objRoleResponseViewModel.rolename = findRole.RoleName;
                    objRoleResponseViewModel.roledescription = findRole.RoleDescription;
                    objRoleResponseViewModel.IsDeleted = findRole.IsDeleted;
                    
                }
                else
                {
                    var errorMessage = _errorMessageService.GetErrorMessagesData("125");                 
                    var objError = new Error { Code = "125", Message = errorMessage };
                    lstError.Add(objError);
                }

                if (lstError.Count == 0)
                {
                    response.Status = true;
                    response.Message = "Role get sucessfully";
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Result = objRoleResponseViewModel;
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
