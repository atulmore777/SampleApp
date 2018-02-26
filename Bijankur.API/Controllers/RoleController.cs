using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bijankur.BL.ViewModels.RequestViewModel;
using Bijankur.BL.Services;
using Bijankur.BL.ViewModels.ResponseViewModel;
using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Bijankur.API.Controllers
{
    [Route("api/[controller]")]
    public class RoleController : Controller
    {
        private readonly IErrorMessageService _errorMessageService;
        private readonly IRoleService _roleService;
        private readonly ILogger _loggerService;
        public RoleController(IErrorMessageService errorMessageService,
                              ILoggerFactory loggerService,
                              IRoleService roleService)
        {
            _errorMessageService = errorMessageService;
            _roleService = roleService;
            _loggerService = loggerService.CreateLogger<RoleController>();
        }



        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            try
            {

                ResponseViewModel<List<RoleResponseViewModel>> response = _roleService.GetAll();

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
                    _loggerService.LogError(1, "## [UserController][GetAll] innerexception :" + ex.InnerException.ToString());

                    if (ex.InnerException.Message != null)
                    {
                        _loggerService.LogError(1, "## [UserController][GetAll] innerexception message :" + ex.InnerException.Message.ToString());
                    }
                }
                else
                {
                    _loggerService.LogError(1, "## [UserController][GetAll] exception :" + ex.Message.ToString());
                }

                var Message = _errorMessageService.GetErrorMessagesData("501");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiInternalServerErrorResponse((int)HttpStatusCode.InternalServerError, false, Message, ""));
            }
        }

        //[Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {

                ResponseViewModel<RoleResponseViewModel> response = _roleService.GetById(id);

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
                    _loggerService.LogError(1, "## [UserController][GetById] innerexception :" + ex.InnerException.ToString());

                    if (ex.InnerException.Message != null)
                    {
                        _loggerService.LogError(1, "## [UserController][GetById] innerexception message :" + ex.InnerException.Message.ToString());
                    }
                }
                else
                {
                    _loggerService.LogError(1, "## [UserController][GetById] exception :" + ex.Message.ToString());
                }

                var Message = _errorMessageService.GetErrorMessagesData("501");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiInternalServerErrorResponse((int)HttpStatusCode.InternalServerError, false, Message, ""));
            }
        }

        //[Authorize]
        [HttpPost]
        public IActionResult Post([FromBody]RoleCreateRequestViewModel inputModel)
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
                }

                ResponseViewModel<RoleCreateResponseViewModel> response = _roleService.CreateRole(inputModel);

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
                    _loggerService.LogError(1, "## [RoleController][Post] innerexception :" + ex.InnerException.ToString());

                    if (ex.InnerException.Message != null)
                    {
                        _loggerService.LogError(1, "## [RoleController][Post] innerexception message :" + ex.InnerException.Message.ToString());
                    }
                }
                else
                {
                    _loggerService.LogError(1, "## [RoleController][Post] exception :" + ex.Message.ToString());
                }

                var Message = _errorMessageService.GetErrorMessagesData("501");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiInternalServerErrorResponse((int)HttpStatusCode.InternalServerError, false, Message, ""));
            }
        }

        /*
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
