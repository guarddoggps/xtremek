
using System;
using System.Data;

using AlarmasABC.Core;
using AlarmasABC.DAL.Select;

namespace AlarmasABC.BLL
{
	public class ProcessCompany
	{
		public const int COMPANY_EXISTS = 0;
		public const int COMPANY_NOEXIST = 1;
		
		public ProcessCompany()
		{
			companyData = new Company();
		}
		
		private string companyName;
		public string CompanyName
		{
			get { return companyName; }
			set { companyName = value; }
		}
		
		private Company companyData;
		public Company CompanyData
		{
			get { return companyData; }
		}
		
		public int Run()
		{
			DataTable dt;
			
			CompanySelect companySelect = new CompanySelect();
			companySelect.CompanyName = companyName;
			dt = companySelect.Run();
			
			if (dt.Rows[0]["id"].ToString() != "")
			{
				companyData.SetData(dt.Rows[0]);
				return COMPANY_EXISTS;
			}
			else
			{
				return COMPANY_NOEXIST;
			}
		}
	}
}
