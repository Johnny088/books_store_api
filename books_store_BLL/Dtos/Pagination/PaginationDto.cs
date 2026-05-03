using System;
using System.Collections.Generic;
using System.Text;

namespace books_store_BLL.Dtos.Pagination
{
    public class PaginationDto
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
