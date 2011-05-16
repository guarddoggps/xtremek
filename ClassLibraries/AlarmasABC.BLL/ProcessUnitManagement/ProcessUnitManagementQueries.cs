using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AlarmasABC.Core.Admin;
using AlarmasABC.DAL.Select;
using AlarmasABC.DAL.Insert;
using AlarmasABC.DAL.Delete;
using AlarmasABC.DAL.Update;

namespace AlarmasABC.BLL.ProcessUnitManagement
{
   public class ProcessUnitManagementQueries:IAlopekBusinessLogic
   {
       private InvokeOperations.operations _mode;
       public ProcessUnitManagementQueries(InvokeOperations.operations _mode)
       {
           this._mode = _mode;
       }

       public ProcessUnitManagementQueries()
       {
 
       }

       public ProcessUnitManagementQueries(Units _units)
       {
           this.Units = _units;
       }
       
       private DataSet _ds;

       public DataSet Ds
       {
           get { return _ds; }
           set { _ds = value; }
       }

       private Units _Units;

       public Units Units
       {
           get { return _Units; }
           set { _Units = value; }
       }


       public void invoke()
       {

           switch (this._mode)
           {
               case InvokeOperations.operations.INSERT:
                   addUnitInfo();
                   break;
               case InvokeOperations.operations.SELECT:
                   selectUnitInfo();
                   break;
               case InvokeOperations.operations.UPDATE:
                   updateUnitInfo();
                   break;
               case InvokeOperations.operations.DELETE:
                   deleteUnitInfo();
                   break;
               default:
                   break;
           }

       }

       private void addUnitInfo()
       {
           UnitsInfoInsert _insData = new UnitsInfoInsert();
           try
           {
               _insData.Units = this._Units;
               _insData.addUnitInfo();
           }
           catch (Exception ex)
           {
               throw new Exception(" ProcessUser :: addUser " + ex.Message);
           }
           finally
           {
               _insData = null;
               _Units = null;
           }
 
       }

       public void addUnitUserInfo()
       {
           UnitsUserInfoInsert _insData = new UnitsUserInfoInsert();
           try
           {
               _insData.Units = this._Units;
               _insData.addUnitUserInfo();
           }
           catch (Exception ex)
           {
               throw new Exception(" UnitsUserInfoInsert :: addUnitUserInfo " + ex.Message);
           }
           finally
           {
               _insData = null;
               _Units = null;
           }

       }

       private void selectUnitInfo()
       {
           UnitInfoSelect _UnitInfoSelect = new UnitInfoSelect(this._Units);
           _UnitInfoSelect.Units = this._Units;
           _UnitInfoSelect.selectUnits();
           this._ds = _UnitInfoSelect.Ds;
       }

       private void updateUnitInfo()
       {
           UnitsInfoUpdate _UpdateData = new UnitsInfoUpdate();
           try
           {
               _UpdateData.Units = this._Units;
               _UpdateData.UpdateUnitInfo();
           }
           catch (Exception ex)
           {
               throw new Exception(" UpdateData :: UpdateUnit " + ex.Message);
           }
           finally
           {
               _UpdateData = null;
               _Units = null;
           }
       }



       public void updateUnitUserInfo()
       {
           UnitUserInfoUpdate _UpData = new UnitUserInfoUpdate();
           try
           {
               _UpData.Units = this._Units;
               _UpData.updateUnitUserInfo();
           }
           catch (Exception ex)
           {
               throw new Exception(" UnitsUserInfoUpdate :: updateUnitUserInfo " + ex.Message);
           }
           finally
           {
               _UpData = null;
               _Units = null;
           }

       }

       private void deleteUnitInfo()
       {
            
       }

       public void deleteUnitUserInfo()
       {
           UnitUserInfoDelete _DeleteData = new UnitUserInfoDelete();
           try
           {
               _DeleteData.Units = this._Units;
               _DeleteData.deleteUserUnits();
           }
           catch (Exception ex)
           {
               throw new Exception(" UpdateData :: UpdateUnit " + ex.Message);
           }
           finally
           {
               _DeleteData = null;
               _Units = null;
           }
       }


