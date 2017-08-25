using Domain;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repository
{
    /// <summary>
    /// 仓储基础类
    /// </summary>
    public class BaseRepository
    {

        //private readonly ThreadLocal<EntityDB> localCtx = new ThreadLocal<EntityDB>(() => new EntityDB());
        //public EntityDB Entity
        //{
        //    get { return localCtx.Value; }
        //}

        //public BaseRepository()
        //{
            
        //}

        /// <summary>
        /// 新建一个DbContext
        /// </summary>
        /// <returns></returns>
        //public EntityDB NewEntity()
        //{
        //    return new EntityDB();
        //}
    }
}
