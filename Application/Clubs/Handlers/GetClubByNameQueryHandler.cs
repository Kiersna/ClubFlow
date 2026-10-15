using Application.Clubs.DTOs;
using Application.Clubs.Queries;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Text;
using Application.Common;
//using Infrastructure.Data; // niemozna bo clean architecture 

namespace Application.Clubs.Handlers
{
    public class GetClubByNameQueryHandler : IRequestHandler<GetClubByNameQuery, Application.Common.PagedResult<ClubDto>>
    {

        private readonly IApplicationDbContext _dbContext;
        public GetClubByNameQueryHandler(IApplicationDbContext appDbContext)
        {
            _dbContext= appDbContext;
        }

        public async Task<Application.Common.PagedResult<ClubDto>> Handle(GetClubByNameQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Clubs.Where(x => x.Name.Contains(request.clubName) && x.City.Contains(request.clubCity));
            var totalItems = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ClubDto(x.Name, x.City))
                .ToListAsync(cancellationToken);
            return new Application.Common.PagedResult<ClubDto>(items, totalItems, request.Page, request.PageSize);
        }
    }
}
