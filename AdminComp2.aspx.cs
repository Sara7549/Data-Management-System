using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Configuration;
using System.Web.Configuration;

namespace WebApplication1
{
    public partial class AdminComp : System.Web.UI.Page
    {
        String connStr = WebConfigurationManager.ConnectionStrings["Milestone2DB_24"].ToString();

        
        protected void ViewCustomerProfiles(object sender, EventArgs e)
        {
            BindDataToGrid("SELECT * FROM allCustomerAccounts", CommandType.Text); 
        }

        
        protected void ViewStoresAndVouchers(object sender, EventArgs e)
        {
            BindDataToGrid("Select * from PhysicalStoreVouchers", CommandType.Text);
        }

        
        protected void ViewResolvedTickets(object sender, EventArgs e)
        {
            BindDataToGrid("SELECT * FROM allResolvedTickets", CommandType.Text); 
        }

        
        protected void ViewAccountsWithPlans(object sender, EventArgs e)
        {
            BindDataToGrid("Account_Plan", CommandType.StoredProcedure); 
        }

        
        protected void ViewPlanSubscriptions(object sender, EventArgs e)
        {
            string date = txtPlanDate.Text.Trim();
            string planId = txtPlanID.Text.Trim();

            if (string.IsNullOrEmpty(date) || string.IsNullOrEmpty(planId))
            {
                Response.Write("Please provide both Date and Plan ID.");
                return;
            }

            string query = "SELECT * FROM Account_Plan_date(@date, @planId)";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@planId", planId);
                BindDataToGrid(cmd);
            }
        }

        
        protected void ViewTotalUsage(object sender, EventArgs e)
        {
            string mobileNo = txtUsageMobileNo.Text.Trim();
            string fromDate = txtUsageFromDate.Text.Trim();

            if (string.IsNullOrEmpty(mobileNo) || string.IsNullOrEmpty(fromDate))
            {
                Response.Write("Please provide both Mobile No and From Date.");
                return;
            }

            string query = "SELECT * FROM Account_Usage_Plan(@mobileNo, @fromDate)";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@mobileNo", mobileNo);
                cmd.Parameters.AddWithValue("@fromDate", fromDate);
                BindDataToGrid(cmd);
            }
        }

       
        protected void RemoveBenefits(object sender, EventArgs e)
        {
            string mobileNo = txtRemoveMobileNo.Text.Trim();
            string planId = txtRemovePlanID.Text.Trim();

            if (string.IsNullOrEmpty(mobileNo) || string.IsNullOrEmpty(planId))
            {
                Response.Write("Please provide both Mobile No and Plan ID.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("Benefits_Account", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mobile_num", mobileNo);
                cmd.Parameters.AddWithValue("@plan_id", int.Parse(planId));

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    Response.Write(rowsAffected > 0
                        ? "Benefits removed successfully."
                        : "No benefits found to remove.");
                }
                catch (Exception ex)
                {
                    Response.Write($"Error: {ex.Message}");
                }
            }
        }

        
        protected void ViewSMSOffers(object sender, EventArgs e)
        {
            string mobileNo = txtSMSMobileNo.Text.Trim();

            if (string.IsNullOrEmpty(mobileNo))
            {
                Response.Write("Please provide a Mobile No.");
                return;
            }

            string query = "SELECT * FROM Account_SMS_Offers(@mobileNo)";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@mobileNo", mobileNo);
                BindDataToGrid(cmd);
            }
        }

        
        private void BindDataToGrid(string query, CommandType cmdType)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(query, conn) { CommandType = cmdType };
                conn.Open();
                GridViewData.DataSource = cmd.ExecuteReader();
                GridViewData.DataBind();
            }
        }

        
        protected void btnFetchAcceptedTransactions_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(txtAccountID.Text.Trim()))
            {
                Response.Write("Please provide a mobile number.");
                return;
            }

            if (!int.TryParse(txtAccountID.Text.Trim(), out int mobileNumber))
            {
                Response.Write("Please enter a valid numeric mobile number.");
                return;
            }

            string query = "Account_Payment_Points";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@mobile_num", mobileNumber);

                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) 
                            {
                                
                                int acceptedPayments = reader.IsDBNull(0) ? 0 : reader.GetInt32(0); 
                                int earnedPoints = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);     

                                lblAcceptedPayments.Text = $"Accepted payments: {acceptedPayments}, Earned points: {earnedPoints}";
                            }
                            else
                            {
                                lblAcceptedPayments.Text = "No transactions found for this mobile number.";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"Error: {ex.Message}");
                    }
                }
            }
        }


        
        private void BindDataToGrid(SqlCommand cmd)
        {
            using (SqlConnection conn = cmd.Connection)
            {
                conn.Open();
                GridViewData.DataSource = cmd.ExecuteReader();
                GridViewData.DataBind();
            }
        }

        
        protected void btnFetchWalletLink_Click(object sender, EventArgs e)
        {
            BindDataToGrid("Select * from CustomerWallet", CommandType.Text);
        }

       
        protected void btnFetchEShops_Click(object sender, EventArgs e)
        {
            BindDataToGrid("SELECT * FROM E_shopVouchers", CommandType.Text);
        }

        
        protected void btnFetchPayments_Click(object sender, EventArgs e)
        {
            BindDataToGrid("Select * from AccountPayments", CommandType.Text);
        }

       
        protected void btnFetchCashbacks_Click(object sender, EventArgs e)
        {
            BindDataToGrid("Select * from Num_of_cashback", CommandType.Text);
        }

        
        protected void btnCheckMobileWallet_Click(object sender, EventArgs e)
        {
            string mobileNumber = txtMobileNumber.Text.Trim();
            string query = "SELECT COUNT(*) FROM customer_account WHERE mobileNo = @MobileNumber";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MobileNumber", mobileNumber);

                conn.Open();
                int result = (int)cmd.ExecuteScalar();
                lblWalletLinkStatus.Text = result > 0 ? "Linked to Wallet" : "Not Linked";
            }
        }

        
        protected void btnUpdatePoints_Click(object sender, EventArgs e)
        {
            string mobileNumber = txtMobileNumberForPoints.Text.Trim();
            string query = @"
                UPDATE customer_account
                SET total_points = (
                    SELECT SUM(points_earned)
                    FROM Payment
                    WHERE mobileNo = @MobileNumber
                )
                WHERE mobileNo = @MobileNumber";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Account_Payment_Points", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@mobile_num", mobileNumber);
                    lblUpdateStatus.Text = ((int)cmd.ExecuteScalar()).ToString();
                }
            }

        }
        
        protected void btnFetchAverage_Click(object sender, EventArgs e)
        {
            
            string walletID = txtWalletIDForAvg.Text.Trim();
            if (string.IsNullOrEmpty(walletID))
            {
                lblAverageAmount.Text = "Please provide a valid Wallet ID.";
                return;
            }

           
            if (!DateTime.TryParse(txtStartDate.Text.Trim(), out DateTime startDate) ||
                !DateTime.TryParse(txtEndDate.Text.Trim(), out DateTime endDate))
            {
                lblAverageAmount.Text = "Please provide valid Start Date and End Date.";
                return;
            }

            if (startDate > endDate)
            {
                lblAverageAmount.Text = "Start Date cannot be later than End Date.";
                return;
            }

            string query = "SELECT dbo.Wallet_Transfer_Amount(@walletID, @start_date, @end_date)";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@walletID", walletID);
                    cmd.Parameters.AddWithValue("@start_date", startDate);
                    cmd.Parameters.AddWithValue("@end_date", endDate);

                    try
                    {
                        
                        object result = cmd.ExecuteScalar();

                        
                        if (result != null && result != DBNull.Value)
                        {
                            lblAverageAmount.Text = $"Average Amount: {result}";
                        }
                        else
                        {
                            lblAverageAmount.Text = "No transactions found for the given Wallet ID and date range.";
                        }
                    }
                    catch (Exception ex)
                    {
                        lblAverageAmount.Text = $"Error fetching average amount: {ex.Message}";
                    }
                }
            }
        }



        
        protected void btnFetchCashback_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(txtWalletID.Text.Trim()) || string.IsNullOrEmpty(txtPlanIDForCashback.Text.Trim()))
            {
                lblCashbackAmount.Text = "Please provide valid Wallet ID and Plan ID.";
                return;
            }

            if (!int.TryParse(txtWalletID.Text.Trim(), out int walletID) || !int.TryParse(txtPlanIDForCashback.Text.Trim(), out int planID))
            {
                lblCashbackAmount.Text = "Wallet ID and Plan ID must be numeric.";
                return;
            }

            string query = "SELECT dbo.Wallet_Cashback_Amount(@WalletID, @PlanID)";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@WalletID", walletID);
                    cmd.Parameters.AddWithValue("@PlanID", planID);

                    try
                    {
                       
                        object result = cmd.ExecuteScalar();

                        
                        if (result != null && result != DBNull.Value)
                        {
                            lblCashbackAmount.Text = $"Total Cashback: {result}";
                        }
                        else
                        {
                            lblCashbackAmount.Text = "No cashback found for the provided Wallet ID and Plan ID.";
                        }
                    }
                    catch (Exception ex)
                    {
                        lblCashbackAmount.Text = $"Error fetching cashback: {ex.Message}";
                    }
                }
            }
        }

    }
}
