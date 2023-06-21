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
    public class ExpertViewModel
    {
        public List<ExpertView> ExpertViewstab { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public int countall { get; set; }
        public FilterExperts FilterExpert { get; set; }
        public SortingExpert SortingExpert { get; set; }
    }
}
