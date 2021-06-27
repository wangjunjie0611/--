using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Model
{
    public class GoodsInfo
    {
        public int GoodsId			     { get; set; }
        public string GoodsSerial { get; set; }
        public string GoodsName       { get; set; }
        public string GoodsKeyword    { get; set; }
        public string GoodsIntro      { get; set; }
        public string GoodsDetails    { get; set; }
        public int GoodsTypeId     { get; set; }
        public int FreightTemplate { get; set; }
        public string GoodsPicture    { get; set; }
        public decimal GoodsPrice      { get; set; }
        public int GoodsSales      { get; set; }
        public int GoodsIntegral   { get; set; }
        public string GoodsUnit       { get; set; }
        public int GoodsStock      { get; set; }
        public int GoodsSort       { get; set; }
        public int GoodsState      { get; set; }
        public DateTime OperationTime   { get; set; }
        public string OperationTimes { get { return OperationTime.ToString("yyyy-MM-dd hh:mm:ss"); } }
        public int IsDelete { get; set; }
        public int GoodsSpf { get; set; }

    }
}
