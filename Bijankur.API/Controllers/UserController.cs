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


        [HttpPost]
        public IActionResult Post([FromBody]UserRequestViewModel inputModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                      return BadRequest(new ApiBadRequestResponse(ModelState));
                }

                ResponseViewModel<UserResponseViewModel> response = userService.CreateUser(inputModel);

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
    }
}
