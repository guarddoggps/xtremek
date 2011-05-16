using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AlarmasABC.DAL.Select;
using AlarmasABC.DAL.Delete;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.BLL.ProcessFleetPattern
{
    public class ProcessPattern:IAlopekBusinessLogic
    {

        public InvokeOperations.operations mode;

        public ProcessPattern()
        {
            ///Default Constructor
        }

        public ProcessPattern(InvokeOperations.operations mode)
        {
            this.mode = mode;
        }

        private int _comID;

        public int ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }

        private int _PatternID;

        public int PatternID
        {
            get { return _PatternID; }
            set { _PatternID = value; }
        }


        private DataSet _ds;

        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public void invoke()
        {
            switch (this.mode)
            {
                case InvokeOperations.operations.SELECT:
                    selectPattern();
                    break;
                case InvokeOperations.operations.INSERT:

                    break;
                case InvokeOperations.operations.UPDATE:

                    break;
                case InvokeOperations.operations.DELETE:
                    deletePattern();
                    break;
                default: break;

            }
        }

        private void selectPattern()
        {
            Pattern _newPat = new Pattern();
            _newPat.ComID = this._comID;
            PatternSelect _patternSelect = new PatternSelect(_newPat);
            try
            {
                _patternSelect.selectPattern();
                this._ds = _patternSelect.Ds;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                _patternSelect = null;
            }
        }

        private void deletePattern()
        {
            PatternDelete _DelPattern = new PatternDelete();
            try
            {
                _DelPattern.PatternId = this._PatternID;
                _DelPattern.deletePattern();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                _DelPattern = null;
            }
        }

    }
}
