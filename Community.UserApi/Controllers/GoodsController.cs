using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Community.Model;
using Community.IRepository;
using Community.Repository;
using Community.Common.Session;


namespace Community.UserApi.Controllers
{

    [ApiController]
    public class GoodsController : ControllerBase
    {
        
        IGoodRepository _goodsRepository;
        

        public GoodsController(IGoodRepository goodsRepository)
        {
            _goodsRepository = goodsRepository;
        }
        static int pid = 0;
               

        [HttpPost]
        [Route("api/AddType")]
        /// <summary>
        /// 添加商品分类
        /// </summary>
        /// <param name="goodType"></param>
        /// <returns></returns>
        public ResultData AddType(GoodsType goodType)
        {
            
            string sql = $"insert into GoodsType(TypeId,GoodsTypeName,TypeIcon,GoodsOneId,Sort,State) values (null,@gname,@pic,@pid,@sort,@stat)";
            int i = _goodsRepository.Execute(sql, new
            {
                @gname = goodType.GoodsTypeName,     
                @pic=goodType.TypeIcon,
                @pid=goodType.GoodsOneId,
                @sort = goodType.Sort,
                @stat = goodType.State
            });
            if (i > 0)
            {
                return new ResultData
                {
                    Status = 200,
                    Msg = "成功",
                    Data = i
                };
            }
            else
            {
                return new ResultData
                {
                    Status = 500,
                    Msg = "失败",
                    Data = i
                };
            }
        }
        
        /// <summary>
        /// 获取不属于自己的子类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/GetSubdirectory")]
        [HttpGet]
        public ResultData GetSubdirectory(int gid)
        {
            pid = gid;
            string sql = $"select * from GoodsOneType a join GoodsType b on a.GoodsTypeId=b.GoodsOneId where b.GoodsOneId!=@id";
            List<GoodsType> goodsTypes = _goodsRepository.Query<GoodsType>(sql, new { @id = gid });
            return new ResultData
            {
                Status = 200,
                Msg = "成功",
                Data = goodsTypes
            };
        }
        /// <summary>
        /// 添加字目录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/AddSubdirectory")]
        [HttpPut]
        public ResultData AddSubdirectory(int id)
        {
            
            string sql = $"update GoodsType set GoodsOneId=@pid where TypeId=@id";
            int i = _goodsRepository.Execute(sql, new { @id = id, @pid =pid });
            if (i > 0)
            {
                return new ResultData
                {
                    Status = 200,
                    Msg = "成功",
                    Data = i
                };
            }
            else
            {
                return new ResultData
                {
                    Status = 500,
                    Msg = "失败",
                    Data = i
                };
            }
        }

       /// <summary>
       /// 添加商品信息
       /// </summary>
       /// <param name="goodsInfo"></param>
       /// <returns></returns>
       [HttpPost]
       [Route("api/AddGoods")]
        public ResultData AddGoods(GoodsInfo goodsInfo)
        {
            DateTime OperationTime = DateTime.Now;
            string sql = $"insert into GoodsInfo values (null,@gno,@gname,@keyword,@intro,@detail,@tid,@fid,@pic,@price,@sales,@integral,@unit,@stock,@sort,@state,@time,@isdel,@gspf)";
            int i = _goodsRepository.Execute(sql, new
            {
                @gno = goodsInfo.GoodsSerial,
                @gname = goodsInfo.GoodsName,
                @keyword = goodsInfo.GoodsKeyword,
                @intro = goodsInfo.GoodsIntro,
                @detail = goodsInfo.GoodsDetails,
                @tid = goodsInfo.GoodsTypeId,
                @fid = goodsInfo.FreightTemplate,
                @pic = goodsInfo.GoodsPicture,
                @price = goodsInfo.GoodsPrice,
                @sales = goodsInfo.GoodsSales,
                @integral = goodsInfo.GoodsIntegral,
                @unit = goodsInfo.GoodsUnit,
                @stock = goodsInfo.GoodsStock,
                @sort = goodsInfo.GoodsSort,
                @state=goodsInfo.GoodsState,
                @time = OperationTime,
                @isdel = goodsInfo.IsDelete,
                @gspf=goodsInfo.GoodsSpf
            });
            if (i > 0)
            {
                return new ResultData
                {
                    Status = 200,
                    Msg = "成功",
                    Data = i
                };
            }
            else
            {
                return new ResultData
                {
                    Status = 500,
                    Msg = "失败",
                    Data = i
                };
            }
        }

