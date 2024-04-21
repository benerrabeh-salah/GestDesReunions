<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListeReunions.aspx.cs" Inherits="GestDesReunions.MAJ.ListeReunions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Asset/css/styleNotif.css?v=<?=filemtime('../Asset/css/styleNotif.css')?>" rel="stylesheet" />
    <div class="row pt-1" style="height: 100vh; width: 100vw">
        <div class="col-md-10 col-md-offset-1">
            <div class="form-group">


                <div id="notif">
                    <asp:Label ID="lbNotif" runat="server" Text="Cliquer sur le lien suivant pour voir les prochaines réunions : "></asp:Label>
                    <a href="./reunionProche.aspx" id="linkNotif">Cliquer ici</a>
                </div>

                <br />

                <div id="notif2">
                    <asp:Label ID="lbNotif2" runat="server" Text="Cliquer sur le lien suivant pour voir les réunions non encore programmées : "></asp:Label>
                    <a href="./reunionNonEncoreProgrammee" id="linkNotif2" style="margin-left: 10px;">Cliquer ici</a>
                </div>

                <div class="table-responsive pt-2">
                    <table class="table table-striped table-bordered table-responsive table-hover">
                        <thead class="table-success">
                            <tr>
                                <th style="width: 40px">ID</th>
                                <th style="width: 150px">Date Réunion</th>
                                <th>Objet Réunion</th>
                                <th style="width: 50px">Division</th>
                                <th colspan="3" style="text-align: center">Action</th>

                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater runat="server" ID="ReunionTable">
                                <ItemTemplate>
                                    <tr>
                                        <td style="width: 40px"><%# Eval("id") %></td>
                                        <td style="width: 150px"><%# Eval("date_reunion") %></td>
                                        <td><%# Eval("sujet_reunion") %></td>
                                        <td style="width: 50px"><%# Eval("division") %></td>
                                        <td id="Details_" style="width: 100px">
                                            <asp:Button runat="server" Text="Details" OnClick="Details_Click" CommandArgument='<%# Eval("id") %>' />
                                        </td>
                                        <td id="Modifier_" style="width: 100px">
                                            <asp:Button runat="server" Text="Modifier" OnClick="Modifier_Click" CommandArgument='<%# Eval("id") %>' />
                                        </td>
                                        <td id="Fichier_" style="width: 100px">
                                            <asp:Button runat="server" Text="Fichier" OnClick="Fichier_Click" CommandArgument='<%# Eval("id") %>' />
                                        </td>

                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>


                </div>
            </div>
    </div>
    </div>


    <script>
        // Function to hide the div (réunions proches)
        function hideNotification() {
            document.getElementById('notif').style.display = 'none';
        }

        // Function to show the div (réunions proches)
        function showNotification() {
            document.getElementById('notif').style.display = 'block';
        }

        // Function to hide the div (réunions non encore programées)
        function hideNotification2() {
            document.getElementById('notif2').style.display = 'none';
        }

        // Function to show the div (réunions non encore programées)
        function showNotification2() {
            document.getElementById('notif2').style.display = 'block';
        }

        // Function to hide Détails button
        function hideDetailsButton() {
            document.getElementById('Details_').style.display = 'none';
        }

        // Function to hide Modifier button
        function hideModifierButton() {
            document.getElementById('Modifier_').style.display = 'none';
        }

        // Function to hide Supprimer button
        function hideSupprimerButton() {
            document.getElementById('Fichier_').style.display = 'none';
        }
    </script>

</asp:Content>
