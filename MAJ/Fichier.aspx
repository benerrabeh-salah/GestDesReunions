<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Fichier.aspx.cs" Inherits="GestDesReunions.MAJ.Fichier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <h1 style="text-align: center; font-weight: bold; color: blue; font-size: 24px; font-family: Arial, sans-serif;">Gestion des fichiers : </h1><br />
        <asp:Label ID="lblSucess" ForeColor="Green" Font-Bold="true" CssClass="label label-success" runat="server" Font-Names="Comic Sans MS" Font-Size="Large"></asp:Label>
        <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Times New Roman" Font-Size="Large" ForeColor="Red"></asp:Label><br />
        
        <asp:FileUpload ID="fileUpload" CssClass="col-md-4" runat="server" AllowedFileTypes=".jpg,.jpeg,.png,.pdf" />
        <asp:Button ID="btnUpload" CssClass="col-md-2" runat="server" Text="Charger Fichier" OnClick="btnUpload_Click" />
    </div>

    <div class="row mt-3">
        <div class="table-responsive pt-4">
            <table class="table table-striped table-bordered table-responsive table-hover">
                <thead class="table-success">
                    <tr>
                        <th>Nom du fichier</th>
                        <th colspan="2" style="text-align:center">Action</th>

                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="RepeaterFiles">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%# Eval("FileName_") %>
                                </td>
                                <td id="Afficher" style="width: 100px">
                                    <asp:Button runat="server" Text="Télécharger" OnClick="afficherFicher_Click" CommandArgument='<%# String.Format("{0},{1}", Eval("ReunionId"), Eval("FileID")) %>' />

                                </td>

                                <td id="Supprimer" style="width: 100px">
                                    <asp:Button runat="server" Text="Supprimer" OnClick="supprimerFicher_Click" CommandArgument='<%# String.Format("{0},{1}", Eval("ReunionId"), Eval("FileID")) %>' />

                                </td>
                                
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
  
    </div>

</asp:Content>
