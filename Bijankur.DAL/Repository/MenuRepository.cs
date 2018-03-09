using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BJK.DAL.Models;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;
using BJK.DAL.UserDefineModel;
using BJK.DAL.SqlHelper;

namespace BJK.DAL.Repository
{
    public interface IMenuRepository
    {
        List<MenuWithPermission> GetAllMenuWithPermission(int roleId);
    }
    public class MenuRepository : IMenuRepository
    {
        public DataLayerContext _context { get; set; }
        public MenuRepository(DataLayerContext context)
        {
            _context = context;
        }

        public List<MenuWithPermission> GetAllMenuWithPermission(int roleId)
        {
            List<MenuWithPermission> lstMenuWithPerm = new List<MenuWithPermission>();
            try
            {
                System.Data.Common.DbDataReader sqlReader;
                using (var cmd = _context.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandText = "p_P_GetAllMenuWithPermission";
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (cmd.Connection.State != ConnectionState.Open)
                        cmd.Connection.Open();

                    cmd.Parameters.Add(new SqlParameter("@RoleId", SqlDbType.Int)
                    {
                        Value = roleId
                    });                 

                    sqlReader = (System.Data.Common.DbDataReader)cmd.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        lstMenuWithPerm.Add(new MenuWithPermission()
                        {
                           MenuId = SqlDb.CheckIntDBNull(sqlReader["MenuId"]),
                           Icon = SqlDb.CheckStringDBNull(sqlReader["Icon"]),
                           MenuCode = SqlDb.CheckStringDBNull(sqlReader["MenuCode"]),
                           MenuName = SqlDb.CheckStringDBNull(sqlReader["MenuName"]),
                           ParentMenuId = SqlDb.CheckIntDBNull(sqlReader["ParentMenuId"]),
                           Result = SqlDb.CheckStringDBNull(sqlReader["Result"]),
                           RoleId  = SqlDb.CheckIntDBNull(sqlReader["RoleId"]),
                           Sequence = SqlDb.CheckIntDBNull(sqlReader["Sequence"]),

                           UpdatedBy = SqlDb.CheckStringDBNull(sqlReader["UpdatedBy"]),
                           CreatedBy = SqlDb.CheckStringDBNull(sqlReader["CreatedBy"]),
                           CreatedOn = SqlDb.CheckDateTimeDBNull(sqlReader["CreatedOn"]),
                           UpdatedOn = SqlDb.CheckDateTimeDBNull(sqlReader["UpdatedOn"])
                        });
                    }

                }
            }
            catch (Exception ex)
            {

                return null;
            }

            return lstMenuWithPerm;
        }
    }
}
