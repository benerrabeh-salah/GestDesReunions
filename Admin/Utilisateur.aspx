<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Utilisateur.aspx.cs" Inherits="GestDesReunions.Admin.Utilisateur" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row " style="display: flex; align-items: center; justify-content: center">



        <div class="col-md-8">
            <div class="card mt-4" style="background-color: #EAE7DD">
                <div class="card-header d-flex justify-content-center">
                    <h1>Gestion des utilisateurs</h1>
                </div>

                <div class="card-body" style="background-color: #add8e6">
                    <label id="errorMessage" runat="server" class="text-dark bg-danger mt-3"></label>
                    <asp:Label Text="" ForeColor="Green" Font-Bold="true" ID="lblmessage" CssClass="label label-success" runat="server" />
                    <div class="row mt-3 d-flex justify-content-center">
                        <div class="col-md-4 col-md-offset-1">
                            <div class="form-group">
                                <asp:Label runat="server"><b>Nom d'utilisateur :</b></asp:Label><br />
                                <asp:TextBox runat="server" placeholder="Nom d'utilisateur" Enabled="True" name="BrandName" ID="T_username" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row mt-3 d-flex justify-content-center">
                        <div class="col-md-4 col-md-offset-1">
                            <div class="form-group">
                                <asp:Label runat="server"><b>Mot de passe :</b></asp:Label><br />
                                <asp:TextBox runat="server" TextMode="Password" placeholder="Mot de passe" Enabled="True" name="BrandName" ID="T_Password" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>
                    </div>




                    <div class="row mt-3 d-flex justify-content-center">
                        <div class="col-md-4 col-md-offset-1">
                            <div class="form-group">
                                <asp:TextBox runat="server" TextMode="Password" placeholder="Retaper le mot de passe" Enabled="True" name="BrandName" ID="T_Password2" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>
                    </div>


                    <div class="row mt-3 d-flex justify-content-center">
                        <div class="col-md-4 col-md-offset-1">
                            <div class="form-group">
                                <asp:Label runat="server" for="NLettreArrTb"><b>Actif :</b></asp:Label><br />
                                <input type="radio" class="form-check-input" id="Actif_oui" checked="true" name="actifRadio" runat="server" />
                                <label class="form-check-label" for="UserRadio">Oui</label>

                                <input type="radio" class="form-check-input" id="Actif_non" name="actifRadio" runat="server" />
                                <label class="form-check-label" for="AdminRadio">Non</label>
                            </div>
                        </div>
                    </div>

                    <div class="row mt-3 d-flex justify-content-center">
                        <div class="col-md-4 col-md-offset-1">

                            <div class="form-group">
                                <asp:Label runat="server" for="NLettreArrTb"><b>Administrateur :</b></asp:Label><br />
                                <input type="radio" class="form-check-input" id="Admin_oui" name="role" runat="server" />
                                <label class="form-check-label" for="UserRadio">Oui</label>

                                <input type="radio" class="form-check-input" id="Admin_non" checked="true" name="role" runat="server" />
                                <label class="form-check-label" for="AdminRadio">Non</label>
                            </div>
                        </div>
                    </div>


                    <div class="row mt-3 d-flex justify-content-center">
                        <div class="col-md-4 col-md-offset-1">
                            <div class="form-group">
                                <asp:Label runat="server" for="DeProvstiTb"><b>Droits d'utilisateur :</b></asp:Label><br />
                                <asp:CheckBox ID="check_insert" Text="&nbsp;&nbsp;Insertion" runat="server" />
                                <br />
                                <asp:CheckBox ID="check_update" Text="&nbsp;&nbsp;Modification" runat="server" />
                                <br />
                                <asp:CheckBox ID="check_delete" Text="&nbsp;&nbsp;Suppression" runat="server" />
                                <br />
                                <asp:CheckBox ID="check_search" Text="&nbsp;&nbsp;Consultation" runat="server" />
                                <br />
                                <asp:CheckBox ID="check_print" Text="&nbsp;&nbsp;Impression" runat="server" />
                                <br />
                                <asp:CheckBox ID="check_instruction" Text="&nbsp;&nbsp;Instruction" runat="server" />
                                <br />
                            </div>
                        </div>
                    </div>






                    <br />
                    <div class="mb-3 d-grid justify-content-center">
                        <div class="form-group">
                            <asp:Button Text="Ajouter" ID="btn_save" CssClass="btn btn-success btn-lg" runat="server" OnClick="btn_save_Click" />
                            <asp:Button Text="Modifier" ID="btn_edit" CssClass="btn btn-primary btn-lg" runat="server" />

                        </div>

                    </div>

                <br />
                <div class="row">
                    <div class="col-md-12 col-md-offset-1">

                        <div class="form-group">
                            <div class="table-responsive">


                                <asp:GridView runat="server" class="table table-hover" ID="users_GV" AutoGenerateSelectButton="True"
                                    CssClass="table table-bordered table-condensed table-responsive table-hover">
                                    <AlternatingRowStyle BackColor="White" />
                                    <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Larger" ForeColor="White" />
                                    <RowStyle BackColor="#f5f5f5" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="true" ForeColor="White" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkBtnDelete" OnClientClick="return confirm('Voulez vous confirmer la suppression ?');" runat="server" OnClick="LinkBtnDelete_Click">Supprimer</asp:LinkButton>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </div>
                        </div>

                    </div>
                </div>


            </div>


        </div>
    </div>
    </div>
</asp:Content>
