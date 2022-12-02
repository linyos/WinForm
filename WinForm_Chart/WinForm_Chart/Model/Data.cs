using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForm_Chart.Model
{



    public class  DataList  
    {
        [DisplayName("編號")]
        public int Id { get; set; }
    }


    public class Data : IData
    {
        [DisplayName("編號")]
        public int Id { get; set; }
        [DisplayName("姓")]
        public string Firstname { get; set; }
        [DisplayName("名字")]
        public string Lastname { get; set; }
       [DisplayName("城市")]
        public string City { get; set; }
        
        public List<ParamData> ParamData { get; set; }

        //public object ParamData ParamData { get; set; }
    }
    public class ParamData
    {
        [DisplayName("稱號")]
        public string Name { get; set; }
       [DisplayName("型態")]
        public int Type { get; set; }
    }
}
