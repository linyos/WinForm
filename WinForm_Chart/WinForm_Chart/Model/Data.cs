﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForm_Chart.Model
{

    public class Data : IData
    {
        public int Id { get; set; }
        
        public string Firstname { get; set; }
        
        public string Lastname { get; set; }
       
        public string City { get; set; }

        public  ParamData ParamData { get; set; }
    }
    public class ParamData
    {
      
        public string Name { get; set; }
       
        public int Type { get; set; }
    }
}
