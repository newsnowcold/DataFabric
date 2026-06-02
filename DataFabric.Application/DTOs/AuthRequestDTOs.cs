using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFabric.Application.DTOs
{
    public class RegisterRequest
    {
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }

    public class LoginRequest
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }

    public class TokenRequest
    {
        public string RefreshToken { get; set; } = default!;
    }

    public class ForgotPasswordRequest
    {
        public string Email { get; set; } = default!;
    }

    public class ResetPasswordRequest
    {
        public string Email { get; set; } = default!;
        public string Token { get; set; } = default!;
        public string NewPassword { get; set; } = default!;
    }
}
