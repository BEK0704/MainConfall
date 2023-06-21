using MainConf.Models.Main;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.TagHelpers.Filtering
{
    public class FilteringPart
    {
        public FilteringPart( string name, List<Levels> levels, int? levels1)
        {
            levels.Insert(0, new Levels { Names_level = "Barchasi", Id_level = 0 });
            Levels1 = new SelectList(levels, "Id_level", "Names_level", levels1);
            SelectedLevel = levels1;            
            SelectedName = name;
        }
        public SelectList Levels1 { get; private set; }
        public int? SelectedLevel { get; private set; }
        public string SelectedName { get; private set; }    // введенное имя
    }
}

