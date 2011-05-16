using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AlarmasABC.DAL.Select;
using AlarmasABC.DAL.Insert;
using AlarmasABC.DAL.Update;
using AlarmasABC.DAL.Delete;
using AlarmasABC.Core.Tracking;

namespace AlarmasABC.BLL.ProcessSafetyZone
{
    public class ProcessSafetyZone:IAlopekBusinessLogic
    {
        private InvokeOperations.operations _mode;

        public ProcessSafetyZone(InvokeOperations.operations _mode)
        {
            this._mode = _mode;
        }
        public ProcessSafetyZone()
        {
            
        }

        #region Private Variables and Properties

        private int _unitID;
        public int UnitID
        {
            get { return _unitID; }
            set { _unitID = value; }
        }

        private int _comID;
        public int ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }

        private Geofence _geoInfo;

        public Geofence GeoInfo
        {
            get { return _geoInfo; }
            set { _geoInfo = value; }
        }

        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }
        
        #endregion

        public void invoke()
        {

            switch (this._mode)
            { 
                case InvokeOperations.operations.SELECT:
                    SelectZone();
                    break;
                case InvokeOperations.operations.INSERT:
                    SaveZoneData();
                    break;
                case InvokeOperations.operations.UPDATE:
                    UpdateGeofence();
                    break;
                case InvokeOperations.operations.DELETE:
                    DeleteUnitZone();
                    break;
                default:
                    break;
            }
        }

        private void SelectZone()
        {
            SafetyZoneSelect _zoneSelect = new SafetyZoneSelect();
            try
            {
                _zoneSelect.ComID = this.ComID;
                _zoneSelect.UnitID = this.UnitID;

                _zoneSelect.GetZoneList();
                this.Ds = _zoneSelect.Ds;
            }
            catch (Exception ex)
            {
                throw new Exception("BLL::ZoneSelect::" + ex.Message);
            }
            finally
            {
                _zoneSelect = null;
            }
        }

        private void SaveZoneData()
        {

            SafetyZoneDataInsert _zoneInsert = new SafetyZoneDataInsert();
            try
            {
                _zoneInsert.GeoInfo = this._geoInfo;

                _zoneInsert.SaveSafetyData();
                
            }
            catch (Exception ex)
            {
                throw new Exception("BLL::ZoneInsert::" + ex.Message);
            }
            finally
            {
                _zoneInsert = null;
            }

        }

        public void LoadGeoData()
        {
            try
            {
                GeofenceDataSelect _geoData = new GeofenceDataSelect();

                _geoData.GeoData = this._geoInfo;
                _geoData.selectGeofenceData();
                this._ds = _geoData.Ds;
            }
            catch (Exception ex)
            {
				throw new Exception("ProcessSafetyZone::LoadGeoData(): " + ex.Message);
            }

        }

        public void GeofenceUnitInsert()
        {
            try
            {
                GeofenceDataInsert _geoData = new GeofenceDataInsert();

                _geoData.GeoInfo = this._geoInfo;
                _geoData.UnitInsert();
                //this.Ds = _geoData.Ds;
            }
            catch (Exception ex)
            {
				throw new Exception("ProcessSafetyZone::GeofenceUnitInsert(): " + ex.Message);
            }

        }

        public void GeofenceUnitGroupInsert()
        {
            try
            {
                GeofenceDataInsert _geoData = new GeofenceDataInsert("group");

                _geoData.GeoInfo = this._geoInfo;
                _geoData.UnitGroupInsert();
                //this.Ds = _geoData.Ds;
            }
            catch (Exception ex)
            {
				throw new Exception("ProcessSafetyZone::GeofenceUnitGroupInsert(): " + ex.Message);
            }
        }

        private void UpdateGeofence()
        {
            GeofenceDataUpdate _goeUpdate = new GeofenceDataUpdate();
            try
            {
                

                _goeUpdate.GeoData = this._geoInfo;
                _goeUpdate.UpdateGeofence();
            }
            catch (Exception ex)
            {
                throw new Exception("BLL::UpdateGeofence::" + ex.Message);
            }
            finally
            {
                _goeUpdate = null;
            }
        }

        private void DeleteUnitZone()
        {
            DeleteSafetyZone _del = new DeleteSafetyZone();
            try
            {
                _del.Gf = this._geoInfo;
                _del.DeleteZone();
            }
            catch (Exception ex)
            {
                throw new Exception("BLL::DeleteUnitZone::" + ex.Message);
            }
            finally
            {
                _del = null;
            }
        }
    }
}
