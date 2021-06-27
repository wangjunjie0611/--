using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.Model;

namespace Community.IRepository
{
    public interface IGoodRepository:IBaseRepository
    {
        //获取商品信息
        List<GoodsInfo> GetGoods(string sql);

        //获取商品类型的信息
        List<GoodsType> GetGoodTypes(string sql);
        //获取商品规格
        List<Specification> GetSpecifications(string sql);
        //获取运费模板
        List<FreightTemplate> GetFreightTemplates(string sql);
    }
}
