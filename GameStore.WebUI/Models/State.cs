using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace GameStore.WebUI.Models
{
    public class State
    {
        public string Key { get; set; }
        public string Name { get; set; }

        public static Collection<State> List()
        {
            Collection<State> list = new Collection<State>();
            list.Add(new State() { Key = "AL", Name = "Alabama" });
            list.Add(new State() { Key = "AK", Name = "Alaska" });
            list.Add(new State() { Key = "AZ", Name = "Arizona" });
            list.Add(new State() { Key = "AR", Name = "Arkansas" });
            list.Add(new State() { Key = "CA", Name = "California" });
            list.Add(new State() { Key = "CO", Name = "Colorado" });
            list.Add(new State() { Key = "CT", Name = "Connecticut" });
            list.Add(new State() { Key = "DE", Name = "Delaware" });
            list.Add(new State() { Key = "FL", Name = "Florida" });
            list.Add(new State() { Key = "GA", Name = "Georgia" });
            list.Add(new State() { Key = "HI", Name = "Hawaii" });
            list.Add(new State() { Key = "ID", Name = "Idaho" });
            list.Add(new State() { Key = "IL", Name = "Illinois" });
            list.Add(new State() { Key = "IN", Name = "Indiana" });
            list.Add(new State() { Key = "IA", Name = "Iowa" });
            list.Add(new State() { Key = "KS", Name = "Kansas" });
            list.Add(new State() { Key = "KY", Name = "Kentucky" });
            list.Add(new State() { Key = "LA", Name = "Louisiana" });
            list.Add(new State() { Key = "ME", Name = "Maine" });
            list.Add(new State() { Key = "MD", Name = "Maryland" });
            list.Add(new State() { Key = "MA", Name = "Massachusetts" });
            list.Add(new State() { Key = "MI", Name = "Michigan" });
            list.Add(new State() { Key = "MN", Name = "Minnesota" });
            list.Add(new State() { Key = "MS", Name = "Mississippi" });
            list.Add(new State() { Key = "MO", Name = "Missouri" });
            list.Add(new State() { Key = "MT", Name = "Montana" });
            list.Add(new State() { Key = "NE", Name = "Nebraska" });
            list.Add(new State() { Key = "NV", Name = "Nevada" });
            list.Add(new State() { Key = "NH", Name = "New Hampshire" });
            list.Add(new State() { Key = "NJ", Name = "New Jersey" });
            list.Add(new State() { Key = "NM", Name = "New Mexico" });
            list.Add(new State() { Key = "NY", Name = "New York" });
            list.Add(new State() { Key = "NC", Name = "North Carolina" });
            list.Add(new State() { Key = "ND", Name = "North Dakota" });
            list.Add(new State() { Key = "OH", Name = "Ohio" });
            list.Add(new State() { Key = "OK", Name = "Oklahoma" });
            list.Add(new State() { Key = "OR", Name = "Oregon" });
            list.Add(new State() { Key = "PA", Name = "Pennsylvania" });
            list.Add(new State() { Key = "RI", Name = "Rhode Island" });
            list.Add(new State() { Key = "SC", Name = "South Carolina" });
            list.Add(new State() { Key = "SD", Name = "South Dakota" });
            list.Add(new State() { Key = "TN", Name = "Tennessee" });
            list.Add(new State() { Key = "TX", Name = "Texas" });
            list.Add(new State() { Key = "UT", Name = "Utah" });
            list.Add(new State() { Key = "VT", Name = "Vermont" });
            list.Add(new State() { Key = "VA", Name = "Virginia" });
            list.Add(new State() { Key = "WA", Name = "Washington" });
            list.Add(new State() { Key = "WV", Name = "West Virginia" });
            list.Add(new State() { Key = "WI", Name = "Wisconsin" });
            list.Add(new State() { Key = "WY", Name = "Wyoming" });

            return list;
        }
    }
}