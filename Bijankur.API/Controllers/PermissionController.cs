using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BJK.BL.ViewModels.RequestViewModel;
using BJK.BL.Services;
using BJK.BL.ViewModels.ResponseViewModel;
using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BJK.Controllers
{
    [Route("api/[controller]")]
    public class PermissionController : Controller
    {
        private readonly IMenuService _menuService;
        private readonly IPermissionService _permissionService;
        private readonly IErrorMessageService _errorMessageService;
        private readonly ILogger _loggerService;
        public PermissionController(IErrorMessageService errorMessageService,
                                    ILoggerFactory loggerService,
                                    IPermissionService permissionService,
                                    IMenuService menuService)
        {
            _errorMessageService = errorMessageService;
            _permissionService = permissionService;
            _menuService = menuService;
            _loggerService = loggerService.CreateLogger<PermissionController>();
        }


        [HttpGet("GetAllPermission/{roleid}")]
        public IActionResult Get(int roleid)
        {
            try
            {

                ResponseViewModel<List<PermissionResponseViewModel>> response = _permissionService.GetAllPermissionByRole(roleid);

                if (response.Status)
                {
                    return Ok(new ApiOkResponse((int)HttpStatusCode.OK, true, response.Message, response.Result));
                }
                else
                {
                    return Ok(new ApiBadResponse(response.StatusCode, response.Status, response.Message, "", response.Errors));
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    _loggerService.LogError(1, "## [PermissionController][GetPermission] innerexception :" + ex.InnerException.ToString());

                    if (ex.InnerException.Message != null)
                    {
                        _loggerService.LogError(1, "## [PermissionController][GetPermission] innerexception message :" + ex.InnerException.Message.ToString());
                    }
                }
                else
                {
                    _loggerService.LogError(1, "## [PermissionController][GetPermission] exception :" + ex.Message.ToString());
                }

                var Message = _errorMessageService.GetErrorMessagesData("501");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiInternalServerErrorResponse((int)HttpStatusCode.InternalServerError, false, Message, ""));
            }
        }


        [HttpGet("GetAllMenu/{roleid}")]
        public IActionResult GetAllMenu(int roleid)
        {
            try
            {

                ResponseViewModel<List<MenuResponseViewModel>> response = _menuService.GetAll(roleid);

                if (response.Status)
                {
                    return Ok(new ApiOkResponse((int)HttpStatusCode.OK, true, response.Message, response.Result));
                }
                else
                {
                    return Ok(new ApiBadResponse(response.StatusCode, response.Status, response.Message, "", response.Errors));
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    _loggerService.LogError(1, "## [PermissionController][GetAll] innerexception :" + ex.InnerException.ToString());

                    if (ex.InnerException.Message != null)
                    {
                        _loggerService.LogError(1, "## [PermissionController][GetAll] innerexception message :" + ex.InnerException.Message.ToString());
                    }
                }
                else
                {
                    _loggerService.LogError(1, "## [PermissionController][GetAll] exception :" + ex.Message.ToString());
                }

                var Message = _errorMessageService.GetErrorMessagesData("501");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiInternalServerErrorResponse((int)HttpStatusCode.InternalServerError, false, Message, ""));
            }
        }

        [HttpPost("AssignPermissionToRole")]
        public IActionResult Post([FromBody]AssignPermissionRequestViewModel inputModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiBadRequestResponse(ModelState));
                }

                var currentUser = HttpContext.User;
                if (currentUser.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    string email = currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
                    inputModel.createdby = email;
                    inputModel.updatedby = email;
                }

                ResponseViewModel<AssignPermissionResponseViewModel> response = _permissionService.AssignPermissionToRole(inputModel);

                if (response.Status)
                {
                    return Ok(new ApiOkResponse((int)HttpStatusCode.OK, true, response.Message, response.Result));
                }
                else
                {
                    return Ok(new ApiBadResponse(response.StatusCode, response.Status, response.Message, "", response.Errors));
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    _loggerService.LogError(1, "## [PermissionController][AssignPermissionToRole] innerexception :" + ex.InnerException.ToString());

                    if (ex.InnerException.Message != null)
                    {
                        _loggerService.LogError(1, "## [PermissionController][AssignPermissionToRole] innerexception message :" + ex.InnerException.Message.ToString());
                    }
                }
                else
                {
                    _loggerService.LogError(1, "## [PermissionController][AssignPermissionToRole] exception :" + ex.Message.ToString());
                }

                var Message = _errorMessageService.GetErrorMessagesData("501");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiInternalServerErrorResponse((int)HttpStatusCode.InternalServerError, false, Message, ""));
            }
        }
    }
}
