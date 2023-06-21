using MainConf.Models.Main;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.TagHelpers.Filtering
{
    public class FilterIp
    {
        public FilterIp(List<Viloyat> locations, int? location1, string name)
        {
          
            // устанавливаем начальный элемент, который позволит выбрать всех
            locations.Insert(0, new Viloyat { Names_V = "Barchasi", Id_viloyat = 0 });
            Locations1 = new SelectList(locations, "Id_viloyat", "Names_V", location1);
            SelectedLocation = location1;
            SelectedName = name;
        }
        public SelectList Locations1 { get; private set; } // список компаний
        public int? SelectedLocation { get; private set; }   // выбранная компания
        public string SelectedName { get; private set; }    // введенное имя
    }
}
