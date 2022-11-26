using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.ViewModels.DashBoard
{

    public enum ReportType
    {
        All,
        Order,
        Income,
        Customer
    }
    public enum ReportDateType
    {
        Day,
        Month,
        Year
    }
    public class ReportSearchModel
    {
        public ReportType ReportType { get; set; }
        public ReportDateType ReportDate { get; set; }  
    }
    public class ReportModel
    {
        public ReportDateType DateType { get; set; }
        public ReportType Type { get; set; }
        public string Count  { get; set; }
        public string Percent { get; set; }

        public bool IsDecrease { get; set; }
        public ReportModel()
        {
            Count = "0";
            Percent = "0";
            IsDecrease = false;
        }
    }
}
