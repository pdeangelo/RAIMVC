using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAI_MVC.Models
{
    public class MiscValue
    {
        public int MiscValueID { get; set; }
        public string MiscValueName { get; set; }
        public MiscValue(int miscValueId, string miscValueName)
        {
            MiscValueID = miscValueId;
            MiscValueName = miscValueName;
        }
    }
}