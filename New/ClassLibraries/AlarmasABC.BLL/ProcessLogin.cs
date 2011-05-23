
using System;
using System.Data;

using AlarmasABC.Core;
using AlarmasABC.DAL.Select;

namespace AlarmasABC.BLL
{
	public class ProcessLogin
	{
		public ProcessLogin()
		{
			loginData = new Login();
		}
		
		public const int LOGIN_SUCCESS    		= 	0;
		public const int LOGIN_INCORRECT  		= 	1;
		public const int LOGIN_INACTIVE   		= 	2;
		public const int LOGIN_DISALLOWED 		= 	3;
		public const int LOGIN_COMPANY_NOEXIST 	=   4;
		
		private string companyName;
		public string CompanyName
		{
			get { return companyName; }
			set { companyName = value; }
		}
		
		private string userName;
		public string UserName
		{
			get { return userName; }
			set { userName = value; }
		}
		
		private string password;
		public string Password
		{
			get { return password; }
			set { password = value; }
		}
		
		private Login loginData;
		public Login LoginData
		{
			get { return loginData; }
		}
		
		public int LoginUser()
		{
			DataTable dt;
			
			// First get the company information
			CompanySelect companySelect = new CompanySelect();
			companySelect.CompanyName = companyName;
			dt = companySelect.Run();
			
			if (dt.Rows[0]["id"].ToString() != "") 
			{
				loginData.ComID = int.Parse(dt.Rows[0]["id"].ToString());
				loginData.CompanyName = dt.Rows[0]["name"].ToString();
			}
			else 
			{
				return LOGIN_COMPANY_NOEXIST;
			}
			
			// Now try to get the user login info
			UserSelect userSelect = new UserSelect();
			userSelect.UserName = userName;
			userSelect.Password = password;
			dt = userSelect.Run();
						
			// If we have more than 0 rows
			if (dt.Rows[0]["id"].ToString() != "")
			{
				if (bool.Parse(dt.Rows[0]["allowLogin"].ToString())) 
				{
					if (bool.Parse(dt.Rows[0]["active"].ToString()))
					{
						loginData.UserID = int.Parse(dt.Rows[0]["id"].ToString());
						loginData.UserName = dt.Rows[0]["username"].ToString();
						loginData.Role = int.Parse(dt.Rows[0]["role"].ToString());
					}
					else
					{
						return LOGIN_INACTIVE;
					}
				}
				else 
				{
					return LOGIN_DISALLOWED;
				}
			}
			else 
			{
				return LOGIN_INCORRECT;
			}
			
			return LOGIN_SUCCESS;
		}
	}
}
