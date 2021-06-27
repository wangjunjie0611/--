using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Model
{
    public class Specification
    {
        public int SpecificationId    { get; set; }
        public string SpecificationName  { get; set; }
        public string GoodsSpecification { get; set; }


        public int ValueId { get; set; }
        public string ValueName { get; set; }
        public int SpecificationValueId { get; set; }
    }
}
