using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using WebRequests.DAL.Common;
using WebRequests.Models;

namespace WebRequests.DAL
{
    public static class sqlWriter
    {
        public static string saveNewRequestTitle(NewRequestModel newRequestModel, ref int recordId)
        {
            string sReturnedMessage = string.Empty;
            string connectionstring = ConfigurationManager.ConnectionStrings["sqlReader"].ConnectionString;

            SqlDataReader reader;
            using (var con = new SqlConnection(connectionstring))
            using (var cmd = new SqlCommand("uspSaveNewRequest", con))
            {
                //cmd.CommandTimeout = 60;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@requestName", SqlDbType.VarChar, 255).Value = newRequestModel.RequestName;
                cmd.Parameters.Add("@requestDescription", SqlDbType.VarChar, 1000).Value = newRequestModel.RequestDescription;
                cmd.Parameters.Add("@requestor", SqlDbType.VarChar, 255).Value = newRequestModel.Requestor;
                cmd.Parameters.Add("@bU_id", SqlDbType.Int).Value = newRequestModel.SelectedBU;
                cmd.Parameters.Add("@functionalArea_id", SqlDbType.Int).Value = newRequestModel.SelectedFunctionalArea;
                cmd.Parameters.Add("@businessOwner", SqlDbType.VarChar, 255).Value = newRequestModel.BusinessOwner;
                cmd.Parameters.Add("@wE_id", SqlDbType.Int).Value = newRequestModel.SelectedWE;
                cmd.Parameters.Add("@requestType_id", SqlDbType.Int).Value = newRequestModel.SelectedRequestType;
                cmd.Parameters.Add("@linkToExistingReport", SqlDbType.VarChar, 1000).Value =(object)newRequestModel.LinkToExistingReport ?? DBNull.Value;
                cmd.Parameters.Add("@dataSource_id", SqlDbType.Int).Value = newRequestModel.SelectedDataSource;
                cmd.Parameters.Add("@reportArea_id", SqlDbType.Int).Value = newRequestModel.SelectedReportArea;
                cmd.Parameters.Add("@linkToExternalDataSource", SqlDbType.VarChar, 1000).Value = (object)newRequestModel.LinkToExternalDS ?? DBNull.Value;
                cmd.Parameters.Add("@businessImpact_id", SqlDbType.Int).Value = newRequestModel.SelectedBusinessImpact;
                cmd.Parameters.Add("@businessImpactDescription", SqlDbType.VarChar, 1000).Value = newRequestModel.BusinessImpactDescription;
                cmd.Parameters.Add("@tacticalProject_id", SqlDbType.Int).Value = newRequestModel.SelectedTacticalProject;
                cmd.Parameters.Add("@tacticalProjectName", SqlDbType.VarChar, 255).Value = (object)newRequestModel.TacticalProjectName ?? DBNull.Value;
                cmd.Parameters.Add("@desiredDueDate", SqlDbType.Date).Value = newRequestModel.DesiredDueDate;
                cmd.Parameters.Add("@sessionGuid", SqlDbType.VarChar, 50).Value = newRequestModel.SessionGuid;

                cmd.Parameters.Add("@ErrorMessage", SqlDbType.NVarChar, 2000).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@record_id", SqlDbType.Int).Direction = ParameterDirection.Output;

                try
                {
                    con.Open();
                    reader = cmd.ExecuteReader();
                    sReturnedMessage = Convert.ToString(cmd.Parameters["@ErrorMessage"].Value);
                    recordId = ConvertFunctions.GetConvertedToIntValue(cmd.Parameters["@record_id"].Value);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    sReturnedMessage = ex.Message;
                }
            }

            return sReturnedMessage;
        }

