<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModifReunion.aspx.cs" Inherits="GestDesReunions.MAJ.ModifReunion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--   <div class="container-fluid"> -->

    <div class="row " style="display: flex; align-items: center; justify-content: center">



        <div class="col-md-8">
            <div class="card mt-4" style="background-color: #EAE7DD">
                <div class="card-header d-flex justify-content-center">
                    <h1>Modification d'une réunion</h1>
                </div>

                <div class="card-body" style="background-color: #add8e6">
                    <asp:Label ID="errorMessage" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Times New Roman" Font-Size="Large" ForeColor="Red" />
                    <asp:Label ForeColor="Green" Font-Bold="true" ID="lblmessage" CssClass="label label-success" runat="server" Font-Names="Comic Sans MS" Font-Size="Large" />

                    <div class="row mt-3 d-flex justify-content-center">
                        <div class="col-md-4 col-md-offset-1">


                            <div class="form-group">
                                <asp:Label runat="server"><b>ID Réunion :</b></asp:Label><br />
                                <asp:TextBox runat="server" Enabled="True" name="BrandName" ID="TId" CssClass="form-control input-sm"></asp:TextBox>
                            </div>

                        </div>

                        <div class="col-md-4 col-md-offset-1">
                            <br />
                            <div class="form-group">
                                <asp:Button Text="  Chercher  " ID="btnsearch" Class="btn btn-danger btn-block" runat="server" OnClick="btnsearch_Click" />
                            </div>
                        </div>


                    </div>


                    <div class="row mt-3 d-flex justify-content-center">
                        <div class="col-md-6 col-md-offset-1">


                            <div class="form-group">
                                <asp:Label runat="server" for="DateRenTb"><b>Date Réunion :</b></asp:Label><br />
                                <asp:TextBox Enabled="false" runat="server" name="BrandName" ID="Tdate_reunion" CssClass="form-control input-sm"></asp:TextBox>
                            </div>


                        </div>

                        <div class="col-md-4 col-md-offset-1">

                            <div class="form-group">
                                <asp:Label runat="server"><b>Division : </b></asp:Label>
                                <!--    <asp:Label ID="lblDivision" runat="server" Visible="false"></asp:Label><br />  -->
                                <asp:DropDownList ID="Ddivision" Enabled="false" runat="server" name="BrandName" CssClass="form-control dropdown-toggle"></asp:DropDownList>
                            </div>

                        </div>
                    </div>

                    <div class="row mt-3 d-flex justify-content-center">
                        <div class="col-md-10 col-md-offset-1">


                            <div class="form-group">
                                <asp:Label runat="server"><b>Objet Réunion :</b></asp:Label><br />
                                <asp:TextBox runat="server" TextMode="MultiLine" Enabled="false" name="BrandName" ID="Tsujet_reunion" CssClass="form-control input-sm"></asp:TextBox>
                            </div>

                        </div>
                    </div>


                    <div class="row mt-3 d-flex justify-content-center">

                        <div class="col-md-4 col-md-offset-1">
                            <div class="form-group">
                                <asp:Label runat="server"><b>Cadre</b></asp:Label><br />
                                <asp:DropDownList ID="DCadre" Enabled="False" runat="server" name="BrandName" CssClass="form-control dropdown-toggle" OnSelectedIndexChanged="DCadre_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Text="-- Selectionner le cadre --" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Convention" Value="Convention" />
                                    <asp:ListItem Text="Programme" Value="Programme" />
                                    <asp:ListItem Text="Autre (à préciser)" Value="Autre" />
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2 col-md-offset-1">
                            <div class="form-group">
                                <asp:Label runat="server"><b>Code cadre</b></asp:Label><br />
                                <asp:TextBox runat="server" Enabled="False" placeholder="Code" name="BrandName" ID="T_IdCadre" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-4 col-md-offset-1">
                            <div class="form-group">
                                <asp:Label runat="server"><b>Coût</b></asp:Label><br />
                                <asp:TextBox runat="server" Enabled="False" placeholder="Coût" name="BrandName" ID="T_cout" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>


                    </div>


                    <div class="row mt-3 d-flex justify-content-center">
                        <div class="col-md-10 col-md-offset-1">
                            <div class="form-group">
                                <asp:Label runat="server"><b>Objet</b></asp:Label><br />
                                <asp:TextBox TextMode="MultiLine" runat="server" Enabled="False" placeholder="Objet" name="BrandName" ID="TObjet" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>
                    </div>


                    <div class="row mt-3 d-flex justify-content-center">
                        <div class="col-md-10 col-md-offset-1">

                            <div class="form-group">
                                <asp:Label runat="server"><b> Secteur : </b></asp:Label>

                                <asp:Label ID="lblSecteur" runat="server" Visible="false"></asp:Label><br />

                                <table border="0" cellpadding="10" cellspacing="5">

                                    <tr>
                                        <td>
                                            <asp:ListBox ID="lstLeft_1" runat="server" SelectionMode="Multiple" Width="200"></asp:ListBox>
                                        </td>
                                        <td>
                                            <asp:Button Enabled="false" ID="btnLeft" Text="<<" runat="server" OnClick="LeftClick" />
                                            <asp:Button Enabled="false" ID="btnRight" Text=">>" runat="server" OnClick="RightClick" />
                                        </td>
                                        <td>
                                            <asp:ListBox ID="lstRight_1" runat="server" SelectionMode="Multiple" Width="200"></asp:ListBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>


                        </div>
                    </div>

                    <div class="row mt-3 d-flex justify-content-center">
                        <div class="col-md-10 col-md-offset-1">
                            <asp:Button Text="Ajouter Secteur" ID="btnSecteur" CssClass="btn btn-primary" runat="server" OnClick="btnSecteur_Click" />
                            <br />
                            <asp:TextBox runat="server" Visible="false" name="BrandName" ID="T_secteur" placeholder="Secteur" CssClass="form-control input-sm"></asp:TextBox>

                            <label id="errorSecteur" runat="server" class="text-dark bg-danger mt-3"></label>
                        </div>
                    </div>

                    <div class="row mt-3 d-flex justify-content-center">
                        <div class="col-md-10 col-md-offset-1">
                            <div class="form-group">
                                <asp:Label runat="server"><b>Partenaires et leur contribution financière</b></asp:Label><br />

                                <%--      <table border="0" cellpadding="10" cellspacing="5">

                                    <tr>
                                        <td>
                                            <asp:ListBox ID="lstLeft_2" runat="server" SelectionMode="Multiple" Width="200"></asp:ListBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnLeft2" Text="<<" runat="server" OnClick="Left2Click" />
                                            <asp:Button ID="btnRight2" Text=">>" runat="server" OnClick="Right2Click" />
                                        </td>
                                        <td>
                                            <asp:ListBox ID="lstRight_2" runat="server" SelectionMode="Multiple" Width="200"></asp:ListBox>
                                        </td>
                                    </tr>
                                </table>--%>
                                <asp:TextBox runat="server" TextMode="MultiLine" Enabled="False" name="BrandName" ID="TPartenaire" CssClass="form-control input-sm"></asp:TextBox>

                            </div>
                        </div>
                    </div>

                    <%--             <div class="row mt-3 d-flex justify-content-center">
                        <div class="col-md-8 col-md-offset-1">

                            <div class="form-group">
                                <asp:Label runat="server"><b>Partenaire et leur contribution financière :</b></asp:Label>

                                <asp:Label ID="lblPartenaire" runat="server" Visible="false"></asp:Label><br />

                                <table border="0" cellpadding="10" cellspacing="5">

                                    <tr>
                                        <td>
                                            <asp:ListBox ID="lstLeft_2" runat="server" SelectionMode="Multiple" Width="200"></asp:ListBox>
                                        </td>
                                        <td>
                                            <asp:Button Enabled="false" ID="btnLeft2" Text="<<" runat="server" OnClick="Left2Click" />
                                            <asp:Button Enabled="false" ID="btnRight2" Text=">>" runat="server" OnClick="Right2Click" />
                                        </td>
                                        <td>
                                            <asp:ListBox ID="lstRight_2" runat="server" SelectionMode="Multiple" Width="200"></asp:ListBox>
                                        </td>
                                    </tr>
                                </table>

                            </div>


                        </div>
                    </div>

                    <div class="row mt-3 d-flex justify-content-center">
                        <div class="col-md-8 col-md-offset-1">

                            <asp:Button Text="Ajouter Partenaire" ID="btnPartenaire" CssClass="btn btn-primary" runat="server" OnClick="btnPartenaire_Click" />
                            <asp:TextBox runat="server" Visible="false" name="BrandName" ID="T_partenaire" placeholder="Partenaire" CssClass="form-control input-sm"></asp:TextBox>
                            <br />
                            <label id="errorPartenaire" runat="server" class="text-dark bg-danger mt-3"></label>

                        </div>
                    </div>--%>

                    <!--
                     <div class="row mt-3 d-flex justify-content-center">
                        <div class="col-md-8 col-md-offset-1">

                            <asp:Label runat="server"><b>Contribution financière</b></asp:Label><br />
                            <asp:TextBox runat="server" enabled="false" name="BrandName" ID="T_contri_fina" placeholder="Contribution" CssClass="form-control input-sm"></asp:TextBox>
                            <br />


                        </div>
                    </div>
                    -->

                    <div class="row mt-3 d-flex justify-content-center">
                        <div class="col-md-10 col-md-offset-1">


                            <div class="form-group">
                                <asp:Label runat="server"><b>Recommandation :</b></asp:Label><br />
                                <asp:TextBox runat="server" TextMode="MultiLine" Enabled="false" name="BrandName" ID="T_recommandation" CssClass="form-control input-sm"></asp:TextBox>
                            </div>

                        </div>
                    </div>


                    <div class="row mt-3 d-flex justify-content-center">
                        <div class="col-md-10 col-md-offset-1">


                            <div class="form-group">
                                <asp:Label runat="server"><b>État d'avancement</b></asp:Label><br />
                                <asp:TextBox runat="server" TextMode="MultiLine" Enabled="false" name="BrandName" ID="T_etatAv" CssClass="form-control input-sm"></asp:TextBox>
                            </div>

                        </div>
                    </div>

                    <div class="row mt-3 d-flex justify-content-center">
                        <div class="col-md-6 col-md-offset-1">


                            <div class="form-group">
                                <asp:Label runat="server"><b>Date prochaine réunion :</b></asp:Label><br />
                                <asp:TextBox runat="server" Enabled="false" name="BrandName" ID="T_proch_reunion" CssClass="form-control input-sm"></asp:TextBox>
                            </div>


                        </div>

                        <div class="col-md-4 col-md-offset-1">
                            <div class="form-group">
                                <asp:Label runat="server"><b>Statut Réunion : </b></asp:Label>
                                <asp:Label ID="lblStatut" runat="server" Visible="false"></asp:Label><br />
                                <asp:DropDownList Enabled="false" ID="DStatut" runat="server" name="BrandName" CssClass="form-control dropdown-toggle">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <br />

                    <!-- modify buttons -->
                    <div class="mb-3 d-grid justify-content-center">
                        <div class="form-group">

                            <asp:Button Text="  Modifier  " ID="btnedit" Width="200px" Class="btn btn-success btn-block" runat="server" OnClick="btnedit_Click" />

                        </div>
                    </div>

                    <!------------------------------->




                </div>


            </div>
        </div>
    </div>
    <!--   </div> -->
</asp:Content>
