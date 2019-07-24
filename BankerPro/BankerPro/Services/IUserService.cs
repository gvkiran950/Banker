using BankerPro.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BankerPro.Services
{
    public interface IUserService
    {
        string Login(User user);
        Task<List<string>> GetValues();
    }
}
