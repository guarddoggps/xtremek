
using System;
using System.Diagnostics;

namespace AlarmasABC.Core
{
	public class ErrorHandling
	{
		
		public ErrorHandling()
		{
		}
		
		public static void ErrorOcurred(Exception ex)
		{
			string errorMsg;
			StackTrace st = new StackTrace(1);
			StackFrame sf = st.GetFrame(0);
			
			errorMsg = sf.GetMethod().DeclaringType.FullName + "." + sf.GetMethod().Name + "(): ";

			errorMsg += ex.Message.ToString();
			
#if DEBUG
			Console.WriteLine(errorMsg);	
#endif
			
			// TODO: Log this stuff to a file
			
			// If we want to see an exception message, throw a new exception
			throw new Exception(errorMsg);
		}
	}
}
