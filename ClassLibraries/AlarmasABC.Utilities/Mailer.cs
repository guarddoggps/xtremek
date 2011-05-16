using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using AlarmasABC.BLL.ProcessCompany;

namespace AlarmasABC.Utilities
{
    public class Mailer
    {
        public static void SendMailMessage(String Sender, String Receipent, String CC, String BCC, String Subject, String Body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(Receipent);
                mail.From = new MailAddress(Sender);
                mail.Subject = Subject;
                mail.Body = Body;

                if (CC != "")
                    mail.CC.Add(CC);
                if (BCC != "")
                    mail.Bcc.Add(BCC);
				
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

				
				SmtpClient client = new SmtpClient();
				client.Send(mail);
                //lblMessage.Text = "Mail Sent Successfully";
            }
            catch (Exception Ex)
            {
				throw new Exception(" AlarmasABC::Security::Mailer :: " + Ex.Message.ToString());
            }           

        }
		
		public static void SendWelcomeMail(string email, string username, string password, string comID)
    	{
	        string msg;
	
	        try
	        {
				// Get the name of the company
		        ProcessCompanyQueries processCompany = new ProcessCompanyQueries(int.Parse(comID));
		        processCompany.invoke();
		        DataSet _ds = processCompany.Ds;
				
				string companyName = _ds.Tables[0].Rows[0]["companyName"].ToString();
				
	            msg = "Welcome to the " + companyName + " Tracking System!\r\n\r\n";
	            msg += "Your username and password have been added to our database. ";
	            msg += "Please write them down and store them in a safe place for your reference.";
	            msg += "\r\n\r\n";
	            msg += "Username: " + username + "\r\n";
	            msg += "Password: " + password + "\r\n";
	            msg += "\r\n";
	            msg += "Head over to http://xtremek.com/" + _ds.Tables[0].Rows[0]["serverDirectory"].ToString();
	            msg += " to login!\r\n\r\n";
	            msg += "Thank you for using the " + companyName + " Tracking System!\r\n";
	
	            SendMailMessage("webmaster@xtremek.com", email, "", "", "Welcome to the " 
	                                    + companyName + " Tracking System!", msg);
	        }
	        catch (Exception ex)
	        {
	            Console.WriteLine("SendWelcomeMail(): " + ex.Message.ToString());
	        }
    	}

    }
}
