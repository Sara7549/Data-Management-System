<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminComp.aspx.cs" Inherits="WebApplication1.AdminComp" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Dashboard</title>
    <style>
        body {
            font-family: 'Arial', sans-serif;
            background: linear-gradient(135deg, #74ebd5, #acb6e5);
            margin: 0;
            padding: 0;
            color: #333;
        }

        h1 {
            text-align: center;
            color: #333;
            font-size: 36px;
            margin-top: 30px;
        }

        .section {
            margin-bottom: 20px;
            padding: 20px;
            border-radius: 8px;
            background: #fff;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            margin-left: 10%;
            margin-right: 10%;
        }

        .section h3, .section h4 {
            color: #4CAF50;
            text-align: center;
            font-size: 24px;
            margin-bottom: 15px;
        }

        .section label {
            font-size: 14px;
            margin-bottom: 5px;
            color: #555;
            display: block;
        }

        .section input[type="text"] {
            width: 100%;
            padding: 10px;
            margin-bottom: 15px;
            border-radius: 5px;
            border: 1px solid #ccc;
            font-size: 14px;
        }

        .section input[type="text"]:focus {
            outline: none;
            border-color: #4CAF50;
            box-shadow: 0 0 5px rgba(76, 175, 80, 0.4);
        }

        .section input[type="button"], .section input[type="submit"], .section button {
            background-color: #4CAF50;
            color: white;
            border: none;
            padding: 10px 20px;
            border-radius: 25px;
            font-size: 16px;
            cursor: pointer;
            margin-top: 10px;
            transition: background-color 0.3s ease;
        }

        .section input[type="button"]:hover, .section input[type="submit"]:hover, .section button:hover {
            background-color: #45a049;
        }

        .section .btn-group {
            display: flex;
            justify-content: space-evenly;
            flex-wrap: wrap;
        }

        .section .btn-group button {
            margin: 5px;
            flex: 1;
            min-width: 200px;
        }

        .section .btn-group input {
            margin: 5px;
            flex: 1;
            min-width: 200px;
        }

        .section label, .section input, .section button {
            width: 100%;
            max-width: 300px;
        }

        .section .subsection {
            margin-top: 30px;
        }

        .section .subsection label {
            font-weight: bold;
        }

        .section .subsection input {
            background-color: #f1f1f1;
            border-radius: 5px;
            border: 1px solid #ddd;
        }

        .section .subsection input:focus {
            border-color: #007bff;
        }

        .section .label-text {
            font-size: 14px;
            color: #555;
        }

        .section .subsection .text {
            margin-top: 10px;
        }

        .section .label-text {
            font-size: 16px;
            color: #333;
        }

        .section h4 {
            margin-top: 20px;
        }

        /* Grid Styling */
        asp\:GridView {
            margin-top: 30px;
            width: 90%;
            margin-left: 5%;
            border-collapse: collapse;
            border-radius: 8px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
        }

        asp\:GridView th {
            background-color: #4CAF50;
            color: white;
            padding: 12px 15px;
            text-align: left;
            font-size: 16px;
        }

        asp\:GridView td {
            padding: 12px 15px;
            text-align: left;
            font-size: 14px;
            border: 1px solid #ddd;
            background-color: #f9f9f9;
        }

        asp\:GridView tr:nth-child(even) td {
            background-color: #f2f2f2;
        }

        asp\:GridView tr:hover td {
            background-color: #ddd;
        }

        asp\:GridView td, asp\:GridView th {
            border-radius: 5px;
        }

        .emptyGrid {
            text-align: center;
            padding: 20px;
            color: #777;
            font-style: italic;
        }

    
        .grid-pagination {
            display: flex;
            justify-content: center;
            margin-top: 15px;
        }

        .grid-pagination a {
            margin: 0 5px;
            padding: 8px 12px;
            background-color: #4CAF50;
            color: white;
            text-decoration: none;
            border-radius: 5px;
        }

        .grid-pagination a:hover {
            background-color: #45a049;
        }

        .grid-pagination a.active {
            background-color: #388e3c;
        }
    </style>
</head>
<body>
    <form id="form4" runat="server">
        <h1>Admin Dashboard</h1>

        <div class="section">
            <asp:Button ID="btnViewCustomers" runat="server" Text="View Customer Profiles" OnClick="ViewCustomerProfiles" />
            <asp:Button ID="btnViewStores" runat="server" Text="View Stores & Vouchers" OnClick="ViewStoresAndVouchers" />
            <asp:Button ID="btnViewResolvedTickets" runat="server" Text="View Resolved Tickets" OnClick="ViewResolvedTickets" />
            <asp:Button ID="btnViewAccountsPlans" runat="server" Text="View Accounts & Plans" OnClick="ViewAccountsWithPlans" />
            <asp:Button ID="btnFetchWallets" runat="server" Text="Fetch Wallets" OnClick="btnFetchWalletLink_Click" />
            <asp:Button ID="btnFetchEShops" runat="server" Text="Fetch E-Shops" OnClick="btnFetchEShops_Click" />
            <asp:Button ID="btnFetchPayments" runat="server" Text="Fetch Payments" OnClick="btnFetchPayments_Click" />
            <asp:Button ID="btnFetchCashbacks" runat="server" Text="Fetch Cashbacks" OnClick="btnFetchCashbacks_Click" />
       
            <h4>Plan Subscriptions</h4>
            <label for="txtPlanDate">Date:</label>
            <asp:TextBox ID="txtPlanDate" runat="server" Placeholder="Enter Date" />
            <label for="txtPlanID">Plan ID:</label>
            <asp:TextBox ID="txtPlanID" runat="server" Placeholder="Enter Plan ID" />
            <asp:Button ID="btnViewPlanSubscriptions" runat="server" Text="View Plan Subscriptions" OnClick="ViewPlanSubscriptions" />
            <h4>Total Usage</h4>
            <label for="txtUsageMobileNo">Mobile No:</label>
            <asp:TextBox ID="txtUsageMobileNo" runat="server" Placeholder="Enter Mobile Number" />
            <label for="txtUsageFromDate">From Date:</label>
            <asp:TextBox ID="txtUsageFromDate" runat="server" Placeholder="Enter Date" />
            <asp:Button ID="btnViewTotalUsage" runat="server" Text="View Total Usage" OnClick="ViewTotalUsage" />
            <h4>Remove Benefits</h4>
            <label for="txtRemoveMobileNo">Mobile No:</label>
            <asp:TextBox ID="txtRemoveMobileNo" runat="server" Placeholder="Enter Mobile Number"/>
            <label for="txtRemovePlanID">Plan ID:</label>
            <asp:TextBox ID="txtRemovePlanID" runat="server" Placeholder="Enter Plan ID" />
            <asp:Button ID="btnRemoveBenefits" runat="server" Text="Remove Benefits" OnClick="RemoveBenefits" />
            <h4>SMS Offers</h4>
            <label for="txtSMSMobileNo">Mobile No:</label>
            <asp:TextBox ID="txtSMSMobileNo" runat="server" Placeholder="Enter Mobile Number" />
            <asp:Button ID="btnViewSMSOffers" runat="server" Text="View SMS Offers" OnClick="ViewSMSOffers" />
        </div>

        <div class="section">
            
                 <h4>Accepted Transactions</h4>
            <asp:TextBox ID="txtAccountID" runat="server" Placeholder="Enter Mobile Number"></asp:TextBox>
            <asp:Button ID="btnFetchAcceptedTransactions" runat="server" Text="Fetch Transactions" OnClick="btnFetchAcceptedTransactions_Click" />
            <asp:Label ID="lblAcceptedPayments" runat="server" Text=""></asp:Label>
            <h4>Cashback for Wallet</h4>
            <asp:TextBox ID="txtWalletID" runat="server" Placeholder="Enter Wallet ID"></asp:TextBox>
            <asp:TextBox ID="txtPlanIDForCashback" runat="server" Placeholder="Enter Plan ID"></asp:TextBox>
            <asp:Button ID="btnFetchCashback" runat="server" Text="Fetch Cashback" OnClick="btnFetchCashback_Click" />
            <asp:Label ID="lblCashbackAmount" runat="server" Text=""></asp:Label>
            <h4>Average Transaction</h4>
            <asp:TextBox ID="txtWalletIDForAvg" runat="server" Placeholder="Enter Wallet ID"></asp:TextBox>
            <asp:TextBox ID="txtStartDate" runat="server" Placeholder="Start Date (yyyy-MM-dd)"></asp:TextBox>
            <asp:TextBox ID="txtEndDate" runat="server" Placeholder="End Date (yyyy-MM-dd)"></asp:TextBox>
            <asp:Button ID="btnFetchAverage" runat="server" Text="Fetch Average" OnClick="btnFetchAverage_Click" />
            <asp:Label ID="lblAverageAmount" runat="server" Text=""></asp:Label>
            <h4>Mobile Wallet Check</h4>
            <asp:TextBox ID="txtMobileNumber" runat="server" Placeholder="Enter Mobile Number"></asp:TextBox>
            <asp:Button ID="btnCheckWalletLink" runat="server" Text="Check Wallet Link" OnClick="btnCheckMobileWallet_Click" />
            <asp:Label ID="lblWalletLinkStatus" runat="server" Text=""></asp:Label>
            <h4>Update Points</h4>
            <asp:TextBox ID="txtMobileNumberForPoints" runat="server" Placeholder="Enter Mobile Number"></asp:TextBox>
            <asp:Button ID="btnUpdatePoints" runat="server" Text="Update Points" OnClick="btnUpdatePoints_Click" />
            <asp:Label ID="lblUpdateStatus" runat="server" Text=""></asp:Label>
        </div>

       
        <asp:GridView ID="GridViewData" runat="server" AutoGenerateColumns="true"></asp:GridView>
    </form>
</body>
</html>