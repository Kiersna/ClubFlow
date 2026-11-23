using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Clubs.DTOs
{
    public class ClubSearchDto : PaginationParams
    {
        public string? clubName;
        public string? clubCity;
        public string? sortBy;
        public string? sortDirection;
    }
}
