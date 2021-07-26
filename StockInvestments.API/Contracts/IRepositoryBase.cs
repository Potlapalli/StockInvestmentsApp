namespace StockInvestments.API.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepositoryBase<T> where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool Save();
    }
}
