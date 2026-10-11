using Application.Clubs.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
namespace Application.Clubs.Queries
{
    public record GetClubByNameQuery(string clubName, string clubCity) : IRequest<ClubDto>
    {
    }
}
