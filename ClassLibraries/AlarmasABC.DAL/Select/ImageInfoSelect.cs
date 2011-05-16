using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;

using Microsoft.ApplicationBlocks.Data;

namespace AlarmasABC.DAL.Select
{
    public class ImageInfoSelect:DataAccessBase
    {
        public ImageInfoSelect( )
        {

			Command = @"SELECT id,'../AdvertisingImages/' || imageName AS Image,imageURL,isActive,imageName" +
            		@" FROM tblImage WHERE comID = :comID";
        }
        public ImageInfoSelect(string ActiveImage)
        {

			Command = @"SELECT 'AdvertisingImages/' || imageName AS Image,isActive,'http://' || imageURL AS URL"+
        			@" FROM tblImage WHERE comID = :comID";
        }

       
        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public void GetImageInfo(int comID)
        {
            try
            {
                DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
				this._ds = _db.Run(base.ConnectionString, returnParam(comID));
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            { 
            
            }
        }

        

       private NpgsqlParameter[] returnParam(int comID)
        {
            NpgsqlParameter[] _param = { 
                                        DataBaseHelper.MakeParam("comID", NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   comID)
                                    };

            return _param;
        }


    }
}
