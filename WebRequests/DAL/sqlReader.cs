using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using WebRequests.Models;

namespace WebRequests.DAL
{
    public static class sqlReader
    {
        public static DataTable GetOrderList(string filterName)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["sqlReader"].ConnectionString;
            DataTable oOutDt = new DataTable();
            oOutDt.TableName = $"tbl-{filterName}";

            if (!string.IsNullOrWhiteSpace(filterName))
            {
                using (var con = new SqlConnection(connectionstring))
                using (var cmd = new SqlCommand("uspGetRequestList", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@filterName", SqlDbType.VarChar, 50).Value = filterName;
                    con.Open();
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(oOutDt);
                    }
                }
            }

            return oOutDt;
        }

        public static IEnumerable<SelectListItem> getFilterList()
        {
            List<SelectListItem> FormatList = new List<SelectListItem>();

            DataTable dt = GetDtBySP("uspGetRequestTypeForFilter");

            foreach (DataRow dr in dt.Rows)
            {
                SelectListItem selectListItem = new SelectListItem();
                selectListItem.Text = dr[0]?.ToString();
                selectListItem.Value = dr[1]?.ToString();

                FormatList.Add(selectListItem);
            }

            return FormatList;
        }

        public static DataTable GetDtBySP(string spName)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["sqlReader"].ConnectionString;
            DataTable oOutDt = new DataTable();
            oOutDt.TableName = "tbl" + spName;

            if (!string.IsNullOrWhiteSpace(spName))
            {
                using (var con = new SqlConnection(connectionstring))
                using (var cmd = new SqlCommand(spName, con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(oOutDt);
                    }
                }
            }

            return oOutDt;
        }

        public static NewRequestModel GetAllDictionaries()
        {
            NewRequestModel newRequestModel = new NewRequestModel();
            DataTable dt = GetDtBySP("uspGetAllDictionaries");

            newRequestModel.BUList = getItemList(dt, "tblBU");
            newRequestModel.BusinessImpactList = getItemList(dt, "tblBusinessImpact");
            newRequestModel.ComplexityList = getItemList(dt, "tblComplexity");
            newRequestModel.DataSourceList = getItemList(dt, "tblDataSource");
            newRequestModel.FunctionalAreaList = getItemList(dt, "tblFunctionalArea");
            newRequestModel.PriorityList = getItemList(dt, "tblPriority");
            newRequestModel.RequestTypeList = getItemList(dt, "tblRequestType");
            newRequestModel.StatusList = getItemList(dt, "tblStatus");
            newRequestModel.WEList = getItemList(dt, "tblWE");
            newRequestModel.TacticalProjectList = getItemList(dt, "tblTacticalProject");

            return newRequestModel;
        }

        public static IEnumerable<SelectListItem> getItemList(DataTable dt, string TableName)
        {
            List<SelectListItem> ItemList = new List<SelectListItem>();

            foreach (DataRow dr in dt.Rows)
            {
                SelectListItem selectListItem = new SelectListItem();
                selectListItem.Text = dr[1]?.ToString();
                selectListItem.Value = dr[0]?.ToString();

                if (dr[2].ToString().Contains(TableName))
                    ItemList.Add(selectListItem);
            }

            return ItemList;
        }

        public static DataTable GetDtBySPandId(string spName, int id)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["sqlReader"].ConnectionString;
            DataTable oOutDt = new DataTable();
            oOutDt.TableName = $"tbl";

            using (var con = new SqlConnection(connectionstring))
            using (var cmd = new SqlCommand(spName, con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                if (id >= 0)
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                con.Open();
                using (var da = new SqlDataAdapter(cmd))
                {
                    da.Fill(oOutDt);
                }
            }

            return oOutDt;
        }

        public static DataTable GetAttachedFileValue(string requestId, string fileName)
        {
            DataTable oOutDt = new DataTable();
            string connectionstring = ConfigurationManager.ConnectionStrings["sqlReader"].ConnectionString;

            using (var con = new SqlConnection(connectionstring))
            using (var cmd = new SqlCommand("uspGetAttachedFileValue", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@requestId", SqlDbType.Int).Value = requestId;
                cmd.Parameters.Add("@fileName", SqlDbType.NVarChar, 255).Value = fileName;
                con.Open();
                using (var da = new SqlDataAdapter(cmd))
                {
                    da.Fill(oOutDt);
                }
            }

            return oOutDt;
        }

        public static void SendEmailToExecutor(string targetEmailAddress)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["sqlReader"].ConnectionString;

            using (var con = new SqlConnection(connectionstring))
            using (var cmd = new SqlCommand("uspSendEmail_newTask", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@targetEmailAddress", SqlDbType.VarChar, 255).Value = targetEmailAddress;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }
        public static void SendEmailForApprove(NewRequestModel requestModel)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["sqlReader"].ConnectionString;

            using (var con = new SqlConnection(connectionstring))
            using (var cmd = new SqlCommand("uspSendEmail_ApproveTask", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@targetEmailAddress", SqlDbType.VarChar, 255).Value = requestModel.BusinessOwner;
                cmd.Parameters.Add("@requestGuid", SqlDbType.VarChar, 50).Value = requestModel.SessionGuid;
                cmd.Parameters.Add("@taskNumber", SqlDbType.Int).Value = requestModel.id;
                cmd.Parameters.Add("@taskName", SqlDbType.VarChar, 50).Value = requestModel.RequestName;
                cmd.Parameters.Add("@taskDescription", SqlDbType.VarChar, 1000).Value = requestModel.RequestDescription;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public static bool GetIsTaskApproved(int taskId)
        {
            bool IsTaskApproved = false;
            string connectionstring = ConfigurationManager.ConnectionStrings["sqlReader"].ConnectionString;

            using (var con = new SqlConnection(connectionstring))
            using (var cmd = new SqlCommand("uspGetIsTaskApproved", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@requestId", SqlDbType.Int).Value = taskId;
                cmd.Parameters.Add("@IsTaskApproved", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteScalar();
                IsTaskApproved = Common.ConvertFunctions.GetConvertedToBoolValue(cmd.Parameters["@IsTaskApproved"].Value);
            }

            return IsTaskApproved;
        }

        public static string GetExecutorName(string requestGuid)
        {
            string ExecutorName = string.Empty;
            string connectionstring = ConfigurationManager.ConnectionStrings["sqlReader"].ConnectionString;

            using (var con = new SqlConnection(connectionstring))
            using (var cmd = new SqlCommand("uspGetTaskExecutor", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@requestGuid", SqlDbType.VarChar, 50).Value = requestGuid;
                cmd.Parameters.Add("@taskExecutor", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteScalar();
                ExecutorName = Common.ConvertFunctions.GetConvertedToString(cmd.Parameters["@taskExecutor"].Value);
            }

            return ExecutorName;
        }

        public static int GetSelectedReportArea(int requestId)
        {
            int SelectedReportArea = -1;
            string connectionstring = ConfigurationManager.ConnectionStrings["sqlReader"].ConnectionString;

            using (var con = new SqlConnection(connectionstring))
            using (var cmd = new SqlCommand("uspGetSelectedReportArea", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@requestid", SqlDbType.Int).Value = requestId;
                cmd.Parameters.Add("@selectedReportArea", SqlDbType.Int).Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteScalar();
                SelectedReportArea = Common.ConvertFunctions.GetConvertedToIntValue(cmd.Parameters["@selectedReportArea"].Value);
            }

            return SelectedReportArea;
        }
    }
}