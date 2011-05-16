using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlarmasABC.Core.Tracking;
using AlarmasABC.DAL.Select;

namespace AlarmasABC.BLL.ProcessMapData
{
    public class ProcessMainMapData:IAlopekBusinessLogic
    {
        private int _typeData;

        public ProcessMainMapData(int _typeData)
        {
            this._typeData = _typeData;
        }

        #region Private Variables and Properties

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

            private int _typeID;
            public int TypeID
            {
                get { return _typeID; }
                set { _typeID = value; }
            }

            private IList<MapData> _mapData;
            public IList<MapData> MapData
            {
                get { return _mapData; }
                set { _mapData = value; }
            }

        #endregion
        
        public void invoke()
        {
            switch (this._typeData)
            { 
                case 1:
                    TypeData();
                    break;

                case 0:
                    NonTypeData();
                    break;

                default:
                    break;
            }
        }

        private void TypeData()
        {
            MainMapData _mainMapData = new MainMapData(true);

            try
            {
                MapData _mapData = new MapData();
                _mainMapData.ComID = this._comID;
                _mainMapData.UserID = this._userID;
                _mainMapData.TypeID = this._typeID;
                _mainMapData.Mapdata = _mapData._mapData;
                _mainMapData.getTypedMapData();
                MapData = _mainMapData.Mapdata;
            }
            catch (Exception ex)
            {
                throw new Exception(" BLL:: ProcessMainMapData::TypedData::" + ex.Message);
            }

            finally
            {
                _mainMapData = null;
            }
        }

        private void NonTypeData()
        {
            MainMapData _mainMapData = new MainMapData();

            try
            {
                MapData _mapData = new MapData();
                _mainMapData.ComID = this._comID;
                _mainMapData.UserID = this._userID;
                _mainMapData.Mapdata = _mapData._mapData;
                _mainMapData.getMapData();
                MapData = _mainMapData.Mapdata;
            }
            catch (Exception ex)
            {
                throw new Exception(" BLL:: ProcessMainMapData::" + ex.Message);
            }

            finally
            {
                _mainMapData = null;
            }
        }
        
    }
}
