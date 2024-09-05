using BKBNIDVerify.DataModel;
using BKBNIDVerify.Models;
using log4net;
using Newtonsoft.Json;
using System;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Script.Serialization;


namespace BKBNIDVerify.Controllers
{
    public class HomeController : Controller
    {
        BKBNIDDBEntities db = new BKBNIDDBEntities();
        Covid19LoanDBEntities db1 = new Covid19LoanDBEntities();
        ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public ActionResult Index()
        {
            var login = (BKBNIDVerify.Models.LoginModels)Session["LoginCredentials"];

            if (login != null)
            {
                return View();
            }
            else
            {

                return RedirectToAction("Index", "Login");
            }
        }
        public ActionResult Welcome()
        {

            LoginModels logIn = new LoginModels();
            var login = (BKBNIDVerify.Models.LoginModels)Session["LoginCredentials"];

            if (login != null)
            {


                return View();

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        [HttpPost]
        public ActionResult Index(GETNIDDetails GNID)
        {
            var login = (BKBNIDVerify.Models.LoginModels)Session["LoginCredentials"];
            SearchLog sl = new SearchLog();
            SuccessDetailsResponse successDetails = new SuccessDetailsResponse();
            TokenManagerModels token = new TokenManagerModels();
            //for storage of login details

            try
            {
                var loginDetails = (BKBNIDVerify.Models.LoginModels)Session["LoginCredentials"];

                sl.branchCode = loginDetails.BranchCode;
                sl.DOB = (GNID.dob).ToString("yyyy-MM-dd");
                sl.NID = GNID.nid;
                sl.createdBy = loginDetails.BranchCode;
                sl.createdOn = System.DateTime.Now;
                db.SearchLogs.Add(sl);
                db.SaveChanges();
            }

            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        logger.Error("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                TempData["error"] = ex.Message;
            }


            try
            {

                var httpWebRequestD = (HttpWebRequest)WebRequest.Create("https://prportal.nidw.gov.bd/partner-service/rest/voter/details");
                httpWebRequestD.ContentType = "application/json";
                httpWebRequestD.Method = "POST";
                httpWebRequestD.Headers.Add("Authorization", "Bearer " + token.GetToken());
                string dateofbirth = (GNID.dob).ToString("yyyy-MM-dd");

                using (var streamWriter = new StreamWriter(httpWebRequestD.GetRequestStream()))
                {
                    string json = "";
                    if (GNID.nid.Length == 17)
                    {
                        json = new JavaScriptSerializer().Serialize(new
                        {
                            dateOfBirth = dateofbirth,
                            nid17Digit = GNID.nid
                        });
                    }
                    else
                    {
                        json = new JavaScriptSerializer().Serialize(new
                        {
                            dateOfBirth = dateofbirth,
                            nid10Digit = GNID.nid
                        });
                    }
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponseDetails = (HttpWebResponse)httpWebRequestD.GetResponse();


                using (var streamReader = new StreamReader(httpResponseDetails.GetResponseStream()))
                {

                    var result = streamReader.ReadToEnd();
                    logger.Debug(result);
                    if (httpResponseDetails.StatusCode.ToString() == "OK")
                    {
                        successDetails = JsonConvert.DeserializeObject<SuccessDetailsResponse>(result);
                        PermanentAddress pa = new PermanentAddress();
                        pa = successDetails.success.data.permanentAddress;
                        PresentAddress pra = new PresentAddress();
                        pra = successDetails.success.data.presentAddress;


                        NIDDetails c = new NIDDetails();
                        c = successDetails.success.data;
                        c.presentAddress = pra;
                        c.permanentAddress = pa;

                        NIDDataSuccess d = new NIDDataSuccess();
                        d.data = c;

                        SuccessDetailsResponse e = new SuccessDetailsResponse();
                        e.success = d;

                        //Code for downloading image
                        string remoteFileUrl = e.success.data.photo;
                        var Id = e.success.data.pin;
                        //var image = "image";
                        var destinationExt = ".jpg";
                        var destinationFileName = Id + destinationExt;
                        //stores images in the project folder
                        var destinationDir = AppDomain.CurrentDomain.BaseDirectory + "NIDImages\\"; ;
                        //stores images in IIS Express directory
                        //var destinationDir = Environment.CurrentDirectory + "\\NIDImages\\";
                        var destinationPath = destinationDir + destinationFileName;
                        if (!Directory.Exists(destinationDir))
                        {
                            Directory.CreateDirectory(destinationDir);
                        }
                        var myWebClient = new WebClient();
                        myWebClient.BaseAddress = "https://prportal.nidw.gov.bd/partner-service/rest/voter/details";
                        //Download file from remote url and save it in destination dir
                        myWebClient.DownloadFile(remoteFileUrl, destinationPath);
                        // end of downloading image


                        //return Json(e.success.data.photo, JsonRequestBehavior.AllowGet);
                        return View("NidDetails", e);



                    }
                    else
                    {
                        FailureResponse errResponse = JsonConvert.DeserializeObject<FailureResponse>(result);
                        successDetails.status = "Status:" + errResponse.status + " Status Code:" + errResponse.statusCode;
                        TempData["error"] = "Status:" + errResponse.status + " Status Code:" + errResponse.statusCode;
                        return View();
                    }
                }

            }
            catch (Exception Ex)
            {
                logger.Debug(Ex.Message);
                successDetails.status = "Connection Error/ Unauthorized Access";
                TempData["error"] = Ex.Message;
                return View();
            }


        }




        public ActionResult GeneralReport(string Division, string Region, string Branch)
        {
            var login = (BKBNIDVerify.Models.LoginModels)Session["LoginCredentials"];



            if (login != null)
            {
                try
                {


                    HierarchyReportModel vm = new HierarchyReportModel();
                    var queryDiv = from i in db.SearchLogs
                                   join b in db1.Branches on i.branchCode equals b.branch_code
                                   where i.branchCode == b.branch_code

                                   select new
                                   {
                                       value = i.branchCode,
                                       description = b.branch_name
                                   };


                    vm.Branch_Name = Branch;
                    return View(vm);
                }
                catch (Exception ex)
                {
                    TempData["retMsg"] = "Error" + ex.Message;
                    return View();
                }

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Report()
        {
            BKBNIDDBEntities dBEntities = new BKBNIDDBEntities();
            //Covid19LoanDBEntities cdBEntities = new Covid19LoanDBEntities();

            //    var queryString = from c in db.SearchLogs select sum(c.branchCode) group by c.branchCode;
            var login = (BKBNIDVerify.Models.LoginModels)Session["LoginCredentials"];

            if (login != null)
            {
                return Redirect("/ReportFile/RDLC/Report.aspx");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

    }
}


