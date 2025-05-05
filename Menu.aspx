<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="WebApplication1.Menu" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Welcome</title>
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
            width: 350px;
        }
        h1 {
            font-size: 26px;
            color: #fff;
            margin-bottom: 10px;
        }
        p {
            margin-bottom: 20px;
            font-size: 18px;
            color: #fff;
        }
        asp\:Button {
            background: linear-gradient(135deg, #ffafbd, #ffc3a0);
            color: white;
            border: none;
            padding: 12px 20px;
            border-radius: 50px;
            font-size: 16px;
            cursor: pointer;
            margin: 10px 0;
            transition: all 0.3s ease-in-out;
            text-shadow: 1px 1px 2px rgba(0, 0, 0, 0.2);
        }
        asp\:Button:hover {
            background: linear-gradient(135deg, #ff77a9, #ff8c94);
            transform: scale(1.1);
        }
    </style>
</head>
<body>
    <form id="form10" runat="server">
        <h1>Hello There!</h1>
        <p>Choose whether you are a Customer or an Admin:</p>
        <asp:Button ID="adminbutton" runat="server" Text="Admin?" OnClick="Adminredirect" />
        <asp:Button ID="customerbutton" runat="server" Text="Customer?" OnClick="Customerredirect" />
    </form>
</body>
</html>