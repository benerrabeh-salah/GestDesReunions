using GestDesReunions.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestDesReunions.MAJ
{
    public partial class ModifReunion : System.Web.UI.Page
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

        //chargement du secteur
        private void chargerSecteur()
        {
            if (lstLeft_1.Items.Count == 0)
            {


                string qry2 = "select Id, nom_secteur from SecteurTbl";
                SqlDataAdapter sda2 = new SqlDataAdapter(qry2, dbHelper.Connection);
                DataTable dt2 = new DataTable();
                sda2.Fill(dt2);
                lstLeft_1.DataSource = dt2;
                lstLeft_1.DataTextField = "nom_secteur";
                lstLeft_1.DataValueField = "Id";
                lstLeft_1.DataBind();

            }

        }

        //chargement du partenaire
        //private void chargerPartenaire()
        //{
        //    if (lstLeft_2.Items.Count == 0)
        //    {


        //        string qry3 = "select Id, nom_partenaire from PartenaireTbl";
        //        SqlDataAdapter sda3 = new SqlDataAdapter(qry3, dbHelper.Connection);
        //        DataTable dt3 = new DataTable();
        //        sda3.Fill(dt3);
        //        lstLeft_2.DataSource = dt3;
        //        lstLeft_2.DataTextField = "nom_partenaire";
        //        lstLeft_2.DataValueField = "Id";
        //        lstLeft_2.DataBind();


        //    }

        //}
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
            //chargerDivision();
            //chargerStatut();
            //AfficherSecteur();
            //AfficherPartenaire();


            if (int.TryParse(Request.QueryString["reunionId"], out int reunionId))
            {
                TId.Text = reunionId.ToString();
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
        
        //private void AfficherPartenaire()
        //{
        //    using (DataTable dt3 = new DataTable())
        //    {
        //        string qry3 = "select Id, nom_partenaire from PartenaireTbl";
        //        SqlDataAdapter sda3 = new SqlDataAdapter(qry3, dbHelper.Connection);
        //        sda3.Fill(dt3);
        //        lstLeft_2.DataSource = dt3;
        //        lstLeft_2.DataTextField = "nom_partenaire";
        //        lstLeft_2.DataValueField = "Id";
        //        lstLeft_2.DataBind();
        //    }
        //}
        
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                int parsedValue;
                if (string.IsNullOrEmpty(TId.Text))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Merci de saisir ID Réunion !!', 'warning')", true);
                    errorMessage.Text = "Merci de saisir ID Réunion !!";
                }
                else if (!int.TryParse(TId.Text, out parsedValue))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Merci de saisir une valeur numérique', 'warning')", true);
                    errorMessage.Text = "Merci de saisir une valeur numérique";
                    return;

                }
                else
                {
                    Tdate_reunion.Enabled = true;
                    Ddivision.Enabled = true;
                    Tsujet_reunion.Enabled = true;
                    T_recommandation.Enabled = true;
                    T_etatAv.Enabled = true;
                    DCadre.Enabled = true;
                    btnLeft.Enabled = true;
                    btnRight.Enabled = true;
                    TPartenaire.Enabled = true;
                    //btnLeft2.Enabled = true;
                    //btnRight2.Enabled = true;
                    T_contri_fina.Enabled = true;
                    T_proch_reunion.Enabled = true;
                    DStatut.Enabled = true;

                    string query = "select * from ReunionTbl where Id = '" + int.Parse(TId.Text) + "'";
                    DataTable dt = dbHelper.GetDataTable(query);

                    if (dt.Rows.Count == 0)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Aucune Réunion trouvé avec ID saisi !!', 'warning')", true);
                        errorMessage.Text = "Aucune Réunion trouvé avec ID saisi !!";
                        //lblmessage.Text = "Aucune Réunion trouvé avec l'ID saisi !!";

                    }
                    else
                    {
                        errorMessage.Text = "";
                        TId.Text = dt.Rows[0]["Id"].ToString();
                        Tdate_reunion.Text = Convert.ToDateTime(dt.Rows[0]["date_reunion"]).ToString("dd/MM/yyyy");
                        Tsujet_reunion.Text = dt.Rows[0]["sujet_reunion"].ToString();
                        //Ddivision.SelectedItem.Value = dt.Rows[0]["division"].ToString();
                        //string divi = dt.Rows[0]["division"].ToString();
                        Ddivision.SelectedValue = dt.Rows[0]["division"].ToString();
                        /*lblDivision.Visible = true;
                        lblDivision.Text = dt.Rows[0]["division"].ToString();*/

                        //T_contri_fina.Text= dt.Rows[0]["contribution_partenaire"].ToString();
                        T_recommandation.Text = dt.Rows[0]["recommendation"].ToString();
                        T_etatAv.Text = dt.Rows[0]["etat_avancement"].ToString();

                        if (!string.IsNullOrEmpty(dt.Rows[0]["secteur"].ToString()))
                        {
                            string query2 = "select SUBSTRING(secteur,1,LEN(secteur)-1) from ReunionTbl where Id = '" + int.Parse(TId.Text) + "'";
                            DataTable dt2 = dbHelper.GetDataTable(query2);

                            lblSecteur.Visible = true;
                            lblSecteur.Text = dt2.Rows[0][0].ToString();
                        }


                        //if (!string.IsNullOrEmpty(dt.Rows[0]["partenaire"].ToString()))
                        //{
                        //    string query3 = "select SUBSTRING(partenaire,1,LEN(partenaire)-1) from ReunionTbl where Id = '" + int.Parse(TId.Text) + "'";
                        //    DataTable dt3 = dbHelper.GetDataTable(query3);

                        //    lblPartenaire.Visible = true;
                        //    lblPartenaire.Text = dt3.Rows[0][0].ToString();
                        //}


                        if (!string.IsNullOrEmpty(dt.Rows[0]["cadre"].ToString()))
                        {
                            DCadre.SelectedItem.Text = dt.Rows[0]["cadre"].ToString();
                            /*lblCadre.Visible = true;
                            lblCadre.Text = dt.Rows[0]["cadre"].ToString();*/
                            T_IdCadre.Enabled = true;
                            TObjet.Enabled = true;
                            T_cout.Enabled = true;
                        }

                        if (!string.IsNullOrEmpty(dt.Rows[0]["idcadre"].ToString()))
                        {
                            T_IdCadre.Text = dt.Rows[0]["idcadre"].ToString();
                            /*lblIdCadre.Visible = true;
                            lblIdCadre.Text = dt.Rows[0]["idcadre"].ToString(); */
                        }

                        if (!string.IsNullOrEmpty(dt.Rows[0]["objet"].ToString()))
                        {
                            TObjet.Text = dt.Rows[0]["objet"].ToString();
                            /*lblObjet.Visible = true;
                            lblObjet.Text = dt.Rows[0]["objet"].ToString();*/
                        }

                        if (!string.IsNullOrEmpty(dt.Rows[0]["cout_cadre"].ToString()))
                        {
                            T_cout.Text = dt.Rows[0]["cout_cadre"].ToString();
                            /*lblObjet.Visible = true;
                            lblObjet.Text = dt.Rows[0]["objet"].ToString();*/
                        }


                        if (string.IsNullOrEmpty(dt.Rows[0]["proch_reunion"].ToString()))
                        {
                            T_proch_reunion.Text = "";
                        }
                        else
                        {
                            T_proch_reunion.Text = Convert.ToDateTime(dt.Rows[0]["proch_reunion"]).ToString("dd/MM/yyyy");
                        }

                        /*lblStatut.Visible = true;
                        lblStatut.Text = dt.Rows[0]["statut"].ToString();*/
                        //DStatut.SelectedItem.Value = dt.Rows[0]["statut"].ToString();
                        DStatut.SelectedValue = dt.Rows[0]["statut"].ToString();
                        //DStatut.Text = dt.Rows[0]["statut"].ToString();


                    }
                }
            }
            catch (Exception ex)
            {

                lblmessage.Text = ex.Message;
            }
        }

        protected void btnedit_Click(object sender, EventArgs e)
        {
            decimal parsedValue2;
            decimal parsedValue3;

            try
            {
                if (string.IsNullOrEmpty(TId.Text) || string.IsNullOrEmpty(Tdate_reunion.Text))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Merci de chercher une réunion avant de la modifier !!', 'warning')", true);
                    errorMessage.Text = "Merci de chercher une réunion avant de la modifier !!";
                }
                
                else if (Ddivision.SelectedValue == "0")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Merci de choisir la division!!', 'warning')", true);
                    errorMessage.Text = "Merci de choisir la division!!";
                    Ddivision.Focus();
                }
                else if (DStatut.SelectedValue == "0")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Merci de choisir le statut de la réunion!!', 'warning')", true);
                    errorMessage.Text = "Merci de choisir le statut de la réunion!!";
                    Ddivision.Focus();
                }
                
                else
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = dbHelper.Connection;
                        cmd.CommandText = "update ReunionTbl set date_reunion = @date_reunion, sujet_reunion = @sujet_reunion, division = @division, etat_avancement = @etat_avancement, recommendation = @recommendation, secteur = @secteur, partenaire = @partenaire, proch_reunion = @proch_reunion, statut = @statut, cadre = @cadre, objet = @objet, cout_cadre = @cout_cadre, saisi_par = @saisi_par, date_de_modification = @date_de_modification where Id = '" + int.Parse(TId.Text) + "' ";

                        //vérification de la forme de la date prochaine de la réunion
                        DateTime dateformat;
                        if (DateTime.TryParseExact(Tdate_reunion.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out dateformat) == false)
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Merci de rentrer une date valide!!', 'warning')", true);
                            errorMessage.Text = "Merci de rentrer une date valide!!";
                            return;
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@date_reunion", Convert.ToDateTime(Tdate_reunion.Text));
                        }

                        cmd.Parameters.AddWithValue("@sujet_reunion", Tsujet_reunion.Text);
                        cmd.Parameters.AddWithValue("@division", Ddivision.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@etat_avancement", T_etatAv.Text);
                        cmd.Parameters.AddWithValue("@recommendation", T_recommandation.Text);


                        //vérification de la forme de la date prochaine de la réunion
                        //DateTime dateformat;

                        if (string.IsNullOrEmpty(T_proch_reunion.Text))
                        {
                            cmd.Parameters.AddWithValue("@proch_reunion", DBNull.Value);

                        }
                        else
                        {
                            if (DateTime.TryParseExact(T_proch_reunion.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out dateformat) == false)
                            {
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Merci de rentrer une date valide!!', 'warning')", true);
                                errorMessage.Text = "Merci de rentrer une date valide proch!!";
                                return;
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@proch_reunion", Convert.ToDateTime(T_proch_reunion.Text));
                            }

                        }



                        cmd.Parameters.AddWithValue("@saisi_par", Session["id"].ToString());
                        cmd.Parameters.AddWithValue("@date_de_modification", DateTime.Now);
                        cmd.Parameters.AddWithValue("@statut", DStatut.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@partenaire", TPartenaire.Text);


                        //récuperation de la valeur du liste SECTEUR
                        string var_secteur = "";
                        if (lstRight_1.Items.Count == 0)
                        {
                            cmd.Parameters.AddWithValue("@secteur", lblSecteur.Text + ",".ToString());
                        }
                        else
                        {
                            foreach (ListItem listItem in lstRight_1.Items)
                            {

                                var_secteur = var_secteur + listItem.Text + ",";
                            }
                            cmd.Parameters.AddWithValue("@secteur", var_secteur);
                        }



                        //récuperation de la valeur du liste PARTENAIRE
                        //string var_partenaire = "";
                        //if (lstRight_2.Items.Count == 0)
                        //{
                        //    cmd.Parameters.AddWithValue("@partenaire", lblPartenaire.Text + ",".ToString());
                        //}
                        //else
                        //{
                        //    foreach (ListItem listItem in lstRight_2.Items)
                        //    {
                        //        var_partenaire = var_partenaire + listItem.Text + ",";
                        //    }
                        //    cmd.Parameters.AddWithValue("@partenaire", var_partenaire);
                        //}





                        //test sur le cadre de la réunion
                        if (DCadre.SelectedValue.ToString() == "0")
                        {
                            cmd.Parameters.AddWithValue("@cadre", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@cadre", DCadre.SelectedItem.ToString());
                        }

                        //test sur l'Id cadre de la réunion
                        if (string.IsNullOrEmpty(T_IdCadre.Text) || T_IdCadre.Enabled == false)
                        {
                            cmd.Parameters.AddWithValue("@id_cadre", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@id_cadre", Convert.ToInt32(T_IdCadre.Text));
                        }

                        //test sur l'objet de la réunion
                        if (string.IsNullOrEmpty(TObjet.Text) || TObjet.Enabled == false)
                        {
                            cmd.Parameters.AddWithValue("@objet", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@objet", TObjet.Text);
                        }


                     

                        //test sur le cout du cadre réunion
                        if (string.IsNullOrEmpty(T_cout.Text) || T_cout.Enabled == false)
                        {
                            cmd.Parameters.AddWithValue("@cout_cadre", DBNull.Value);
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
                            cmd.Parameters.AddWithValue("@cout_cadre", T_cout.Text);
                        }


                        //contribution financière
                        /*
                        if (string.IsNullOrEmpty(T_contri_fina.Text) || T_contri_fina.Enabled == false)
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

                        dbHelper.OpenConnection();

                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Succès!', 'Réunion a bien été modifié !', 'success')", true);
                            lblmessage.Text = "Réunion a bien été modifié !";
                            errorMessage.Text = "";
                            TId.Text = "";
                            Tdate_reunion.Text = "";
                            Tsujet_reunion.Text = "";
                            Ddivision.SelectedValue = "0";
                            T_recommandation.Text = "";
                            T_etatAv.Text = "";
                            T_proch_reunion.Text = "";
                            DStatut.SelectedValue = "0";
                            TPartenaire.Text = "";

                            DCadre.SelectedValue = "0";
                            T_IdCadre.Text = "";
                            TObjet.Text = "";
                            T_cout.Text = "";
                            DStatut.SelectedValue = "0";


                            lblDivision.Text = "";
                            lblSecteur.Text = "";
                            //lblPartenaire.Text = "";
                            lblStatut.Text = "";

                            //vider liste secteur
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

                            //vider liste partenaire
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