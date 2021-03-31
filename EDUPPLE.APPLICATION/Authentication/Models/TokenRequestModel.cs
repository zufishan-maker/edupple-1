using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDUPPLE.APPLICATION.Authentication.Models
{
   

    public class TokenRequestModel
    {       
        public string GrantType { get; set; }         
        public string UserName { get; set; }      
        public string Password { get; set; }      
        public string RefreshToken { get; set; }

    }
}
