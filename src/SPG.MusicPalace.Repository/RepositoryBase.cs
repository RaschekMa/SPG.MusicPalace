using Microsoft.EntityFrameworkCore;
using Spg.MusicPalace.Domain.Exceptions;
using Spg.MusicPalace.Domain.Model;
using Spg.MusicPalace.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SPG.MusicPalace.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        private readonly MusicPalaceDbContext _dbContext;

        public RepositoryBase(MusicPalaceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> GetQueryable(
            out bool hasMore,
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? sortOrder = null,
            string? includeNavigationProperty = "",
            int? skip = null,
            int? take = null)
        {
            hasMore = false;

            IQueryable<TEntity> result = _dbContext.Set<TEntity>();

            if (filter != null)
            {
                result = result.Where(filter);
            }
            if (sortOrder != null)
            {
                result = sortOrder(result);
            }

            if (string.IsNullOrEmpty(includeNavigationProperty))
            {
                foreach (var item in includeNavigationProperty.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    result = result.Include(item);
                }
            }

            int count = result.Count();
            if (skip.HasValue)
            {
                result = result.Skip(skip.Value);
            }
            if (take.HasValue)
            {
                result = result.Take(take.Value);
            }

            if (result.Count() < count)
            {
                hasMore = true;
            }
            return result;
        }

        public IQueryable<TEntity> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeNavigationProperty = "",
            int? skip = null,
            int? take = null)
        {
            bool hasMore;
            return GetQueryable(
                out hasMore,
                null,
                orderBy,
                includeNavigationProperty,
                skip,
                take
            );
        }

        public IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeNavigationProperty = "",
            int? skip = null,
            int? take = null)
        {
            bool hasMore;

            return GetQueryable(
                out hasMore,
                filter,
                orderBy,
                includeNavigationProperty,
                skip,
                take
            );
        }

        public TEntity? GetSingle(
            Expression<Func<TEntity, bool>>? filter = null,
            string includeNavigationProperty = "")
        {
            bool hasMore;
            return GetQueryable(
                out hasMore,
                filter,
                null,
                includeNavigationProperty: includeNavigationProperty
            ).SingleOrDefault()
                ?? null; // throw new KeyNotFoundException("Datensatz konnte nicht eindeutig gefunden werden!");
        }

        public TEntity Create(TEntity newModel)
        {
            _dbContext.Add(newModel);
            try
            {
                _dbContext.SaveChanges();
            }
            catch (InvalidOperationException ex)
            {
                throw new RepositoryCreateException("Methode TEntity Create(TEntity) ist fehlgeschlagen!", ex);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new RepositoryCreateException("Methode TEntity Create(TEntity) ist fehlgeschlagen!", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryCreateException("Methode TEntity Create(TEntity) ist fehlgeschlagen!", ex);
            }
            return newModel;
        }

        public TEntity Edit(TEntity newModel)
        {
            _dbContext.Update(newModel);
            try
            {
                _dbContext.SaveChanges();
                return newModel;
            }
            catch (InvalidOperationException ex)
            {
                throw new RepositoryUpdateException("Methode TEntity Edit(TEntity) ist fehlgeschlagen!", ex);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new RepositoryUpdateException("Methode TEntity Edit(TEntity) ist fehlgeschlagen!", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryUpdateException("Methode TEntity Edit(TEntity) ist fehlgeschlagen!", ex);
            }
        }
    }
}
