using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace WinForm_Chart.Model
{
    public  interface IData
    {
        [Description("id")]
        public int Id { get; set; }
        
        [Description("FirstName")]
        public string Firstname { get; set; }
        [Description("LastName")]
        public string Lastname { get; set; }
        [Description("City")]
        public string City { get; set; }

        //[Description("ParamData")]
        public ParamData ParamData { get; set; }
    }
}
