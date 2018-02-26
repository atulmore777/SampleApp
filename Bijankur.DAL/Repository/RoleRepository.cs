using Bijankur.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bijankur.DAL.Repository
{
    public interface IRoleRepository
    {
        int Add(Roles item);
        bool Update(Roles item);
        bool Remove(int roleid);
        Roles Find(int roleid);
        Roles FindByName(string rolename);
        IEnumerable<Roles> GetAll();
    }
    public class RoleRepository : IRoleRepository
    {
        public DataLayerContext _context { get; set; }
        public RoleRepository(DataLayerContext context)
        {
            _context = context;
        }

        public int Add(Roles item)
        {
            int result = 0;
            try
            {
                _context.Roles.Add(item);
                _context.SaveChanges();
                result = item.RoleId;
            }
            catch (Exception ex)
            {
                result = 0;
            }
            return result;
        }

        public bool Update(Roles item)
        {
            bool result = false;
            try
            {
                var itemToUpdate = _context.Roles.SingleOrDefault(x => x.RoleId == item.RoleId);

                if (itemToUpdate != null)
                {
                    itemToUpdate.RoleDescription = item.RoleDescription;
                    itemToUpdate.IsDeleted = item.IsDeleted;
                    itemToUpdate.RoleName = item.RoleName;
                    itemToUpdate.UpdatedOn = item.UpdatedOn;
                    itemToUpdate.UpdatedBy = item.UpdatedBy;
                    _context.Roles.Update(itemToUpdate);
                    _context.SaveChanges();
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public bool Remove(int roleid)
        {
            bool result = false;
            try
            {
                var itemToRemove = _context.Roles.SingleOrDefault(x => x.RoleId == roleid);
                if (itemToRemove != null)
                {
                    _context.Roles.Remove(itemToRemove);
                    _context.SaveChanges();
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public Roles Find(int roleid)
        {
            var roleData = _context.Roles.SingleOrDefault(a => a.RoleId == roleid);
            return roleData;
        }

        public Roles FindByName(string rolename)
        {
            var roleData = _context.Roles.SingleOrDefault(a => a.RoleName == rolename);
            return roleData;
        }

        public IEnumerable<Roles> GetAll()
        {
            IEnumerable<Roles> allRoles = _context.Roles.AsQueryable<Roles>().ToList();
            return allRoles;
        }
    }
}
