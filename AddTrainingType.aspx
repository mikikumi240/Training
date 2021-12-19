<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AddTrainingType.aspx.cs" Inherits="Tyuta.AddTrainingType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="height: 315px">

        <asp:Panel ID="Panel1" runat="server" Height="87px" Width="779px" GroupingText="הוספת הכשרה" style="margin-top:20px;" Font-Bold="False" Font-Names="Arial" ForeColor="RoyalBlue" >
            <div style="display:flex;justify-content:space-between">
                <asp:Label ID="Label1" runat="server" Text="שם ההדרכה" Font-Names="Arial"></asp:Label>
                <asp:TextBox runat="server" id="txtDesc"></asp:TextBox>
                <asp:Label ID="Label2" runat="server" Text="קוד ההדרכה" Font-Names="Arial"></asp:Label>
                <asp:TextBox runat="server" id="txtCode"></asp:TextBox>
                <asp:Button runat="server" Text="הוסף" ID="cmdAdd" OnClick="cmdAdd_Click" Width="65px" Font-Names="Arial"></asp:Button>
            </div>
            
        </asp:Panel>
        <%=errMsg %>
        <asp:gridview runat="server" ID="GridView1" OnRowDataBound="GridView1_RowDataBound"></asp:gridview>                

    </div>
</asp:Content>
