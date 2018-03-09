using BJK.DAL.Models;
using BJK.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJK.BL.Services
{
    public interface IErrorMessageService
    {
        string GetErrorMessagesData(string ErrorCode);
        List<Error> GetModelErrorList(Dictionary<string, string> error);
    }
    public class ErrorMessageService : IErrorMessageService
    {
        private IErrorMessageRepository errorMessageRepository;
        public ErrorMessageService(IErrorMessageRepository errorManagementRepository)
        {
            this.errorMessageRepository = errorManagementRepository;
        }

        public string GetErrorMessagesData(string Errorcode)
        {
            return errorMessageRepository.GetByCode(Errorcode);
        }

        public List<Error> GetModelErrorList(Dictionary<string, string> error)
        {
            List<Error> mylist = new List<Error>();
            if (error != null)
            {
                foreach (KeyValuePair<string, string> entry in error)
                {
                    Error err = new Error();
                    err.Code = entry.Key.ToString();
                    err.Message = entry.Value.ToString();
                    mylist.Add(err);
                }
            }
            return mylist.ToList();

        }
    }
}
