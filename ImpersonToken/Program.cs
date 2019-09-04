using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ImpersonToken
{
    class Program
    {

        public const int LOGON32_PROVIDER_DEFAULT = 0;
        public const int LOGON32_LOGON_INTERACTIVE = 2;

        [DllImport("advapi32.dll",SetLastError = true)]
        public static extern bool LogonUser(string userName, string domainName, string password, int logonType,
                                    int logonProvider, ref IntPtr token);

        [DllImport("advapi32.dll" , CharSet = CharSet.Auto,  SetLastError = true)]
        public static extern bool DuplicateToken(IntPtr existingToken, int imperonationLevel, ref IntPtr newToken);

        public static WindowsIdentity CreateWindowsIdentity(string userName,  string password, TokenImpersonationLevel tokenImpersonationLevel) {
            IntPtr token = IntPtr.Zero;
            IntPtr duplicateToken = IntPtr.Zero;
            if (LogonUser(userName, string.Empty, password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref token))
            {
                int impersonationLevel;
                switch (tokenImpersonationLevel)
                {
                    case TokenImpersonationLevel.Anonymous:
                        impersonationLevel = 0;
                        break;
                    case TokenImpersonationLevel.Impersonation:
                        impersonationLevel = 2;
                        break;
                    case TokenImpersonationLevel.Delegation:
                        impersonationLevel = 3;
                        break;
                    default:
                        impersonationLevel = 1;
                        break;
                }
                if (DuplicateToken(token, impersonationLevel, ref duplicateToken))
                    return new WindowsIdentity(duplicateToken);
                else
                    throw new InvalidOperationException(string.Format(" 创建模拟令牌失败 （错误代码：{0}） ", Marshal.GetLastWin32Error()));

            }
            else
                throw new InvalidOperationException(string.Format(" 用户登录失败 （错误代码：{0}） ", Marshal.GetLastWin32Error()));
        }

        public static void ReadFile(string userName, string password) {
            try
            {

                WindowsIdentity identity = CreateWindowsIdentity(userName, password, TokenImpersonationLevel.Impersonation);
                using (WindowsImpersonationContext context = identity.Impersonate())
                {
                    Console.WriteLine("当前用户为：{0}", WindowsIdentity.GetCurrent().Name);
                    File.ReadAllText(@"G:\Git\Test\README.md");
                    Console.WriteLine("成功读取文件内容！");

                }

            }
            catch (Exception ex){
                Console.WriteLine("读取文件失败！");
            }

        }



        static void Main(string[] args)
        {
            ReadFile("Foo", "Password");

            ReadFile("Bar", "Password");

            Console.ReadKey();
        }
    }
}
