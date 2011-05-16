using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmasABC.Core.Admin
{
    public class SecurityQuestion
    {

        public IList<SecurityQuestion> _dropDownList = new List<SecurityQuestion>();

        public SecurityQuestion()
        {
 
        }
        public SecurityQuestion(int Value, string Name)
        {
            _id = Value; _question = Name;
        }

        public int Value
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Name
        {
            get { return _question; }
            set { _question = value; }
        }

        #region instance variable and Properties
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _question;

        public string Question
        {
            get { return _question; }
            set { _question = value; }
        }
        
        #endregion
    }
}

