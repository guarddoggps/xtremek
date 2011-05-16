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

namespace AlarmasABC.BLL.ProcessUserGroup
{
    public class ProcessUserGroup:InvokeOperations
    {
        InvokeOperations.operations _mode;
        public ProcessUserGroup(InvokeOperations.operations _mode)
        {
            this._mode = _mode;
        }

               
        
        private UserGroup _userGroup;
        public UserGroup UserGroup
        {
            get { return _userGroup; }
            set { _userGroup = value; }
        }

        private DataSet _ds;

        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public void invoke()
        {
            switch (this._mode)
            { 
                case operations.INSERT:
                    InsertUserGroup();
                    break;
                case operations.UPDATE:
                    UpdateUserGroup();
                    break;
                case operations.DELETE:
                    DeleteUserGroup();
                    break;
                case operations.SELECT:
                    SelectUserGroup();
                    break;
                default:
                    break;
            }
        }

        private void InsertUserGroup()
        {
            UserGroupDataInsert _groupInsert = new UserGroupDataInsert();
            try
            {
                _groupInsert.UserGroup = this._userGroup;
                _groupInsert.addUserGroup();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                _groupInsert = null;
            }
        }

        private void UpdateUserGroup()
        {
            UserGroupUpdate _updateGroup = new UserGroupUpdate();
            try
            {
                _updateGroup.Usergroup = this._userGroup;
                _updateGroup.deleteGroup();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                _updateGroup = null;
            }
        }

        private void DeleteUserGroup()
        {
            UserGroupDelete _deleteGroup = new UserGroupDelete();
            try
            {
                _deleteGroup.UserGroup = this._userGroup;
                _deleteGroup.deleteUserGroup();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                _deleteGroup = null;
            }
        }

        private void SelectUserGroup()
        {
            UserGroupSelect _groupSelect = new UserGroupSelect();
            try
            {
                _groupSelect.selectUserGroup();
                this._ds = _groupSelect.Ds;
            }
            catch (Exception ex)
            {
				throw new Exception("ProcessUserGroup::SelectUserGroup(): " + ex.Message);
            }
            finally
            {
                _groupSelect = null;
            }
        }

        public static void fillDropDownItems(System.Web.UI.WebControls.DropDownList ddl,UserGroup _userGroupObj)
        {
            try
            {
                //if (Branch.dropDownItems.Count < 1)
                //{
                //    new BranchSelectDataByUser().getBranchDropDownItemsByUser(-1);
                //}

                UserGroup _u = new UserGroup();
                _u = _userGroupObj;
                new UserGroupSelect(_u).UserGroupDropDownList(_u._dropDownList);

                ddl.DataSource = _u._dropDownList;
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
    }
}
