using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AlarmasABC.Core.Admin;
using AlarmasABC.DAL.Insert;
using AlarmasABC.DAL.Delete;
using AlarmasABC.DAL.Select;
using AlarmasABC.DAL.Update;

namespace AlarmasABC.BLL.ProcessUser
{
    public class ProcessUser:IAlopekBusinessLogic
    {
        private InvokeOperations.operations _mode;
        public ProcessUser(InvokeOperations.operations _mode)
        {
            this._mode = _mode;
        }
        public ProcessUser()
        {
            
        }

        private DataSet _ds;

        /// <summary>
        ///  Set/Get Dataset
        /// </summary>
        
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        private User _userObj;
        /// <summary>
        ///  Set/Get User Object
        /// </summary>
        public User UserObj
        {
            get { return _userObj; }
            set { _userObj = value; }
        }

        private int _uID;
        public int UID
        {
            get { return _uID; }
            set { _uID = value; }
        }

        public void invoke()
        {
            switch (this._mode)
            { 
                case InvokeOperations.operations.INSERT:
                    addUser();
                    break;
                case InvokeOperations.operations.SELECT:
                    selectUser();
                    break;
                case InvokeOperations.operations.UPDATE:
                    updateUser();
                    break;
                case InvokeOperations.operations.DELETE:
                    deleteUser();
                    break;
                default:
                    break;
            }
        }

        private void addUser()
        {
            UserDataInsert _insData = new UserDataInsert();
            try
            {
                _insData.UserObj = this._userObj;
                _insData.addUser();
                this._uID = _insData.UserObj.UID;
            }
            catch (Exception ex)
            {
                throw new Exception(" ProcessUser :: addUser " + ex.Message);
            }
            finally
            {
                _insData = null;
                _userObj = null;
            }
        }

        private void selectUser()
        {
            try
            {
                UserSelect _userSelect = new UserSelect();
                _userSelect.User = this._userObj;
                _userSelect.selectUser();
                this._ds = _userSelect.Ds;

            }
            catch (Exception ex)
            {
                throw new Exception("selectUser():: " + ex.Message);
            }
        }

        private void updateUser()
        {
            try
            {
                UserUpdate _userUpdate = new UserUpdate();
                _userUpdate.UserObj = this._userObj;
                _userUpdate.updateUser();
            }
            catch (Exception ex)
            {
                throw new Exception("BLL::UpdateUser():: " + ex.Message);
            }
        }
        private void deleteUser()
        {
            DeleteUser _del = new DeleteUser();

            try
            {
                _del.UID = this.UID;
                _del.DelUser();
            }
            catch (Exception ex)
            {
                throw new Exception("BLL::Deleteuser::" + ex.Message);
            }
            finally
            {
                _del = null;
            }
        }
        public void disableUser()
        {
            DeleteUser _del = new DeleteUser("disable");

            try
            {
                _del.UID = this.UID;
                _del.DisableUser();
            }
            catch (Exception ex)
            {
                throw new Exception("BLL::Disableuser::" + ex.Message);
            }
            finally
            {
                _del = null;
            }
        }

        public static void fillDropDownItems(System.Web.UI.WebControls.DropDownList ddl, User _user)
        {
            try
            {
                //if (Branch.dropDownItems.Count < 1)
                //{
                //    new BranchSelectDataByUser().getBranchDropDownItemsByUser(-1);
                //}

                User _U = new User();
                _U = _user;

                new UserSelect(_U).UserDropDownList(_U._userDropDown);

                ddl.DataSource = _U._userDropDown;
                ddl.DataTextField = "Name";
                ddl.DataValueField = "Value";
                ddl.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ddl = null;
            }
        }

        public static void fillDropDownItems1(System.Web.UI.WebControls.DropDownList ddl, User _user)
        {
            try
            {
                //if (Branch.dropDownItems.Count < 1)
                //{
                //    new BranchSelectDataByUser().getBranchDropDownItemsByUser(-1);
                //}

                User _U = new User();
                _U = _user;

                new UserSelect(_U).CreateUserDropDownList(_U._userDropDown);

                ddl.DataSource = _U._userDropDown;
                ddl.DataTextField = "Name";
                ddl.DataValueField = "Value";
                ddl.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ddl = null;
            }
        }


        public static void fillDropDownSecurityQuestions(System.Web.UI.WebControls.DropDownList _ddl)
        {
            try
            {

                SecurityQuestion _secQues = new SecurityQuestion();
                new SecurityQuestionSelect().SecurityQuestionDropDownList(_secQues._dropDownList);

                _ddl.DataSource = _secQues._dropDownList;
                _ddl.DataTextField = "Name";
                _ddl.DataValueField = "Value";
                _ddl.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _ddl = null;
            }
        }
    }
}
