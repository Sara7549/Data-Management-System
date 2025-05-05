using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Configuration;

namespace WebApplication1
{
    public partial class Customer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void LoadServicePlansButton_Click(object sender, EventArgs e)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Milestone2DB_24"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Service_Plan";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable servicePlans = new DataTable();
                adapter.Fill(servicePlans);

                ServicePlansGrid.DataSource = servicePlans;
                ServicePlansGrid.DataBind();
            }
        }
        protected void ViewAvailablePlansButton_Click(object sender, EventArgs e)
        {
            string mobileNo = MobileNoTextBox.Text.Trim();

           
            DisplayAvailablePlans(mobileNo);

           
            DisplayActivePlansUsage(mobileNo);
        }

        private void DisplayAvailablePlans(string mobileNo)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Milestone2DB_24"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT * FROM Service_Plan
                     WHERE planID NOT IN (SELECT planID FROM Subscription WHERE mobileNo = @mobileNo)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@mobileNo", mobileNo);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                AvailablePlansGrid.DataSource = reader;
                AvailablePlansGrid.DataBind();
            }
        }

        private void DisplayActivePlansUsage(string mobileNo)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Milestone2DB_24"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT * FROM Plan_Usage
                     WHERE mobileNo = @mobileNo AND MONTH(start_date) = MONTH(GETDATE()) AND YEAR(start_date) = YEAR(GETDATE())";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@mobileNo", mobileNo);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                ActivePlansGrid.DataSource = reader;
                ActivePlansGrid.DataBind();
            }
        }

        
        protected void TrackUsageButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PlanNameTextBox.Text) || string.IsNullOrWhiteSpace(StartDateTextBox.Text)
                || string.IsNullOrWhiteSpace(EndDateTextBox.Text))
            {
                planusageerror.Text = "The Plan name or the start date or the end date is not correct.Please try again";
                return;
            }
            string planName = PlanNameTextBox.Text.Trim();
            DateTime startDate = Convert.ToDateTime(StartDateTextBox.Text);
            DateTime endDate = Convert.ToDateTime(EndDateTextBox.Text);

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Milestone2DB_24"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT SUM(SMS_sent) AS TotalSMS, SUM(minutes_used) AS TotalMinutes, SUM(data_consumption) AS TotalInternet
                     FROM Plan_Usage
                     WHERE planID = (SELECT planID FROM Service_Plan WHERE name = @planName)
                     AND start_date >= @startDate AND end_date <= @endDate";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@planName", planName);
                command.Parameters.AddWithValue("@startDate", startDate);
                command.Parameters.AddWithValue("@endDate", endDate);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    TotalUsageLabel.Text = $"Total SMS: {reader["TotalSMS"]} | Total Minutes: {reader["TotalMinutes"]} | Total Internet: {reader["TotalInternet"]}";
                }
            }
        }

        
        protected void ShowCashbackButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NationalIDTextBox.Text)) {
                nationalIdError.Text = "Please enter a valid national ID";
               
            }
            else {
                nationalIdError.Text = "";
                int nationalID = Convert.ToInt32(NationalIDTextBox.Text.Trim());

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Milestone2DB_24"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT cashbackID, amount, credit_date
                     FROM Cashback
                     WHERE walletID = (SELECT walletID FROM Wallet WHERE nationalID = @nationalID)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@nationalID", nationalID);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                CashbackGridView.DataSource = reader;
                CashbackGridView.DataBind();
            }
            }
        }
        protected void BtnUnresolvedTickets_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Milestone2DB_24"].ConnectionString;
            int unresolvedTickets = 0;
            if (string.IsNullOrWhiteSpace(TxtNationalID.Text) || !int.TryParse(TxtNationalID.Text, out int nationalID))
            {
                LblResultTickets.Text = "Please enter a valid National ID.";
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string checkQuery = "SELECT COUNT(*) FROM customer_profile WHERE nationalID = @NationalID";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@NationalID", nationalID);
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count == 0)
                        {
                            LblResultTickets.Text = "National ID does not exist in the database.";
                            return;
                        }
                    }
                    using (SqlCommand cmd = new SqlCommand("Ticket_Account_Customer", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NID", nationalID);
                        unresolvedTickets = (int)cmd.ExecuteScalar();
                        LblResultTickets.Text = "Unresolved Tickets: " + unresolvedTickets;
                    }
                }
            }
            catch (Exception ex)
            {
                LblResultTickets.Text = "An error occurred: " + ex.Message;
            }
        }

        protected void BtnHighestVoucher_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Milestone2DB_24"].ConnectionString;
            int voucherID = 0;
            if (string.IsNullOrWhiteSpace(TxtMobileNo.Text) || !int.TryParse(TxtMobileNo.Text, out int mobileNo))
            {
                LblResultVoucher.Text = "Please enter a valid Mobile Number.";
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string checkQuery = "SELECT COUNT(*) FROM customer_account WHERE mobileNo = @MobileNo";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("MobileNo", mobileNo);
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count == 0)
                        {
                            LblResultVoucher.Text = "Mobile number does not exist in the database.";
                            return;
                        }
                    }
                    using (SqlCommand cmd = new SqlCommand("Account_Highest_Voucher", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@mobile_num", mobileNo);
                        var result = cmd.ExecuteScalar();
                        if (result != DBNull.Value && result != null)
                        {
                            voucherID = Convert.ToInt32(result);
                            LblResultVoucher.Text = "Highest Value Voucher ID: " + voucherID;
                        }
                        else
                        {
                            LblResultVoucher.Text = "No voucher found for the provided mobile number.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LblResultVoucher.Text = "An error occurred: " + ex.Message;
            }

        }

        protected void BtnRemainingAmount_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Milestone2DB_24"].ConnectionString;
            int remainingAmount = 0;
            if (string.IsNullOrWhiteSpace(TxtPlanMobileNo.Text) || !int.TryParse(TxtPlanMobileNo.Text, out int mobileNo))
            {
                LblResultRemaining.Text = "Please enter a valid Mobile Number.";
                return;
            }
            if (string.IsNullOrWhiteSpace(TxtPlanName.Text))
            {
                LblResultRemaining.Text = "Please enter a valid Plan Name.";
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string checkQuery1 = "SELECT COUNT(*) FROM customer_account WHERE mobileNo = @MobileNo";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery1, conn))
                    {
                        checkCmd.Parameters.AddWithValue("MobileNo", mobileNo);
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count == 0)
                        {
                            LblResultRemaining.Text = "Mobile number does not exist in the database.";
                            return;
                        }
                    }
                    string checkQuery = "SELECT COUNT(*) FROM Service_plan WHERE name = @name";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@name", TxtPlanName.Text);
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count == 0)
                        {
                            LblResultRemaining.Text = "Plan name does not exist in the database.";
                            return;
                        }
                    }
                    string query = "SELECT dbo.Remaining_plan_amount(@MobileNo, @PlanName)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MobileNo", mobileNo);
                        cmd.Parameters.AddWithValue("@PlanName", TxtPlanName.Text);
                        var result = cmd.ExecuteScalar();
                        if (result != DBNull.Value && result != null)
                        {
                            remainingAmount = (int)result;
                        }
                        else
                        {
                            LblResultRemaining.Text = "No remaining amount found.";
                        }
                        LblResultRemaining.Text = "Remaining Amount: " + remainingAmount;
                    }

                }
            }
            catch (Exception ex)
            {
                LblResultRemaining.Text = "An error occurred: " + ex.Message;
            }
        }

        protected void BtnExtraAmount_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Milestone2DB_24"].ConnectionString;
            int extraAmount = 0;
            if (string.IsNullOrWhiteSpace(TxtExtraMobileNo.Text) || !int.TryParse(TxtExtraMobileNo.Text, out int mobileNo))
            {
                LblResultExtra.Text = "Please enter a valid Mobile Number.";
                return;
            }
            if (string.IsNullOrWhiteSpace(TxtExtraPlanName.Text))
            {
                LblResultExtra.Text = "Please enter a valid Plan Name.";
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string checkQuery1 = "SELECT COUNT(*) FROM customer_account WHERE mobileNo = @MobileNo";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery1, conn))
                    {
                        checkCmd.Parameters.AddWithValue("MobileNo", mobileNo);
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count == 0)
                        {
                            LblResultExtra.Text = "Mobile number does not exist in the database.";
                            return;
                        }
                    }
                    string checkQuery = "SELECT COUNT(*) FROM Service_plan WHERE name = @name";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@name", TxtExtraPlanName.Text);
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count == 0)
                        {
                            LblResultExtra.Text = "Plan name does not exist in the database.";
                            return;
                        }
                    }
                    string query = "SELECT dbo.Extra_plan_amount(@MobileNo, @PlanName)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@MobileNo", mobileNo);
                    cmd.Parameters.AddWithValue("@PlanName", TxtExtraPlanName.Text);
                    var result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        extraAmount = (int)result;
                    }
                    else
                    {
                        LblResultExtra.Text = "No extra amount found.";
                    }
                    LblResultExtra.Text = "Extra Amount: " + extraAmount;
                }

            }
            catch (Exception ex)
            {
                LblResultExtra.Text = "An error occurred: " + ex.Message;
            }
        }

        protected void BtnTopPayments_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Milestone2DB_24"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (string.IsNullOrWhiteSpace(TxtTopPaymentsMobileNo.Text) || !int.TryParse(TxtTopPaymentsMobileNo.Text, out int mobileNo))
                {
                    Lblpay.Text = "Please enter a valid Mobile Number.";
                    return;
                }
                SqlCommand cmd = new SqlCommand("Top_Successful_Payments", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mobile_num", TxtTopPaymentsMobileNo.Text);


                try
                {
                    conn.Open();
                    string checkQuery1 = "SELECT COUNT(*) FROM customer_account WHERE mobileNo = @MobileNo";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery1, conn))
                    {
                        checkCmd.Parameters.AddWithValue("MobileNo", mobileNo);
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count == 0)
                        {
                            Lblpay.Text = "Mobile number does not exist in the database.";
                            return;
                        }
                    }
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    GridViewPayments.DataSource = dt;
                    GridViewPayments.DataBind();
                    Lblpay.Text = "";
                }
                catch (Exception ex)
                {
                    Lblpay.Text = "An error occurred: " + ex.Message;
                }
            }
        }

        protected void BtnActiveBenefits_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Milestone2DB_24"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM allBenefits";
                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    GridViewBenefits.DataSource = dt;
                    GridViewBenefits.DataBind();
                }
                catch (Exception ex)
                {
                    LblResult.Text = "An error occurred: " + ex.Message;
                }
            }
        }

        
        protected void btnViewShops_Click(object sender, EventArgs e)
        {
            try
            {
                LoadShops();
                lblShopsStatus.Text = "Successfully loaded all shops.";
                lblShopsStatus.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblShopsStatus.Text = $"Error: Could not load shops. Please try again later. Details: {ex.Message}";
                lblShopsStatus.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void LoadShops()
        {
            String connStr = WebConfigurationManager.ConnectionStrings["Milestone2DB_24"].ToString();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM allShops", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ShopsGridView.DataSource = dt;
                ShopsGridView.DataBind();
            }
        }

        
        protected void btnShowSubscribedPlans_Click(object sender, EventArgs e)
        {
            string mobileNo = txtMobileNoPlans.Text.Trim();
            if (string.IsNullOrEmpty(mobileNo) || mobileNo.Length != 11)
            {
                lblPlansStatus.Text = "Error: Please enter a valid 11-digit mobile number.";
                lblPlansStatus.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {

                ShowSubscribedPlans(mobileNo);
                lblPlansStatus.Text = "Successfully retrieved subscribed plans for the past 5 months.";
                lblPlansStatus.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblPlansStatus.Text = $"Error: Could not retrieve subscribed plans. Details: {ex.Message}";
                lblPlansStatus.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void ShowSubscribedPlans(String mobileNo)
        {

            string connStr = WebConfigurationManager.ConnectionStrings["Milestone2DB_24"].ToString();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                String query = "SELECT * From dbo.Subscribed_plans_5_Months(@MobileNo)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MobileNo", mobileNo);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        SubscribedPlansGridView.DataSource = dt;
                        SubscribedPlansGridView.DataBind();
                    }
                    else
                    {
                        lblPlansStatus.Text = "No subscribed plans found for the given mobile number in the past 5 months.";
                        lblPlansStatus.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        
        protected void btnRenew_Click(object sender, EventArgs e)
        {
            string mobileNo = txtMobileNoRenew.Text.Trim();
            string paymentMethod = txtPaymentMethod.Text.Trim();
            decimal amount;
            int planId;

            if (string.IsNullOrEmpty(mobileNo) || mobileNo.Length != 11 ||
                !decimal.TryParse(txtAmount.Text.Trim(), out amount) ||
                !int.TryParse(txtPlanId.Text.Trim(), out planId) ||
                string.IsNullOrEmpty(paymentMethod))
            {
                lblRenewStatus.Text = "Error: Please provide valid input values for all fields.";
                lblRenewStatus.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                RenewSubscription(mobileNo, amount, paymentMethod, planId);
                lblRenewStatus.Text = "Subscription renewed successfully.";
                lblRenewStatus.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblRenewStatus.Text = $"Error: Could not renew subscription. Details: {ex.Message}";
                lblRenewStatus.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void RenewSubscription(string mobileNo, decimal amount, string paymentMethod, int planId)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ToString();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Initiate_plan_payment", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mobile_num", mobileNo);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@payment_method", paymentMethod);
                cmd.Parameters.AddWithValue("@plan_id", planId);
                cmd.ExecuteNonQuery();
            }
        }

        
        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            string mobileNo = txtMobileNoCashback.Text.Trim();
            int paymentId, benefitId;

            if (string.IsNullOrEmpty(mobileNo) || mobileNo.Length != 11 ||
                !int.TryParse(txtPaymentId.Text.Trim(), out paymentId) ||
                !int.TryParse(txtBenefitId.Text.Trim(), out benefitId))
            {
                lblCashbackStatus.Text = "Error: Please provide valid input values.";
                lblCashbackStatus.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                CalculateCashback(mobileNo, paymentId, benefitId);
                lblCashbackStatus.Text = "Cashback calculated successfully.";
                lblCashbackStatus.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblCashbackStatus.Text = $"Error: Could not calculate cashback. Details: {ex.Message}";
                lblCashbackStatus.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void CalculateCashback(string mobileNo, int paymentId, int benefitId)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["Milestone2DB_24"].ToString();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Payment_wallet_cashback", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mobile_num", mobileNo);
                cmd.Parameters.AddWithValue("@payment_id", paymentId);
                cmd.Parameters.AddWithValue("@benefit_id", benefitId);
                cmd.ExecuteNonQuery();
            }
        }

        
        protected void btnRecharge_Click(object sender, EventArgs e)
        {
            string mobileNo = txtMobileNoRecharge.Text.Trim();
            string paymentMethod = txtRechargeMethod.Text.Trim();
            decimal amount;

            if (string.IsNullOrEmpty(mobileNo) || mobileNo.Length != 11 ||
                !decimal.TryParse(txtRechargeAmount.Text.Trim(), out amount) ||
                string.IsNullOrEmpty(paymentMethod))
            {
                lblRechargeStatus.Text = "Error: Please provide valid input values.";
                lblRechargeStatus.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                RechargeBalance(mobileNo, amount, paymentMethod);
                lblRechargeStatus.Text = "Balance recharged successfully.";
                lblRechargeStatus.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblRechargeStatus.Text = $"Error: Could not recharge balance. Details: {ex.Message}";
                lblRechargeStatus.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void RechargeBalance(string mobileNo, decimal amount, string paymentMethod)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["Milestone2DB_24"].ToString();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Initiate_balance_payment", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mobile_num", mobileNo);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@payment_method", paymentMethod);
                cmd.ExecuteNonQuery();
            }
        }

        
        protected void btnRedeemVoucher_Click(object sender, EventArgs e)
        {
            string mobileNo = txtMobileNoRedeem.Text.Trim();
            int voucherId;

            if (string.IsNullOrEmpty(mobileNo) || mobileNo.Length != 11 ||
                !int.TryParse(txtVoucherId.Text.Trim(), out voucherId))
            {
                lblRedeemStatus.Text = "Error: Please provide valid input values.";
                lblRedeemStatus.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                RedeemVoucher(mobileNo, voucherId);
                lblRedeemStatus.Text = "Voucher redeemed successfully.";
                lblRedeemStatus.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblRedeemStatus.Text = $"Error: Could not redeem voucher. Details: {ex.Message}";
                lblRedeemStatus.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void RedeemVoucher(string mobileNo, int voucherId)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["Milestone2DB_24"].ToString();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Redeem_voucher_points", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mobile_num", mobileNo);
                cmd.Parameters.AddWithValue("@voucher_id", voucherId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}