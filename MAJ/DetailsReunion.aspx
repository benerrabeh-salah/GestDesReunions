<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetailsReunion.aspx.cs" Inherits="GestDesReunions.MAJ.DetailsReunion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function printDiv(divId) {
            var printContents = document.getElementById(divId).innerHTML;
            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;

            window.print();

            document.body.innerHTML = originalContents;
        }
    </script>

    <div class="container">
        <div class="row ">
            <div class="col-md-10 offset-md-1">
                <div id="printableArea">
                    <div class="card  mt-4">

                        <div class="card-header text-white" style="background-color: mediumaquamarine">
                            <h2 class="text-center">Détails de la réunion</h2>
                        </div>
                        <div class="card-body">
                            <dl class="row">

                                <dt class="col-sm-4">Date Réunion :</dt>
                                <dd class="col-sm-8">
                                    <asp:Label runat="server" ID="lblDateReunion" Font-Bold="True" Font-Size="Medium" /></dd>

                                <dt class="col-sm-4">Objet Réunion :</dt>
                                <dd class="col-sm-8">
                                    <%--<asp:Label runat="server" ID="lblSujetReunion" CssClass="font-weight-bold" /></dd>--%>
                                    <pre style="width: 600px; white-space: pre-wrap;"><asp:Label runat="server" ID="lblSujetReunion" Font-Bold="True" Font-Size="Medium" /></pre>
                                </dd>


                                <dt class="col-sm-4">Division :</dt>
                                <dd class="col-sm-8">
                                    <asp:Label runat="server" ID="lblDivision" Font-Bold="True" Font-Size="Medium" /></dd>

                                <dt class="col-sm-4">Cadre :</dt>
                                <dd class="col-sm-8">
                                    <asp:Label runat="server" ID="lblCadre" Font-Bold="True" Font-Size="Medium" /></dd>


                                <dt class="col-sm-4">Coût :</dt>
                                <dd class="col-sm-8">
                                    <asp:Label runat="server" ID="lblCoutCadre" Font-Bold="True" Font-Size="Medium" /></dd>

                                <dt class="col-sm-4">Objet :</dt>
                                <dd class="col-sm-8">
                                    <%--<asp:Label runat="server" ID="lblObjet" CssClass="font-weight-bold" /></dd>--%>
                                    <pre style="width: 600px; white-space: pre-wrap;"><asp:Label runat="server" ID="lblObjet" Font-Bold="True" Font-Size="Medium" /></pre>
                                </dd>

                                <dt class="col-sm-4">Secteur :</dt>
                                <dd class="col-sm-8">
                                    <asp:Label runat="server" ID="lblSecteur" CssClass="font-weight-bold" /></dd>

                                <dt class="col-sm-4">Partenaire et leur contribution financière :</dt>
                                <dd class="col-sm-8">
                                    <%--<asp:Label runat="server" ID="lblPartenaire" CssClass="font-weight-bold" /></dd>--%>
                                    <pre style="width: 600px; white-space: pre-wrap;"><asp:Label runat="server" ID="lblPartenaire" Font-Bold="True" Font-Size="Medium" /></pre>
                                </dd>

                                <dt class="col-sm-4">Recommandation :</dt>
                                <dd class="col-sm-8">
                                    <%--<asp:Label runat="server" ID="lblRecommandation" CssClass="font-weight-bold" /></dd>--%>
                                    <pre style="width: 600px; white-space: pre-wrap;"><asp:Label runat="server" ID="lblRecommandation" Font-Bold="True" Font-Size="Medium" /></pre>
                                </dd>
                                <dt class="col-sm-4">État d'avancement :</dt>
                                <dd class="col-sm-8">
                                    <%--<asp:Label runat="server" ID="lblEtatAvancement" CssClass="font-weight-bold" /></dd>--%>
                                    <pre style="width: 600px; white-space: pre-wrap;"><asp:Label runat="server" ID="lblEtatAvancement" Font-Bold="True" Font-Size="Medium" /></pre>
                                </dd>
                                <dt class="col-sm-4">Prochaine Réunion:</dt>
                                <dd class="col-sm-8">
                                    <asp:Label runat="server" ID="lblProchaineReunion" Font-Bold="True" Font-Size="Medium" /></dd>

                                <dt class="col-sm-4">Statut :</dt>
                                <dd class="col-sm-8">
                                    <asp:Label runat="server" ID="lblStatut" Font-Bold="True" /></dd>

                                <dt class="col-sm-4">
                                    <asp:Label Visible="false" ID="lblInstruction1" runat="server" Text="Label">Instructions :</asp:Label></dt>
                                <dd class="col-sm-8">
                                    <asp:Label runat="server" ID="lblInstruction2" Font-Bold="True" /></dd>
                            </dl>
                        </div>
                    </div>
                </div>
                <br />

                <div class=" d-grid justify-content-center">
                    <div class="form-group">
                        <input type="button" class="btn btn-primary btn-block" onclick="printDiv('printableArea')" value="  Imprimer  " />
                        <asp:Button Text="  Supprimer  " ID="btndelete" Class="btn btn-danger btn-block" runat="server" OnClientClick="return confirm('Voulez vous supprimer cette réunion ?');" OnClick="btndelete_Click" />
                        <asp:Button ID="BtnInstruction" Class="btn btn-info btn-block" runat="server" Text="Instruction" OnClick="BtnInstruction_Click" />
                    </div>

                </div>
                <div class="row  d-flex justify-content-center">
                    <div class="col-md-12 col-md-offset-1">
                        <div class="form-group">
                            <asp:Label runat="server" ID="Lbinst"><b>Instructions : </b></asp:Label>
                            <asp:Label ID="errorMessage" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Times New Roman" Font-Size="Large" ForeColor="Red" />
                            <asp:Label ForeColor="Green" Font-Bold="true" ID="lblmessage" CssClass="label label-success" runat="server" Font-Names="Comic Sans MS" Font-Size="Large" />
                            <br />
                            <asp:TextBox runat="server" TextMode="MultiLine" Enabled="True" name="InstructionName" ID="TInstructionId" CssClass="form-control input-sm"></asp:TextBox>
                        </div>
                    </div>
                </div>


            </div>

        </div>
    </div>

</asp:Content>
