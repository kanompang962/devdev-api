using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace devdev_api.Interfaces.Services.IAuth
{
    public interface ICookieService
    {
        void SetRefreshToken(HttpResponse response, string token);
        void DeleteRefreshToken(HttpResponse response);
    }
}