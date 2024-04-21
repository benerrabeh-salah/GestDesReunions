using GestDesReunions.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestDesReunions.MAJ
{
    public partial class SaisieReunion : System.Web.UI.Page
    {
        AdoNetHelper dbHelper = new AdoNetHelper("cnxStrGestionReunion");


        //chargement de la division
        private void chargerDivision()
        {
            if (Ddivision.Items.Count == 0)
            {
                using (DataTable dt2 = new DataTable())
                {
                    string qry = "select Id, nom_division from DivisionTbl";
                    SqlDataAdapter sda = new SqlDataAdapter(qry, dbHelper.Connection);
                    sda.Fill(dt2);
                    Ddivision.DataSource = dt2;
                    Ddivision.DataTextField = "nom_division";
                    Ddivision.DataValueField = "nom_division";
                    Ddivision.DataBind();
                }

                Ddivision.Items.Insert(0, new ListItem("-- Selectionner la division --", "0"));
            }
        }

        //chargement du statut
        private void chargerStatut()
        {
            if (DStatut.Items.Count == 0)
            {
                using (DataTable dt = new DataTable())
                {

                    //SqlConnection cnx = new SqlConnection(con.connString);
                    string qry = "select Id, status from StatutTbl";
                    SqlDataAdapter sda = new SqlDataAdapter(qry, dbHelper.Connection);
                    //DataTable dt = new DataTable();
                    sda.Fill(dt);

                    //chargement du statut réunion
                    DStatut.DataSource = dt;
                    DStatut.DataTextField = "status";
                    DStatut.DataValueField = "status";
                    DStatut.DataBind();
                }
                DStatut.Items.Insert(0, new ListItem("-- Statut de la réunion --", "0"));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Login/Login.aspx");
            }
            if (!IsPostBack)
            {
                chargerDivision();
                chargerStatut();
                AfficherSecteur();
            }
            
        }

        //méthode chargement du secteur
        private void AfficherSecteur()
        {
            using (DataTable dt2 = new DataTable())
            {
                string qry2 = "select Id, nom_secteur from SecteurTbl";
                SqlDataAdapter sda2 = new SqlDataAdapter(qry2, dbHelper.Connection);
                sda2.Fill(dt2);
                lstLeft_1.DataSource = dt2;
                lstLeft_1.DataTextField = "nom_secteur";
                lstLeft_1.DataValueField = "Id";
                lstLeft_1.DataBind();
            }
        }

        //méthode chargement du partenaire
        /*
        private void AfficherPartenaire()
        {
            using (DataTable dt3 = new DataTable())
            {
                string qry3 = "select Id, nom_partenaire from PartenaireTbl";
                SqlDataAdapter sda3 = new SqlDataAdapter(qry3, dbHelper.Connection);
                sda3.Fill(dt3);
                lstLeft_2.DataSource = dt3;
                lstLeft_2.DataTextField = "nom_partenaire";
                lstLeft_2.DataValueField = "Id";
                lstLeft_2.DataBind();
            }
        }
        */

        //bouton ajouter
        protected void btnsave_Click(object sender, EventArgs e)
        {

            try
            {
                lblmessage.Text = "";
                int parsedValue;
                decimal parsedValue2;
                decimal parsedValue3;

                if (string.IsNullOrEmpty(Tdate_reunion.Text))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Merci de saisir la date du réunion!!', 'warning')", true);
                    errorMessage.Text = "Merci de saisir la date du réunion!!";
                    Tdate_reunion.Focus();
                }

                else if (Ddivision.SelectedValue == "0")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Merci de choisir la division!!', 'warning')", true);
                    errorMessage.Text = "Merci de choisir la division!!";
                    Ddivision.Focus();
                }
                else if (string.IsNullOrEmpty(Tsujet_reunion.Text))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Merci de saisir sujet de la réunion!!', 'warning')", true);
                    errorMessage.Text = "Merci de saisir sujet de la réunion!!";
                    Tsujet_reunion.Focus();
                }

                
                else if (DStatut.SelectedValue == "0")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Merci de choisir le statut de la réunion!!', 'warning')", true);
                    errorMessage.Text = "Merci de choisir le statut de la réunion!!";
                    T_proch_reunion.Focus();
                }

                else
                {
                    //(date_reunion, sujet_reunion, division) 
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = dbHelper.Connection;
                        cmd.CommandText = "insert into ReunionTbl (date_reunion, sujet_reunion, division, etat_avancement, recommendation, proch_reunion, saisi_par, date_de_saisie, cadre, idcadre, cout_cadre, objet, secteur, partenaire, statut) values (@date_reunion, @sujet_reunion, @division, @etat_avancement, @recommendation, @proch_reunion, @saisi_par, @date_de_saisie, @cadre, @id_cadre, @cout, @objet, @secteur, @partenaire, @statut)";
                        cmd.Parameters.AddWithValue("@date_reunion", Convert.ToDateTime(Tdate_reunion.Text).ToString("dd/MM/yyyy"));
                        cmd.Parameters.AddWithValue("@sujet_reunion", Tsujet_reunion.Text);
                        cmd.Parameters.AddWithValue("@division", Ddivision.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@etat_avancement", T_etatAv.Text);
                        
                        cmd.Parameters.AddWithValue("@recommendation", T_recommendation.Text);
                        cmd.Parameters.AddWithValue("@partenaire", TPartenaire.Text);

                        cmd.Parameters.AddWithValue("@statut", DStatut.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@saisi_par", Session["id"].ToString());
                        cmd.Parameters.AddWithValue("@date_de_saisie", DateTime.Now);

                        //test sur le cadre de la réunion
                        if (DCadre.SelectedValue.ToString() == "0")
                        {
                            cmd.Parameters.AddWithValue("@cadre", DBNull.Value);
                            cmd.Parameters.AddWithValue("@id_cadre", DBNull.Value);
                            cmd.Parameters.AddWithValue("@objet", DBNull.Value);
                            cmd.Parameters.AddWithValue("@cout", DBNull.Value);
                        }

                        else
                        {                           
                            cmd.Parameters.AddWithValue("@cadre", DCadre.SelectedValue.ToString());

                            if (string.IsNullOrEmpty(T_IdCadre.Text))
                            {
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Merci de saisir Id du cadre !!', 'warning')", true);
                                errorMessage.Text = "Merci de saisir l'Id du cadre !!";
                                T_IdCadre.Focus();
                                return;
                            }
                            else if (!int.TryParse(T_IdCadre.Text, out parsedValue))
                            {
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Merci de saisir une valeur numérique pour Cadre ID !!', 'warning')", true);
                                errorMessage.Text = "Merci de saisir une valeur numérique pour Cadre ID !!";
                                T_IdCadre.Focus();
                                return;
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@id_cadre", int.Parse(T_IdCadre.Text.ToString()));
                            }

                            if (string.IsNullOrEmpty(T_cout.Text))
                            {
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Merci de saisir Coût du cadre !!', 'warning')", true);
                                errorMessage.Text = "Merci de saisir Coût du cadre !!";
                                T_cout.Focus();
                                return;
                            }
                            /*
                            else if (!decimal.TryParse(T_cout.Text, out parsedValue2))
                            {
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Merci de saisir une valeur numérique pour Coût du cadre !!', 'warning')", true);
                                errorMessage.Text = "Merci de saisir une valeur numérique pour Coût du cadre !!";
                                T_cout.Focus();
                                return;
                            }
                            */
                            else
                            {
                                cmd.Parameters.AddWithValue("@cout", T_cout.Text.ToString());
                            }

                            if (string.IsNullOrEmpty(TObjet.Text))
                            {
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Merci de saisir l objet du cadre !!', 'warning')", true);
                                errorMessage.Text = "Merci de saisir l'objet du cadre !!";
                                TObjet.Focus();
                                return;
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@objet", TObjet.Text);
                            }
                            
                        }

                  

                        //récuperation de la valeur du liste SECTEUR
                        string var_secteur = "";
                        foreach (ListItem listItem in lstRight_1.Items)
                        {

                            var_secteur = var_secteur + listItem.Text + ", ";
                        }

                        if (string.Equals(var_secteur, ", "))
                        {
                            cmd.Parameters.AddWithValue("@secteur", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@secteur", var_secteur);
                        }
                        

                        //récuperation de la valeur du liste PARTENAIRE
                        /*
                        string var_partenaire = "";
                        foreach (ListItem listItem in lstRight_2.Items)
                        {
                            var_partenaire = var_partenaire + listItem.Text + ", ";
                        }

                        if (string.Equals(var_partenaire, ", "))
                        {
                            cmd.Parameters.AddWithValue("@partenaire", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@partenaire", var_partenaire);
                        }
                        */


                        //contribution financière
                        /*
                        if (string.IsNullOrEmpty(T_contri_fina.Text))
                        {
                            
                            cmd.Parameters.AddWithValue("@contribution_financiere", DBNull.Value);
                            
                        }

                        
                        else if (!decimal.TryParse(T_contri_fina.Text, out parsedValue3))
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Merci de saisir une valeur numérique pour Contribution financière !!', 'warning')", true);
                            errorMessage.Text = "Merci de saisir une valeur numérique pour Contribution financière !!";
                            T_contri_fina.Focus();
                            return;
                        }
                        
                        else
                        {
                            cmd.Parameters.AddWithValue("@contribution_financiere", T_contri_fina.Text);
                        }
                        */

                        if (string.IsNullOrEmpty(T_proch_reunion.Text))
                        {

                            cmd.Parameters.AddWithValue("@proch_reunion", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@proch_reunion", Convert.ToDateTime(T_proch_reunion.Text));
                        }
                        dbHelper.OpenConnection();

                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Succès!', 'Réunion a bien été ajouté!', 'success')", true);
                            lblmessage.Text = "Réunion a bien été ajouté!";
                            errorMessage.Text = "";
                            Tdate_reunion.Text = "";
                            Tsujet_reunion.Text = "";
                            Ddivision.SelectedValue = "0";
                            T_recommendation.Text = "";
                            T_etatAv.Text = "";
                            DCadre.SelectedValue = "0";
                            T_IdCadre.Text= "";
                            TObjet.Text = "";
                            T_proch_reunion.Text = "";
                            DStatut.SelectedValue = "0";
                            TPartenaire.Text = "";
                            //clear secteur list
                            if (lstRight_1.Items.Count != 0)
                            {
                                //List will hold items to be removed.
                                List<ListItem> removedItems = new List<ListItem>();

                                //Loop and transfer the Items to Destination ListBox.
                                foreach (ListItem item in lstRight_1.Items)
                                {
                                    item.Selected = false;
                                    //lstLeft.Items.Add(item);
                                    lstLeft_1.Items.Add(item);
                                    removedItems.Add(item);
                                }

                                //Loop and remove the Items from the Source ListBox.
                                foreach (ListItem item in removedItems)
                                {
                                    lstRight_1.Items.Remove(item);
                                }
                            }

                            //clear partenaire list
                            //if (lstRight_2.Items.Count != 0)
                            //{
                            //    //List will hold items to be removed.
                            //    List<ListItem> removedItems = new List<ListItem>();

                            //    //Loop and transfer the Items to Destination ListBox.
                            //    foreach (ListItem item in lstRight_2.Items)
                            //    {
                            //        item.Selected = false;
                            //        //lstLeft.Items.Add(item);
                            //        lstLeft_2.Items.Add(item);
                            //        removedItems.Add(item);
                            //    }

                            //    //Loop and remove the Items from the Source ListBox.
                            //    foreach (ListItem item in removedItems)
                            //    {
                            //        lstRight_2.Items.Remove(item);
                            //    }
                            //}

                        }
                        dbHelper.CloseConnection();
                        //}


                    }
                }
            }
            catch (Exception ex)
            {

                lblmessage.Text = ex.Message;
            }

        }

        protected void LeftClick(object sender, EventArgs e)
        {
            //List will hold items to be removed.
            List<ListItem> removedItems = new List<ListItem>();

            //Loop and transfer the Items to Destination ListBox.
            foreach (ListItem item in lstRight_1.Items)
            {
                if (item.Selected)
                {
                    item.Selected = false;
                    //lstLeft.Items.Add(item);
                    lstLeft_1.Items.Add(item);
                    removedItems.Add(item);
                }
            }

            //Loop and remove the Items from the Source ListBox.
            foreach (ListItem item in removedItems)
            {
                lstRight_1.Items.Remove(item);
            }
        }

        protected void RightClick(object sender, EventArgs e)
        {
            //List will hold items to be removed.
            List<ListItem> removedItems = new List<ListItem>();

            //Loop and transfer the Items to Destination ListBox.
            foreach (ListItem item in lstLeft_1.Items)
            {
                if (item.Selected)
                {
                    item.Selected = false;
                    lstRight_1.Items.Add(item);
                    removedItems.Add(item);
                }
            }

            //Loop and remove the Items from the Source ListBox.
            foreach (ListItem item in removedItems)
            {
                lstLeft_1.Items.Remove(item);
            }
        }

        //protected void Left2Click(object sender, EventArgs e)
        //{
        //    //List will hold items to be removed.
        //    List<ListItem> removedItems = new List<ListItem>();

        //    //Loop and transfer the Items to Destination ListBox.
        //    foreach (ListItem item in lstRight_2.Items)
        //    {
        //        if (item.Selected)
        //        {
        //            item.Selected = false;
        //            //lstLeft.Items.Add(item);
        //            lstLeft_2.Items.Add(item);
        //            removedItems.Add(item);
        //        }
        //    }

        //    //Loop and remove the Items from the Source ListBox.
        //    foreach (ListItem item in removedItems)
        //    {
        //        lstRight_2.Items.Remove(item);
        //    }
        //}

        //protected void Right2Click(object sender, EventArgs e)
        //{
        //    //List will hold items to be removed.
        //    List<ListItem> removedItems = new List<ListItem>();

        //    //Loop and transfer the Items to Destination ListBox.
        //    foreach (ListItem item in lstLeft_2.Items)
        //    {
        //        if (item.Selected)
        //        {
        //            item.Selected = false;
        //            lstRight_2.Items.Add(item);
        //            removedItems.Add(item);
        //        }
        //    }

        //    //Loop and remove the Items from the Source ListBox.
        //    foreach (ListItem item in removedItems)
        //    {
        //        lstLeft_2.Items.Remove(item);
        //    }
        //}

        protected void DCadre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DCadre.SelectedValue.ToString() != "0")
            {
                TObjet.Enabled = true;
                T_IdCadre.Enabled = true;
                T_cout.Enabled = true;
            }
            else
            {
                TObjet.Enabled = false;
                T_IdCadre.Enabled = false;
                T_cout.Enabled = false;
            }
        }

        protected void btnSecteur_Click(object sender, EventArgs e)
        {
            if (T_secteur.Visible == false)
            {
                T_secteur.Visible = true;
            }
            else
            {
                if (string.IsNullOrEmpty(T_secteur.Text))
                {
                    errorSecteur.InnerText = "Merci de saisir un nouveau secteur !!";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Merci de saisir un nouveau secteur !!', 'warning')", true);
                }
                else
                {

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = dbHelper.Connection;
                        cmd.CommandText = "insert into SecteurTbl values (@secteur)";

                        cmd.Parameters.AddWithValue("@secteur", T_secteur.Text);

                        dbHelper.OpenConnection();

                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            /*
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Succès!', 'Secteur a bien été ajouté!', 'success')", true);
                            */
                            T_secteur.Text = "";
                        }
                        dbHelper.CloseConnection();
                    }
                    AfficherSecteur();
                }
            }
        }

        //protected void btnPartenaire_Click(object sender, EventArgs e)
        //{
        //    if (T_partenaire.Visible == false)
        //    {
        //        T_partenaire.Visible = true;
        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(T_partenaire.Text))
        //        {
        //            errorPartenaire.InnerText = "Merci de saisir un nouveau partenaire !!";
        //            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Merci de saisir un nouveau partenaire !!', 'warning')", true);
        //        }
        //        else
        //        {

        //            using (SqlCommand cmd = new SqlCommand())
        //            {
        //                cmd.Connection = dbHelper.Connection;
        //                cmd.CommandText = "insert into PartenaireTbl values (@partenaire)";

        //                cmd.Parameters.AddWithValue("@partenaire", T_partenaire.Text);

        //                dbHelper.OpenConnection();

        //                if (cmd.ExecuteNonQuery() > 0)
        //                {
        //                    /*
        //                    ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Succès!', 'Partenaire a bien été ajouté!', 'success')", true);
        //                    */
        //                    T_partenaire.Text = "";
        //                }
        //                dbHelper.CloseConnection();
        //            }
        //            AfficherPartenaire();
        //        }
        //    }
        //}
    }
}