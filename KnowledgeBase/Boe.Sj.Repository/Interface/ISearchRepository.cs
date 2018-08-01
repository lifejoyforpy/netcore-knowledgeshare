﻿using Nest;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BoeSj.KnowledgeBase.Repository.Interface
{

 

    public  interface ISearchRepository
    {
        //Task<Tuple<long, List<TEntity>>> GetListAsync(Expression<Func<TEntity, bool>> filterExp = null,
        //  Expression<Func<TEntity, object>> includeFieldExp = null,
        //  Expression<Func<TEntity, object>> sortExp = null, SortOrder sortType = SortOrder.Ascending
        // , int limit = 10, int skip = 0);

        //  Task<Tuple<long, List<TEntity>>> GetListAsync(Func<QueryContainerDescriptor<TEntity>, QueryContainer> filterFunc = null,
        //      Func<SourceFilterDescriptor<TEntity>, ISourceFilter> includeFieldFunc = null,
        //      Expression<Func<TEntity, object>> sortExp = null, SortOrder sortType = SortOrder.Ascending
        //     , int limit = 10, int skip = 0);

        Task<ISearchResponse<T>> SearchSuggest<T>(Func<SearchDescriptor<T>, ISearchRequest> searchRequest) where T : class;
        
        Task<Tuple<long, List<T>>> Search<T>(Func<SearchDescriptor<T>, ISearchRequest> searchRequest) where T: class;
    }
}
