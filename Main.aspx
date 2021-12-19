<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Tyuta.Main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" >
    <%--<h1>פה תהיה כותרת המסך</h1>--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div  id ="main" style="display:flex;flex-direction: column;justify-content:space-around;align-items:center;width:50%;margin:0 auto;height:500px;border:solid 2px rgb(0, 148, 255);" >                            
        <asp:Image ID="Image1" runat="server"  style="border:solid 1px white" ImageUrl="~/icons/training.jpg"/>
        <asp:Label ID="Label1" runat="server" Text="מערכת הדרכות" style="display:block;text-align:center;font-weight:bold;font-size:xx-large;color:royalblue;height:30%;"></asp:Label>
    </div>
    
</asp:Content>

