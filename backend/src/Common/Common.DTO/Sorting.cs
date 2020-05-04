using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTO
{
    public class Sorting
    {
        public string columnName { get; set; }
        public string sortDerection
        {
            get
            {
                return sortDerection;
            }
            set
            {
                value = value[0].ToString().ToUpper() + value.Substring(1);
            }
        }
    }
}
