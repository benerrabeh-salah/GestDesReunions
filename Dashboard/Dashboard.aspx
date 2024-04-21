<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="GestDesReunions.Dashboard.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

   <div class="container">
    <div class="row pt-5">
        <div class="col-md-4 mb-3">
            <div class="card">
                <div class="card-body">
                    <h5 class="card-title">Total des réunions</h5>
                    <asp:Label class="card-text" ID="TotalMeetingsCard" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
        <div class="col-md-4 mb-3">
            <div class="card">
                <div class="card-body">
                    <h5 class="card-title">Réunions Clôturées</h5>
                    <asp:Label class="card-text" ID="ClotureMeetingsCard" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
        <div class="col-md-4 mb-3">
            <div class="card">
                <div class="card-body">
                    <h5 class="card-title">Réunions Programmées</h5>
                    <asp:Label class="card-text" ID="ProgrammeeMeetingsCard" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="card">
                <div class="card-body">
                    <h5 class="card-title">Réunions non encore programmées</h5>
                    <asp:Label class="card-text" ID="non_encore_programmeeMeetingsCard" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>

         <div class="col-md-4">
            <div class="card">
                <div class="card-body">
                    <h5 class="card-title">Autre réunions</h5>
                    <asp:Label class="card-text" ID="AutreMeetingsCard" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>

        <div class="col-md-4">
            <div class="card">
                <div class="card-body">
                    <h5 class="card-title">Réunions avec recommandation</h5>
                    <asp:Label class="card-text" ID="RecommandationMeetingsCard" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
        
        <!-- Ajoutez d'autres cartes pour afficher d'autres statistiques -->
    </div>
</div>


</asp:Content>