        public static string saveEditedRequest(NewRequestModel newRequestModel)
        {
            string sReturnedMessage = string.Empty;
            string connectionstring = ConfigurationManager.ConnectionStrings["sqlReader"].ConnectionString;

            SqlDataReader reader;
            using (var con = new SqlConnection(connectionstring))
            using (var cmd = new SqlCommand("uspSaveEditedRequest", con))
            {
                //cmd.CommandTimeout = 60;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@id", SqlDbType.Int).Value = newRequestModel.id;
                cmd.Parameters.Add("@sessionGuid", SqlDbType.VarChar, 50).Value = newRequestModel.SessionGuid;

                cmd.Parameters.Add("@requestName", SqlDbType.VarChar, 255).Value = newRequestModel.RequestName;
                cmd.Parameters.Add("@requestDescription", SqlDbType.VarChar, 1000).Value = newRequestModel.RequestDescription;
                cmd.Parameters.Add("@requestor", SqlDbType.VarChar, 255).Value = newRequestModel.Requestor;
                cmd.Parameters.Add("@bU_id", SqlDbType.Int).Value = newRequestModel.SelectedBU;
                cmd.Parameters.Add("@functionalArea_id", SqlDbType.Int).Value = newRequestModel.SelectedFunctionalArea;
                cmd.Parameters.Add("@businessOwner", SqlDbType.VarChar, 255).Value = newRequestModel.BusinessOwner;
                cmd.Parameters.Add("@wE_id", SqlDbType.Int).Value = newRequestModel.SelectedWE;
                cmd.Parameters.Add("@requestType_id", SqlDbType.Int).Value = newRequestModel.SelectedRequestType;
                cmd.Parameters.Add("@linkToExistingReport", SqlDbType.VarChar, 1000).Value = (object)newRequestModel.LinkToExistingReport ?? DBNull.Value;
                cmd.Parameters.Add("@dataSource_id", SqlDbType.Int).Value = newRequestModel.SelectedDataSource;
                cmd.Parameters.Add("@reportArea_id", SqlDbType.Int).Value = newRequestModel.SelectedReportArea;
                cmd.Parameters.Add("@linkToExternalDataSource", SqlDbType.VarChar, 1000).Value = (object)newRequestModel.LinkToExternalDS ?? DBNull.Value;
                cmd.Parameters.Add("@businessImpact_id", SqlDbType.Int).Value = newRequestModel.SelectedBusinessImpact;
                cmd.Parameters.Add("@businessImpactDescription", SqlDbType.VarChar, 1000).Value = newRequestModel.BusinessImpactDescription;
                cmd.Parameters.Add("@tacticalProject_id", SqlDbType.Int).Value = newRequestModel.SelectedTacticalProject;
                cmd.Parameters.Add("@tacticalProjectName", SqlDbType.VarChar, 255).Value = (object)newRequestModel.TacticalProjectName ?? DBNull.Value;
                cmd.Parameters.Add("@desiredDueDate", SqlDbType.Date).Value = newRequestModel.DesiredDueDate;

                cmd.Parameters.Add("@status_id", SqlDbType.Int).Value = (object)newRequestModel.SelectedStatus ?? DBNull.Value;
                cmd.Parameters.Add("@priority_id", SqlDbType.Int).Value = (object)newRequestModel.SelectedPriority ?? DBNull.Value;
                cmd.Parameters.Add("@complexity_id", SqlDbType.Int).Value = (object)newRequestModel.SelectedComplexity ?? DBNull.Value;
                cmd.Parameters.Add("@developmentHours", SqlDbType.Int).Value = (object)newRequestModel.DevelopmentHours ?? DBNull.Value;
                cmd.Parameters.Add("@assignedTo", SqlDbType.VarChar, 255).Value = (object)newRequestModel.AssignedTo ?? DBNull.Value;
                cmd.Parameters.Add("@onHold", SqlDbType.Bit).Value = (object)newRequestModel.OnHold ?? DBNull.Value;
                cmd.Parameters.Add("@holdDescription", SqlDbType.VarChar, 500).Value = (object)newRequestModel.HoldDescription ?? DBNull.Value;
                cmd.Parameters.Add("@promiseDate", SqlDbType.Date).Value = (object)newRequestModel.PromiseDate ?? DBNull.Value;
                cmd.Parameters.Add("@startDate", SqlDbType.Date).Value = (object)newRequestModel.StartDate ?? DBNull.Value;
                cmd.Parameters.Add("@userTestStart", SqlDbType.Date).Value = (object)newRequestModel.UserTestStart ?? DBNull.Value;
                cmd.Parameters.Add("@userTestEnd", SqlDbType.Date).Value = (object)newRequestModel.UserTestEnd ?? DBNull.Value;
                cmd.Parameters.Add("@cancelledDate", SqlDbType.Date).Value = (object)newRequestModel.CancelledDate ?? DBNull.Value;
                cmd.Parameters.Add("@completionDate", SqlDbType.Date).Value = (object)newRequestModel.CompletionDate ?? DBNull.Value;
                cmd.Parameters.Add("@approved", SqlDbType.VarChar, 255).Value = (object)newRequestModel.Approved ?? DBNull.Value;


                cmd.Parameters.Add("@ErrorMessage", SqlDbType.NVarChar, 2000).Direction = ParameterDirection.Output;

                try
                {
                    con.Open();
                    reader = cmd.ExecuteReader();
                    sReturnedMessage = Convert.ToString(cmd.Parameters["@ErrorMessage"].Value);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    sReturnedMessage = ex.Message;
                }
            }

            return sReturnedMessage;
        }

