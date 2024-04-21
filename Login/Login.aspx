<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GestDesReunions.Login.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link rel="stylesheet" href="../Content/bootstrap.min.css" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

</head>
<body style="background-color : #dcf9ff">



    <div class="container-fluid">
        <div class="row " style="height: 200px">
            <h1 class="d-flex align-items-center justify-content-center">
                Bienvenue
            </h1>
        </div>
        <div class="row">
            
            <div class="col-md-6">
             <!--   <img src="~/Asset/images/Meeting-Vector-PNG-HD-Isolated.png" class="img-fluid" /> -->
             <!--   <img src="@Url.Content("~/images/Meeting-Vector-PNG-HD-Isolated.png")" /> -->
                <img src="<%=ResolveUrl("~/Asset/images/Meeting-Vector-PNG-HD-Isolated.png")%>" />
            </div>

            <div class="col-md-4">
                <div class="card">
                    <div class="card-header">
                        <h1>Identification</h1>
                    </div>

                    <div class="card-body">


                        <form runat="server">

                            <div class="mb-3">

                                <label for="UserName" class="form-label">Nom d'utilisateur :</label>


                                <input type="text" placeholder="Utilisateur" class="form-control" id="UserName" runat="server" required="required" />

                            </div>

                            <div class="mb-3">
                                <label for="UserPasswordTb" class="form-label">Mot de passe :</label>
                                <input type="password" placeholder="Mot de passe" class="form-control" id="UserPasswordTb" runat="server" required="required" />
                            </div>

                            <div class="mb-3 d-grid">

                                <label id="infoMessage" runat="server" class="text-dark bg-danger mt-3"></label>

                                <asp:Button Text="  Connecter  " class="btn btn-primary btn-block" runat="server" ID="SaveBtn" OnClick="SaveBtn_Click" />
                            </div>

                        </form>
                    </div>


                </div>
            </div>
        </div>
    </div>

</body>
</html>
