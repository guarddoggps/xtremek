using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmasABC.Core.Fleet
{
    public  class Supplies
    {
        public Supplies()
        { 
        }
        /*
         @supplies	varchar(50),
	@comID		int,
	@Quantity	int,
	@cost		decimal(13,3),
	@unit		varchar(30),
	@retVal		int out
         */

        private int _iD;

        public int ID
        {
            get { return _iD; }
            set { _iD = value; }
        }

        private string _supplies;
        public string SupplieS
        {
            get { return _supplies; }
            set { _supplies = value; }
        }

        private int _comID;

        public int ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }
        private int _Quantity;

        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        private double _cost;

        public double Cost
        {
            get { return _cost; }
            set { _cost = value; }
        }
        private string _unit;

        public string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }
        private int retVal;

        public int RetVal
        {
            get { return retVal; }
            set { retVal = value; }
        }
        
    }
}
