using System.Collections;
using System.Collections.Generic;

namespace repack.Entities
{
    public class SearchResult<T>
    {
        public int Page { get; set; }
        public int DataCount { get; set; }
        public IEnumerable<T> Data { get; set; }
        public int MaxPage => (DataCount - 1) / DataPerPage + 1;
        public int StartPage => Page - PageCount / 2 > 0 ? Page - PageCount / 2 : 1;

        public int EndPage => Page + PageCount / 2 > MaxPage ? MaxPage : Page + PageCount / 2;

        public int PageCount { get; set; } = 5;
        public int DataPerPage { get; set; } = 100;
    }
}