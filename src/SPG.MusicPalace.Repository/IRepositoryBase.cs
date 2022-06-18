using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SPG.MusicPalace.Repository
{
    public interface IRepositoryBase<TEntity>
    {
        IQueryable<TEntity> GetQueryable(
            out bool hasMore,
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? sortOrder = null,
            string? includeNavigationProperty = null,
            int? skip = null,
            int? take = null);

        IQueryable<TEntity> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeNavigationProperty = "",
            int? skip = null,
            int? take = null);

        IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeNavigationProperty = "",
            int? skip = null,
            int? take = null);

        TEntity? GetSingle(
            Expression<Func<TEntity, bool>>? filter = null,
            string includeNavigationProperty = "");

        TEntity Create(TEntity newModel);

        TEntity Edit(TEntity newModel);
    }
}

