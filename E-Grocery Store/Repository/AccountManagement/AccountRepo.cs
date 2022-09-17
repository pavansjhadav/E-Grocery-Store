using E_Grocery_Store.Common.CustomException;
using E_Grocery_Store.DataAccess;
using E_Grocery_Store.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Grocery_Store.Repository.AccountManagement
{
    public class AccountRepo : IAccountRepo
    {
        private readonly AppDbContext appDbContext;

        public AccountRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task SignUp(User user)
        {
            try
            {
                if (user == null)
                {
                    throw new RequestException("Request body is empty");
                }

                //Logic to ignore role and gender object received from request body
                if (user.Role != null)
                {
                    user.Role = null;
                }
                if (user.Gender != null)
                {
                    user.Gender = null;
                }

                user.RoleId = 2;

                var existingUser = await appDbContext.Users.FirstOrDefaultAsync(u => u.Email.Equals(user.Email));
                if (existingUser != null)
                {
                    throw new DuplicateMailIdException("This mail id is already in use");
                }
                await appDbContext.Users.AddAsync(user);
                await appDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> SignIn(SignInRequest credentials)
        {
            try
            {
                if (credentials == null)
                {
                    throw new RequestException("Request body is empty");
                }
                var user = await appDbContext.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == credentials.Email && u.Password == credentials.Password);
                if (user == null)
                {
                    throw new InvalidCredentialException("Invalid Credentials");
                }
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
