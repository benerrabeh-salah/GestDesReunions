using GestDesReunions.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;



namespace GestDesReunions.MAJ
{
    public partial class ListeReunions : System.Web.UI.Page
    {
        AdoNetHelper dbHelper = new AdoNetHelper("cnxStrGestionReunion");

        protected void Page_Load(object sender, EventArgs e)
        {
            //ol testt = false;

            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Login/Login.aspx");
            }


            /* ------------ début : test des droits d'utilisateur ------------ */

            if (Session["consult_"] != null && Session["edit_"] != null)
            {
                if (Session["consult_"].ToString() == "False")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "hideDetailsButton", "hideDetailsButton();", true);
                }
                if (Session["edit_"].ToString() == "False")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "hideModifierButton", "hideModifierButton();", true);
                }
            }
            else
            {
                Response.Redirect("~/Login/Login.aspx");
            }
            /* ------------ fin : test des droits d'utilisateur ------------ */

            /* début */
            if (!IsPostBack)
            {FillTable();
                /* début : réunions programées */
                string query = "SELECT Id, FORMAT(date_reunion, 'dd/MM/yyyy') AS date_reunion, sujet_reunion, division FROM ReunionTbl where DATEDIFF(day, GETDATE() , proch_reunion) > 0 AND DATEDIFF(day, GETDATE() , proch_reunion) < 8 and supprimer is null";
                DataTable dt = dbHelper.GetDataTable(query);
                bool showNotification = (dt.Rows.Count > 0);
                /* fin : réunions programées */

                /* début : réunions non encore programées */
                string query2 = "SELECT id, FORMAT(date_reunion, 'dd/MM/yyyy') AS date_reunion, sujet_reunion, division FROM ReunionTbl where DATEDIFF(MONTH, date_reunion , GETDATE()) > 0 AND DATEDIFF(MONTH, date_reunion , GETDATE()) < 2 AND statut = 'Non encore programmée' AND supprimer is null";
                DataTable dt2 = dbHelper.GetDataTable(query2);
                bool showNotification2 = (dt2.Rows.Count > 0);
                /* fin : réunions non encore programées */
                

                bool conditionMet = false;

                if (showNotification && showNotification2)
                {
                    string message = "Il y a des réunions proches et des réunions non encore programmées !!";
                    // Show alert message
                    ShowAlert(message);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowNotification", "showNotification();", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowNotification2", "showNotification2();", true);
                    conditionMet = true;
                }

                if (!conditionMet && showNotification)
                {
                    string message = "Attention : il y a des réunions proches !!";
                    ShowAlert(message);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowNotification", "showNotification();", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "HideNotification2", "hideNotification2();", true);
                    conditionMet = true;
                }

                if (!conditionMet && showNotification2)
                {
                    string message = "Attention : ca passé un mois sur des réunions non encore programmées !!";
                    // Show alert message
                    ShowAlert(message);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowNotification2", "showNotification2();", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "HideNotification", "hideNotification();", true);
                    conditionMet = true;
                }

                if (!conditionMet)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "HideNotification", "hideNotification();", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "HideNotification2", "hideNotification2();", true);
                }
            }

            ///* début : réunions programées */
            //string query = "SELECT Id, FORMAT(date_reunion, 'dd/MM/yyyy') AS date_reunion, sujet_reunion, division FROM ReunionTbl where DATEDIFF(day, GETDATE() , proch_reunion) > 0 AND DATEDIFF(day, GETDATE() , proch_reunion) < 8 and supprimer is null";
            //DataTable dt = dbHelper.GetDataTable(query);
            //bool showNotification = (dt.Rows.Count > 0);
            ///* fin : réunions programées */

            ///* début : réunions non encore programées */
            //string query2 = "SELECT id, FORMAT(date_reunion, 'dd/MM/yyyy') AS date_reunion, sujet_reunion, division FROM ReunionTbl where DATEDIFF(MONTH, date_reunion , GETDATE()) > 0 AND DATEDIFF(MONTH, date_reunion , GETDATE()) < 2 AND statut = 'Non encore programmée' AND supprimer is null";
            //DataTable dt2 = dbHelper.GetDataTable(query2);
            //bool showNotification2 = (dt2.Rows.Count > 0);
            ///* fin : réunions non encore programées */



            //if (showNotification && showNotification2)
            //{
            //    string message = "Il y a des réunions proches et des réunions non encore programmées !!" + dt2.Rows.Count.ToString();
            //    // Show alert message
            //    ShowAlert(message);
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowNotification", "showNotification();", true);
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowNotification2", "showNotification2();", true);
            //    //stt = true;
            //    return;
            //}
            //else
            //{

            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "HideNotification", "hideNotification();", true);
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "HideNotification2", "hideNotification2();", true);
            //    return;
            //}




            //if (showNotification)
            //{
            //    string message = "Attention : il y a des réunions proches !!";
            //    ShowAlert(message);
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowNotification", "showNotification();", true);
            //    return;
            //}
            //else
            //{
            //    //stt = true;
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "HideNotification", "hideNotification();", true);
            //    return;
            //}



            //if (showNotification2)
            //{
            //    string message = "Attention : ca passé un mois sur des réunions non encore programmées !!";
            //    // Show alert message
            //    ShowAlert(message);
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowNotification2", "showNotification2();", true);
            //    return;
            //}
            //else
            //{

            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "HideNotification2", "hideNotification2();", true);
            //    return;
            //}


            //if (showNotification && showNotification2)
            //{
            //    string message = "Il y a des réunions proches et des réunions non encore programmées !!" + dt2.Rows.Count.ToString();
            //    // Show alert message
            //    ShowAlert(message);
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowNotification", "showNotification();", true);
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowNotification2", "showNotification2();", true);
            //    return; // Exit the method
            //}
            //else
            //{
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "HideNotification", "hideNotification();", true);
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "HideNotification2", "hideNotification2();", true);
            //    return; // Exit the method
            //}

            //if (showNotification)
            //{
            //    string message = "Attention : il y a des réunions proches !!";
            //    ShowAlert(message);
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowNotification", "showNotification();", true);
            //    return; // Exit the method
            //}
            //else
            //{
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "HideNotification", "hideNotification();", true);
            //    return; // Exit the method
            //}

            //if (showNotification2)
            //{
            //    string message = "Attention : ca passé un mois sur des réunions non encore programmées !!";
            //    // Show alert message
            //    ShowAlert(message);
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowNotification2", "showNotification2();", true);
            //    return; // Exit the method
            //}
            //else
            //{
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "HideNotification2", "hideNotification2();", true);
            //    return; // Exit the method
            //}




            //if (showNotification && showNotification2)
            //{

            //    string message = "Il y a des réunions proches et des réunions non encore programmées !! !!";
            //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //    sb.Append("<script type = 'text/javascript'>");
            //    sb.Append("window.onload=function(){");
            //    sb.Append("alert('");
            //    sb.Append(message);
            //    sb.Append("')};");
            //    sb.Append("</script>");
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

            //    return;
            //}

            //if (showNotification)
            //{
            //    string message = "Attention : il y a des réunions proches !!";
            //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //    sb.Append("<script type = 'text/javascript'>");
            //    sb.Append("window.onload=function(){");
            //    sb.Append("alert('");
            //    sb.Append(message);
            //    sb.Append("')};");
            //    sb.Append("</script>");
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
            //}

            //if (showNotification2)
            //{
            //    string message = "Attention : c'a passé un mois sur des réunions non encore programmées !!";
            //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //    sb.Append("<script type = 'text/javascript'>");
            //    sb.Append("window.onload=function(){");
            //    sb.Append("alert('");
            //    sb.Append(message);
            //    sb.Append("')};");
            //    sb.Append("</script>");
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
            //}

            //else if (showNotification)
            //{
            //    string message = "Attention : il y a des réunions proches !!";
            //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //    sb.Append("<script type = 'text/javascript'>");
            //    sb.Append("window.onload=function(){");
            //    sb.Append("alert('");
            //    sb.Append(message);
            //    sb.Append("')};");
            //    sb.Append("</script>");
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
            //}
            //else if (showNotification2)
            //{
            //    string message = "Attention : c'a passé un mois sur des réunions non encore programmées !!";
            //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //    sb.Append("<script type = 'text/javascript'>");
            //    sb.Append("window.onload=function(){");
            //    sb.Append("alert('");
            //    sb.Append(message);
            //    sb.Append("')};");
            //    sb.Append("</script>");
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
            //}
            //else
            //{
            //    string message = "Nothing !!";
            //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //    sb.Append("<script type = 'text/javascript'>");
            //    sb.Append("window.onload=function(){");
            //    sb.Append("alert('");
            //    sb.Append(message);
            //    sb.Append("')};");
            //    sb.Append("</script>");
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
            //}
            /* fin */










        }

        // Function to display alert message
        private void ShowAlert(string message)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type='text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(message);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        }

        protected void FillTable()
        {
            try
            {
                string query = "SELECT id, FORMAT(date_reunion, 'dd/MM/yyyy') AS date_reunion, sujet_reunion, division FROM ReunionTbl where supprimer is null";
                ReunionTable.DataSource = dbHelper.GetDataTable(query);
                ReunionTable.DataBind();
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine($"Error filling table: {ex.Message}");
            }
        }

        protected void Details_Click(object sender, EventArgs e)
        {
            Button btnDetails = (Button)sender;
            string reunionId = btnDetails.CommandArgument;

            Response.Redirect($"DetailsReunion.aspx?reunionId={reunionId}");
        }

        protected void Modifier_Click(object sender, EventArgs e)
        {
            Button btnModifier = (Button)sender;
            string reunionId = btnModifier.CommandArgument;

            Response.Redirect($"ModifReunion.aspx?reunionId={reunionId}");
        }

        //protected void Supprimer_Click(object sender, EventArgs e)
        //{
        //    Button btnSupprimer = (Button)sender;
        //    string reunionId = btnSupprimer.CommandArgument;

        //    try
        //    {
        //        if (!string.IsNullOrEmpty(reunionId) && int.TryParse(reunionId, out int id))
        //        {
        //            using (SqlConnection cnx = dbHelper.Connection)
        //            {
        //                cnx.Open();

        //                using (SqlCommand cmd = new SqlCommand("Update ReunionTbl set supprimer = 'supprimée' WHERE Id = @id_reunion ", cnx))
        //                {
        //                    cmd.Parameters.AddWithValue("@id_reunion", id);
        //                    int rowsAffected = cmd.ExecuteNonQuery();
        //                    if (rowsAffected > 0)
        //                    {
        //                        FillTable();
        //                    }
        //                    else
        //                    {
        //                        Console.WriteLine($"Reunion with ID {id} not found.");
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine($"Invalid reunionId: {reunionId}");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error: {ex.Message}");
        //    }
        //}

        protected void Fichier_Click(object sender, EventArgs e)
        {
            Button btnFichier = (Button)sender;
            string reunionId = btnFichier.CommandArgument;

            Response.Redirect($"Fichier.aspx?reunionId={reunionId}");
        }
    }
}
