using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
namespace BKBNIDVerify.Models
{

    public class GETNIDDetails
    {

        [Display(Name = "NID (17 or 10 digits")]
        [Required]
        [StringLength(17, MinimumLength = 10)]
        public string nid { get; set; }
        [Required]

        [Display(Name = "Date Of Birth")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime dob { get; set; }

    }
    public class HierarchyReportModel
    {

        public string total { get; set; }
        public string Branch_Code { get; set; }
        public string Branch_Name { get; set; }

    }


    public class SuccessDetailsResponse
    {
        //added in 2nd release dated:2019-12-11
        [Display(Name = "Status")]

        [JsonProperty("status")]
        public string status { get; set; }
        [Display(Name = "Status Code")]

        [JsonProperty("statusCode")]
        public string statusCode { get; set; }
        [Display(Name = "Success")]


        [JsonProperty("success")]
        public NIDDataSuccess success { get; set; }

    }

    public class NIDDataSuccess
    {
        public NIDDetails data { get; set; }
    }

    public class NIDDetails
    {
        [Display(Name = "Request ID")]
        [JsonProperty("requestId")]
        public string requestId { get; set; }

        [Display(Name = "Name(Bangla)")]
        [JsonProperty("name")]
        public string name { get; set; }
        [Display(Name = "Name(English)")]
        [JsonProperty("nameEn")]
        public string nameEn { get; set; }
        [Display(Name = "Date Of Birth")]
        [JsonProperty("dateOfBirth")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime dateOfBirth { get; set; }
        // public string dateOfBirth { get; set; }
        [Display(Name = "Father's Name")]
        [JsonProperty("father")]
        public string father { get; set; }
        [Display(Name = "Mother's Name")]
        [JsonProperty("mother")]
        public string mother { get; set; }
        [Display(Name = "Spouse Name")]
        [JsonProperty("spouse")]
        public string spouse { get; set; }
        [Display(Name = "Occupation")]
        [JsonProperty("occupation")]
        public string occupation { get; set; }
        [Display(Name = "Blood Group")]
        [JsonProperty("bloodGroup")]
        public string bloodGroup { get; set; }
        [Display(Name = "National ID")]
        [JsonProperty("nationalId")]
        public string nationalId { get; set; }
        [Display(Name = "PIN")]
        [JsonProperty("pin")]
        public string pin { get; set; }

        [JsonProperty("permanentAddress")]
        public PermanentAddress permanentAddress { get; set; }
        [JsonProperty("presentAddress")]
        public PresentAddress presentAddress { get; set; }
        [Display(Name = "Photo")]
        [JsonProperty("photo")]
        public string photo { get; set; }


    }

    public class PermanentAddress
    {
        [Display(Name = "Division")]
        [JsonProperty("division")]
        public string division { get; set; }
        [Display(Name = "District")]
        [JsonProperty("district")]
        public string district { get; set; }
        [Display(Name = "RMO")]
        [JsonProperty("rmo")]
        public string rmo { get; set; }

        [Display(Name = "City Corporation/Municipality")]
        [JsonProperty("cityCorporationOrMunicipality")]
        public string cityCorporationOrMunicipality { get; set; }

        [Display(Name = "Upozila")]
        [JsonProperty("upozila")]
        public string upozila { get; set; }

        [Display(Name = "Union/Ward")]
        [JsonProperty("unionOrWard")]
        public string unionOrWard { get; set; }

        [Display(Name = "Post Office")]
        [JsonProperty("postOffice")]
        public string postOffice { get; set; }
        [Display(Name = "Postal Code")]
        [JsonProperty("postalCode")]
        public string postalCode { get; set; }
        [Display(Name = "Mouza/Moholla")]
        [JsonProperty("mouzaOrMoholla")]
        public string mouzaOrMoholla { get; set; }

        [Display(Name = "Additional Mouza/Moholla")]
        [JsonProperty("additionalMouzaOrMoholla")]
        public string additionalMouzaOrMoholla { get; set; }


