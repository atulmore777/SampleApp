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

namespace Bijankur.API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IErrorMessageService errorMessageService;
        private readonly IUserService userService;
        private readonly ILogger loggerService;
        public UserController(IErrorMessageService errorMessageService,
                              ILoggerFactory loggerService,
                              IUserService userService)
        {
            this.errorMessageService = errorMessageService;
            this.userService = userService;
            this.loggerService = loggerService.CreateLogger<UserController>();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {

                ResponseViewModel<List<UserRegisterResponseViewModel>> response = userService.GetAll();

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
                    loggerService.LogError(1, "## [UserController][GetAll] innerexception :" + ex.InnerException.ToString());

                    if (ex.InnerException.Message != null)
                    {
                        loggerService.LogError(1, "## [UserController][GetAll] innerexception message :" + ex.InnerException.Message.ToString());
                    }
                }
                else
                {
                    loggerService.LogError(1, "## [UserController][GetAll] exception :" + ex.Message.ToString());
                }

                var Message = errorMessageService.GetErrorMessagesData("501");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiInternalServerErrorResponse((int)HttpStatusCode.InternalServerError, false, Message, ""));
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            try
            {

                ResponseViewModel<UserRegisterResponseViewModel> response = userService.GetById(id);

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
                    loggerService.LogError(1, "## [UserController][GetById] innerexception :" + ex.InnerException.ToString());

                    if (ex.InnerException.Message != null)
                    {
                        loggerService.LogError(1, "## [UserController][GetById] innerexception message :" + ex.InnerException.Message.ToString());
                    }
                }
                else
                {
                    loggerService.LogError(1, "## [UserController][GetById] exception :" + ex.Message.ToString());
                }

                var Message = errorMessageService.GetErrorMessagesData("501");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiInternalServerErrorResponse((int)HttpStatusCode.InternalServerError, false, Message, ""));
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody]UserRegisterRequestViewModel inputModel)
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


                ResponseViewModel<UserRegisterResponseViewModel> response = userService.CreateUser(inputModel);

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
                    loggerService.LogError(1, "## [UserController][POST] innerexception :" + ex.InnerException.ToString());

                    if (ex.InnerException.Message != null)
                    {
                        loggerService.LogError(1, "## [UserController][POST] innerexception message :" + ex.InnerException.Message.ToString());
                    }
                }
                else
                {
                    loggerService.LogError(1, "## [UserController][POST] exception :" + ex.Message.ToString());
                }

                var Message = errorMessageService.GetErrorMessagesData("501");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiInternalServerErrorResponse((int)HttpStatusCode.InternalServerError, false, Message, ""));
            }
        }


        [Authorize]
        [HttpPut]
        public IActionResult Put([FromBody]UserUpdateRequestViewModel inputModel)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiBadRequestResponse(ModelState));
                }

                ResponseViewModel<UserUpdateResponseViewModel> response = userService.UpdateUser(inputModel);

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
                    loggerService.LogError(1, "## [UserController][Put] innerexception :" + ex.InnerException.ToString());

                    if (ex.InnerException.Message != null)
                    {
                        loggerService.LogError(1, "## [UserController][Put] innerexception message :" + ex.InnerException.Message.ToString());
                    }
                }
                else
                {
                    loggerService.LogError(1, "## [UserController][Put] exception :" + ex.Message.ToString());
                }

                var Message = errorMessageService.GetErrorMessagesData("501");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiInternalServerErrorResponse((int)HttpStatusCode.InternalServerError, false, Message, ""));
            }
        }



        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody]UserLoginRequestViewModel inputModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiBadRequestResponse(ModelState));
                }

                ResponseViewModel<UserLoginResponseViewModel> response = userService.Authenticate(inputModel);

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
                    loggerService.LogError(1, "## [UserController][Authenticate] innerexception :" + ex.InnerException.ToString());

                    if (ex.InnerException.Message != null)
                    {
                        loggerService.LogError(1, "## [UserController][Authenticate] innerexception message :" + ex.InnerException.Message.ToString());
                    }
                }
                else
                {
                    loggerService.LogError(1, "## [UserController][Authenticate] exception :" + ex.Message.ToString());
                }

                var Message = errorMessageService.GetErrorMessagesData("501");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiInternalServerErrorResponse((int)HttpStatusCode.InternalServerError, false, Message, ""));
            }

        }

    
    }
}