        public static string saveAttachedFiles(List<AttachedFile> fileList, string SessionGuid)
        {
            string sReturnedMessage = string.Empty;
            string connectionstring = ConfigurationManager.ConnectionStrings["sqlReader"].ConnectionString;

            DataTable dt = getDtFromList(fileList);

            SqlDataReader reader;
            using (var con = new SqlConnection(connectionstring))
            using (var cmd = new SqlCommand("uspSaveAttachements", con))
            {
                cmd.CommandTimeout = 180; //3min
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@attached_Files", SqlDbType.Structured).Value = dt;
                cmd.Parameters.Add("@sessionGuid", SqlDbType.VarChar, 50).Value = SessionGuid;
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.NVarChar, 2000).Direction = ParameterDirection.Output;

                try
                {
                    con.Open();
                    reader = cmd.ExecuteReader();
                    sReturnedMessage = Convert.ToString(cmd.Parameters["@ErrorMessage"].Value);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    sReturnedMessage = ex.Message;
                }
            }

            return sReturnedMessage;
        }

        public static string updateIdForAttachedFiles(string SessionGuid, int id)
        {
            string sReturnedMessage = string.Empty;
            string connectionstring = ConfigurationManager.ConnectionStrings["sqlReader"].ConnectionString;

            using (var con = new SqlConnection(connectionstring))
            using (var cmd = new SqlCommand("uspUpdateAttachements", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@SessionGuid", SqlDbType.VarChar, 50).Value = SessionGuid;
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.NVarChar, 2000).Direction = ParameterDirection.Output;

                try
                {
                    con.Open();
                    cmd.ExecuteScalar();
                    sReturnedMessage = Convert.ToString(cmd.Parameters["@ErrorMessage"].Value);
                }
                catch (Exception ex)
                {
                    sReturnedMessage = ex.Message;
                }
            }

            return sReturnedMessage;
        }

        public static string deleteAttachedFile(string fileName, string SessionGuid)
        {
            string sReturnedMessage = string.Empty;
            string connectionstring = ConfigurationManager.ConnectionStrings["sqlReader"].ConnectionString;

            using (var con = new SqlConnection(connectionstring))
            using (var cmd = new SqlCommand("uspDeleteFile", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@sessionGuid", SqlDbType.VarChar, 50).Value = SessionGuid;
                cmd.Parameters.Add("@fileName", SqlDbType.VarChar, 1000).Value = fileName;

                try
                {
                    con.Open();
                    cmd.ExecuteScalar();
                    con.Close();
                }
                catch (Exception ex)
                {
                    sReturnedMessage = ex.Message;
                }
            }

            return sReturnedMessage;
        }

        public static string DeleteExistAttachment(string requestId, string fileName)
        {
            string sReturnedMessage = string.Empty;
            string connectionstring = ConfigurationManager.ConnectionStrings["sqlReader"].ConnectionString;

            using (var con = new SqlConnection(connectionstring))
            using (var cmd = new SqlCommand("uspDeleteExistAttachment", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@requestId", SqlDbType.Int).Value = requestId;
                cmd.Parameters.Add("@fileName", SqlDbType.VarChar, 1000).Value = fileName;

                try
                {
                    con.Open();
                    cmd.ExecuteScalar();
                    con.Close();
                }
                catch (Exception ex)
                {
                    sReturnedMessage = ex.Message;
                }
            }

            return sReturnedMessage;
        }


        private static DataTable getDtFromList(List<AttachedFile> fileList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("File_Info", typeof(string));
            dt.Columns.Add("File_Value", typeof(byte[]));

            foreach (AttachedFile fl in fileList)
            {
                DataRow dr = dt.NewRow();
                dr[0] = fl.fileInfo.Name;
                dr[1] = fl.FileBody;

                dt.Rows.Add(dr);
            }
            dt.AcceptChanges();

            return dt;
        }

        public static string approveReques(string SessionGuid)
        {
            string sReturnedMessage = string.Empty;
            string connectionstring = ConfigurationManager.ConnectionStrings["sqlReader"].ConnectionString;

            using (var con = new SqlConnection(connectionstring))
            using (var cmd = new SqlCommand("uspApproveRequest", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@requestGuid", SqlDbType.VarChar, 50).Value = SessionGuid;
                cmd.Parameters.Add("@approveMessage", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

                try
                {
                    con.Open();
                    cmd.ExecuteScalar();
                    sReturnedMessage = Convert.ToString(cmd.Parameters["@approveMessage"].Value);
                }
                catch (Exception ex)
                {
                    sReturnedMessage = ex.Message;
                }
            }

            return sReturnedMessage;
        }
    }
}