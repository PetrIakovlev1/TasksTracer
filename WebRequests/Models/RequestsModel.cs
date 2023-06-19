using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Web.Mvc;

namespace WebRequests.Models
{
    public class RequestsModel
    {
        public IEnumerable<SelectListItem> FilterList { get; set; }

        [Display(Name = "Show requests:")]
        public string SelectedFilter { get; set; }

        public DataTable dt { get; set; }

        public List<NewRequestModel> newRequest { get; set; }
    }

    public class NewRequestModel
    {
        public string SessionGuid { get; set; }

        [Display(Name = "#")]
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Request Title")]
        public string RequestName { get; set; }

        [Required]
        [StringLength(1000)]
        [Display(Name = "Request Description")]
        public string RequestDescription { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Requestor")]
        public string Requestor { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Business Owner")]
        public string BusinessOwner { get; set; }

        [Display(Name = "Assigned To")]
        [StringLength(255)]
        public string AssignedTo { get; set; }

        [Display(Name = "On Hold")]
        public bool OnHold { get; set; }

        [Display(Name = "Approved")]
        public bool Approved { get; set; }

        [StringLength(1000)]
        [Display(Name = "Link to Existing Report/App")]
        public string LinkToExistingReport { get; set; }

        [StringLength(1000)]
        [Display(Name = "Business Impact Description")]
        public string BusinessImpactDescription { get; set; }

        [Display(Name = "Attached Files")]
        public List<AttachedFile> AttachedFiles { get; set; }

        [StringLength(255)]
        [Display(Name = "Tactical Project Name")]
        public string TacticalProjectName { get; set; }

        [StringLength(500)]
        [Display(Name = "Hold Description")]
        public string HoldDescription { get; set; }

        [Display(Name = "Attachement_id")]
        public string Attachement_id { get; set; }

        [Display(Name = "CreatedBy")]
        public string CreatedBy { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Desired Due Date")]
        public DateTime? DesiredDueDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Promise Date")]
        public DateTime? PromiseDate { get; set; }

        //================================
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Star tDate")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "User Test Start")]
        public DateTime? UserTestStart { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "User Test End")]
        public DateTime? UserTestEnd { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Cancelled Date")]
        public DateTime? CancelledDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Completion Date")]
        public DateTime? CompletionDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Created Date")]
        public DateTime? CreatedDate { get; set; }


        [Display(Name = "Link to External DataSource")]
        public string LinkToExternalDS { get; set; }

        public IEnumerable<SelectListItem> BUList { get; set; }

        [Display(Name = "Business Unit")]
        public string SelectedBU { get; set; }

        public IEnumerable<SelectListItem> BusinessImpactList { get; set; }

        [Display(Name = "Business Impact")]
        public string SelectedBusinessImpact { get; set; }

        public IEnumerable<SelectListItem> ComplexityList { get; set; }

        [Display(Name = "Complexity")]
        public string SelectedComplexity { get; set; }

        [Display(Name = "Development Hours")]
        public int DevelopmentHours { get; set; }

        public IEnumerable<SelectListItem> DataSourceList { get; set; }

        [Display(Name = "Data Source")]
        public string SelectedDataSource { get; set; }

        public IEnumerable<SelectListItem> FunctionalAreaList { get; set; }

        [Display(Name = "Functional Area")]
        public string SelectedFunctionalArea { get; set; }

        public IEnumerable<SelectListItem> PriorityList { get; set; }

        [Display(Name = "Priority")]
        public string SelectedPriority { get; set; }

        public IEnumerable<SelectListItem> RequestTypeList { get; set; }

        [Display(Name = "Request Type")]
        public string SelectedRequestType { get; set; }

        public IEnumerable<SelectListItem> StatusList { get; set; }

        [Display(Name = "Status")]
        public string SelectedStatus { get; set; }

        public IEnumerable<SelectListItem> WEList { get; set; }

        [Display(Name = "WE")]
        public string SelectedWE { get; set; }

        public IEnumerable<SelectListItem> TacticalProjectList { get; set; }

        [Display(Name = "Tactical Project (Y/N)")]
        public string SelectedTacticalProject { get; set; }


        //Select according DataSource values
        public IEnumerable<SelectListItem> ReportAreaList { get; set; }

        [Display(Name = "Report Area")]
        public string SelectedReportArea { get; set; }

    }

    public class ReportArea
    {
        public IEnumerable<SelectListItem> ReportAreaList { get; set; }

        [Display(Name = "Report Area")]
        public string SelectedReportArea { get; set; }

    }

    public class AttachedFile
    {
        public byte[] FileBody { get; set; }
        public FileInfo fileInfo { get; set; }
        public string fileGuid { get; set; }
    }

    public class AdUserInfo
    {
        public string UserName { get; set; }
        public string Email { get; set; }

    }

    public class UploadResult
    {
        public string Message { get; set; }
    }

}