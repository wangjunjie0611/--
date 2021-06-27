using System;
using System.Collections.Generic;
using Community.IRepository;
using Community.Model;
using Community.Common;
using Community.Repository.DBContext;
using System.Linq;

namespace Community.Repository
{
    public class GoodRepository : IGoodRepository
    {
        //实例化工厂
        DbFactory db = new DbFactory();

        AllContext allContext = new AllContext();
        
        
        public int Execute(string sql, object param = null)
        {
            return db.DbHelper().Execute(sql,param);
        }

        public int ExecuteTransaction(string[] sqlarr, object[] param)
        {
            throw new NotImplementedException();
        }

        public int ExecuteTransaction(Dictionary<string, object> dic)
        {
            throw new NotImplementedException();
        }
        //获取运费模板
        public List<FreightTemplate> GetFreightTemplates(string sql)
        {
            throw new NotImplementedException();
        }

        //商品信息
        public List<GoodsInfo> GetGoods(string sql)
        {
            throw new NotImplementedException();

        }
        //商品类型的信息
        public List<GoodsType> GetGoodTypes(string sql)
        {
            throw new NotImplementedException();
        }
        //获取规格
        public List<Specification> GetSpecifications(string sql)
        {
            throw new NotImplementedException();
        }

        public List<T> Query<T>(string sql, object param = null) where T : class, new()
        {
            return db.DbHelper().Query<T>(sql,param);
        }

      
    }
}
