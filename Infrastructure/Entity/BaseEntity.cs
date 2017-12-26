using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entity
{
    /// <summary>
    /// 基础实体
    /// </summary>
    public abstract class BaseEntity
    {
        public bool IsDel { get; set; }
    }
}
