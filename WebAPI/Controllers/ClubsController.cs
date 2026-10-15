using Application.Clubs.DTOs;
using Application.Clubs.Queries;
using Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/clubs")]
    public class ClubsController : ControllerBase
    {
        private readonly ISender _mediator; 
        //private readonly ApplicationDbContext _context; // chwilowo do testu 

        public ClubsController(ISender mediator)//, ApplicationDbContext context)
        {
            _mediator = mediator;
            //_context = context;
        }
        [HttpGet] 
        public async Task<ActionResult<IEnumerable<ClubDto>>> GetClubByName([FromQuery] string clubName, [FromQuery] string clubCity, CancellationToken cancellationToken)
        {
            var query = new GetClubByNameQuery(1, 10, clubName, clubCity);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
        //[HttpGet("getall")]
        //public async Task<ActionResult<IEnumerable<ClubDto>>> GetAll(CancellationToken cancellationToken)
        //{
        //    // Bezpośrednie zapytanie do bazy z pominięciem MediatR i handlera
        //    var clubs = await _context.Clubs
        //        .ToListAsync(cancellationToken);

        //    return Ok(clubs);
        //}
    }
}
