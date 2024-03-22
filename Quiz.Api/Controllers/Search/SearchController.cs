using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuizService;


namespace QuizApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class SearchController : Controller
    {
        #region properties
        
        private readonly IMapper _mapper;
        private readonly ISearchService<QuizData.Quiz> _searchService;
        #endregion

        #region ctor
        
        public SearchController(IMapper mapper, ISearchService<QuizData.Quiz> searchService)
        {
            _searchService = searchService;
            _mapper = mapper;
        }
        
        #endregion
        
        #region methods
        
        public async Task<IActionResult> Find(string query)
        {
            var response = await _searchService.SearchByQery(query);
            var docs = response.Documents.ToList();
            return new JsonResult(docs);
        }

        
        #endregion
    }
}