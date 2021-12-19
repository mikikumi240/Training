<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Tyuta.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>מערכת הדרכות</title>
    <script src ="Test.js"></script>
    <%--<link href="Content/bootstrap.css" rel="stylesheet" />--%>
</head>
<body dir="rtl">
    <form id="form1" runat="server" >
    <h2 style="color:royalblue">כניסה למערכת הדרכות</h2>
    <div class="divider" style="margin-right:100px;margin-top:100px;display:flex;justify-content:space-around;flex-direction:column;align-items:center;height: 87px">    
        <div>
            <label for="userNameTxt">שם משתמש:</label>        
            <input type="text" id="userNameTxt" runat="server" placeholder="הזן כאן"/>                             
        </div>
        <div>
            <span class="alert-warning"><%=errorMsg %></span>   
            <asp:Button Text="כניסה" ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Width="66px" />  
        </div>              
    </div>
    </form>
     
</body>
</html>
