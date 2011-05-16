using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmasABC.Core.Fleet
{
    public  class SuppliesPerPattern
    {
        public SuppliesPerPattern()
        { 
        }
        /*
         patternID, int,>
           ,<suppliesID, int,>
           ,<Quantity, int,>
           ,<kmInterval, int,>
           ,<daysInterval, int,>)
         */
        private int _patternID;

        public int PatternID
        {
            get { return _patternID; }
            set { _patternID = value; }
        }
        private int _suppliesID;

        public int SuppliesID
        {
            get { return _suppliesID; }
            set { _suppliesID = value; }
        }
        private int _qyantity;

        public int Qyantity
        {
            get { return _qyantity; }
            set { _qyantity = value; }
        }
        private int _kmInterval;

        public int KmInterval
        {
            get { return _kmInterval; }
            set { _kmInterval = value; }
        }
        private int _daysInterval;
                
        public int DaysInterval
        {
            get { return _daysInterval; }
            set { _daysInterval = value; }
        }
       
        
    }
}