        /// <summary>
        /// 商品信息展示与查询
        /// </summary>
        /// <param name="goodname"></param>
        /// <returns></returns>
        [Route("api/GetGoods")]
        [HttpGet]
        public ResultData GetGoods(string goodname="",int page=1,int limit=3,int? pid=0)
        {
            //获取所有商品信息
            string sql = $"select * from GoodsInfo where 1=1";
            if (!string.IsNullOrEmpty(goodname))
            {
                sql += $" and GoodsName like concat('%',@gname,'%')";
            }
            if (pid != 0&&pid!=null)
            {
                sql += $" and GoodsTypeId=@pid";
            }
            List<GoodsInfo> goods = _goodsRepository.Query<GoodsInfo>(sql,new { @gname=goodname,@pid=pid});
            goods = goods.Where(p => p.IsDelete == 1).ToList();
            //获取条数
            int count = goods.Count;
            //分页
            goods = goods.Skip((page - 1) * limit).Take(limit).ToList();
            //返回
            return new ResultData
            {
                Status = 200,
                Msg = "成功",
                Data = goods,
                totalCount=count
            };
        }
        /// <summary>
        /// 回收站信息
        /// </summary>
        /// <param name="goodname"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/GetRecycle")]
        public ResultData Recycle(string goodname = "", int page = 1, int limit = 3)
        {
            //获取所有商品信息
            string sql = $"select * from GoodsInfo where 1=1";
            if (!string.IsNullOrEmpty(goodname))
            {
                sql += $" and GoodsName like concat('%',@gname,'%')";
            }
            List<GoodsInfo> goods = _goodsRepository.Query<GoodsInfo>(sql, new { @gname = goodname });
            goods = goods.Where(p => p.IsDelete == 2).ToList();
            //获取条数
            int count = goods.Count;
            //分页
            goods = goods.Skip((page - 1) * limit).Take(limit).ToList();
            //返回
            return new ResultData
            {
                Status = 200,
                Msg = "成功",
                Data = goods,
                totalCount = count
            };
        }

        //获取商品类型信息
        [HttpGet]
        [Route("api/GetType")]
        public ResultData GetGoodType(string tname = "",int page=1,int limit=3,int? stat=0)
        {
            string sql = $"select * from GoodsOneType a join GoodsType b on a.GoodsTypeId=b.GoodsOneId where 1=1";
            if (!string.IsNullOrEmpty(tname))
            {
                sql += $" and b.GoodsTypeName like concat('%',@tname,'%')";
            }
            if (stat != 0&&stat!=null)
            {
                sql += $" and b.State=@stat";
            }
            List<GoodsType> goodTypes = _goodsRepository.Query<GoodsType>(sql, new { @tname = tname,@stat=stat });
            int totalCount = goodTypes.Count;
            goodTypes = goodTypes.Skip((page - 1) * limit).Take(limit).ToList();
            return new ResultData
            {
                Status = 200,
                Msg = "成功",
                Data = goodTypes,
                totalCount=totalCount
            };
        }

