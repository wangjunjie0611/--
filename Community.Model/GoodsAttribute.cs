using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Model
{
   public  class GoodsAttribute
    {
        public int AttributeId    { get; set; }
        public string Specification  { get; set; }
        public string Picture        { get; set; }
        public decimal Price          { get; set; }
        public decimal CostPrice      { get; set; }
        public decimal OriginalPrice  { get; set; }
        public int Inventory      { get; set; }
        public decimal Weight         { get; set; }
        public decimal Bulk           { get; set; }
        public string GoodsSerial    { get; set; }


    }
}
