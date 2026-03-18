using System;
using System.Collections.Generic;
using System.Text;

namespace books_store_DAL.Entities
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        public DateTime CreateDate { get; set; }
    }
    public class BaseEntity: IBaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    }
}
