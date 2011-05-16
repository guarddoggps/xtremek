using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlarmasABC.Core.Admin;
using AlarmasABC.DAL.Insert;
using System.Data;
using AlarmasABC.DAL.Select;
using AlarmasABC.DAL.Update;
using AlarmasABC.DAL.Delete;

namespace AlarmasABC.BLL.ProcessCompany
{
   public class ProcessImageUrl
    {

        private ImageURL _iUrl;

        public ImageURL IUrl
        {
            get { return _iUrl; }
            set { _iUrl = value; }
        }

        private DataSet _ds;

        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        private string _iName;

        public string IName
        {
            get { return _iName; }
            set { _iName = value; }
        }


        public void AddImageUrl()
        {
            try
            {
                ImageURLInsert imgUrl = new ImageURLInsert();
                imgUrl.IUrl = this._iUrl;
                imgUrl.AddImageUrl();
                
            }
            catch (Exception ex)
            {
				throw new Exception("ProcessImageUrl::AddImageUrl(): " + ex.Message);
            }
            finally
            {

            }
        }

        public void GetImageInfo(int ComID)
        {
            try
            {
                
                ImageInfoSelect _iInfo = new ImageInfoSelect();
                _iInfo.GetImageInfo(ComID);
                this._ds = _iInfo.Ds;

            }
            catch(Exception ex)
            {
				throw new Exception("ProcessImageUrl::GetImageInfo(): " + ex.Message);
            }
        }

        public void GetActiveImageInfo(int ComID)
        {
            try
            {

                ImageInfoSelect _iInfo = new ImageInfoSelect("ActiveImage");
                _iInfo.GetImageInfo(ComID);
                this._ds = _iInfo.Ds;

            }
            catch (Exception ex)
            {
				throw new Exception("ProcessImageUrl::GetActiveImageInfo(): " + ex.Message);
            }
        }

        public void UpdateImageInfo()
        {
            try
            {

                ImageInfoUpdate _iInfo = new ImageInfoUpdate();
                _iInfo.IUrl = this._iUrl;
                _iInfo.UpdateImageInfo();
                

            }
            catch (Exception ex)
            {
				throw new Exception("ProcessImageUrl::UpdateImageInfo(): " + ex.Message);
            }
        }

        public void DeleteImageInfo()
        {
            try
            {

                DeleteImageInfo _iDel = new DeleteImageInfo();
                _iDel.IUrl = this._iUrl;
                _iDel.DeleteImage();
                this._iName = _iDel.IName;


            }
            catch (Exception ex)
            {
				throw new Exception("ProcessImageUrl::DeleteImageInfo(): " + ex.Message);
            }
        }
    }
}
