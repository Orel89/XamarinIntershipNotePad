using MapNotepad.Model.Pin;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad.Services.SearchService
{
    public interface ISearchService
    {
        List<PinModel> Search(string search_query, IEnumerable<PinModel> list);
    }
}
