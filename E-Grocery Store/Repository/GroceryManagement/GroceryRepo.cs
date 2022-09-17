using E_Grocery_Store.Common.CustomException;
using E_Grocery_Store.DataAccess;
using E_Grocery_Store.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Grocery_Store.Repository.GroceryManagement
{
    public class GroceryRepo : IGroceryRepo
    {
        private readonly AppDbContext appDbContext;

        public GroceryRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task AddGrocery(Grocery newGrocery)
        {
            try
            {
                if (newGrocery == null)
                {
                    throw new RequestException("Request body is empty");
                }

                //Logic to ignore category and vender object received from request body
                newGrocery.Category = null;
                newGrocery.Vendor = null;
                newGrocery.StatusId = 1;

                await appDbContext.Groceries.AddAsync(newGrocery);
                await appDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Grocery>> GetGroceries()
        {
            try
            {
                var result = await appDbContext.Groceries.Include(g => g.Category).Include(g => g.Vendor).Include(g => g.Status).ToListAsync();
                if (result.Count == 0)
                {
                    throw new ResponseException("No Groceris available");
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GroceryCategory>> GetGroceryCategories()
        {
            try
            {
                var result = await appDbContext.GroceryCategories.ToListAsync();
                if (result.Count == 0)
                {
                    throw new ResponseException("No Category available");
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Grocery>> GetVendorGroceries(int vendorId)
        {
            try
            {
                var result = await appDbContext.Groceries.Include(g => g.Category).Include(g => g.Vendor).Include(g => g.Status).Where(g => g.VendorId == vendorId).ToListAsync();
                if (result.Count == 0)
                {
                    throw new ResponseException("No Groceris available");
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Grocery> GetGrocery(int groceryId)
        {
            try
            {
                var result = await appDbContext.Groceries.Include(g => g.Category).Include(g => g.Vendor).Include(g => g.Status).FirstOrDefaultAsync(g => g.Id == groceryId);
                if (result == null)
                {
                    throw new RequestException($"No Grocery with id:{groceryId} present");
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateGrocery(Grocery grocery)
        {
            try
            {
                if (grocery == null)
                {
                    throw new RequestException("Request body is empty");
                }

                var existingGrocery = await appDbContext.Groceries.FirstOrDefaultAsync(g => g.Id == grocery.Id);

                if (existingGrocery == null)
                {
                    throw new RequestException($"No Grocery with id:{grocery.Id} present");
                }
                existingGrocery.Name = grocery.Name;
                existingGrocery.CategoryId = grocery.CategoryId;
                existingGrocery.Price = grocery.Price;

                await appDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteGrocery(int id)
        {
            try
            {
                var existingGrocery = await appDbContext.Groceries.FirstOrDefaultAsync(g => g.Id == id);

                if (existingGrocery == null)
                {
                    throw new RequestException($"No Grocery with id:{id} present");
                }
                appDbContext.Groceries.Remove(existingGrocery);
                await appDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
