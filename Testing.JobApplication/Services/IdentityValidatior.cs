using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.JobApplication.Services
{
    public class IdentityValidatior : IIdentityValidatior
    {
        public bool IsValid(string identityNumber)
        {
            return true;
        }
    }
}
