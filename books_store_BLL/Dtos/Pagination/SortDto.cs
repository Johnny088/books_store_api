using System;
using System.Collections.Generic;
using System.Text;

namespace books_store_BLL.Dtos.Pagination
{
    public class SortDto
    {

        public string SortBy { get; set; } = "Id";
        public bool Desc { get; set; } = false;

    }

}
