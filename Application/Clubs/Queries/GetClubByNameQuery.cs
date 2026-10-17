using Application.Clubs.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using System.Linq.Dynamic.Core;
using Application.Common;
namespace Application.Clubs.Queries
{
    public record GetClubByNameQuery(int Page, int PageSize, string clubName, string clubCity, string? SortBy, string? SortDirection) : IRequest<Application.Common.PagedResult<ClubDto>>
    {
    }
}
