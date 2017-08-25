using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infrastructure;
using Infrastructure.Entity;
using log4net.Config;
[assembly: XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
namespace UnitTest
{
    [TestClass]
    public class TestBase
    {
        [TestMethod]
        public void TestLog4net()
        {
            
            log4net.ILog myLogger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            myLogger.Fatal("测试");
            try 
	        {	        
		        throw new ArgumentNullException("我借了");
	        }
	        catch (Exception e)
	        {
                myLogger.Warn("发生",e);
		        throw;
	        }
            
        }

        //[TestMethod]
        //public void TestEF()
        //{
        //    var EntityDB = new EntityDB();
        //    EntityDB.Sys_ManageBizLog.Add(new Sys_BizLog() { Action = "Index", Controller = "Test", AddTime = DateTime.Now, Message = "测试", Role_ManageID = Guid.NewGuid() });
        //    EntityDB.SaveChanges();
        //}
    }
}
