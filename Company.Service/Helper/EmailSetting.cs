﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Helper
{
	public class EmailSetting
	{
		public static void SendEmail(Email input)
		{
			var client = new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("isso.adel3@gmail.com", "cbdnrwbljffclfea");
			client.Send("isso.adel3@gmail.com",input.To,input.Subject,input.Body);

		} 
	}
}
