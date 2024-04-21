<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reunionNonEncoreProgrammee.aspx.cs" Inherits="GestDesReunions.MAJ.reunionNonEncoreProgrammee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="table-responsive pt-4">
        <table class="table table-striped table-bordered table-responsive table-hover">
            <thead class="table-success">
                <tr>
                    <th style="width: 40px">ID</th>
                    <th style="width: 200px">Date réunion</th>
                    <th>Objet réunion</th>
                    <th style="width: 50px">Division</th>
                    <th>Action</th>

                </tr>
            </thead>
            <tbody>
                <asp:Repeater runat="server" ID="ReunionTable">
                    <ItemTemplate>
                        <tr>
                            <td style="width: 40px"><%# Eval("id") %></td>
                            <td style="width: 200px"><%# Eval("date_reunion") %></td>
                            <td><%# Eval("sujet_reunion") %></td>
                            <td style="width: 50px"><%# Eval("division") %></td>
                            <td style="width: 100px">
                                <asp:Button runat="server" Text="Details" OnClick="Details_Click" CommandArgument='<%# Eval("id") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>

</asp:Content>
