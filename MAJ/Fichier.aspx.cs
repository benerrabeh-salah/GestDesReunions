using GestDesReunions.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestDesReunions.MAJ
{
    public partial class Fichier : System.Web.UI.Page
    {
        AdoNetHelper dbHelper = new AdoNetHelper("cnxStrGestionReunion");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Login/Login.aspx");
            }

            if (!IsPostBack)
            {
                BindRepeater();
            }
            
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            bool isInt = int.TryParse(Request.QueryString["reunionId"], out int reunionId);
            if (fileUpload.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    byte[] fileData;

                    using (Stream inputStream = fileUpload.PostedFile.InputStream)
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            inputStream.CopyTo(memoryStream);
                            fileData = memoryStream.ToArray();
                        }
                    }
                    //SqlConnection connection = new SqlConnection(@"Data Source = SERV-IWSVA\SQLEXPRESS; Initial Catalog = GesReunions ;Integrated Security = true")
                    // Enregistrer les informations dans la base de données
                    using (SqlConnection connection = new SqlConnection(@"Data Source = DEV_AZILAL2\SQLEXPRESS; Initial Catalog = GesReunions ;User Id = Dsic ;Password= 123;"))
                    {
                        connection.Open();
                        string insertQuery = "INSERT INTO UploadedFiles (ReunionId,FileName_ , FileContent_) VALUES (@ReunionId,@FileName_, @FileContent_)";
                        SqlCommand command = new SqlCommand(insertQuery, connection);
                        command.Parameters.AddWithValue("@ReunionId", reunionId);
                        command.Parameters.AddWithValue("@FileName_", filename);
                        command.Parameters.AddWithValue("@FileContent_", fileData);
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    lblError.Text = "";

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Succès!', 'Fichier a bien été enregistré ... !!', 'success')", true);
                    lblSucess.Text = "Fichier a bien été enregistré ... !!";
                    
                }
                catch (Exception ex)
                {
                    lblSucess.Text = "";

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Une erreur est servenue lors du chargement du fichier !!', 'warning')", true);
                    lblError.Text = "Une erreur est servenue lors du chargement du fichier : " + ex.Message;
                }
            }
            else
            {
                lblSucess.Text = "";

                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Sélectionner un fichier ... !!', 'warning')", true);
                lblError.Text = "Sélectionner un fichier ... !!";
            }

            BindRepeater();
        }

        protected void afficherFicher_Click(object sender, EventArgs e)
        {
            Button btnAfficher = (Button)sender;
            string[] values = btnAfficher.CommandArgument.Split(',');
            string reunionId = values[0];
            string FileID = values[1];

            if (!string.IsNullOrEmpty(reunionId))
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source = DEV_AZILAL2\SQLEXPRESS; Initial Catalog = GesReunions ;User Id = Dsic ;Password= 123;"))
                {
                    connection.Open();
                    string selectQuery = "SELECT FileName_, FileContent_ FROM UploadedFiles WHERE ReunionId = @ReunionId and FileID = @FileID";
                    SqlCommand command = new SqlCommand(selectQuery, connection);
                    command.Parameters.AddWithValue("@ReunionId", reunionId);
                    command.Parameters.AddWithValue("@FileID", FileID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string fileName = reader["FileName_"].ToString();
                            byte[] fileData = (byte[])reader["FileContent_"];

                            string fileExtension = System.IO.Path.GetExtension(fileName);

                            // Affiche le nom du fichier
                            lblError.Text = "";
                            lblSucess.Text += "Nom du fichier: " + fileName + "a bien été ajouté !! <br />";

                            if (fileExtension.Equals(".jpg", StringComparison.InvariantCultureIgnoreCase) ||
                                fileExtension.Equals(".jpeg", StringComparison.InvariantCultureIgnoreCase) ||
                                fileExtension.Equals(".png", StringComparison.InvariantCultureIgnoreCase))
                            {
                                // Télécharger l'image
                                Response.Clear();
                                Response.Buffer = true;
                                Response.ContentType = "application/octet-stream";
                                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                                Response.BinaryWrite(fileData);
                                Response.End();
                            }
                            else if (fileExtension.Equals(".pdf", StringComparison.InvariantCultureIgnoreCase))
                            {
                                // Télécharger le PDF
                                Response.Clear();
                                Response.Buffer = true;
                                Response.ContentType = "application/pdf";
                                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                                Response.BinaryWrite(fileData);
                                Response.End();
                                Response.Flush(); // Assurez-vous de vider le tampon avant de terminer la réponse
                            }
                            else if (fileExtension.Equals(".docx", StringComparison.InvariantCultureIgnoreCase))
                            {
                                // Télécharger le fichier Word
                                Response.Clear();
                                Response.Buffer = true;
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                                Response.BinaryWrite(fileData);
                                Response.End();
                                Response.Flush(); // Assurez-vous de vider le tampon avant de terminer la réponse
                            }
                            else if (fileExtension.Equals(".xlsx", StringComparison.InvariantCultureIgnoreCase))
                            {
                                // Télécharger le fichier Excel
                                Response.Clear();
                                Response.Buffer = true;
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                                Response.BinaryWrite(fileData);
                                Response.End();
                                Response.Flush(); // Assurez-vous de vider le tampon avant de terminer la réponse
                            }
                            else
                            {
                                lblSucess.Text = "";
                                // Gérer d'autres types de fichiers si nécessaire
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Type de fichier non pris en charge !!', 'warning')", true);
                                lblError.Text += "Type de fichier non pris en charge: " + fileExtension + "<br />";
                            }
                        }

                        if (!reader.HasRows)
                        {
                            lblSucess.Text = "";

                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Pas de fichier trouvé pour cette réunion !!', 'warning')", true);
                            lblError.Text = "Pas de fichier trouvé pour cette réunion !!";
                        }
                    }
                    connection.Close();
                }
            }
            else
            {
                lblSucess.Text = "";

                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'ID Réunion non spécifié !!', 'warning')", true);
                lblError.Text = "ID Réunion non spécifié !!";
            }
        }

        protected void supprimerFicher_Click(object sender, EventArgs e)
        {
            Button btnAfficher = (Button)sender;
            string[] values = btnAfficher.CommandArgument.Split(',');
            string reunionId = values[0];
            string FileID = values[1];

            if (!string.IsNullOrEmpty(reunionId))
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source = DEV_AZILAL2\SQLEXPRESS; Initial Catalog = GesReunions ;User Id = Dsic ;Password= 123;"))
                {
                    connection.Open();
                    string selectQuery = "Delete FROM UploadedFiles WHERE ReunionId = @ReunionId and FileID = @FileID";
                    SqlCommand command = new SqlCommand(selectQuery, connection);
                    command.Parameters.AddWithValue("@ReunionId", reunionId);
                    command.Parameters.AddWithValue("@FileID", FileID);

                    
                    if (command.ExecuteNonQuery() > 0)
                    {
                        lblError.Text = "";

                        ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Succès!', 'Fichier a bien été supprimé !!', 'success')", true);
                        lblSucess.Text = "Fichier a bien été supprimé !!";
                    }
                    connection.Close();                    
                }
            }
            else
            {
                lblSucess.Text = "";

                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'ID Réunion non spécifié !!', 'warning')", true);
                lblError.Text = "ID Réunion non spécifié !!";
            }
            BindRepeater();
        }




        private void BindRepeater()
        {
            using (SqlConnection connection = dbHelper.Connection)
            {
                //RepeaterFiles.DataSource = null;

                int reunionId;
                if (int.TryParse(Request.QueryString["reunionId"], out reunionId))
                {
                    //dbHelper.OpenConnection();
                    string selectQuery = "SELECT FileID, ReunionId, FileName_ FROM UploadedFiles WHERE ReunionId = " + reunionId;
                    DataTable dataTable = dbHelper.GetDataTable(selectQuery);

                    RepeaterFiles.DataSource = dataTable;
                    RepeaterFiles.DataBind();
                    //dbHelper.CloseConnection();
                }
                else
                {
                    // Gérer l'erreur de l'ID de réunion manquant ou non valide
                }
            }
        }

    }
}