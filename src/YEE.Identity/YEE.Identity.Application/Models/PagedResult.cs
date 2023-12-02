using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEE.Identity.Application.Models
{
    public class PagedResult<TEntity>
    {
        public IEnumerable<TEntity> Items { get; set; }
        public int TotalCount { get; set; }
        public PagedResult(IEnumerable<TEntity> items, int totalCount)
        {
            Items = items;
            TotalCount = totalCount;
        }
    }
}
