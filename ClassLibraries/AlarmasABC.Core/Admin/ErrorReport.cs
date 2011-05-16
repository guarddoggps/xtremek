using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmasABC.Core.Admin
{
    public class ErrorReport
    {
        public ErrorReport()
        { }

        private string _serviceName;

        public string ServiceName
        {
            get { return _serviceName; }
            set { _serviceName = value; }
        }
        private string _startTime;

        public string StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }
        private string _endTime;

        public string EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }
    }
}
