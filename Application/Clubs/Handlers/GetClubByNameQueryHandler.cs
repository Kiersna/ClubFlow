using Application.Clubs.DTOs;
using Application.Clubs.Queries;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
//using Infrastructure.Data; // niemozna bo clean architecture 

namespace Application.Clubs.Handlers
{
    public class GetClubByNameQueryHandler : IRequestHandler<GetClubByNameQuery, ClubDto>
    {

        private readonly IApplicationDbContext _dbContext;
        public GetClubByNameQueryHandler(IApplicationDbContext appDbContext)
        {
            _dbContext= appDbContext;
        }

        public async Task<ClubDto> Handle(GetClubByNameQuery request, CancellationToken cancellationToken)
        {
            var club = await _dbContext.Clubs.Where(x => x.Name == request.clubName && x.City == request.clubCity).Select(x => new ClubDto(x.Name, x.City)).FirstOrDefaultAsync(cancellationToken);
            return club ?? throw new InvalidOperationException("Club not found");
        }
    }
}
