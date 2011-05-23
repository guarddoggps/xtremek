
using System;
using System.Data;

namespace AlarmasABC.Core
{
	public class Company
	{
		public Company()
		{
		}
		
		public int ID;
		
		public string Name;
		
		public string Address;
		
		public string Phone;
		
		public string Email;
		
		public string Website;
		
		public DateTime RegisterDate;
		
		public string Directory;
		
		public void SetData(DataRow data)
		{
			ID = 			int.Parse(data["id"].ToString());
			
			Name = 			data["name"].ToString();
			Address = 		data["address"].ToString();
			Phone = 		data["phone"].ToString();
			Email = 		data["email"].ToString();
			Website = 		data["website"].ToString();
			
			RegisterDate = 	DateTime.Parse(data["regdate"].ToString());
			
			Directory = 	data["directory"].ToString();
		}
	}
}