        /// <summary>
        /// 删除商品类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/DelType")]
        public ResultData DelType(int id)
        {
            string sql = $"delete from GoodsType where TypeId=@id";
            int i = _goodsRepository.Execute(sql, new { @id = id });
            //判断
            if (i > 0)
            {
                return new ResultData
                {
                    Status = 200,
                    Msg = "成功",
                    Data = i
                };
            }
            else
            {
                return new ResultData
                {
                    Status = 500,
                    Msg = "失败",
                    Data = i
                };
            }
        }
        [HttpPut]
        [Route("api/EditGood")]
        /// <summary>
        /// 加入回收站
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultData EditGood(int id)
        {
            string sql = $"update GoodsInfo set IsDelete=2 where GoodsId=@id";
            int i = _goodsRepository.Execute(sql, new { @id = id });
            if (i > 0)
            {
                return new ResultData
                {
                    Status = 200,
                    Msg = "修改成功",
                    Data = i
                };
            }
            else
            {
                return new ResultData
                {
                    Status = 500,
                    Msg = "修改失败",
                    Data = i
                };
            }
        }
        /// <summary>
        /// 用于绑定下拉框
        /// </summary>
        /// <returns></returns>
       [HttpGet]
       [Route("api/GetTypes")]
        public ResultData GetTypes()
        {
            string sql = $"select * from GoodsOneType";
            List<GoodsOneType> types = _goodsRepository.Query<GoodsOneType>(sql);
            return new ResultData
            {
                Status = 200,
                Msg = "成功",
                Data = types
            };
        }
        /// <summary>
        /// 恢复商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/Recover")]
        [HttpPut]
        public ResultData Recover(int id)
        {
            string sql = $"update GoodsInfo set IsDelete=1 where GoodsId=@id";
            int i = _goodsRepository.Execute(sql, new { @id = id });
            if (i > 0)
            {
                return new ResultData
                {
                    Status = 200,
                    Msg = "修改成功",
                    Data = i
                };
            }
            else
            {
                return new ResultData
                {
                    Status = 500,
                    Msg = "修改失败",
                    Data = i
                };
            }
        }
      
        /// <summary>
        /// 获取规格
        /// </summary>
        /// <returns></returns>
        [Route("api/GetSpecification")]
        [HttpGet]
        public ResultData GetSpecification()
        {
            string sql = $"select * from Specification a join SpecificationValue b on a.SpecificationId=b.SpecificationValueId";
            List<Specification> specifications = _goodsRepository.Query<Specification>(sql);
            return new ResultData
            {
                Status = 200,
                Msg = "成功",
                Data = specifications
            };
        }
        
        /// <summary>
        /// 获取运费模板
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/GetFreight")]
        public ResultData GetFreight()
        {
            string sql = $"select * from FreightTemplate";
            List<FreightTemplate> freightTemplates = _goodsRepository.Query<FreightTemplate>(sql);
            return new ResultData
            {
                Status = 200,
                Msg = "成功",
                Data = freightTemplates
            };
        }
        /// <summary>
        /// 修改商品类型状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/EditStat")]
        public ResultData EditStat(int id,int state)
        {
            int stat = 0;
            if (state == 0)
            {
                stat = 1;
            }
            if (state == 1)
            {
                stat = 0;
            }
            string sql = $"update GoodsType set State=@stat where TypeId=@id";
            int i = _goodsRepository.Execute(sql, new { @id = id, @stat = stat });
            if (i > 0)
            {
                return new ResultData
                {
                    Status = 200,
                    Msg = "成功",
                    Data = i
                };
            }
            else
            {
                return new ResultData
                {
                    Status = 500,
                    Msg = "失败",
                    Data = i
                };
            }
        }
        /// <summary>
        /// 修改商品状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/EditGoodStat")]       
        public ResultData EditGoodStat(int id,int state)
        {
            int stat = 0;
            if (state == 1)
            {
                stat = 0;
            }
            if (state == 0)
            {
                stat = 1;
            }
            string sql = $"update GoodsInfo set GoodsState=@stat where GoodsId=@id";
            int i = _goodsRepository.Execute(sql, new { @id = id, @stat = stat });
            if (i > 0)
            {
                return new ResultData
                {
                    Status = 200,
                    Msg = "成功",
                    Data = i
                };
            }
            else
            {
                return new ResultData
                {
                    Status = 500,
                    Msg = "失败",
                    Data = i
                };
            }
        }

        //public ResultData EditGoods(GoodsInfo goodsInfo)
        //{
        //    string sql=$"update GoodsInfo set "
        //}

    }
}
