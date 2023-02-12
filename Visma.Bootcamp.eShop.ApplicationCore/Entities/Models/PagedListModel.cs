using System.Collections.Generic;

namespace Visma.Bootcamp.eShop.ApplicationCore.Entities.Models
{
    public class PagedListModel<T>
    {
        public PagedListModel(List<T> items, int pageSize)
        {
            Items = items;
            PageSize = pageSize;
        }

        public List<T> Items { get; }
        public int PageSize { get; }
    }
}