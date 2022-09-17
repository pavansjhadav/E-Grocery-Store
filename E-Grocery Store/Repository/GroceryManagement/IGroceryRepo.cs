using E_Grocery_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Grocery_Store.Repository.GroceryManagement
{
    public interface IGroceryRepo
    {
        public Task AddGrocery(Grocery newGrocery);
        public Task UpdateGrocery(Grocery grocery);
        public Task<List<Grocery>> GetGroceries();
        public Task<Grocery> GetGrocery(int groceryId);
        public Task<List<Grocery>> GetVendorGroceries(int vendorId);
        public Task<List<GroceryCategory>> GetGroceryCategories();
        public Task DeleteGrocery(int id);
    }
}
