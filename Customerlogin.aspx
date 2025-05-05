<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Customerlogin.aspx.cs" Inherits="WebApplication1.Customerlogin" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customer Login</title>
    <style>
        body {
            font-family: 'Comic Sans MS', sans-serif;
            background: linear-gradient(135deg, #ff9a9e, #fad0c4);
            color: #333;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }
        form {
            background: linear-gradient(135deg, #a1c4fd, #c2e9fb);
            border: 3px solid #77a9f9;
            border-radius: 20px;
            padding: 30px;
            box-shadow: 0px 6px 15px rgba(0, 0, 0, 0.2);
            text-align: center;
            width: 400px;
        }
        h1 {
            font-size: 26px;
            color: #fff;
            margin-bottom: 20px;
        }
        asp\:Label {
            font-size: 16px;
            color: #fff;
            margin-bottom: 10px;
            display: block;
            text-align: left;
            margin-left: 10%;
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
            margin-top: 10px;
        }
        asp\:Button:hover {
            background: linear-gradient(135deg, #ff77a9, #ff8c94);
            transform: scale(1.1);
        }
        asp\:Label[ForeColor="Red"] {
            color: #ff4d4f;
            font-size: 14px;
            margin-top: 10px;
        }
    </style>
</head>
<body>
    <form id="form7" runat="server">
        <h1>Customer Login</h1>
        <asp:Label ID="Label2" runat="server" Text="Mobile Number:"></asp:Label>
        <asp:TextBox ID="MobileNumber" runat="server" Placeholder="Mobile Number"></asp:TextBox><br />
        <asp:Label ID="Label3" runat="server" Text="Password:"></asp:Label>
        <asp:TextBox ID="Password" runat="server" TextMode="Password" Placeholder="Password"></asp:TextBox><br />
        <asp:Button ID="LoginButton" runat="server" Text="Login" OnClick="LoginButton_Click" /><br />
        <asp:Label ID="ErrorLabel" runat="server" ForeColor="Red"></asp:Label>
    </form>
</body>
</html>
