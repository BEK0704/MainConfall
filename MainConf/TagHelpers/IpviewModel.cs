using MainConf.Models;
using MainConf.Models.Writing;
using MainConf.TagHelpers.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.TagHelpers
{
    public class IpviewModel
    {
        public List<Ipalls> Ip { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public int countall { get; set; }
        public FilterIp FilterIp { get; set; }
    }
}
