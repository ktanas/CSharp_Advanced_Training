﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPExtensions
{
    public static class Extension
    {
        public static List<T> Filter<T>(this List<T> records, Func <T, bool> func)
        {
            List<T> filteredList = new List<T>();

            foreach (T record in records)
            {
                if (func(record) == true)
                {
                    filteredList.Add(record);
                }

            }
            return filteredList;
        }
    }
}
