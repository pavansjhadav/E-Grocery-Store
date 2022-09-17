using E_Grocery_Store.Models;
using System.Threading.Tasks;

namespace E_Grocery_Store.Repository.AccountManagement
{
    public interface IAccountRepo
    {
        public Task SignUp(User user);
        public Task<User> SignIn(SignInRequest credential);
    }
}
