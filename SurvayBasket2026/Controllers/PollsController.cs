using Microsoft.AspNetCore.Authorization;
using SurvayBasket2026.Contracts.Poll;

namespace SurvayBasket2026.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PollsController(IPollService pollService) : ControllerBase
    {
        private readonly IPollService _pollService = pollService;

        [HttpGet]
        public async Task<IActionResult> GetAllPolls(CancellationToken cancellationToken)
        {
            var polls = await _pollService.GetAllPollsAsync(cancellationToken);
            return Ok(polls);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPoll(int id, CancellationToken cancellationToken)
        { 
            var poll = await _pollService.GetPollByIdAsync(id, cancellationToken);
           
            return poll is null ? NotFound() : Ok(poll);
        }

        [HttpPost]
        public async Task<IActionResult> createPoll([FromBody] PollRequest request, CancellationToken cancellationToken)
        {
            var createdPoll = await _pollService.CreatePollAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetPoll), new { id = createdPoll.Id }, createdPoll);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> update(int id, [FromBody] PollRequest request, CancellationToken cancellationToken)
        {
            var result = await _pollService.UpdatePollAsync(id, request, cancellationToken);
            return !result ? NotFound("this id isn't exist") : NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id, CancellationToken cancellationToken)
        {
            var result = await _pollService.DeletePollAsync(id, cancellationToken);

            return !result ? NotFound("this id isn't exist") : NoContent();

        }
    }
}
