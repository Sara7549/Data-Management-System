<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="WebApplication1.Customer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customer Portal</title>
    <style>
        body {
            font-family: 'Comic Sans MS', sans-serif;
            background: linear-gradient(135deg, #ff9a9e, #fad0c4);
            margin: 0;
            padding: 20px;
            color: #333;
        }
        form {
            max-width: 1200px;
            margin: auto;
            background: linear-gradient(135deg, #a1c4fd, #c2e9fb);
            border-radius: 15px;
            padding: 30px;
            box-shadow: 0 8px 15px rgba(0, 0, 0, 0.2);
        }
        h2, h3 {
            color: #fff;
            text-align: center;
        }
        .container {
            margin-top: 20px;
            padding: 15px;
            border: 2px solid #77a9f9;
            border-radius: 15px;
            background: linear-gradient(135deg, #d4fc79, #96e6a1);
            box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.2);
        }
        label, asp\:Label {
            font-size: 16px;
            margin-bottom: 5px;
            display: block;
            color: #fff;
        }
        asp\:TextBox {
            width: 95%;
            padding: 10px;
            margin-bottom: 10px;
            border-radius: 5px;
            border: 1px solid #ddd;
            font-size: 14px;
        }
        asp\:TextBox:focus {
            outline: none;
            border-color: #ff77a9;
            box-shadow: 0px 0px 5px rgba(255, 119, 169, 0.5);
        }
        button, input[type="button"], input[type="submit"] {
    background-color: #4CAF50; 
    color: white; 
    font-size: 16px; 
    padding: 12px 20px; 
    border-radius: 25px; 
    border: none;
    cursor: pointer; 
    transition: all 0.3s ease-in-out; 
    margin: 8px 0; 
    text-align: center;
    width: 100%; 
    max-width: 300px; 
}


button:hover, input[type="button"]:hover, input[type="submit"]:hover {
    background-color: #45a049; 
    transform: scale(1.05); 
}


button:focus, input[type="button"]:focus, input[type="submit"]:focus {
    outline: none; 
    box-shadow: 0px 0px 8px rgba(0, 128, 0, 0.5); 
}


.btn-group {
    display: flex;
    justify-content: center;
    flex-wrap: wrap; 
    gap: 15px; 
}


@media (max-width: 600px) {
    button, input[type="button"], input[type="submit"] {
        width: 100%; 
    }
}


button:disabled, input[type="button"]:disabled, input[type="submit"]:disabled {
    background-color: #ccc; 
    cursor: not-allowed; 
    opacity: 0.6; 
}
     asp\:GridView {
    margin-top: 15px;
    width: 100%;
    border-collapse: collapse;
    border-radius: 10px;
    overflow: hidden;
    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
    background-color: #ffffff !important; 
    opacity: 1 !important; 
}

asp\:GridView th {
    background-color: #4CAF50;
    color: white;
    padding: 12px 15px;
    text-align: left;
    font-size: 16px;
    text-transform: uppercase;
    border: 1px solid #ddd;
    background-color: #4CAF50 !important; 
}

asp\:GridView td {
    padding: 12px 15px;
    text-align: left;
    font-size: 14px;
    border: 1px solid #ddd;
    background-color: #ffffff !important; 
}

asp\:GridView tr:nth-child(even) td {
    background-color: #f9f9f9 !important; 
}

asp\:GridView tr:hover td {
    background-color: #ddd;
    cursor: pointer;
    transition: all 0.3s ease-in-out;
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

asp\:GridView tr:focus td {
    background-color: #d9ffd9;
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
    <form id="form2244" runat="server">
        <h2>Customer Portal</h2>
       <div class="container">
<h3>Offered Service Plans</h3>
<asp:Button ID="servicePlans" runat="server" Text="View service plans" OnClick="LoadServicePlansButton_Click" /><br />
<asp:GridView ID="ServicePlansGrid" runat="server" AutoGenerateColumns="True"></asp:GridView>
        </div>
	 <div class="container">
            <h3>Track Plan Usage</h3>
Plan Name: <asp:TextBox ID="PlanNameTextBox" runat="server"></asp:TextBox><br />
Start Date: <asp:TextBox ID="StartDateTextBox" runat="server"></asp:TextBox><br />
End Date: <asp:TextBox ID="EndDateTextBox" runat="server"></asp:TextBox><br />
<asp:Button ID="TrackUsageButton" runat="server" Text="Track Usage" OnClick="TrackUsageButton_Click" /><br />
<asp:Label ID="planusageerror" runat="server"></asp:Label><br />
<h3>Total Usage</h3>
<asp:Label ID="TotalUsageLabel" runat="server"></asp:Label><br />
         </div>
        <div class="container">
<h3>Cashback Transactions</h3>
National ID: <asp:TextBox ID="NationalIDTextBox" runat="server"></asp:TextBox><br />
<asp:Button ID="ShowCashbackButton" runat="server" Text="Show Cashback" OnClick="ShowCashbackButton_Click" /><br />
<asp:GridView ID="CashbackGridView" runat="server" AutoGenerateColumns="True"></asp:GridView>
<asp:Label ID="nationalIdError" runat="server"></asp:Label><br />
        </div>
	<div class="container">
            <h3>Plans Not Subscribed to</h3>
Mobile Number: <asp:TextBox ID="MobileNoTextBox" runat="server"></asp:TextBox><br />
<asp:Button ID="ViewAvailablePlansButton" runat="server" Text="View Available Plans" OnClick="ViewAvailablePlansButton_Click" /><br />
<asp:GridView ID="AvailablePlansGrid" runat="server" AutoGenerateColumns="True"></asp:GridView>
        </div>

        <div class="container">
<h3>Active Plans Usage for This Month</h3>
<asp:GridView ID="ActivePlansGrid" runat="server" AutoGenerateColumns="True"></asp:GridView>
 </div>

<div class="container">
    <h2>Active Benefits</h2>
    <asp:Button ID="BtnActiveBenefits" runat="server" Text="View Active Benefits" OnClick="BtnActiveBenefits_Click" /><br />
    <asp:GridView ID="GridViewBenefits" runat="server" AutoGenerateColumns="True"></asp:GridView>
    <asp:Label ID="LblResult" runat="server" ForeColor="Blue"></asp:Label>
</div>
<div class="container">
    <h2>Retrieve Unresolved Tickets</h2>
    <label for="TxtNationalID">National ID:</label>
    <asp:TextBox ID="TxtNationalID" runat="server"></asp:TextBox><br />
    <asp:Button ID="BtnUnresolvedTickets" runat="server" Text="Get Unresolved Tickets" OnClick="BtnUnresolvedTickets_Click" /><br />
    <asp:Label ID="LblResultTickets" runat="server" ForeColor="Blue"></asp:Label>
</div>

<div class="container">
    <h2>Get Highest Value Voucher</h2>
    <label for="TxtMobileNo">Mobile Number:</label>
    <asp:TextBox ID="TxtMobileNo" runat="server"></asp:TextBox><br />
    <asp:Button ID="BtnHighestVoucher" runat="server" Text="Get Highest Voucher" OnClick="BtnHighestVoucher_Click" /><br />
    <asp:Label ID="LblResultVoucher" runat="server" ForeColor="Blue"></asp:Label>
</div>

<div class="container">
    <h2>Remaining Plan Amount</h2>
    <label for="TxtPlanMobileNo">Mobile Number:</label>
    <asp:TextBox ID="TxtPlanMobileNo" runat="server"></asp:TextBox><br />
    <label for="TxtPlanName">Plan Name:</label>
    <asp:TextBox ID="TxtPlanName" runat="server"></asp:TextBox><br />
    <asp:Button ID="BtnRemainingAmount" runat="server" Text="Get Remaining Amount" OnClick="BtnRemainingAmount_Click" /><br />
    <asp:Label ID="LblResultRemaining" runat="server" ForeColor="Blue"></asp:Label>
</div>

<div class="container">
    <h2>Extra Plan Amount</h2>
    <label for="TxtExtraMobileNo">Mobile Number:</label>
    <asp:TextBox ID="TxtExtraMobileNo" runat="server"></asp:TextBox><br />
    <label for="TxtExtraPlanName">Plan Name:</label>
    <asp:TextBox ID="TxtExtraPlanName" runat="server"></asp:TextBox><br />
    <asp:Button ID="BtnExtraAmount" runat="server" Text="Get Extra Amount" OnClick="BtnExtraAmount_Click" /><br />
    <asp:Label ID="LblResultExtra" runat="server" ForeColor="Blue"></asp:Label>
</div>

<div class="container">
    <h2>Top 10 Successful Payments</h2>
    <label for="TxtTopPaymentsMobileNo">Mobile Number:</label>
    <asp:TextBox ID="TxtTopPaymentsMobileNo" runat="server"></asp:TextBox><br />
    <asp:Button ID="BtnTopPayments" runat="server" Text="Get Top Payments" OnClick="BtnTopPayments_Click" /><br />
    <asp:GridView ID="GridViewPayments" runat="server" AutoGenerateColumns="True"></asp:GridView>
    <asp:Label ID="Lblpay" runat="server" ForeColor="Blue"></asp:Label>
</div>
        
    
    
        <div class="container">
    <h3>1. View All Shops</h3>
    <asp:Button ID="btnViewShops" runat="server" Text="View Shops" OnClick="btnViewShops_Click" />
    <br />
    <asp:GridView ID="ShopsGridView" runat="server" AutoGenerateColumns="True"></asp:GridView>
    <asp:Label ID="lblShopsStatus" runat="server" Text="" ForeColor="Red"></asp:Label>
   
</div>
    
            <div class="container">
    <h3>2. Show Subscribed Plans in the Past 5 Months</h3>
    <asp:Label ID="lblMobileNoPlans" runat="server" Text="Enter Mobile Number:" />
    <asp:TextBox ID="txtMobileNoPlans" runat="server" />
    <asp:Button ID="btnShowSubscribedPlans" runat="server" Text="Show Subscribed Plans" OnClick="btnShowSubscribedPlans_Click" />
    <br />
    <asp:GridView ID="SubscribedPlansGridView" runat="server" AutoGenerateColumns="True"></asp:GridView>
    <asp:Label ID="lblPlansStatus" runat="server" Text="" ForeColor="Red"></asp:Label>
    
</div>
    
                <div class="container">
    <h3>3. Renew Subscription</h3>
    <asp:Label ID="lblMobileNoRenew" runat="server" Text="Mobile Number:" />
    <asp:TextBox ID="txtMobileNoRenew" runat="server" />
    <br />
    <asp:Label ID="lblPlanId" runat="server" Text="Plan ID:" />
    <asp:TextBox ID="txtPlanId" runat="server" />
    <br />
    <asp:Label ID="lblAmount" runat="server" Text="Payment Amount:" />
    <asp:TextBox ID="txtAmount" runat="server" />
    <br />
    <asp:Label ID="lblPaymentMethod" runat="server" Text="Payment Method:" />
    <asp:TextBox ID="txtPaymentMethod" runat="server" />
    <br />
    <asp:Button ID="btnRenew" runat="server" Text="Renew Subscription" OnClick="btnRenew_Click" />
    <asp:Label ID="lblRenewStatus" runat="server" Text="" ForeColor="Red"></asp:Label>

    
</div>
   
                    <div class="container">
    <h3>4. Calculate Cashback</h3>
    <asp:Label ID="lblMobileNoCashback" runat="server" Text="Enter Mobile Number:" />
    <asp:TextBox ID="txtMobileNoCashback" runat="server" />
    <br />
    <asp:Label ID="lblPaymentId" runat="server" Text="Payment ID:" />
    <asp:TextBox ID="txtPaymentId" runat="server" />
    <br />
    <asp:Label ID="lblBenefitId" runat="server" Text="Benefit ID:" />
    <asp:TextBox ID="txtBenefitId" runat="server" />
    <br />
    <asp:Button ID="btnCalculate" runat="server" Text="Calculate Cashback" OnClick="btnCalculate_Click" />
    <asp:Label ID="lblCashbackStatus" runat="server" Text="" ForeColor="Red"></asp:Label>

</div>
   
                        <div class="container">
    <h3>5. Recharge Balance</h3>
    <asp:Label ID="lblMobileNoRecharge" runat="server" Text="Mobile Number:" />
    <asp:TextBox ID="txtMobileNoRecharge" runat="server" />
    <br />
    <asp:Label ID="lblRechargeAmount" runat="server" Text="Recharge Amount:" />
    <asp:TextBox ID="txtRechargeAmount" runat="server" />
    <br />
    <asp:Label ID="lblRechargeMethod" runat="server" Text="Payment Method:" />
    <asp:TextBox ID="txtRechargeMethod" runat="server" />
    <br />
    <asp:Button ID="btnRecharge" runat="server" Text="Recharge" OnClick="btnRecharge_Click" />
    <asp:Label ID="lblRechargeStatus" runat="server" Text="" ForeColor="Red"></asp:Label>

</div>
   
                            <div class="container">
    <h3>6. Redeem Voucher</h3>
    <asp:Label ID="lblMobileNoRedeem" runat="server" Text="Enter Mobile Number:" />
    <asp:TextBox ID="txtMobileNoRedeem" runat="server" />
    <br />
    <asp:Label ID="lblVoucherId" runat="server" Text="Voucher ID:" />
    <asp:TextBox ID="txtVoucherId" runat="server" />
    <br />
    <asp:Button ID="btnRedeemVoucher" runat="server" Text="Redeem Voucher" OnClick="btnRedeemVoucher_Click" />
     <asp:Label ID="lblRedeemStatus" runat="server" Text="" ForeColor="Red"></asp:Label>
    </form>
</body>
</html>