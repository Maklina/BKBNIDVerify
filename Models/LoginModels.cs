using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BKBNIDVerify.Models
{
    public class LoginModels
    {
        [Required(ErrorMessage = "Please enter the User Name")]
        [Display(Name = "User ID")]
        public string UserId { get; set; }

        [Display(Name = "User Name")]
        public string userName { get; set; }

        [Required(ErrorMessage = "Please enter the Password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Branch Name")]
        public string BranchCode { get; set; }

        [Display(Name = "User Status")]
        public string UserStatus { get; set; }
        [Display(Name = "User Mobile")]
        public string UserMobile { get; set; }

        public string role_priority { get; set; }

        public int? UserRoleId { get; set; }

        public int BankID { get; set; }
        [Display(Name = "Bank Name")]
        public string BranchName { get; set; }
        [Display(Name = "XP Receving Agent Code")]
        public string XPreceivingagentcode { get; set; }
        public string RoleName { get; set; }
        public string userIPAddress { get; set; }

        public string agentID { get; set; }
        public string agentSequence { get; set; }

        public string token { get; set; }
        public string region { get; set; }
        public string division { get; set; }
    }

    [DataContract]
    public class DataPoint
    {
        public String x { get; set; }
        public Nullable<decimal> y { get; set; }
    }
}