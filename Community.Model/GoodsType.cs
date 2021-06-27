using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Model
{
    /// <summary>
    /// 商品类型表
    /// </summary>
    public class GoodsType 
    {
        /// <summary>
        /// 商品类别ID
        /// </summary>
        public int TypeId { get; set; }
        /// <summary>
        /// 商品分类名称
        /// </summary>
        public string GoodsTypeName { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        /// <summary>
        /// 分类图标
        /// </summary>
        public string TypeIcon { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 父级id
        /// </summary>

        public int GoodsOneId { get; set; }
        public int IsDelete { get; set; }
        public int GoodsTypeId { get; set; }

        public string GoodsOneName { get; set; }

    }
}
