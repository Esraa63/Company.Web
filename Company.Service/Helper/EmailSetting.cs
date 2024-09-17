using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Helper
{
	public class EmailSetting
	{
		public static void SendEmail()
		{
			var client = new SmtpClient("smtp.gmail.com", 587);

		} 
	}
}
