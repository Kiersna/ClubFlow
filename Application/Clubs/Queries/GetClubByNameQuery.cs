using Application.Clubs.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using System.Linq.Dynamic.Core;
using Application.Common;
using Domain.Models;
namespace Application.Clubs.Queries
{
    public record GetClubByNameQuery(ClubSearchDto szukanyKlub) : IRequest<Application.Common.PagedResult<ClubDto>>
    {
    }
}
