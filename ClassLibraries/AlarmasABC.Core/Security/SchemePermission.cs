using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmasABC.Core.Security
{
   public class SchemePermission
    {
       
       public SchemePermission()
       {

       }

        #region instance variables and Properties

       private int _ID;

       public int ID
       {
           get { return _ID; }
           set { _ID = value; }
       }
       private int _schemeID;

       public int SchemeID
       {
           get { return _schemeID; }
           set { _schemeID = value; }
       }
       private int _comID;

       public int ComID
       {
           get { return _comID; }
           set { _comID = value; }
       }
       private int _formID;

       public int FormID
       {
           get { return _formID; }
           set { _formID = value; }
       }
       private bool _fullAccess;

       public bool FullAccess
       {
           get { return _fullAccess; }
           set { _fullAccess = value; }
       }
       private bool _delete;

       public bool Delete
       {
           get { return _delete; }
           set { _delete = value; }
       }
       private bool _view;

       public bool View
       {
           get { return _view; }
           set { _view = value; }
       }
       private bool _insert;

       public bool Insert
       {
           get { return _insert; }
           set { _insert = value; }
       }
       private bool _Edit;

       public bool Edit
       {
           get { return _Edit; }
           set { _Edit = value; }
       }

        #endregion

    }
}
