using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Young.DAL;
using Young.Model;


namespace Young.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Young.Provider.YoungMembershipProvider p = new Provider.YoungMembershipProvider();

            var pass = p.EncodePassword("111111");
            System.Console.WriteLine(p.EncodePassword("111111"));
            System.Console.WriteLine(p.EncodePassword("111111"));
            System.Console.Read();
        }
    }
}
