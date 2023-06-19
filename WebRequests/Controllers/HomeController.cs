using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebRequests.DAL;
using WebRequests.DAL.Common;
using WebRequests.Models;

namespace WebRequests.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            RequestsModel requestsModel = new RequestsModel();
            DataTable dt = sqlReader.GetOrderList("all");
            requestsModel.FilterList = sqlReader.getFilterList();
            requestsModel.newRequest = fromDtToModel(dt);

            return View(requestsModel);
        }


        [HttpPost]
        public ActionResult Index(string filterName)
        {
            if (string.IsNullOrWhiteSpace(filterName))
                filterName = "all";

            RequestsModel requestsModel = new RequestsModel();
            DataTable dt = sqlReader.GetOrderList(filterName);
            requestsModel.FilterList = sqlReader.getFilterList();
            requestsModel.newRequest = fromDtToModel(dt);

            return View(requestsModel);
        }


        [HttpGet]
        public PartialViewResult ShowPartialRequestList()
        {
            RequestsModel requestsModel = new RequestsModel();
            DataTable dt = sqlReader.GetOrderList("all");
            requestsModel.newRequest = fromDtToModel(dt);

            return PartialView(requestsModel);
        }

        [HttpPost]
        public PartialViewResult ShowPartialRequestList(string filterName)
        {
            if (string.IsNullOrWhiteSpace(filterName))
                filterName = "all";
            RequestsModel requestsModel = new RequestsModel();
            DataTable dt = sqlReader.GetOrderList(filterName);
            requestsModel.newRequest = fromDtToModel(dt);

            return PartialView(requestsModel);
        }



        public ActionResult NewRequest()
        {
            NewRequestModel newRequestModel = sqlReader.GetAllDictionaries();
            newRequestModel.DesiredDueDate = DateTime.Now.AddDays(2);
            newRequestModel.SessionGuid = Guid.NewGuid().ToString();

            return View(newRequestModel);
        }

        [HttpGet]
        public PartialViewResult ShowPartialReportArea()
        {
            ReportArea reportArea = new ReportArea();
            if (Request.Url.Segments.Length > 3)
            {
                int requestId = ConvertFunctions.GetConvertedToIntValue(Request.Url.Segments[3]);
                reportArea.SelectedReportArea = ConvertFunctions.GetConvertedToString(sqlReader.GetSelectedReportArea(requestId));
            }
            else
            {
                DataTable dt = sqlReader.GetDtBySPandId("uspGetReportArea", 1);
                reportArea.ReportAreaList = sqlReader.getItemList(dt, "tblReportArea");
            }

            return PartialView(reportArea);
        }

        [HttpPost]
        public PartialViewResult ShowPartialReportArea(int SelectedDataSource, int requestId)
        {
            ReportArea reportArea = new ReportArea();

            DataTable dt = sqlReader.GetDtBySPandId("uspGetReportArea", SelectedDataSource);
            reportArea.ReportAreaList = sqlReader.getItemList(dt, "tblReportArea");
            if (requestId > 0)
                reportArea.SelectedReportArea = ConvertFunctions.GetConvertedToString(sqlReader.GetSelectedReportArea(requestId));
            else
                reportArea.SelectedReportArea = 1.ToString();

            return PartialView(reportArea);
        }


        [HttpGet]
        public ActionResult ShowRequestor()
        {
            List<AdUserInfo> AdUserList = adReader.GetAdUserList(string.Empty).Take(20).ToList();

            return PartialView(AdUserList);
        }

        [HttpPost]
        public ActionResult ShowRequestor(string SearchString)
        {
            List<AdUserInfo> AdUserList = adReader.GetAdUserList(SearchString).Take(20).ToList();
            return Json(AdUserList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveRequest(NewRequestModel newRequestModel)
        {

            string resultMessage = string.Empty;

            if (newRequestModel.id == 0)
            {
                int titleRecordId = -1;
                resultMessage = sqlWriter.saveNewRequestTitle(newRequestModel, ref titleRecordId);
                resultMessage += sqlWriter.updateIdForAttachedFiles(newRequestModel.SessionGuid, newRequestModel.id);
                sqlReader.SendEmailToExecutor("EUMSolCustomerCare.Reporting@emerson.com");

                sqlReader.SendEmailForApprove(newRequestModel);

            }
            else
            {
                resultMessage = sqlWriter.saveEditedRequest(newRequestModel);

                if (!sqlReader.GetIsTaskApproved(newRequestModel.id))
                    sqlReader.SendEmailForApprove(newRequestModel);

                if (!string.IsNullOrEmpty(newRequestModel.AssignedTo))
                    sqlReader.SendEmailToExecutor(newRequestModel.AssignedTo);
            }


            return RedirectToAction("Index");

        }

        public ActionResult SendForApprove(NewRequestModel newRequestModel)
        {
            sqlReader.SendEmailForApprove(newRequestModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ShowPartialUploadFiles(UploadResult uploadResult)
        {
            return PartialView(uploadResult.Message);
        }

        [HttpPost]
        public ActionResult ShowPartialUploadFiles()
        {
            UploadResult uploadResult = new UploadResult();
            uploadResult.Message = string.Empty;

            HttpFileCollectionBase files = Request.Files;
            string SessionGuid = Request.Form.Count > 0 ? Request.Form[0] : string.Empty;

            if (!string.IsNullOrWhiteSpace(SessionGuid))
            {
                List<AttachedFile> fileList = new List<AttachedFile>();
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase fileData;
                    for (int i = 0; i < files.Count; i++)
                    {
                        fileData = files[i];
                        if (fileData != null && fileData.ContentLength > 0)
                        {
                            byte[] fileBuffer = new byte[fileData.ContentLength];
                            fileData.InputStream.Read(fileBuffer, 0, fileBuffer.Length);

                            AttachedFile file = new AttachedFile();
                            file.fileInfo = new FileInfo(fileData.FileName);
                            file.FileBody = fileBuffer;

                            fileList.Add(file);
                        }
                    }
                }

                uploadResult.Message += sqlWriter.saveAttachedFiles(fileList, SessionGuid);
            }
            else
            {
                uploadResult.Message = "Error: Session GUID is empty";
            }
            return Json(uploadResult.Message, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult ShowPartialDeleteFile()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult ShowPartialDeleteFile(string sessionID, string fileName)
        {
            UploadResult uploadResult = new UploadResult();
            uploadResult.Message = sqlWriter.deleteAttachedFile(fileName, sessionID);

            return Json(uploadResult.Message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteExistAttachment(string requestId, string fileName)
        {
            UploadResult uploadResult = new UploadResult();
            uploadResult.Message = sqlWriter.DeleteExistAttachment(requestId, fileName);

            return Json(uploadResult.Message, JsonRequestBehavior.AllowGet);
        }



        public ActionResult EditRequest(int id)
        {
            NewRequestModel editRequestModel = sqlReader.GetAllDictionaries();

            DataTable dt = sqlReader.GetDtBySPandId("uspGetRequestById", id);
            fromDtToModelEdit(dt, ref editRequestModel);

            DataTable dt2 = sqlReader.GetDtBySPandId("uspGetAttachementsById", id);
            editRequestModel.AttachedFiles = fromDtToModelFiles(dt2);

            return View(editRequestModel);
        }


        public ActionResult DeleteRequest(int requestId)
        {

            sqlReader.GetDtBySPandId("uspDeleteRequest", requestId);

            return RedirectToAction("Index");
        }



        List<NewRequestModel> fromDtToModel(DataTable dt)
        {
            List<NewRequestModel> newRequestModelList = new List<NewRequestModel>();

            if (dt?.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    NewRequestModel newRequestModel = new NewRequestModel();

                    newRequestModel.id = ConvertFunctions.GetConvertedToIntValue(dr["id"]);
                    newRequestModel.RequestName = ConvertFunctions.GetConvertedToString(dr["RequestName"]);
                    newRequestModel.Requestor = ConvertFunctions.GetConvertedToString(dr["Requestor"]);
                    newRequestModel.BusinessOwner = ConvertFunctions.GetConvertedToString(dr["BusinessOwner"]);
                    newRequestModel.SelectedBusinessImpact = ConvertFunctions.GetConvertedToString(dr["BI_Name"]);
                    newRequestModel.SelectedTacticalProject = ConvertFunctions.GetConvertedToString(dr["TacticalProjectFlag"]);
                    newRequestModel.DesiredDueDate = ConvertFunctions.GetConvertedToDateTimeValue(dr["DesiredDueDate"]);
                    newRequestModel.SelectedPriority = ConvertFunctions.GetConvertedToString(dr["Priority_Name"]);
                    newRequestModel.AssignedTo = ConvertFunctions.GetConvertedToString(dr["AssignedTo"]);
                    newRequestModel.OnHold = ConvertFunctions.GetConvertedToBoolValue(dr["OnHold"]);
                    newRequestModel.Approved = ConvertFunctions.GetConvertedToBoolValue(dr["Approved"]);
                    newRequestModel.PromiseDate = ConvertFunctions.GetConvertedToDateTimeValue(dr["PromiseDate"]);
                    newRequestModel.CreatedDate = ConvertFunctions.GetConvertedToDateTimeValue(dr["CreatedDate"]);

                    newRequestModelList.Add(newRequestModel);
                }
            }

            return newRequestModelList;
        }

        void fromDtToModelEdit(DataTable dt, ref NewRequestModel editRequestModel)
        {
            if (dt?.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    editRequestModel.id = ConvertFunctions.GetConvertedToIntValue(dr["id"]);
                    editRequestModel.SessionGuid = ConvertFunctions.GetConvertedToString(dr["RequestGuid"]);

                    editRequestModel.RequestName = ConvertFunctions.GetConvertedToString(dr["RequestName"]);
                    editRequestModel.Requestor = ConvertFunctions.GetConvertedToString(dr["Requestor"]);
                    editRequestModel.BusinessOwner = ConvertFunctions.GetConvertedToString(dr["BusinessOwner"]);
                    editRequestModel.SelectedBusinessImpact = ConvertFunctions.GetConvertedToString(dr["BusinessImpact_id"]);
                    editRequestModel.SelectedTacticalProject = ConvertFunctions.GetConvertedToString(dr["TacticalProject_id"]);
                    editRequestModel.DesiredDueDate = ConvertFunctions.GetConvertedToDateTimeValue(dr["DesiredDueDate"]);
                    editRequestModel.SelectedPriority = ConvertFunctions.GetConvertedToString(dr["Priority_id"]);
                    editRequestModel.AssignedTo = ConvertFunctions.GetConvertedToString(dr["AssignedTo"]);
                    editRequestModel.OnHold = ConvertFunctions.GetConvertedToBoolValue(dr["OnHold"]);
                    editRequestModel.Approved = ConvertFunctions.GetConvertedToBoolValue(dr["Approved"]);
                    editRequestModel.PromiseDate = ConvertFunctions.GetConvertedToDateTimeValue(dr["PromiseDate"]);

                    editRequestModel.RequestDescription = ConvertFunctions.GetConvertedToString(dr["RequestDescription"]);
                    editRequestModel.SelectedBU = ConvertFunctions.GetConvertedToString(dr["BU_id"]);
                    editRequestModel.SelectedFunctionalArea = ConvertFunctions.GetConvertedToString(dr["FunctionalArea_id"]);
                    editRequestModel.SelectedWE = ConvertFunctions.GetConvertedToString(dr["WE_id"]);
                    editRequestModel.SelectedRequestType = ConvertFunctions.GetConvertedToString(dr["RequestType_id"]);
                    editRequestModel.LinkToExistingReport = ConvertFunctions.GetConvertedToString(dr["LinkToExistingReport"]);
                    editRequestModel.SelectedDataSource = ConvertFunctions.GetConvertedToString(dr["DataSource_id"]);
                    editRequestModel.SelectedReportArea = ConvertFunctions.GetConvertedToString(dr["ReportArea_id"]);
                    editRequestModel.LinkToExternalDS = ConvertFunctions.GetConvertedToString(dr["LinkToExternalDataSource"]);
                    editRequestModel.BusinessImpactDescription = ConvertFunctions.GetConvertedToString(dr["BusinessImpactDescription"]);
                    editRequestModel.TacticalProjectName = ConvertFunctions.GetConvertedToString(dr["TacticalProjectName"]);
                    editRequestModel.SelectedStatus = ConvertFunctions.GetConvertedToString(dr["Status_id"]);
                    editRequestModel.SelectedComplexity = ConvertFunctions.GetConvertedToString(dr["Complexity_id"]);
                    editRequestModel.DevelopmentHours = ConvertFunctions.GetConvertedToIntValue(dr["DevelopmentHours"]);
                    editRequestModel.HoldDescription = ConvertFunctions.GetConvertedToString(dr["HoldDescription"]);

                    editRequestModel.StartDate = ConvertFunctions.GetConvertedToDateTimeValue(dr["StartDate"]);
                    editRequestModel.UserTestStart = ConvertFunctions.GetConvertedToDateTimeValue(dr["UserTestStart"]);
                    editRequestModel.UserTestEnd = ConvertFunctions.GetConvertedToDateTimeValue(dr["UserTestEnd"]);
                    editRequestModel.CancelledDate = ConvertFunctions.GetConvertedToDateTimeValue(dr["CancelledDate"]);
                    editRequestModel.CompletionDate = ConvertFunctions.GetConvertedToDateTimeValue(dr["CompletionDate"]);
                    editRequestModel.CreatedDate = ConvertFunctions.GetConvertedToDateTimeValue(dr["CreatedDate"]);
                }
            }

        }

        List<AttachedFile> fromDtToModelFiles(DataTable dt)
        {
            List<AttachedFile> fileList = new List<AttachedFile>();
            if (dt?.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AttachedFile attachedFile = new AttachedFile();
                    attachedFile.fileInfo = new FileInfo(ConvertFunctions.GetConvertedToString(dr["File_Info"]));

                    fileList.Add(attachedFile);

                }
            }

            return fileList;
        }

        public FileResult DownloadFile(string RequestId, string FileName)
        {
            DataTable FileDataDt = sqlReader.GetAttachedFileValue(RequestId, FileName);
            if (FileDataDt.Rows?.Count > 0)
            {
                byte[] bytes = (byte[])FileDataDt?.Rows[0][0];
                return File(bytes, "application/octet-stream", FileName);
            }
            else
                return null;
        }


        public ActionResult ApproveRequest(string requestGuid)
        {

            ApproveModel approveModel = new ApproveModel();
            approveModel.RequestGuid = requestGuid;

            if (!string.IsNullOrWhiteSpace(requestGuid))
            {
                approveModel.ApproveMessage = sqlWriter.approveReques(requestGuid);
                string AssignedTo = sqlReader.GetExecutorName(requestGuid);

                if (!string.IsNullOrEmpty(AssignedTo))
                    sqlReader.SendEmailToExecutor(AssignedTo);
            }


            return View(approveModel);
        }

    }
}