using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJK.BL.ViewModels.RequestViewModel
{
    [DelimitedRecord("|")]
    public class FileUploadUserRequestViewModel
    {
        public string firstname { get; set; }   
        public string lastname { get; set; }   
        public string email { get; set; }
    }
}
