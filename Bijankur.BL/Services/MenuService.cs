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
    public interface IMenuService
    {
        ResponseViewModel<List<MenuResponseViewModel>> GetAll(int roleid);
    }
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IErrorMessageService _errorMessageService;
        private readonly ISecurityHelper _securityHelper;
        private readonly ILogger _loggerService;
        public MenuService(IMenuRepository menuRepository,
                           IErrorMessageService errorMessageService,
                           ISecurityHelper securityHelper,
                           ILoggerFactory loggerService)
        {
            _menuRepository = menuRepository;
            _errorMessageService = errorMessageService;
            _securityHelper = securityHelper;
            _loggerService = loggerService.CreateLogger<MenuService>();
        }

        public ResponseViewModel<List<MenuResponseViewModel>> GetAll(int roleid)
        {
            ResponseViewModel<List<MenuResponseViewModel>> response = new ResponseViewModel<List<MenuResponseViewModel>>();
            List<MenuResponseViewModel> _lstMenuResponse = new List<MenuResponseViewModel>();
            try
            {
                var _lstMenu = _menuRepository.GetAllMenuWithPermission(roleid).ToList();


                for (int i = 0; i < _lstMenu.Count; i++)
                {

                    MenuResponseViewModel _objMenuResponse = new MenuResponseViewModel()
                    {
                        menuid = _lstMenu[i].MenuId,
                        icon = _lstMenu[i].Icon,
                        menucode = _lstMenu[i].MenuCode,
                        menuname = _lstMenu[i].MenuName,
                        parentmenuid = _lstMenu[i].ParentMenuId,
                        sequence = _lstMenu[i].Sequence,
                        result = _lstMenu[i].Result,
                        roleId = _lstMenu[i].RoleId
                    };

                    _lstMenuResponse.Add(_objMenuResponse);
                }

                if (_lstMenuResponse.Count > 0)
                {
                    response.Status = true;
                    response.Message = _errorMessageService.GetErrorMessagesData("133");
                    response.Result = _lstMenuResponse;
                }
                else
                {
                    response.Status = false;
                    response.Message = _errorMessageService.GetErrorMessagesData("132");
                    response.Result = _lstMenuResponse;
                }

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    _loggerService.LogError(1, "## [MenuService][GetAll] innerexception :" + ex.InnerException.ToString());

                    if (ex.InnerException.Message != null)
                    {
                        _loggerService.LogError(1, "## [MenuService][GetAll] innerexception message :" + ex.InnerException.Message.ToString());
                    }
                }
                else
                {
                    _loggerService.LogError(1, "## [MenuService][GetAll] exception :" + ex.Message.ToString());
                }

                response.Status = false;
                response.Message = _errorMessageService.GetErrorMessagesData("501");
                response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            return response;
        }


    }
}
