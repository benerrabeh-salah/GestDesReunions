using GestDesReunions.MAJ;
using GestDesReunions.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestDesReunions.Admin
{
    public partial class Utilisateur : System.Web.UI.Page
    {
        private readonly AdoNetHelper dbHelper = new AdoNetHelper("cnxStrGestionReunion");
        protected void Page_Load(object sender, EventArgs e)
        {
         
            AfficherUtilisateur();
        }

        //méthode pour afficher tout les utilisateurs
        private void AfficherUtilisateur()
        {

            /*string query = "select NumOrdreAnnuel as 'Num Ordre Annuel'" +
                           "DateArrivee as 'Date Arrivee'" +
                           "from ArriveeTbl";*/


            string query = "select Id 'Id Utilisateur', username 'Nom d utilisateur', password 'Mot de passe', isActive 'Actif', isAdmin 'Administrateur', consult_ 'Chercher', input_ 'Ajouter', edit_ 'Modifier', delete_ 'Supprimer', print_ 'Impression' FROM UserTbl";
            users_GV.DataSource = dbHelper.GetDataTable(query);
            users_GV.DataBind();

        }


        //bouton ajouter
        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(T_username.Text))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Merci de saisir un nom d utilisateur!!', 'warning')", true);
                    errorMessage.InnerText = "Merci de saisir un nom d'utilisateur !!";
                    T_username.Focus();
                }

                else if (string.IsNullOrEmpty(T_Password.Text))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Merci de saisir un mot de passe !!', 'warning')", true);
                    errorMessage.InnerText = "Merci de saisir un mot de passe !!";
                    T_Password.Focus();
                }

                
                else if (string.IsNullOrEmpty(T_Password2.Text))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Merci de retaper le mot de passe !!', 'warning')", true);
                    errorMessage.InnerText = "Merci de retaper le mot de passe !!";
                    T_Password2.Focus();
                }

                else if ( (check_insert.Checked == false) && (check_update.Checked == false) && (check_delete.Checked == false) && (check_search.Checked == false) && (check_print.Checked == false))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Merci de de donner des droits à cet utilisateur !!', 'warning')", true);
                    errorMessage.InnerText = "Merci de de donner des droits à cet utilisateur !!";                   
                }

                else if (!string.Equals(T_Password.Text, T_Password2.Text))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'les deux mot de passe entrés ne sont pas identiques !!', 'warning')", true);
                    errorMessage.InnerText = "les deux mot de passe entrés ne sont pas identiques";
                    T_Password.Focus();
                    T_Password2.Focus();
                }


                else
                {                  
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = dbHelper.Connection;
                        cmd.CommandText = "insert into UserTbl (username, password, isActive, isAdmin, consult_, input_, edit_, delete_, print_) values (@username, @password, @isActive, @isAdmin, @consult_, @input_, @edit_, @delete_, @print_)";
                        cmd.Parameters.AddWithValue("@username", T_username.Text);
                        cmd.Parameters.AddWithValue("@password", T_Password.Text);

                        if (Actif_oui.Checked)
                        {
                            cmd.Parameters.AddWithValue("@isActive", true);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@isActive", false);
                        }

                        if (Admin_oui.Checked)
                        {
                            cmd.Parameters.AddWithValue("@isAdmin", true);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@isAdmin", false);
                        }

                        if (check_search.Checked)
                        {
                            cmd.Parameters.AddWithValue("@consult_", true);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@consult_", false);
                        }

                        if (check_insert.Checked)
                        {
                            cmd.Parameters.AddWithValue("@input_", true);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@input_", false);
                        }

                        if (check_delete.Checked)
                        {
                            cmd.Parameters.AddWithValue("@delete_", true);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@delete_", false);
                        }

                        if (check_update.Checked)
                        {
                            cmd.Parameters.AddWithValue("@edit_", true);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@edit_", false);
                        }

                        if (check_print.Checked)
                        {
                            cmd.Parameters.AddWithValue("@print_", true);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@print_", false);
                        }

                        dbHelper.OpenConnection();

                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            errorMessage.InnerText = "";
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Succès!', ' "+T_username.Text+" a bien été ajouté !!', 'success')", true);
                            lblmessage.Text = " "+T_username.Text+" a bien été ajouté !!";
                            T_username.Text = "";
                            T_Password.Text = "";
                            T_Password2.Text = "";
                            Actif_oui.Checked = true;
                            Admin_non.Checked = true;
                            check_insert.Checked = false;
                            check_delete.Checked = false;
                            check_update.Checked = false;
                            check_search.Checked = false;
                            check_print.Checked = false;

                            AfficherUtilisateur();
                        }
                        dbHelper.CloseConnection();

                    }
                }
            }
            catch (Exception ex)
            {

                lblmessage.Text = ex.Message;
            }
        }
        int key = 0;
        protected void LinkBtnDelete_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as Control).NamingContainer).RowIndex;
            int user_id = Convert.ToInt32(users_GV.Rows[rowindex].Cells[2].Text);

            using (SqlCommand cmd = new SqlCommand())
            {
                
                cmd.Connection = dbHelper.Connection;
                cmd.CommandText = "delete from UserTbl where Id = '"+user_id+"'";
                //cmd.Parameters.AddWithValue("@id_reunion", Convert.ToInt32(TId.Text));
                dbHelper.OpenConnection();

                if (cmd.ExecuteNonQuery() > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Utilisateur a bien été supprimé!');", true);
                }
                dbHelper.CloseConnection();
            }
            AfficherUtilisateur();
        }
    }
}