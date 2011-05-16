using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmasABC.Core.Admin
{
    public class Fuel
    {
        public IList<Fuel> _dropDownList = new List<Fuel>();

        public Fuel()
        {

        }

        public Fuel(int Value,string Ftype)
        {
            _id = Value; _fuelType = Ftype;
        }

        public int Value
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Ftype
        {
            get { return _fuelType; }
            set { _fuelType = value; }
        }

        #region instance variable and Properties

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _fuelType;

        public string FuelType
        {
            get { return _fuelType; }
            set { _fuelType = value; }
        }
        #endregion

    }
}
//        id	int	Unchecked
//fuelType	nvarchar(50)	Checked
//        Unchecked