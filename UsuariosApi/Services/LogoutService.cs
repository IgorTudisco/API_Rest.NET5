using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class LogoutService
    {
        private SignInManager<CustomIdentityUser> _signinManager;

        public LogoutService(SignInManager<CustomIdentityUser> signinManager)
        {
            _signinManager = signinManager;
        }

        internal Result DeslogaUsuario()
        {
            var resultadoIdentity = _signinManager.SignOutAsync();
            if (resultadoIdentity.IsCompleted) return Result.Ok();
            return Result.Fail("Logout falhou");
        }
    }
}
