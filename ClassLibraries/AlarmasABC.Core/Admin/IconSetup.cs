using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmasABC.Core.Admin
{
    public class IconSetup
    {
        public IconSetup()
        {
 
        }
        public IList<IconSetup> loadIcon = new List<IconSetup>();
        
        public IconSetup(string IName,int CID)
        {
            this._iconName = IName; this._comID = CID;
        }

        public IconSetup(int IconID,string Name, string IconPath)
        {
            _id = IconID; _iconName = Name; _icon = IconPath;
        }
        public int IconID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _iconName; }
            set { _iconName = value; }
        }
        public string IconPath
        {
            get { return _icon; }
            set { _icon = value; }
        }

        #region instance variable and Properties
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _iconName;

        public string IconName
        {
            get { return _iconName; }
            set { _iconName = value; }
        }

        private string _icon;

        public string Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }

        private int _comID;

        public int ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }


        #endregion
    }
}
//id	int	Unchecked
//iconName	nvarchar(50)	Checked
//comID	int	Unchecked
//        Unchecked