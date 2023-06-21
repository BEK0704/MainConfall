using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.TagHelpers.Sorting
{
    public enum SortStatePart
    {
        
        LanguageAsc,
        LanguageDesc,
        ActiveAsc,
        ActiveDesc,
        TitleAsc,
        TitleDesc,
    }
    public class SortingPart
    {
        public SortStatePart LanguageSort { get; private set; }
        public SortStatePart ActiveSort { get; private set; }
        public SortStatePart TitleSort { get; private set; }
        public SortStatePart current { get; private set; }

        public SortingPart(SortStatePart sort)
        {
            LanguageSort = sort == SortStatePart.LanguageAsc ? SortStatePart.LanguageDesc : SortStatePart.LanguageAsc;
            ActiveSort = sort == SortStatePart.ActiveAsc ? SortStatePart.ActiveDesc : SortStatePart.ActiveAsc;
            TitleSort = sort == SortStatePart.TitleAsc ? SortStatePart.TitleDesc : SortStatePart.TitleAsc;
            current = sort;
        }
    }
}

