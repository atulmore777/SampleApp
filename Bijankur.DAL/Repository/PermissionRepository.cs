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
    public interface IPermissionRepository
    {
        Permission Find(int permissionid);
        List<PermissionWithRoles> GetAllPermissionByRole(int roleId);
        bool AssignPermissionToRole(int roleId, List<RolePermission> lstPermission);
    }
    public class PermissionRepository : IPermissionRepository
    {
        public DataLayerContext _context { get; set; }
        public PermissionRepository(DataLayerContext context)
        {
            _context = context;
        }

        public Permission Find(int permissionid)
        {
            var permissionData = _context.Permissions.SingleOrDefault(a => a.PermissionId == permissionid);
            return permissionData;
        }
        public List<PermissionWithRoles> GetAllPermissionByRole(int roleId)
        {
            List<PermissionWithRoles> lstPermissionByRole = new List<PermissionWithRoles>();
            try
            {
                System.Data.Common.DbDataReader sqlReader;
                using (var cmd = _context.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandText = "p_P_GetAllPermissionByRoleId";
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
                        lstPermissionByRole.Add(new PermissionWithRoles()
                        {
                            RoleId = SqlDb.CheckIntDBNull(sqlReader["RoleId"]),
                            PermissionCode = SqlDb.CheckStringDBNull(sqlReader["PermissionCode"]),
                            PermissionName = SqlDb.CheckStringDBNull(sqlReader["PermissionName"]),
                            Module = SqlDb.CheckStringDBNull(sqlReader["Module"]),               
                            Result = SqlDb.CheckStringDBNull(sqlReader["Result"])          
                        });
                    }

                }
            }
            catch (Exception ex)
            {

                return null;
            }

            return lstPermissionByRole;
        }

        public bool AssignPermissionToRole(int roleId , List<RolePermission> lstPermission)
        {
            bool result = false;
            try
            {
                using (var context = new DataLayerContext())
                {
                    var itemToRemove = context.RolePermissions.Where(x => x.RoleId == roleId).ToList();
                    foreach (RolePermission item in itemToRemove)
                    {
                        context.RolePermissions.Remove(item);
                        context.SaveChanges();
                    }

                    for (int per = 0; per < lstPermission.Count; per++)
                    {
                        context.RolePermissions.Add(lstPermission[per]);
                        context.SaveChanges();
                    }
                }           
            }
            catch(Exception ex)
            {
                result = false;
            }
            return result;
        }
    }
}
