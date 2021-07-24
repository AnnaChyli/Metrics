using System;
namespace Metrics.Domain
{
    public class Credentials
    {
        public Credentials(string login, string pwd)
        {
            Login = login;
            Pwd = pwd;
        }

        public string Login { get; }
        public string Pwd { get; }
    }
}
