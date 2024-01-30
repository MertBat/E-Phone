using E_Phone.BLL.DTOs.Auth;
using E_Phone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Phone.BLL.Handlers.Abstract
{
    public interface ITokenHandler
    {
        TokenDTO CreateAccessToken(User user);
        int GetIdFromToken(string token);
    }
}
