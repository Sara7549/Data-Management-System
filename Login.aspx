<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication1.Login" %>

<!DOCTYPE html>
<html>
<head>
    <title>Admin Login</title>
    <style>
        body {
            font-family: 'Comic Sans MS', sans-serif;
            background: linear-gradient(135deg, #fbc2eb, #a6c1ee);
            color: #333;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }
        form {
            background: linear-gradient(135deg, #ff9a9e, #fad0c4);
            border: 3px solid #ff77a9;
            border-radius: 20px;
            padding: 30px;
            box-shadow: 0px 6px 15px rgba(0, 0, 0, 0.2);
            text-align: center;
            width: 400px;
        }
        h2 {
            font-size: 26px;
            color: #fff;
            margin-bottom: 15px;
        }
        asp\:Label {
            font-size: 16px;
            color: #fff;
            margin-bottom: 10px;
            display: block;
        }
        asp\:TextBox {
            width: 80%;
            padding: 10px;
            border: 1px solid #ddd;
            border-radius: 5px;
            margin-bottom: 15px;
            font-size: 14px;
        }
        asp\:TextBox:focus {
            outline: none;
            border-color: #ff77a9;
            box-shadow: 0px 0px 5px rgba(255, 119, 169, 0.5);
        }
        asp\:Button {
            background: linear-gradient(135deg, #f857a6, #ff5858);
            color: white;
            border: none;
            padding: 12px 20px;
            border-radius: 50px;
            font-size: 16px;
            cursor: pointer;
            transition: all 0.3s ease-in-out;
            text-shadow: 1px 1px 2px rgba(0, 0, 0, 0.2);
        }
        asp\:Button:hover {
            background: linear-gradient(135deg, #ff77a9, #ff8c94);
            transform: scale(1.1);
        }
        asp\:Label[ForeColor="Red"] {
            color: #ff4d4f;
            font-size: 14px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Admin Login</h2>
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label><br />
        <asp:Label Text="Admin ID: " runat="server" />
        <asp:TextBox ID="txtAdminID" runat="server" /><br />
        <asp:Label Text="Password: " runat="server" />
        <asp:TextBox ID="txtAdminPassword" runat="server" TextMode="Password" /><br />
        <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="AdminLogin" />
    </form>
</body>
</html>
