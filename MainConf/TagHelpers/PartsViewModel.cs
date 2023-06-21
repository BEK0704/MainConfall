using MainConf.Models;
using MainConf.Models.Seconds;
using MainConf.TagHelpers.Filtering;
using MainConf.TagHelpers.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.TagHelpers
{
    public class PartsViewModel
    {
        public List<Questions> Questionstab { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public int countall { get; set; }
        public FilteringPart FilteringPart { get; set; }
        public SortingPart SortingPart { get; set; }
    }
}
