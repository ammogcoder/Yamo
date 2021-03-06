﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yamo.Infrastructure;
using Yamo.Sql;

namespace Yamo.PlaygroundCS.Sql
{
    public class MyDateDiff : SqlHelper
    {

        public static bool SameYear(DateTime date1, DateTime date2)
        {
            throw new Exception("This method is not intended to be called directly.");
        }

        public static bool SameQuarter(DateTime date1, DateTime date2)
        {
            throw new Exception("This method is not intended to be called directly.");
        }

        // ...

        public new static string GetSqlFormat(MethodInfo method, SqlFormatter formatter)
        {
            switch (method.Name)
            {
                case nameof(DateDiff.SameYear):
                    return "DATEDIFF(year, {0}, {1}) = 0";
                case nameof(DateDiff.SameQuarter):
                    return "DATEDIFF(quarter, {0}, {1}) = 0";
                // ...
                default:
                    throw new NotSupportedException($"Method '{method.Name}' is not supported.");
            }
        }
    }
}
