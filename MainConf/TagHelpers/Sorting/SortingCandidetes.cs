using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.TagHelpers.Sorting
{
    public enum SortStateCan { 
    RegionAsc,
    RegionDesc,
    LanguageAsc,
    LanguageDesc,
    FioAsc,
    FioDesc,
    PnflAsc,
    PnflDesc,
    }
    public class SortingCandidetes
    {
        public SortStateCan RegionSort { get; private set; }
        public SortStateCan LanguageSort { get; private set; }
        public SortStateCan FioSort { get; private set; }
        public SortStateCan PnflSort { get; private set; }
        public SortStateCan current { get; private set; }

        public SortingCandidetes(SortStateCan sort) {
            RegionSort = sort == SortStateCan.RegionAsc ? SortStateCan.RegionDesc : SortStateCan.RegionAsc;
            LanguageSort = sort == SortStateCan.LanguageAsc ? SortStateCan.LanguageDesc : SortStateCan.LanguageAsc;
            FioSort = sort == SortStateCan.FioAsc ? SortStateCan.FioDesc : SortStateCan.FioAsc;
            PnflSort = sort == SortStateCan.PnflAsc ? SortStateCan.PnflDesc : SortStateCan.PnflAsc;
            current = sort;
        }
    }
}
