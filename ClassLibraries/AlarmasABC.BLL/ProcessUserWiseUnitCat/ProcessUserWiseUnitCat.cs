using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AlarmasABC.DAL.Insert;
using AlarmasABC.DAL.Select;
using AlarmasABC.DAL.Delete;
using AlarmasABC.DAL.Update;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.BLL.ProcessUserWiseUnitCat
{
    public class ProcessUserWiseUnitCat:IAlopekBusinessLogic
    {

        private InvokeOperations.operations _mode;
        public ProcessUserWiseUnitCat()
        {
 
        }
        public ProcessUserWiseUnitCat(InvokeOperations.operations _mode)
        {
            this._mode = _mode;

        }

        #region Private Variables and properties

        private int _groupID;

        public int GroupID
        {
            get { return _groupID; }
            set { _groupID = value; }
        }
        private int _comID;

        public int ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }
        private int _userID;

        public int UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        private int _unitGroupID;

        public int UnitGroupID
        {
            get { return _unitGroupID; }
            set { _unitGroupID = value; }
        }




        #endregion


        public void invoke()
        {
            switch (this._mode)
            {
                case InvokeOperations.operations.INSERT:
                    insertUserWiseUnitCat();
                    break;
                case InvokeOperations.operations.SELECT:
                    break;
                case InvokeOperations.operations.UPDATE:
                    insertUnitGroup();
                    break;
                case InvokeOperations.operations.DELETE:
                    deleteUserWiseUnitCat();
                    break;
                default:
                    break;
            }
        }

        private void deleteUserWiseUnitCat()
        {
            UserWiseUnitCatDelete _userUnitWise = new UserWiseUnitCatDelete();
            try
            {
                _userUnitWise.UserID = this.UserID;
                _userUnitWise.invoke();
            }
            catch (Exception ex)
            {
                throw new Exception("BLL::ProcessUserWiseUnitCatDelete::Invoke" + ex.Message);
            }
            finally
            {
                _userUnitWise = null;
            }
        }

        private void insertUserWiseUnitCat()
        {
            UserWiseUnitCatInsert _userWiseUnitCat = new UserWiseUnitCatInsert();
            try
            {
                _userWiseUnitCat.ComID = this.ComID;
                _userWiseUnitCat.UserID = this.UserID;
                _userWiseUnitCat.UnitGroupID = this.UnitGroupID;
                _userWiseUnitCat.invoke();
            }
            catch (Exception ex)
            {
                throw new Exception("BLL::ProcessUserWiseUnitCatInsert::Invoke" + ex.Message);
            }
            finally
            {
                _userWiseUnitCat = null;
            }
 
        }

        private void insertUnitGroup()
        {
            UnitGroupInsert _unitGrouIns = new UnitGroupInsert();
            try
            {
                _unitGrouIns.GroupID = this.GroupID;
                _unitGrouIns.UserID = this.UserID;
                _unitGrouIns.ComID = this.ComID;
                _unitGrouIns.invoke();
            }
            catch (Exception ex)
            {
                throw new Exception("BLL::ProcessUserWiseUnitCatInsert::Invoke" + ex.Message);
            }
            finally
            {
                _unitGrouIns = null;
            }
 
        }

        public void fillListedUnits(System.Web.UI.WebControls.ListBox lstbx)
        {
            UnitAndUnitGroupSelect _unitNGroup = new UnitAndUnitGroupSelect();
            UserGroup ug = new UserGroup();
            try
            {
                _unitNGroup.UserID = this._userID;
                _unitNGroup.ComID = this.ComID;
                _unitNGroup.LoadListedUnits(ug._dropDownList);
               
                lstbx.DataSource = ug._dropDownList;
                lstbx.DataTextField = "Name";
                lstbx.DataValueField = "Value";
                lstbx.DataBind();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                _unitNGroup = null;
                ug = null;

            }
        }

        public void fillNotListedUnits(System.Web.UI.WebControls.ListBox lstbx)
        {
            UnitAndUnitGroupSelect _unitNGroup = new UnitAndUnitGroupSelect();
            UserGroup ug = new UserGroup();
            try
            {
                
                _unitNGroup.UserID = this._userID;
                _unitNGroup.ComID = this.ComID;
                _unitNGroup.LoadNotListedUnits(ug._dropDownList);

                lstbx.DataSource = ug._dropDownList;
                lstbx.DataTextField = "Name";
                lstbx.DataValueField = "Value";
                lstbx.DataBind();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                _unitNGroup = null;
                ug = null;

            }
        }

        public void fillListedUnitGroups(System.Web.UI.WebControls.ListBox lstbx)
        {
            UnitAndUnitGroupSelect _unitNGroup = new UnitAndUnitGroupSelect();
            UserGroup ug = new UserGroup();
            try
            {
                
                _unitNGroup.UserID = this._userID;
                _unitNGroup.ComID = this._comID;        
                _unitNGroup.LoadListedUnitGroups(ug._dropDownList);

                lstbx.DataSource = ug._dropDownList;
                lstbx.DataTextField = "Name";
                lstbx.DataValueField = "Value";
                lstbx.DataBind();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                _unitNGroup = null;
                ug = null;

            }
        }


         public void fillNotListedUnitGroups(System.Web.UI.WebControls.ListBox lstbx)
        {
            UnitAndUnitGroupSelect _unitNGroup = new UnitAndUnitGroupSelect();
            UserGroup ug = new UserGroup();
            try
            {  
                _unitNGroup.UserID = this._userID;
                _unitNGroup.ComID = this._comID;
                _unitNGroup.LoadNotListedUnitGroups(ug._dropDownList);

                lstbx.DataSource = ug._dropDownList;
                lstbx.DataTextField = "Name";
                lstbx.DataValueField = "Value";
                lstbx.DataBind();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                _unitNGroup = null;
                ug = null;

            }
        }


    }
}
