using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmasABC.Core.Security
{
    public class SecurityScheme
    {
        public IList<SecurityScheme> _schemeList = new List<SecurityScheme>();

        public SecurityScheme()
        {
            ///Default Constructor
        }

        public SecurityScheme(int value, string name)
        {
            _ID = value;
            _schemeName = name;
        }

        public int value
        {
            get { return _ID; }
            set { _ID = value; }
        }
        
        public string name
        {
            get { return _schemeName; }
            set { _schemeName = value; }
        }

        #region Instance Variables and Properties
        
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _schemeName;

        public string SchemeName
        {
            get { return _schemeName; }
            set { _schemeName = value; }
        }

        private int _comID;

        public int ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }

        private bool _defaultScheme;

        public bool DefaultScheme
        {
            get { return _defaultScheme; }
            set { _defaultScheme = value; }
        }

        #endregion

    }
}
