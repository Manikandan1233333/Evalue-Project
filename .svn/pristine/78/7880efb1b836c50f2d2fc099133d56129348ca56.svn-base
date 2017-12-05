using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderClassesII
{
    public class SSRSReportRequest
    {
        public List<ReportDetail> ReportDetails { get; set; }
    }

    public class ReportParameter
    {

        public string Name { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }
    }


    public class ReportDetail
    {
        public ReportDetail()
        {
            Distributions = new List<DistributionDetail>();
            Parameters = new List<ReportParameter>();
        }

        public string ReportPath { get; set; }

        public string ReportDetailIdentifier { get; set; }

        public string ReportName { get; set; }

        public string ReportOffset { get; set; }

        public bool SkipForProcessing { get; set; }

        public List<DistributionDetail> Distributions { get; set; }

        public List<ReportParameter> Parameters { get; set; }
    }

    public class DistributionDetail
    {

        public string FileName { get; set; }

        public string OutputFileType { get; set; }
    }
}
