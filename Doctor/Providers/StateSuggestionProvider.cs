using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Doctor.Model;
using AutoCompleteTextBox.Editors;
using Doctor.Service;

namespace Doctor.Providers
{
    public class StateSuggestionProvider : ISuggestionProvider
    {
        public IEnumerable<Symptom> ListOfStates { get; set; }

        public Symptom GetExactSuggestion(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter)) return null;


            var list = WebApiService.QuerySymptomList(filter, App._patieneInfo);

            return
                list
                    .FirstOrDefault(state => string.Equals(state.symptom, filter, StringComparison.CurrentCultureIgnoreCase));
        }

        public IEnumerable<Symptom> GetSuggestions(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter)) return null;
            System.Threading.Thread.Sleep(1000);

            var list = WebApiService.QuerySymptomList(filter, App._patieneInfo);





            return
                list
                    .Where(state => state.symptom.IndexOf(filter, StringComparison.CurrentCultureIgnoreCase) > -1)
                    .ToList();
        }

        IEnumerable ISuggestionProvider.GetSuggestions(string filter)
        {
            return GetSuggestions(filter);
        }

        public StateSuggestionProvider()
        {
            //var states = StateFactory.CreateStateList();
            //ListOfStates = states;
        }
    }
}