
using System;
using System.Data;

namespace AlarmasABC.Core
{
	public class User
	{
		public User()
		{
		
		}
		
		public const int ROLE_SUPER_ADMIN = 1;
		public const int ROLE_COMPANY_ADMIN = 2;
		public const int ROLE_USER = 3;
		
		/* This is the user ID */
		public int ID;
		
		/* The ID of the company this user belongs to */
		public int AccountID;
		
		/* Real name of the user */
		public string RealName;
		
		/* Username or login of the user */
		public string UserName;
		
		/* The password of the user */
		public string Password;
		
		/* User email */
		public string Email;
		
		/* The security question for the user */
		public string SecurityQuestion;
		
		/* The security answer for the user */
		public string SecurityAnswer;
		
		/* The role of the user */
		public int Role; 
		
		/* User is active */
		public bool Active;
		
		public bool AllowLogin;
		
		/// <summary>
       	/// Gets a DataRow and sets the values of the class parameters the corresponding data
       	/// in the DataRow
        /// </summary>
        /// <param name="data">The DataRow to convert</param>
		public void SetData(DataRow data)
		{
			try
			{
				ID 	= 				int.Parse(data["id"].ToString());
				AccountID = 		int.Parse(data["accountID"].ToString());
				
				RealName = 			data["realName"].ToString();
				UserName = 			data["userName"].ToString();
				Password = 			data["password"].ToString();
				Email = 			data["email"].ToString();
				
				SecurityQuestion = 	data["securityQuestion"].ToString();
				SecurityAnswer = 	data["securityAnswer"].ToString();
				
				Role = 				int.Parse(data["role"].ToString());
				
				Active = 			bool.Parse(data["active"].ToString());
				AllowLogin = 		bool.Parse(data["allowLogin"].ToString());
			}
			catch (Exception ex)
			{
				ErrorHandling.ErrorOcurred(ex);
			}
		}
	}
}
