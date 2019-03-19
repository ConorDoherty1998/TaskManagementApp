using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foop2CA1
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }

        public override string ToString()
        {
            return string.Format($"{FirstName} {LastName}");
        }
    }
}
