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
    public class CandidatesViewModel
    {
        public List<CandidView> CandidViewstab { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public int countall { get; set; }
        public FilterCandidetes FilterCandidetes { get; set; }
        public SortingCandidetes SortingCandidetes { get; set; }
    }
}
