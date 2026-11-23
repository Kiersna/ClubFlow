using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Clubs.DTOs
{
    public class ClubDto
    {
        public ClubDto(string name, string city)
        {
            Name = name;
            City = city;
        }
        public ClubDto()
        {
            Name = "";
            City = "";
        }
        

        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;
    }
}
