using BoeSj.KnowledgeBase.Application.DTO.Input;
using BoeSj.KnowledgeBase.Application.DTO.Output;
using BoeSj.KnowledgeBase.Domain.Model.Response;
using BoeSj.KnowledgeBase.Domain.Model.Search;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoeSj.KnowledgeBase.Application.Interface
{
    public interface ISearchApp
    {
        //Task<Response<List<post>>> SearchPost(SearchInput search);
        Task<Response<List<post>>> SearchPost(SearchInput search);


        Task<Response<List<post>>> SearchPostbyField(SearchInput search);


        Task<Response<IEnumerable<SearchOutput>>> IndexSuggestion(SearchInput search);


        Task<Response<List<post>>> HotSuggest(SearchInput search);

        Task<Response<List<string>>> TopTags();
    }
}
