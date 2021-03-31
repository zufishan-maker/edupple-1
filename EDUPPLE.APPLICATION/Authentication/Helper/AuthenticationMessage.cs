using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDUPPLE.APPLICATION.Authentication.Helper
{
   public static  class AuthenticationMessage
    {
        //login
        public const string InvalidCredential = "User name or password is incorrect; UserName:{0} or User not exists.";
        public const string UserLockOut = "User is locked out; UserName: {0}";
        public const string Login = "Successfully Login.";
        public const string RefreshToken = "Create refresh token for {0}";
        public const string InvalidGrantType = "Invalid Grant Type., ex. GrantType { password or refresh_token";
        public const string RefreshTokenNotFound = "Refresh token not found";
        public const string RefreshTokenExpired = "Refresh token expired.";

        //forgot password
        public const string User_Not_Found = "User is not exists.";
    }
}