        [Display(Name = "Ward For Union Porishod")]
        [JsonProperty("wardForUnionPorishod")]
        public string wardForUnionPorishod { get; set; }

        [Display(Name = "Village/Road")]
        [JsonProperty("villageOrRoad")]
        public string villageOrRoad { get; set; }

        [Display(Name = "Additional Village/Road")]
        [JsonProperty("additionalVillageOrRoad")]
        public string additionalVillageOrRoad { get; set; }
        [Display(Name = "Home/Holding No")]
        [JsonProperty("homeOrHoldingNo")]
        public string homeOrHoldingNo { get; set; }
        [Display(Name = "Region")]
        [JsonProperty("region")]
        public string region { get; set; }

    }


    public class PresentAddress
    {
        [Display(Name = "Division")]
        [JsonProperty("division")]
        public string division { get; set; }
        [Display(Name = "District")]
        [JsonProperty("district")]
        public string district { get; set; }
        [Display(Name = "RMO")]
        [JsonProperty("rmo")]
        public string rmo { get; set; }

        [Display(Name = "City Corporation/Municipality")]
        [JsonProperty("cityCorporationOrMunicipality")]
        public string cityCorporationOrMunicipality { get; set; }

        [Display(Name = "Upozila")]
        [JsonProperty("upozila")]
        public string upozila { get; set; }

        [Display(Name = "Union/Ward")]
        [JsonProperty("unionOrWard")]
        public string unionOrWard { get; set; }

        [Display(Name = "Post Office")]
        [JsonProperty("postOffice")]
        public string postOffice { get; set; }
        [Display(Name = "Postal Code")]
        [JsonProperty("postalCode")]
        public string postalCode { get; set; }
        [Display(Name = "Mouza/Moholla")]
        [JsonProperty("mouzaOrMoholla")]
        public string mouzaOrMoholla { get; set; }

        [Display(Name = "Additional Mouza/Moholla")]
        [JsonProperty("additionalMouzaOrMoholla")]
        public string additionalMouzaOrMoholla { get; set; }


        [Display(Name = "wardForUnionPorishod")]
        [JsonProperty("wardForUnionPorishod")]
        public string wardForUnionPorishod { get; set; }

        [Display(Name = "Village/Road")]
        [JsonProperty("villageOrRoad")]
        public string villageOrRoad { get; set; }

        [Display(Name = "Additional Village/Road")]
        [JsonProperty("additionalVillageOrRoad")]
        public string additionalVillageOrRoad { get; set; }
        [Display(Name = "Home/Holding No")]
        [JsonProperty("homeOrHoldingNo")]
        public string homeOrHoldingNo { get; set; }
        [Display(Name = "Region")]
        [JsonProperty("region")]
        public string region { get; set; }


    }
    public class SuccessResponse
    {
        //added in 2nd release dated:2019-12-11

        [JsonProperty("status")]
        public string status { get; set; }

        [JsonProperty("statusCode")]
        public string statusCode { get; set; }

        [JsonProperty("success")]
        public Success success { get; set; }

    }

    //added in 2nd release dated:2019-12-11


    public class TokenResponse
    {
        [JsonProperty("status")]
        public string status { get; set; }
        [JsonProperty("statusCode")]
        public string statusCode { get; set; }
        [JsonProperty("success")]
        public Success success { get; set; }
    }
    public class Success
    {
        [JsonProperty("data")]
        public Token data { get; set; }
    }

    public class Token
    {
        [JsonProperty("username")]
        public string username { get; set; }
        [JsonProperty("access_token")]
        public string access_token { get; set; }
        [JsonProperty("refresh_token")]
        public string refresh_token { get; set; }
    }
    public class FailureResponse
    {
        [Display(Name = "Status")]
        [JsonProperty("status")]
        public string status { get; set; }

        [Display(Name = "Status Code")]
        [JsonProperty("StatusCode")]
        public string statusCode { get; set; }

        public ErrorResponse error { get; set; }
    }
    public class ErrorResponse
    {
        [Display(Name = "Message")]
        [JsonProperty("message")]
        public string message { get; set; }
    }
}