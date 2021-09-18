using System;
using StockInvestments.API.Contracts;
using StockInvestments.API.DbContexts;

namespace StockInvestments.API.Services
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly StockInvestmentsContext _stockInvestmentsContext;

        protected RepositoryBase(StockInvestmentsContext context)
        {
            _stockInvestmentsContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _stockInvestmentsContext.Set<T>().Add(entity);
        }

        public void Update(T dbClosedPosition)
        {
            // no code in this implementation

        }

        public void Delete(T entity)
        {
            _stockInvestmentsContext.Set<T>().Remove(entity);
        }

        public bool Save()
        {
            return (_stockInvestmentsContext.SaveChanges() >= 0);
        }
    }
}
