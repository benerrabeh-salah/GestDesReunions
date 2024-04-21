<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FiltrerLesReunion.aspx.cs" Inherits="GestDesReunions.Filtrage.FiltrerParId" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

       <script>
        // Disable form submission on page reload
        window.onload = function () {
            if (window.history && window.history.pushState) {
                window.history.pushState('formSubmitted', null, '');
                window.onpopstate = function (event) {
                    if (event.state === 'formSubmitted') {
                        // Redirect to the same page to clear form data
                        window.location.href = 'FiltrerLesReunion.aspx';
                    }
                };
            }
        };
       </script>

    <style>
        #ReunionGV {
            width: 70vw;
            background-color: red;
        }
        /* Style pour les lignes alternées (alternating rows) */
        .alt-row {
            background-color: #F5F5F5; /* Couleur de fond pour les lignes alternées */
        }

        /* Style pour les cellules */
        .grid-cell {
            padding: 8px; /* Espace interne des cellules */
            border: 1px solid #dddddd; /* Bordure des cellules */
        }

        /* Style pour la ligne sélectionnée */
        .selected-row {
            background-color: #B0BED9; /* Couleur de fond pour la ligne sélectionnée */
        }

        /* Style pour l'en-tête */
        .grid-header {
            background-color: #1C5E55; /* Couleur de fond pour l'en-tête */
            color: white; /* Couleur du texte pour l'en-tête */
            font-weight: bold; /* Style de police pour l'en-tête */
        }

        /* Style pour le pied de page */
        .grid-footer {
            background-color: #1C5E55; /* Couleur de fond pour le pied de page */
            color: white; /* Couleur du texte pour le pied de page */
            font-weight: bold; /* Style de police pour le pied de page */
        }
    </style>

    <asp:Label runat="server"><b>Critère de recherche :</b></asp:Label><br />
    <div class="row">
        <asp:Label ID="errorMessage" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Times New Roman" Font-Size="Large" ForeColor="Red" />
        <div class="col-md-8">
            <asp:DropDownList ID="Dsearch" runat="server" name="BrandName" CssClass="form-control dropdown-toggle" AutoPostBack="true" OnSelectedIndexChanged="Dsearch_SelectedIndexChanged">
                <asp:ListItem Text="-- Selectionner le critère --" Value="0"></asp:ListItem>
                <asp:ListItem Text="ID réunion" Value="1" />
                <asp:ListItem Text="Division" Value="2" />
                <asp:ListItem Text="Date réunion" Value="3" />
                <asp:ListItem Text="Date prochaine réunion" Value="4" />
                <asp:ListItem Text="Secteur" Value="5" />
                <asp:ListItem Text="Partenaire" Value="6" />
                <asp:ListItem Text="Cadre" Value="7" />
            </asp:DropDownList>
        </div>
        <div class="col-md-4">
            <asp:Button ID="btnExportExcel" Enabled="false" runat="server" Text="Excel" CssClass="btn btn-success w-25" OnClick="btnExportExcel_Click" />
            <%--<asp:Button ID="btnExportPdf" runat="server" Text="Export PDF" OnClick="btnExportPdf_Click" />--%>

            <asp:Button ID="btnExportPdf" Enabled="false" runat="server" Text="PDF" CssClass="btn btn-primary w-25" OnClick="btnExportPdf_Click" />

        </div>
    </div>

    <br />
    <%-- Ajouter ces contrôles à votre formulaire --%>
    <asp:TextBox ID="txtIdFilter" Visible="false" AutoPostBack="True" runat="server" placeholder="ID"></asp:TextBox>

    <asp:DropDownList ID="DivisionFilter" Visible="false" runat="server" placeholder="Division">
        <%-- <asp:ListItem Text="-- Selectionner la division --" Value="0"></asp:ListItem>
        <asp:ListItem Text="DSIC" />
        <asp:ListItem Text="DBM" />
        <asp:ListItem Text="DCL" />
        <asp:ListItem Text="DAEC" />
        <asp:ListItem Text="SG" />
        <asp:ListItem Text="DE" />
        <asp:ListItem Text="DRH" />--%>
    </asp:DropDownList>

    <!-- filtre date réunion -->
    <asp:Label ID="lbl1" Visible="false" runat="server"><b>Entre</b></asp:Label>
    <asp:TextBox ID="txtDateFilter1" TextMode="Date" Visible="false" runat="server" placeholder="Date"></asp:TextBox>
    <asp:Label ID="lbl2" Visible="false" runat="server"><b>Et </b></asp:Label>
    <asp:TextBox ID="txtDateFilter2" TextMode="Date" Visible="false" runat="server" placeholder="Date"></asp:TextBox>

    <!-- filtre date prochaine réunion -->
    <asp:Label ID="lbl3" Visible="false" runat="server"><b>Entre</b></asp:Label>
    <asp:TextBox ID="txtDateFilter3" TextMode="Date" Visible="false" runat="server" placeholder="Date"></asp:TextBox>
    <asp:Label ID="lbl4" Visible="false" runat="server"><b>Et </b></asp:Label>
    <asp:TextBox ID="txtDateFilter4" TextMode="Date" Visible="false" runat="server" placeholder="Date"></asp:TextBox>

    <!-- filtre secteur -->
    <asp:DropDownList ID="DSceteur" Visible="false" runat="server">
        <asp:ListItem Text="-- Selectionner le secteur --" Value="0"></asp:ListItem>
    </asp:DropDownList>

    <!-- filtre partenaire -->
    <asp:DropDownList ID="DPartenaire" Visible="false" runat="server">
        <asp:ListItem Text="-- Selectionner le partenaire --" Value="0"></asp:ListItem>
    </asp:DropDownList>

    <!-- filtre Cadre -->
    <asp:DropDownList ID="DCadre" Visible="false" runat="server">
        <asp:ListItem Text="-- Selectionner le Cadre --" Value="0"></asp:ListItem>
        <asp:ListItem Text="Convention" />
        <asp:ListItem Text="Programme" />
        <asp:ListItem Text="Autre" />
    </asp:DropDownList>

    <!-- filtre Cadre -->
    <asp:TextBox ID="txtObjet" Visible="false" runat="server" placeholder="Objet"></asp:TextBox>

    <!-- filtre Cadre -->
    <asp:TextBox ID="txtIdCader" Visible="false" runat="server" placeholder="Id"></asp:TextBox>

    <asp:Button ID="btnSearch" runat="server" Text="Rechercher" OnClick="btnSearch_Click" /><br />
    <asp:Label Text="" ForeColor="Green" Font-Bold="true" ID="lblmessage" CssClass="label label-success" runat="server" />

    <br />
    <div class="row">
        <div class="col-md-10 col-md-offset-1">

            <div class="form-group">
                <div class="table-responsive">

                    <asp:Repeater runat="server" ID="ReunionRepeater">
                        <HeaderTemplate>
                            <table class="table table-striped table-bordered table-responsive table-hover">
                                <thead class="table-success">
                                    <tr>
                                        <th style="width: 40px">ID</th>
                                        <th style="width: 150px">Date Réunion</th>
                                        <th>Objet Réunion</th>
                                        <th style="width: 50px">Division</th>
                                        <th>Séléctionner</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>

                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("id") %></td>
                                <td><%# Eval("date_reunion") %></td>
                                <td><%# Eval("sujet_reunion") %></td>
                                <td><%# Eval("division") %></td>

                                <td>
                                    <asp:CheckBox runat="server" ID="chkSelect" data-id='<%# Eval("id") %>' /></td>

                                <td id="Details_" style="width: 100px">
                                    <asp:Button runat="server" Text="Details" OnClick="Details_Click" CommandArgument='<%# Eval("id") %>' />
                                </td>
                            </tr>




                        </ItemTemplate>

                        <FooterTemplate>
                            </tbody>
                         </table>
                        </FooterTemplate>
                    </asp:Repeater>

                </div>

            </div>

        </div>
    </div>

</asp:Content>
