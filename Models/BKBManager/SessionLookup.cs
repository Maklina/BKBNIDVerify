using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using BKBNIDVerify.DataModel;

using System.Configuration;

namespace BKBNIDVerify.Models.BKBManager
{
    public class SessionLookup
    {
        Covid19LoanDBEntities db = new Covid19LoanDBEntities();

        internal string getBranchName(string branchCode)
        {
            var branch = db.Branches.Where(o => o.branch_code.Equals(branchCode)).SingleOrDefault();
            return branch.branch_name;
        }

        internal string getDivisionName(string branchCode)
        {
            var branch = db.Branches.Where(o => o.branch_code.Equals(branchCode)).SingleOrDefault();
            return branch.RoutingNo;
        }

        internal string getRegionName(string branchCode)
        {
            var branch = db.Branches.Where(o => o.branch_code.Equals(branchCode)).SingleOrDefault();
            return branch.region;
        }

        public static string sendMail(string subject, string message, List<string> mailTo)
        {
            string hostAdd = ConfigurationManager.AppSettings["smtpHost"].ToString();
            string smtpPort = ConfigurationManager.AppSettings["smtpPort"].ToString();
            string emailFrom = ConfigurationManager.AppSettings["emailFrom"].ToString();
            string emailPassword = ConfigurationManager.AppSettings["emailPassword"].ToString();
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(emailFrom);
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;

                foreach (var item in mailTo)
                {
                    mail.To.Add(item.Trim());
                }
                /*SmtpClient smtp = new SmtpClient();
                smtp.Host = hostAdd;
                smtp.EnableSsl = true;
                NetworkCredential netCrd = new NetworkCredential();
                netCrd.UserName = mail.From.Address;
                netCrd.Password = emailPassword;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = netCrd;
                smtp.Port = 587;*/

                SmtpClient client = new SmtpClient(hostAdd, Convert.ToInt32(smtpPort));
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(emailFrom, emailPassword);
                client.Send(mail);
                return "sucess";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "failed";
            }

        }
    }
}