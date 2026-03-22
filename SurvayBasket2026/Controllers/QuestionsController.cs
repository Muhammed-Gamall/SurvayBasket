using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace SurvayBasket2026.Controllers
{
    [Route("api/Poll/{pollId}/[controller]")]
    [ApiController]
    public class QuestionsController(IQuestionService questionService) : ControllerBase
    {
        private readonly IQuestionService _questionService = questionService;


        [HttpGet("{questionId}")]
        public async Task<IActionResult> GetById([FromRoute] int pollId, int questionId, CancellationToken cancellation = default)
        {

            var question = await _questionService.GetQuestionAsync(pollId, questionId, cancellation);
            return question is null ? BadRequest("Poll Not Found") : Ok(question);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll([FromRoute] int pollId, CancellationToken cancellation = default)
        {

            var question = await _questionService.GetAllQuestionAsync(pollId, cancellation);

            return question is null ? BadRequest("Poll Not Found") : Ok(question);
        }
        [Authorize]
        [HttpPost("")]
        public async Task<IActionResult> CreateQuestion([FromRoute] int pollId, [FromBody] QuestionRequest request, CancellationToken cancellation = default)
        {
            var result = await _questionService.CreateQuestionAsync(pollId, request, cancellation);

            return result is null ? BadRequest("Poll Not Found or Question Already Exist") : Ok(result);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion([FromRoute] int pollId, int id, [FromBody] QuestionRequest request, CancellationToken cancellation = default)
        {
            var result = await _questionService.UpdateAsync(pollId, id, request, cancellation);
            return result is true ? Ok() : BadRequest("Poll or Question Not Found");
        }
        [Authorize]
        [HttpPut("{id}/Toggle")]
        public async Task<IActionResult> ToggleQuestion([FromRoute] int pollId, int id, CancellationToken cancellation = default)
        {
            var result = await _questionService.ToggleAsync(pollId, id, cancellation);
            return result is true ? Ok() : BadRequest("Poll or Question Not Found");
        }
    }
}
