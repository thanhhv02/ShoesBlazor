using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.ViewModels.Report
{
    public class ChartModelItem
    {

        public DateTime Date { get; set; }
        public decimal Count { get; set; }
    }

    public class ChartModel
    {
        public List<ChartModelItem> ChartNew { get; set; }
        public List<ChartModelItem> ChartOld { get; set; }
        public ChartModel() { ChartNew = new(); ChartOld = new(); }
    }
}
