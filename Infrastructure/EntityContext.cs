﻿using Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Infrastructure
{
    public class EntityContext : DbContext
    {

        public EntityContext()
        {
            this.Configuration.ValidateOnSaveEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
        }

        #region 数据集
        public DbSet<Sys_SysLog> Sys_SysLog { get; set; }
        public DbSet<Sys_BizLog> Sys_BizLog { get; set; }
        public DbSet<Sys_Account> Sys_Account { get; set; }
        public DbSet<Sys_Menu> Sys_Menu { get; set; }
        public DbSet<Sys_ManagePower> Sys_RoleMenu { get; set; }
        public DbSet<Sys_Role> Sys_Role { get; set; }
        public DbSet<Sys_Manage> Sys_Manage { get; set; }

        public DbSet<Sys_Dept> Sys_Dept { get; set; }
        #endregion

        /// <summary>
        /// API配置
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // 禁用默认表名复数形式
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            // 禁用一对多级联删除
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //// 禁用多对多级联删除
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<AC_Menu>().Property(o=>o.MenuCode).
        }

        /// <summary>
        /// Debug中输出SQL到调试窗口
        /// </summary>
        public void DebugWriteSQLLog()
        {
            this.Database.Log = DebugWriteLine;
        }

        /// <summary>
        /// 调试窗口输出EF的生成的SQL
        /// </summary>
        /// <param name="message"></param>
        private static void DebugWriteLine(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }
    }

    
    ///// <summary>
    ///// 定义sqllog格式
    ///// </summary>
    //public class SingleLineFormatter : System.Data.Entity.Infrastructure.Interception.DatabaseLogFormatter
    //{
    //    public SingleLineFormatter(System.Data.Entity.DbContext ctx, Action<string> action)
    //        : base(ctx, action)
    //    {

    //    }

    //    public override void LogResult<TResult>(System.Data.Common.DbCommand command, System.Data.Entity.Infrastructure.Interception.DbCommandInterceptionContext<TResult> interceptionContext)
    //    {
    //        //把EF生成的sql记录到数据库
    //        String connString = System.Configuration.ConfigurationManager.ConnectionStrings["MiDbContext"].ConnectionString;

    //        SqlConnectionStringBuilder connStr = new SqlConnectionStringBuilder(connString);

    //        using (SqlConnection conn = new SqlConnection(connStr.ConnectionString))
    //        {
    //            //拼接SQL语句
    //            StringBuilder strSQL = new StringBuilder();
    //            strSQL.Append("INSERT INTO [MiDemo].[dbo].[SYS_SQLLog]([SQLCommand],[ElapsedMilliseconds],[StartTime]) VALUES (@SQLCommand,@ElapsedMilliseconds,@StartTime)");

    //            //构造Parameter对象
    //            SqlParameter[] paras = new SqlParameter[]{ 
    //                new SqlParameter("@SQLCommand",command.CommandText.Replace(Environment.NewLine, "")),
    //                new SqlParameter("@ElapsedMilliseconds",this.Stopwatch.ElapsedMilliseconds),
    //                new SqlParameter("@StartTime", DateTime.Now),
    //            };

    //            //创建Command对象
    //            SqlCommand cmd = new SqlCommand();
    //            cmd.Connection = conn;
    //            cmd.CommandType = CommandType.Text;
    //            cmd.CommandText = strSQL.ToString();

    //            //遍历添加到Parameters集合中
    //            foreach (var item in paras)
    //            {
    //                cmd.Parameters.Add(item);
    //            }
    //            conn.Open();//一定要注意打开连接
    //            cmd.ExecuteNonQuery();
    //        }
            
    //        base.LogResult<TResult>(command, interceptionContext);
    //    }


    //}

    /// <summary>
    /// 配置格式
    /// </summary>
    //public class DbContextConfiguration : DbConfiguration
    //{
    //    public DbContextConfiguration()
    //    {
    //        SetDatabaseLogFormatter(
    //            (context, action) => new SingleLineFormatter(context, action));
    //    }
    //}

}