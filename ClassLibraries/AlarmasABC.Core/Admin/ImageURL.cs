using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmasABC.Core.Admin
{
    public class ImageURL
    {


        public IList<ImageURL> _dropDownList = new List<ImageURL>();

        public ImageURL()
        {

        }

        //public ImageURL(int Value,string Ftype)
        //{
        //    _id = Value; _fuelType = Ftype;
        //}

        //public int Value
        //{
        //    get { return _id; }
        //    set { _id = value; }
        //}
        //public string Ftype
        //{
        //    get { return _fuelType; }
        //    set { _fuelType = value; }
        //}

        #region instance variable and Properties

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _imageName;

        public string ImageName
        {
            get { return _imageName; }
            set { _imageName = value; }
        }

        private string _imageUrl;

        public string ImageUrl
        {
            get { return _imageUrl; }
            set { _imageUrl = value; }
        }
        private string _fUrl;

        public string FUrl
        {
            get { return _fUrl; }
            set { _fUrl = value; }
        }

        private int _comID;

        public int ComID
        {
          get { return _comID; }
          set { _comID = value; }
        }

        private int _isActive;

        public int IsActive
        {
          get { return _isActive; }
          set { _isActive = value; }
        }
        #endregion

    }
}

 
