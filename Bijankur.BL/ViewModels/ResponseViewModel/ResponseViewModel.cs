using Bijankur.DAL;
using Bijankur.DAL.Models;
using Bijankur.DAL.Repository;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bijankur.BL.ViewModels.ResponseViewModel
{
    public class ResponseViewModel<T>
    {
        public int StatusCode { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }

        public List<Error> Errors { get; set; }
        public ResponseViewModel(int statuscode = 200, string message = null)
        {
            StatusCode = statuscode;
            Status = false;
            Message = message ?? GetDefaultMessageForStatusCode(statuscode); ;
            Result = default(T);
            Errors = new List<Error>();
        }


        private static string GetDefaultMessageForStatusCode(int statusCode)
        {
            switch (statusCode)
            {
                case 400:
                    return "Bad Request";
                case 404:
                    return "Resource not found";
                case 500:
                    return "An unhandled error occurred";
                default:
                    return null;
            }
        }

    }


    public class ApiBadRequestResponse : ResponseViewModel<ApiResult>
    {
        public ApiBadRequestResponse(ModelStateDictionary modelState) : base(400)
        {
            Errors = new List<Error>();
            DataLayerContext dlContext = new DataLayerContext();
            ErrorMessageRepository errorMessageRepository = new ErrorMessageRepository(dlContext);

            IEnumerable<string> LstError = modelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToArray();

            foreach (var errorMessage in LstError)
            {
                Error _objError = new Error();

                if (errorMessage.Contains("$$"))
                {
                    string[] strError = errorMessage.Split(new char[] { '$', '$' }, StringSplitOptions.RemoveEmptyEntries);
                    _objError.Code = strError[0];
                    string requiredMessage = errorMessageRepository.GetByCode(strError[0]);
                    if (requiredMessage.Contains("$$InputData$$")) { requiredMessage = requiredMessage.Replace("$$InputData$$", strError[1]); }
                    _objError.Message = requiredMessage;
                    Errors.Add(_objError);
                }
                else
                {
                    string dbMessage = errorMessageRepository.GetByCode(errorMessage);
                    _objError.Code = errorMessage;
                    _objError.Message = dbMessage;
                    Errors.Add(_objError);
                }
            }
        }
    }

    public class ApiResult
    {
        public object Result { get; }
        public ApiResult(object result)
        {
            Result = result;
        }
    }

    public class ApiOkResponse : ResponseViewModel<ApiResult>
    {
        public object Result { get; }

        public ApiOkResponse(int StatusCode, bool Status, string Message, object result)
        {
            base.StatusCode = StatusCode;
            base.Status = Status;
            base.Message = Message;
            Result = result;
        }
    }

    public class ApiBadResponse : ResponseViewModel<ApiResult>
    {
        public object Result { get; }
        public ApiBadResponse(int StatusCode, bool Status, string Message, object result, List<Error> errorList) : base(400)
        {
            base.StatusCode = StatusCode;
            base.Status = Status;
            base.Message = Message;
            Result = result;
            this.Errors = errorList;
        }
    }

    public class ApiInternalServerErrorResponse : ResponseViewModel<ApiResult>
    {
        public object Result { get; }
        public ApiInternalServerErrorResponse(int StatusCode, bool Status, string Message, object result) : base(500)
        {
            base.StatusCode = StatusCode;
            base.Status = Status;
            base.Message = Message;
            Result = result;
        }
    }
}
