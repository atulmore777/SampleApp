using BJK.BL.Common;
using BJK.BL.ViewModels.RequestViewModel;
using BJK.BL.ViewModels.ResponseViewModel;
using BJK.DAL.Models;
using BJK.DAL.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using BJK.BL.Security;

namespace BJK.BL.Services
{
    public interface IPermissionService
    {
        ResponseViewModel<List<PermissionResponseViewModel>> GetAllPermissionByRole(int roleid);
        ResponseViewModel<AssignPermissionResponseViewModel> AssignPermissionToRole(AssignPermissionRequestViewModel inputModel);
    }
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IErrorMessageService _errorMessageService;
        private readonly ISecurityHelper _securityHelper;
        private readonly ILogger _loggerService;
        public PermissionService(IPermissionRepository permissionRepository,
                                   IErrorMessageService errorMessageService,
                                   ISecurityHelper securityHelper,
                                   ILoggerFactory loggerService)
        {
            _permissionRepository = permissionRepository;
            _errorMessageService = errorMessageService;
            _securityHelper = securityHelper;
            _loggerService = loggerService.CreateLogger<PermissionService>();
        }

        public ResponseViewModel<List<PermissionResponseViewModel>> GetAllPermissionByRole(int roleid)
        {
            ResponseViewModel<List<PermissionResponseViewModel>> response = new ResponseViewModel<List<PermissionResponseViewModel>>();
            List<PermissionResponseViewModel> _lstPermissionResponse = new List<PermissionResponseViewModel>();
            try
            {
                var _lstPermission = _permissionRepository.GetAllPermissionByRole(roleid).ToList();


                for (int i = 0; i < _lstPermission.Count; i++)
                {

                    PermissionResponseViewModel _objPermissionResponse = new PermissionResponseViewModel()
                    {
                        Module = _lstPermission[i].Module,
                        PermissionCode = _lstPermission[i].PermissionCode,
                        PermissionName = _lstPermission[i].PermissionName,
                        Result = _lstPermission[i].Result,
                        RoleId = _lstPermission[i].RoleId                 
                    };

                    _lstPermissionResponse.Add(_objPermissionResponse);
                }

                if (_lstPermissionResponse.Count > 0)
                {
                    response.Status = true;
                    response.Message = _errorMessageService.GetErrorMessagesData("133");
                    response.Result = _lstPermissionResponse;
                }
                else
                {
                    response.Status = false;
                    response.Message = _errorMessageService.GetErrorMessagesData("132");
                    response.Result = _lstPermissionResponse;
                }

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    _loggerService.LogError(1, "## [MenuService][GetAllPermissionByRole] innerexception :" + ex.InnerException.ToString());

                    if (ex.InnerException.Message != null)
                    {
                        _loggerService.LogError(1, "## [MenuService][GetAllPermissionByRole] innerexception message :" + ex.InnerException.Message.ToString());
                    }
                }
                else
                {
                    _loggerService.LogError(1, "## [MenuService][GetAllPermissionByRole] exception :" + ex.Message.ToString());
                }

                response.Status = false;
                response.Message = _errorMessageService.GetErrorMessagesData("501");
                response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            return response;
        }

        public ResponseViewModel<AssignPermissionResponseViewModel> AssignPermissionToRole(AssignPermissionRequestViewModel inputModel)
        {
            ResponseViewModel<AssignPermissionResponseViewModel> response = new ResponseViewModel<AssignPermissionResponseViewModel>();
            try
            {
                List<RolePermission> lstRolePermission = new List<RolePermission>();

                for(int i=0; i< inputModel.lstPermission.Count; i++)
                {
                    RolePermission objRolePermission = new RolePermission
                    {
                        PermissionId = inputModel.lstPermission[i].permissionid,
                        RoleId = inputModel.lstPermission[i].roleid,
                        CreatedBy = inputModel.createdby,
                        UpdatedBy = inputModel.updatedby,
                        CreatedOn = DateTime.UtcNow,
                        UpdatedOn = DateTime.UtcNow,
                        IsDeleted = false
                    };
                    lstRolePermission.Add(objRolePermission);
                }

                bool result = _permissionRepository.AssignPermissionToRole(inputModel.roleid, lstRolePermission);
                if(result)
                {
                    response.Status = true;
                    response.Message = _errorMessageService.GetErrorMessagesData("135");
                    response.Result = null;
                }
                else
                {
                    response.Status = false;
                    response.Message = _errorMessageService.GetErrorMessagesData("136");
                    response.Result = null;
                }

            }
            catch(Exception ex)
            {
                if (ex.InnerException != null)
                {
                    _loggerService.LogError(1, "## [PermissionService][AssignPermissionToRole] innerexception :" + ex.InnerException.ToString());

                    if (ex.InnerException.Message != null)
                    {
                        _loggerService.LogError(1, "## [PermissionService][AssignPermissionToRole] innerexception message :" + ex.InnerException.Message.ToString());
                    }
                }
                else
                {
                    _loggerService.LogError(1, "## [PermissionService][AssignPermissionToRole] exception :" + ex.Message.ToString());
                }

                response.Status = false;
                response.Message = _errorMessageService.GetErrorMessagesData("501");
                response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            return response;
        }

    }
}
