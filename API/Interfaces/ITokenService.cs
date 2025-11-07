using System;
using API.Enttities;

namespace API.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}
