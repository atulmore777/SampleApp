﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJK.DAL.Repository
{
    public interface IErrorMessageRepository
    {
        string GetByCode(string ErrorCode);
    }
    public class ErrorMessageRepository : IErrorMessageRepository
    {
        public ILogger _loggerRepository { get; set; }
        public DataLayerContext _context { get; set; }
    
        public ErrorMessageRepository(DataLayerContext context)
        {
            _context = context;           
        }
        public string GetByCode(string Errorcode)
        {
            string errorMessage = string.Empty;
            try
            {
                var objErrorMessage = _context.ErrorMessage.Where(t => t.ErrorCode.ToString().ToLower() == Errorcode.ToString().ToLower()).FirstOrDefault();
                if (objErrorMessage != null)
                {
                    errorMessage = objErrorMessage.Message;
                }
            }
            catch(Exception ex)
            {

            }
            return errorMessage;
        }
    }
}
