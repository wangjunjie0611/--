using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore;
using Community.Model;
using Community.Common;

namespace Community.Repository.DBContext
{
    public class AllContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(ConfigHelper.ConnecMySql);
           // base.OnConfiguring(optionsBuilder); 
        }

        public DbSet<GoodsInfo> Goods { get; set; }
        public DbSet<GoodsType> GoodTypes { get; set; }


    }
}
