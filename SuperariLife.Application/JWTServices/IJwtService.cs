using SuperariLife.Contracts.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperariLife.Application.JWTServices
{
    public interface IJwtService
    {
        string GenerateToken(LoginUserResponseModel user);
    }
}
