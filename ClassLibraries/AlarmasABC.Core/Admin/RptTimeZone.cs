using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmasABC.Core.Admin
{
    public class RptTimeZone
    {

        public IList<RptTimeZone> _dropDownList=new List<RptTimeZone>();

        public RptTimeZone()
        { 
            /// Default Constructor
        }

        public RptTimeZone(float Value, string Name)
        {
            _tzValue = Value; _timeZone = Name;
        }
        
        public float Value
        {
            get { return _tzValue; }
            set { _tzValue = value; }
        }

        
        public string Name
        {
            get { return _timeZone; }
            set { _timeZone = value; }
        }

        private string _timeZone;

        public string TimeZone
        {
            get { return _timeZone; }
            set { _timeZone = value; }
        }


        private float _tzValue;

        public float TzValue
        {
            get { return _tzValue; }
            set { _tzValue = value; }
        }
        
        
    }
}
