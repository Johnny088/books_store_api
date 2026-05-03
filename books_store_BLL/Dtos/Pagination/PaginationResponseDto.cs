using System;
using System.Collections.Generic;
using System.Text;


namespace books_store_BLL.Dtos.Pagination
{
    public class PaginationResponseDto<T>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public int PageCount { get; set; } = 1;
        public int TotalCount { get; set; } = 0;
        public IEnumerable<T> Data { get; set; } = [];
    }
}
