using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class PaginationParams
    {
        private const int MaxPageSize = 50;

        private int _pageNumber = 1;
        public int PageNumber
        {
            get => _pageNumber;
            init => _pageNumber = value < 1 ? 1 : value;
        }

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            init => _pageSize = value switch
            {
                < 1 => 10,
                > MaxPageSize => MaxPageSize,
                _ => value
            };
        }
    }
}
