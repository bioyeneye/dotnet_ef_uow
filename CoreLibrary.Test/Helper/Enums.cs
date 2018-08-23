using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLibrary.Test.Helper
{
    public class Enums
    {
        public enum ExternalLoginStatus
        {
            Ok = 0,
            Error = 1,
            Invalid = 2,
            TwoFactor = 3,
            Lockout = 4,
            CreateAccount = 5

        }
    }
}
