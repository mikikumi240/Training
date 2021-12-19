<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Content1.aspx.cs" Inherits="Tyuta.Content1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" >
    <%--<h1>פה יופיע שם המסך</h1>--%>
    
</asp:Content>

    

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <p style="text-decoration:underline;color:royalblue">בחר קובץ אקסל לקליטה</p>
        <asp:FileUpload ID="FileUpload1" runat="server"  />
        <asp:Button ID="cmdReadExcel" runat="server" OnClick="cmdReadExcel_Click" Text="קרא קובץ" Width="84px" />
        <div>            
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>                
                <div style="margin:0 auto;width:100px; font-size:20px;font-weight:bold" >המתן....</div>
                <asp:Image ID="Image1" style="display:block;margin:0 auto; width:80px;height:80px" runat="server" ImageUrl="~/icons/Wait.gif" Height="64px" Width="64px"  />
            </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <br />
            <%--<asp:Button text="nisa" runat="server" visible="false" OnClick="Unnamed1_Click" />--%>
            <asp:Button ID="cmdInput" runat="server" Text="קליטת קובץ" OnClientClick="return confirm('אישור קליטת נתונים המופיעים על המסך');"  OnClick="cmdInput_Click" style="margin:10px" Enabled="False"/>
            <p>תוכן הקובץ:</p> <div id="recs_count"></div>
            <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound"></asp:GridView>

            </ContentTemplate>
            
            </asp:UpdatePanel>
        </div>
         
        
        <div  id="WaitMsg" style="margin:0 auto;width:100px; font-size:20px;font-weight:bold" class="imHidden">המתן לא פעיל----....</div>
        <%--display:none;--%>       
        <%--in continue of onClientClick:this.value = 'Submitting, please be patient while your request is processed';--%>
        <%--and: document.getElementById('WaitMsg').style.display='block';--%>
        <%--<asp:Label ID="lbltest" runat="server" Text="wait...." Visible="False"></asp:Label>--%>

        <%--<p>תוכן הקובץ:</p>
        <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound"></asp:GridView>--%>
    </div>

</asp:Content>
