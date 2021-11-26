using MapNotepad.Model.Pin;
using MapNotepad.Services.PinService;
using System;
using System.Collections.Generic;
using System.Linq;

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
            List<PinModel> result = new List<PinModel>();

            var doubleResult = double.TryParse(search_query, out double doubleValue);

            if (doubleResult)
            {
                var searchResult = DoubleSearch(doubleValue, list);
                foreach (PinModel pinModel in searchResult)
                {
                    result.Add(pinModel);
                }
            }
            else
            {
                var searchResultLabel = list.Where(x => x.Label.ToLower().Contains(search_query.ToLower()));
                
                foreach (PinModel pinModel in searchResultLabel)
                {
                    result.Add(pinModel);
                }
                if (result.Count == 0)
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
                                    result.Add(pinModel);
                                    break;
                                }

                            }
                        }
                    }
                }
            }
            return result;
        }

        #endregion

        #region -- Private helpers --
        private IEnumerable<PinModel> DoubleSearch(double coordinate, IEnumerable<PinModel> list)
        {
            double accuracy = 0.4; 

            return list.Where(x => Math.Abs(x.Latitude - coordinate) <= accuracy || Math.Abs(x.Longitude - coordinate) <= accuracy);
        }

        #endregion
    }
}
