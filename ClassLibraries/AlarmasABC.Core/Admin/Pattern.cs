using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmasABC.Core.Admin
{
    public class Pattern
    {

        public IList<Pattern> _dropDownList = new List<Pattern>();

        public Pattern()
        {
 
        }
        public Pattern(int Value, string Name)
        {
            _id = Value; _patternName = Name;
        }

        public int Value
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Name
        {
            get { return _patternName; }
            set { _patternName = value; }
        }

        #region instance variable and Properties
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _comID;

        public int ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }

        private bool _isDelete;

        public bool IsDelete
        {
            get { return _isDelete; }
            set { _isDelete = value; }
        }

        private string _patternName;

        public string PatternName
        {
            get { return _patternName; }
            set { _patternName = value; }
        }
        #endregion
    }
}

