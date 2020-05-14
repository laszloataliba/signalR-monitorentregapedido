﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OrderDeliveryMonitor.Facade.Default
{
    public interface IFDefault<T>
    {
        void Create(T pEntity);
        T Get(Expression<Func<T, bool>> pWhereClause, Expression<Func<T, object>> pInclude = null);
        void Update(T pEntity);
        void Delete(T pEntity);
        IEnumerable<T> GetList(Expression<Func<T, bool>> pWhereClause = null, Expression<Func<T, object>> pInclude = null);
    }
}