       public static void fillDropDownItemsFuel(System.Web.UI.WebControls.DropDownList ddl)
       {
           try
           {


               Fuel _f = new Fuel();
               new FuelSelect().FuelDropDownList(_f._dropDownList);

               ddl.DataSource = _f._dropDownList;
               ddl.DataTextField = "Ftype";
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
		
	   public static void fillDropDownItemsUserUnits(System.Web.UI.WebControls.DropDownList ddl, Units _Units, string userID)
       {
           try
           {
               Units _Unit = new Units();
               _Unit = _Units;
               new UnitNameSelect(_Unit, userID).UnitNameDropDownList(_Unit._dropDownList);

               ddl.DataSource = _Unit._dropDownList;
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
		
       public static void fillDropDownItemsUnits(System.Web.UI.WebControls.DropDownList ddl, Units _Units)
       {
           try
           {
               Units _Unit = new Units();
               _Unit = _Units;
               new UnitNameSelect(_Unit).UnitNameDropDownList(_Unit._dropDownList);

               ddl.DataSource = _Unit._dropDownList;
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

       public static void fillDropDownItemsUnits1(System.Web.UI.WebControls.DropDownList ddl, Units _Units)
       {
           try
           {
               Units _Unit = new Units();
               _Unit = _Units;
               new UnitNameSelect(_Unit).CreateUnitNameDropDownList(_Unit._dropDownList);

               ddl.DataSource = _Unit._dropDownList;
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

       public static void fillDropDownItemsDeviceID(System.Web.UI.WebControls.DropDownList ddl, Units _Units)
       {
           try
           {


               Units _Unit = new Units();
               _Unit = _Units;
               new DeviceIDSelect(_Unit).DeviceNameDropDownList(_Unit._dropDownList);

               ddl.DataSource = _Unit._dropDownList;
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

       public static void fillDropDownItemsPattern(System.Web.UI.WebControls.DropDownList ddl, Pattern _Pattern)
       {
           try
           {


               Pattern _Patterns = new Pattern();
               _Patterns = _Pattern;
               new PatternSelect(_Patterns).PatternDropDownList(_Patterns._dropDownList);

               ddl.DataSource = _Patterns._dropDownList;
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


       public static void fillDropDownItemsUnitModelsAd(System.Web.UI.WebControls.DropDownList ddl, UnitModel _unitModel)
       {
           try
           {


               UnitModel _Unit = new UnitModel();
               _Unit = _unitModel;
               new UnitModelAdSelect(_Unit).UnitModelAdDropDownList(_Unit.
_dropDownList);

               ddl.DataSource = _Unit._dropDownList;
               ddl.DataTextField = "Name";
               ddl.DataValueField = "value";
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


       public static void fillGroupListListBox(System.Web.UI.WebControls.ListBox LstBox,UserGroup _UsrGroup)
       {
           try
           {


               UserGroup _UG = new UserGroup();
               _UG = _UsrGroup;
               new GroupListSelect(_UG).LoadGroupListBox(_UG._dropDownList);
               LstBox.DataSource = _UG._dropDownList;
               LstBox.DataTextField = "name";
               LstBox.DataValueField = "Value";
               LstBox.DataBind();

           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               LstBox = null;
           }
       }


       public static void fillNotListedGroupList(System.Web.UI.WebControls.ListBox LstBox, UserGroup _UsrGroup)
       {
           try
           {


               UserGroup _UG = new UserGroup();
               _UG = _UsrGroup;
               new NotListedGroupSelect(_UG).LoadNotListedGroupList(_UG._dropDownList);
               LstBox.DataSource = _UG._dropDownList;
               LstBox.DataTextField = "Name";
               LstBox.DataValueField = "Value";
               LstBox.DataBind();

           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               //LstBox = null;
           }
       }


       public static void fillListedGroupList(System.Web.UI.WebControls.ListBox LstBox, UserGroup _UsrGroup)
       {
           try
           {


               UserGroup _UG = new UserGroup();
               _UG = _UsrGroup;
               new ListedGroupSelect(_UG).LoadListedGroupList(_UG._dropDownList);
               LstBox.DataSource = _UG._dropDownList;
               LstBox.DataTextField = "Name";
               LstBox.DataValueField = "Value";
               LstBox.DataBind();

           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               //LstBox = null;
           }
       }



       public static void LoadIconOnDlist(System.Web.UI.WebControls.DataList DList)
       {
           try
           {


               IconSetup _UG = new IconSetup();
               new IconSelect().IconLoadOnDataList(_UG.loadIcon);

               DList.DataSource = _UG.loadIcon;
               DList.DataBind();

           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               DList = null;
           }
       }


       public void checkUnitExist()
       {
           CheckUnitExistSelect _CheckInfoSelect = new CheckUnitExistSelect(this._Units);
           _CheckInfoSelect.Units = this._Units;
           _CheckInfoSelect.selectUnitExist();
           this._ds = _CheckInfoSelect.Ds;

       }
       public void checkUnitExist1()
       {
           CheckUnitExistSelect _CheckInfoSelect = new CheckUnitExistSelect(this._Units, "update");
           _CheckInfoSelect.Units = this._Units;
           _CheckInfoSelect.selectUnitExist();
           this._ds = _CheckInfoSelect.Ds;

       }

       public void clearUnitData()
       {
           UnitInfoDelete _UnitClear = new UnitInfoDelete(this._Units);
           _UnitClear.deleteUnits();
           this._ds = _UnitClear.Ds;
   
       }

       public void loadImage()
       {
           LoadImageSelect _LoadImage = new LoadImageSelect(this._Units);
           _LoadImage.selectImage();
           this._ds = _LoadImage.Ds;
       }

       public void MaxUnitId()
       {
           MaxUnitIDSelect _maxUnit = new MaxUnitIDSelect();
           _maxUnit.selectMaxUnitID();
           this._ds = _maxUnit.Ds;
       }
   }
}
