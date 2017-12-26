using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entity
{
    /// <summary>
    /// 逻辑删除接口
    /// </summary>
    public interface ILogicDelete
    {
        /// <summary>
        /// 是否已经删除，默认为false
        /// </summary>
        bool IsDel { get; set; }
    }
}
