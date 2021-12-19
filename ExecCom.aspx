<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExecCom.aspx.cs" Inherits="Tyuta.ExecCom" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" dir="rtl">
    <label>תאריך מגירסה 12 נתמך:</label><input type="date"  />
    <div>
        <%--<asp:Panel runat="server" Height="213px" style="display:flex;justify-content:space-around;align-items:flex-start">
            <asp:Label ID="Label1" runat="server" Text="מתאריך"></asp:Label>
            <asp:Calendar ID="CalendarFrom" runat="server"></asp:Calendar>
            <asp:Label ID="Label2" runat="server" Text="עד תאריך"></asp:Label>
            <asp:Calendar ID="CalendarTo" runat="server"></asp:Calendar>
        </asp:Panel>--%>
        <asp:RadioButtonList ID="RadioButtonList1" runat="server" Width="317px" CellPadding="10">
            <asp:ListItem Value="1109">סמל 1109</asp:ListItem>
            <asp:ListItem Value="112">סמל 112</asp:ListItem>
            <asp:ListItem Value="1021">סמל 1021</asp:ListItem>
            <asp:ListItem Value="114">סמל 114</asp:ListItem>
            <asp:ListItem Value="693">סמל 693</asp:ListItem>
            <asp:ListItem Value="48">סמל 48</asp:ListItem>
            <asp:ListItem Value="CalcPremMonthly">סמלים 0040,0698</asp:ListItem>
            <asp:ListItem Value="tagmulim">119,156-158,1015,1113,1114</asp:ListItem>
            <asp:ListItem Value="86">סמל 86</asp:ListItem>
            <asp:ListItem Value="689">סמל 689</asp:ListItem>
        </asp:RadioButtonList>
    
        <asp:Button ID="cmdExec" runat="server" OnClick="cmdExec_Click" Text="בצע פקודה" />
    
    </div>
    </form>
</body>
</html>
