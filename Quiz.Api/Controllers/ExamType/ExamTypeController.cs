using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuizApi.Models;
using QuizService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace QuizApi.Controllers.ExamType
{
    [Route("api/[controller]/[action]")]
    public class ExamTypeController : Controller
    {
        #region properties

        private readonly IExamTypeService _examTypeService;
        private readonly IMapper _mapper;

        #endregion

        #region ctor

        public ExamTypeController(IExamTypeService service, IMapper mapper)
        {
            _examTypeService = service;
            _mapper = mapper;
        }

        #endregion

        #region actions

        [HttpGet]
        [Produces("application/json")]
        [ActionName("GetAllExamTypes")]
        public async Task<JsonResult> GetAllExamTypes()
        {
            var examTypes = await _examTypeService.GetAllExamTypesAsync();

            if (examTypes != null && examTypes.Count > 0)
            {
                var examTypeDataList = _mapper.Map<List<QuizData.ExamType>, List<ExamTypeData>>(examTypes);
                return Json(examTypeDataList);
            }

            return new JsonResult(null);
        }

        [HttpGet("{examTypeID}")]
        [Produces("application/json")]
        [ActionName("GetExamTypeByID")]
        public async Task<JsonResult> GetExamTypeByID(int examTypeID)
        {
            var examType = await _examTypeService.GetExamTypeByIDAsync(examTypeID);

            if (examType != null)
            {
                var examTypeData = _mapper.Map<ExamTypeData>(examType);
                return Json(examTypeData);
            }

            return new JsonResult(null);
        }

        #endregion
    }
}
