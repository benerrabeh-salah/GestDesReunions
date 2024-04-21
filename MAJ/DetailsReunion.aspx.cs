using System;
using System.Data.SqlClient;
using GestDesReunions.Models; // Assuming AdoNetHelper is in the Models namespace
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestDesReunions.MAJ
{
	public partial class DetailsReunion : System.Web.UI.Page
	{
		private readonly AdoNetHelper dbHelper = new AdoNetHelper("cnxStrGestionReunion");

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!User.Identity.IsAuthenticated)
			{
				Response.Redirect("~/Login/Login.aspx");
			}

			if (int.TryParse(Request.QueryString["reunionId"], out int reunionId))
			{
				GetReunionDetailsFromDatabase(reunionId);
			}
			else
			{
				Response.Redirect("ListeReunions.aspx");
			}

			/* ------------ début : test des droits d'utilisateur ------------ */

			if (Session["instruction_"] != null )
			{
				if (Session["instruction_"].ToString() == "False")
				{
					TInstructionId.Visible = false;
					BtnInstruction.Visible = false;
					Lbinst.Visible = false;
				}
			}
			/* ------------ fin : test des droits d'utilisateur ------------ */
		}

		private void GetReunionDetailsFromDatabase(int reunionId)
		{
			try
			{
				dbHelper.OpenConnection();

				string query = @"SELECT r.Id, r.date_reunion, r.sujet_reunion, r.division, 
                                r.recommendation, r.proch_reunion, 
                                r.cadre, r.objet, r.cout_cadre, r.secteur, 
                                r.partenaire,  r.statut, 
                                r.etat_avancement, i.Objet AS InstructionObjet 
									 FROM ReunionTbl r 
									 LEFT JOIN InstructionTbl i ON r.Id = i.IdReunion 
									 WHERE r.Id = @ReunionId";

				using (SqlCommand command = new SqlCommand(query, dbHelper.Connection))
				{
					command.Parameters.AddWithValue("@ReunionId", reunionId);

					using (SqlDataReader reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							lblDateReunion.Text = reader.GetDateTime(1).ToShortDateString();
							lblSujetReunion.Text = reader.GetString(2);
							lblDivision.Text = reader.GetString(3);
							lblRecommandation.Text = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
							lblProchaineReunion.Text = reader.IsDBNull(5) ? string.Empty : reader.GetDateTime(5).ToShortDateString();
							lblCadre.Text = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
							lblObjet.Text = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
							lblCoutCadre.Text = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                            //lblSecteur.Text = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                            lblSecteur.Text = RemoveLastComma(reader.IsDBNull(9) ? string.Empty : reader.GetString(9));
                            lblPartenaire.Text = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
							lblStatut.Text = reader.GetString(11);
							lblEtatAvancement.Text = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);

							if (reader.IsDBNull(13))
							{
								lblInstruction2.Text = string.Empty;
								lblInstruction1.Visible = false;
								BtnInstruction.Text = "Ajouter Instruction";
							}
							else
							{
								lblInstruction2.Text = reader.GetString(13);
								lblInstruction1.Visible = true;
								BtnInstruction.Text = "Modifier Instruction";
							}
						}
						else
						{
							Response.Redirect("ListeReunions.aspx");
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
				// Gérer ou enregistrer l'exception de manière appropriée
			}
			finally
			{
				dbHelper.CloseConnection();
			}
		}

        private string RemoveLastComma(string input)
        {
            if (!string.IsNullOrEmpty(input) && input.EndsWith(","))
            {
                return input.Substring(0, input.Length - 1);
            }
            return input;
        }


        protected void btndelete_Click(object sender, EventArgs e)
		{
			try
			{
				int.TryParse(Request.QueryString["reunionId"], out int reunionId);
				using (SqlConnection cnx = dbHelper.Connection)
				{
					cnx.Open();

					using (SqlCommand cmd = new SqlCommand("Update ReunionTbl set supprimer = 'supprimée' WHERE Id = @id_reunion ", cnx))
					{
						cmd.Parameters.AddWithValue("@id_reunion", reunionId);
						int rowsAffected = cmd.ExecuteNonQuery();
						if (rowsAffected > 0)
						{
							Response.Redirect("ListeReunions.aspx");
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
			}
		}

		protected void BtnInstruction_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(TInstructionId.Text))
			{
				ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Merci de saisir une instruction !!', 'warning')", true);
				errorMessage.Text = "Merci de saisir une instruction d'abord !!";
				lblmessage.Text = "";
				TInstructionId.Focus();
				return ;
			}
			dbHelper.OpenConnection();
			int idReunion = int.Parse(Request.QueryString["reunionId"]);
			string instructionText = TInstructionId.Text;
			string selectQuery = @"
								SELECT 
									COUNT(*) AS InstructionCount,
									MAX(Objet) AS ExistingInstructionObjet
								FROM 
									[dbo].[InstructionTbl]
								WHERE 
									[IdReunion] = @IdReunion
								GROUP BY 
									IdReunion";
			SqlCommand selectCommand = new SqlCommand(selectQuery, dbHelper.Connection);
			selectCommand.Parameters.AddWithValue("@IdReunion", idReunion);
			SqlDataReader reader = selectCommand.ExecuteReader();

			int instructionCount = 0;
			string existingInstructionObjet = "";

			if (reader.Read())
			{
				instructionCount = Convert.ToInt32(reader["InstructionCount"]);
				existingInstructionObjet = reader["ExistingInstructionObjet"] != DBNull.Value ? reader["ExistingInstructionObjet"].ToString() : "";
			}

			reader.Close(); // Close the reader before executing the update or insert command

			if (instructionCount > 0)
			{
				string updateQuery = "UPDATE [dbo].[InstructionTbl] SET [Objet] = @Objet WHERE [IdReunion] = @IdReunion";
				SqlCommand updateCommand = new SqlCommand(updateQuery, dbHelper.Connection);
				updateCommand.Parameters.AddWithValue("@IdReunion", idReunion);
				updateCommand.Parameters.AddWithValue("@Objet", $"{existingInstructionObjet} / {instructionText}");
				updateCommand.ExecuteNonQuery();
				lblmessage.Text = "Instruction a été modifié avec succès !!";
				errorMessage.Text = "";
				ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Succès!', 'Instruction a été modifié avec succès !!', 'success')", true);
				GetReunionDetailsFromDatabase(idReunion);
			}
			else
			{
				// Insert new instruction
				string insertQuery = "INSERT INTO [dbo].[InstructionTbl] ([IdReunion], [Objet]) VALUES (@IdReunion, @Objet)";
				SqlCommand insertCommand = new SqlCommand(insertQuery, dbHelper.Connection);
				insertCommand.Parameters.AddWithValue("@IdReunion", idReunion);
				insertCommand.Parameters.AddWithValue("@Objet", instructionText);
				insertCommand.ExecuteNonQuery();
                lblmessage.Text = "Instruction a bien été ajoutée !!";
                errorMessage.Text = "";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Succès!', 'Instruction a bien été ajoutée !!', 'success')", true);
				GetReunionDetailsFromDatabase(idReunion);
			}

			dbHelper.CloseConnection();
		}


	}
}
