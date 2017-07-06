using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Models.UtilityViewModels
{
    public class PaginationViewModel<T> : IEnumerable<T>, IPaginable
        where T : class
    {
        public IEnumerable<T> Collection { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public int Count { get; set; }
        public int PageCount { get; set; }
        public List<Tuple<int, string>> Pages { get; set; }

        public PaginationViewModel(IEnumerable<T> collection,
            int skip = 0, int take = 0)
        {
            Count = collection.Count();
            Collection = collection
                .Skip(skip)
                .Take(take).ToList();
            Skip = skip;
            Take = take;
            PageCount = (int)Math.Ceiling((double)Count / (double)Take);
            var pages = new List<Tuple<int, string>>();
            for (int i = 1; i <= PageCount; i++)
            {
                string itemClass = ((i-1)* take == skip) ? "active" : "";
                pages.Add(new Tuple<int, string>(i, itemClass));
            }
            Pages = (PageCount > 1) ? pages : new List<Tuple<int, string>>();

        }

        public IEnumerator<T> GetEnumerator()
        {
            return Collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Collection.GetEnumerator();
        }
    }
}
