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
            string NazwaKlubu;
            string MiastoKlubu;
            int pageNumber;
            int pageSize;
            if(request.szukanyKlub.clubName == null)
            {
                NazwaKlubu = "";
            }
            else
            {
                NazwaKlubu = request.szukanyKlub.clubName;
            }
            if (request.szukanyKlub.clubCity == null)
            {
                MiastoKlubu = "";
            }
            else
            {
                MiastoKlubu = request.szukanyKlub.clubCity;
            }
            if(request.szukanyKlub.PageNumber == null)
            {
                pageNumber = 0;
            }
            else
            {
                pageNumber = request.szukanyKlub.PageNumber;
            }
            if (request.szukanyKlub.PageSize == null)
            {
                pageSize = 0;
            }
            else
            {
                pageSize = request.szukanyKlub.PageSize;
            }

            var query = _dbContext.Clubs.Where(x => x.Name.Contains(NazwaKlubu) && x.City.Contains(MiastoKlubu));
            bool isDescending = request.szukanyKlub.sortDirection?.Equals("desc", StringComparison.OrdinalIgnoreCase) ?? false;
            query = (request.szukanyKlub.sortBy?.ToLower()) switch
            {
                "createdAt" => isDescending ? query.OrderByDescending(c => c.CreatedAt) : query.OrderBy(c => c.CreatedAt), // czemu nie OrderBy(CreatedAt) - bo dzieki tej krotkiej lambdzie szukamy wewnatrz obiektu a tak szukalibysmy zmiennej CreatedAt wewnatrz metody 
                "name" => isDescending ? query.OrderByDescending(c => c.Name) : query.OrderBy(c => c.Name),
                "city" => isDescending ? query.OrderByDescending(c => c.City) : query.OrderBy(c => c.City),
                _ => isDescending ? query.OrderByDescending(c => c.CreatedAt) : query.OrderBy(c => c.CreatedAt)
            };
            var totalItems = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new ClubDto(x.Name, x.City))
                .ToListAsync(cancellationToken);
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            if (request.szukanyKlub.PageNumber > totalPages)
            { 
                throw new KeyNotFoundException($"Strona {pageNumber} nie istnieje. Ostatnia strona to {totalPages}.");
            }
            return new Application.Common.PagedResult<ClubDto>(items, totalItems, pageNumber, pageSize);
        }
    }
}
