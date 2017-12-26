using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class ExtDbSet
    {
        /// <summary>
        /// 扩展逻辑删除,实现了ILogicDelete的实体将被逻辑删除,否则将被物理删除
        /// </summary>
        public override TEntity LogicRemove<TEntity>(this DbSet<TEntity> DbSet, TEntity entity) where TEntity : class
        {
            if (entity != null)
            {
                if (entity is ILogicDelete)
                {
                    (entity as ILogicDelete).IsDel = true;
                }
                else
                {
                    DbSet.Remove(entity);
                }
            }
            return entity;
        }

        /// <summary>
        /// 扩展逻辑删除,实现了ILogicDelete的实体将被逻辑删除,否则将被物理删除
        /// </summary>
        public override IEnumerable<TEntity> LogicRemoveRange(this DbSet<TEntity> DbSet, IEnumerable<TEntity> entities)
        {
            if(entities != null)
            {
                entities.ToList().ForEach(o => {
                    if (o is ILogicDelete)
                    {
                        (o as ILogicDelete).IsDel = true;
                    }
                    else
                    {
                        DbSet.Remove(o);
                    }
                });
            }
            return entities;
        }
    }
}
