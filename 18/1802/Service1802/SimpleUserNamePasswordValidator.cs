using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service1802
{
    public class SimpleUserNamePasswordValidator : UserNamePasswordValidator
    {
        public IDictionary<string, string> UserNamePasswords { get; private set; }

        public SimpleUserNamePasswordValidator() {

            UserNamePasswords = new Dictionary<string, string>();
            UserNamePasswords.Add("foo","password");
            UserNamePasswords.Add("bar", "password");
            UserNamePasswords.Add("baz", "password");

        }

        public override void Validate(string userName, string password)
        {
            Console.WriteLine("开始认证客户端。。。");
            bool authen = true;
            if (!UserNamePasswords.ContainsKey(userName.ToLower()))
                authen = false;
            if (authen)
            {
                if (UserNamePasswords[userName.ToLower()] != "password")
                    authen = false;
            }
            if (!authen)
                throw new  SecurityTokenValidationException("客户端认证失败！用户名密码错误！");
        }
    }
}
