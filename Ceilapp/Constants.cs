using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Ceilapp{
    public static class Constants
    {
        public const string ADMIN = "ADMIN";
         public const string STUDENT = "STUDENT";
          public const string TEACHER = "TEACHER";

          public static readonly string[] Roles = {ADMIN, STUDENT, TEACHER};
    }
}