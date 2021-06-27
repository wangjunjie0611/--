using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Model
{
    public class ResultData
    {
        public int Status { get; set; }
        public string Msg { get; set; }
        public object Data { get; set; }
        public int totalCount { get; set; }
    }
}
