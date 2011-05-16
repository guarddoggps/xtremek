using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmasABC.Core.Admin
{
    public class UnitModel
    {
        /// <summary>
        /// 
        /// </summary>

        public IList<UnitModel> _dropDownList = new List<UnitModel>();

        public UnitModel()
        {

        }

        public UnitModel(int value, string Name)
        {
            _unitModelID = value;
            _unitModels = Name;
        }

        #region Instance variable and properties

        private int _unitModelID;
        public int UnitModelID
        {
            get { return _unitModelID; }
            set { _unitModelID = value; }
        }

        private string _unitModels;
        public string UnitModels
        {
            get { return _unitModels; }
            set { _unitModels = value; }
        }

        private int _comID;
        public int ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        #endregion

        #region for DropdownList section
        public int value
        {
            get { return _unitModelID; }
            set { _unitModelID = value; }
        }

        public string Name
        {
            get { return _unitModels; }
            set { _unitModels = value; }
        }
        #endregion

    }
}

