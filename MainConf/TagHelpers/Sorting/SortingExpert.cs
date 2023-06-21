using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.TagHelpers.Sorting
{
    public enum SortStateExp
    {
        
        LanguageAsc,
        LanguageDesc,
        FioAsc,
        FioDesc,
        PnflAsc,
        PnflDesc,
    }
    public class SortingExpert
    {
        public SortStateExp LanguageSort { get; private set; }
        public SortStateExp FioSort { get; private set; }
        public SortStateExp PnflSort { get; private set; }
        public SortStateExp current { get; private set; }

        public SortingExpert(SortStateExp sort)
        {
            LanguageSort = sort == SortStateExp.LanguageAsc ? SortStateExp.LanguageDesc : SortStateExp.LanguageAsc;
            FioSort = sort == SortStateExp.FioAsc ? SortStateExp.FioDesc : SortStateExp.FioAsc;
            PnflSort = sort == SortStateExp.PnflAsc ? SortStateExp.PnflDesc : SortStateExp.PnflAsc;
            current = sort;
        }

    }
}
