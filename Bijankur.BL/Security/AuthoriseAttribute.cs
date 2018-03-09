using BJK.DAL;
using BJK.DAL.Models;
using BJK.DAL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BJK.BL.Security
{
    public class AuthoriseAttribute : ActionFilterAttribute
    {
        public string Permission { get; set; }

        public class ApiResponse
        {
            public int StatusCode { get; set; }
            public string Message { get; set; }
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string email = string.Empty;
            bool isValid = false;
            ApiResponse _objResponse = new ApiResponse();
            DataLayerContext dl = new DataLayerContext();
            ErrorMessageRepository errorMessageRepository = new ErrorMessageRepository(dl);
            UserRepository userRepository = new UserRepository(dl);
            try
            {
                if (!string.IsNullOrEmpty(Permission))
                {

                    var currentUser = filterContext.HttpContext.User;
                    if (currentUser.HasClaim(c => c.Type == ClaimTypes.Email))
                    {
                        email = currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
                    }
                    isValid = userRepository.AuthoriseUserWithPermission(email, Permission);
                }

                if (isValid == false)
                {
                    _objResponse.Message = errorMessageRepository.GetByCode("131");
                    _objResponse.StatusCode = (int)HttpStatusCode.Forbidden;
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    string json = JsonConvert.SerializeObject(_objResponse, Formatting.Indented);
                    filterContext.Result = new ContentResult { Content = json };
                }
            }
            catch (Exception ex)
            {
                _objResponse.Message = errorMessageRepository.GetByCode("501");
                _objResponse.StatusCode = (int)HttpStatusCode.Unauthorized;
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                string json = JsonConvert.SerializeObject(_objResponse, Formatting.Indented);
                filterContext.Result = new ContentResult { Content = json };
            }
        }

    }
}
