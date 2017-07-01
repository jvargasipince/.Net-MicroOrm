<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Demo_MicroORM.Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
       
         <div class="col-md-6">  
             <h2>Micro ORMs</h2>
             <asp:Button ID="btnDapper" runat="server" Text="Dapper" OnClick="btnDapper_Click" />
             <asp:Button ID="btnMassive" runat="server" Text="Massive" OnClick="btnMassive_Click" />
        </div>

    </div>

</asp:Content>

