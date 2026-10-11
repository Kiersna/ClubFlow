using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Club> Clubs { get; }
    }
}
