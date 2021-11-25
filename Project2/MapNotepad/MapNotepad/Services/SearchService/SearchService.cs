using MapNotepad.Model.Pin;
using MapNotepad.Services.PinService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapNotepad.Services.SearchService
{
    public class SearchService : ISearchService
    {
        private readonly IPinService _pinService;

        public SearchService(IPinService pinService)
        {
            _pinService = pinService;
        }

        #region -- SearchService implementation --

        public List<PinModel> Search(string search_query, IEnumerable<PinModel> list)
        {
            List<PinModel> outValue = new List<PinModel>();

            var doubleResult = double.TryParse(search_query, out double doubleValue);

            if (doubleResult)
            {
                var searchResult = DoubleSearch(doubleValue, list);
                foreach (PinModel pinModel in searchResult)
                {
                    outValue.Add(pinModel);
                }
            }
            else
            {
                var searchResultLabel = list.Where(x => x.Label.ToLower().Contains(search_query.ToLower()));
                
                foreach (PinModel pinModel in searchResultLabel)
                {
                    outValue.Add(pinModel);
                }
                if (outValue.Count == 0)
                {
                    foreach (var pinModel in list)
                    {
                        var keywords = pinModel.Description?.Split(new Char[] { ' ', ',', '.', ':', ';', '!', '?', '\t' });
                        if (keywords != null)
                        {
                            foreach (string keyword in keywords)
                            {
                                if (keyword.ToLower() == search_query.ToLower())
                                {
                                    outValue.Add(pinModel);
                                    break;
                                }

                            }
                        }

                    }
                }
            }
            return outValue;
        }
        #endregion

        #region -- Private helpers --
        private IEnumerable<PinModel> DoubleSearch(double coordinate, IEnumerable<PinModel> list)
        {
            double delta = 0.1; 

            return list.Where(x => Math.Abs(x.Latitude - coordinate) <= delta || Math.Abs(x.Longitude - coordinate) <= delta);
        }
        #endregion
    }
}
