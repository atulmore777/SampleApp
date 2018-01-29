﻿using Bijankur.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bijankur.DAL.Repository
{
    public interface IUserRepository
    {
        long Add(Users item);       
        bool Update(Users item);
        bool Remove(long userid);       
        Users Find(long userid);
        Users FindByEmail(string email);
        IEnumerable<Users> GetAll();
    }
    public class UserRepository : IUserRepository
    {
        public DataLayerContext _context { get; set; }
        public UserRepository(DataLayerContext context)
        {
            _context = context;
        }

        public long Add(Users item)
        {
            long result = 0;
            try
            {
                _context.Users.Add(item);
                _context.SaveChanges();
                result = item.UserId;
            }
            catch (Exception ex)
            {
                result = 0;
            }
            return result;
        }

        public bool Update(Users item)
        {
            bool result = false;
            try
            {
                var itemToUpdate = _context.Users.SingleOrDefault(x => x.UserId == item.UserId);

                if (itemToUpdate != null)
                {
                    itemToUpdate.FirstName = item.FirstName;
                    itemToUpdate.LastName = item.LastName;
                    itemToUpdate.Email = item.Email;                  
                    itemToUpdate.ContactNumber = item.ContactNumber;
                    itemToUpdate.Address = item.Address;
                    itemToUpdate.BirthDate = item.BirthDate;
                    itemToUpdate.Status = item.Status;
                    itemToUpdate.UserType = item.UserType;
                    itemToUpdate.UpdatedOn = item.UpdatedOn;
                    itemToUpdate.UpdatedBy = item.UpdatedBy;

                    _context.Users.Update(itemToUpdate);
                    _context.SaveChanges();
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch(Exception ex)
            {
                result = false;
            }
            return result;
        }

        public bool Remove(long userId)
        {
            bool result = false;
            try
            {
                var itemToRemove = _context.Users.SingleOrDefault(x => x.UserId == userId);
                if (itemToRemove != null)
                {
                    _context.Users.Remove(itemToRemove);
                    _context.SaveChanges();
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch(Exception ex)
            {
                result = false;
            }
            return result;
        }

        public Users Find(long userId)
        {
            var userData = _context.Users.SingleOrDefault(a => a.UserId == userId);
            return userData;
        }

        public Users FindByEmail(string email)
        {
            var userData = _context.Users.SingleOrDefault(a => a.Email == email);
            return userData;
        }

        public IEnumerable<Users> GetAll()
        {
            IEnumerable<Users> allUsers = _context.Users.AsQueryable<Users>().ToList();
            return allUsers;
        }


    }
}
