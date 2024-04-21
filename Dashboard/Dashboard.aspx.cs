using GestDesReunions.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestDesReunions.Dashboard
{
	public partial class Dashboard : System.Web.UI.Page
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
                BindStatistics();
            }
        }



        protected void BindStatistics()
        {
            using (SqlConnection connection = dbHelper.Connection)
            {
                connection.Open();
                // Exemple de requête pour obtenir le total des réunions
                string totalMeetingsQuery = "SELECT COUNT(*) FROM ReunionTbl WHERE supprimer IS NULL";
                SqlCommand command = new SqlCommand(totalMeetingsQuery, connection);
                int totalMeetings = (int)command.ExecuteScalar();

                // Exemple de requête pour obtenir le nombre de réunions Programmee
                string ClotureMeetingsQuery = "SELECT COUNT(*) FROM ReunionTbl WHERE Statut = 'Cloture' AND supprimer IS NULL";
                command = new SqlCommand(ClotureMeetingsQuery, connection);
                int ClotureMeetings = (int)command.ExecuteScalar();

                // Exemple de requête pour obtenir le nombre de réunions Programmee
                string ProgrammeeMeetingsQuery = "SELECT COUNT(*) FROM ReunionTbl WHERE Statut = 'Programmee' AND supprimer IS NULL";
                command = new SqlCommand(ProgrammeeMeetingsQuery, connection);
                int ProgrammeeMeetings = (int)command.ExecuteScalar();

                // Exemple de requête pour obtenir le nombre de réunions Programmee
                string non_encore_programmeeMeetingsQuery = "SELECT COUNT(*) FROM ReunionTbl WHERE Statut = 'non encore programmee' AND supprimer IS NULL";
                command = new SqlCommand(non_encore_programmeeMeetingsQuery, connection);
                int non_encore_programmeeMeetings = (int)command.ExecuteScalar();

                // Exemple de requête pour obtenir le nombre de réunions Programmee
                string AutreMeetingsQuery = "SELECT COUNT(*) FROM ReunionTbl WHERE Statut = 'Autre' AND supprimer IS NULL";
                command = new SqlCommand(AutreMeetingsQuery, connection);
                int AutreMeetings = (int)command.ExecuteScalar();
                
                // Exemple de requête pour obtenir le nombre de réunions Programmee
                string RecommandationMeetingsQuery = "SELECT COUNT(*) FROM ReunionTbl WHERE Statut = 'Avec recommandation' AND supprimer IS NULL";
                command = new SqlCommand(RecommandationMeetingsQuery, connection);
                int RecommandationMeetings = (int)command.ExecuteScalar();

                // Affichage des statistiques dans les cartes
                TotalMeetingsCard.Text = totalMeetings.ToString();
                ClotureMeetingsCard.Text = ClotureMeetings.ToString();
                ProgrammeeMeetingsCard.Text = ProgrammeeMeetings.ToString();
                non_encore_programmeeMeetingsCard.Text = non_encore_programmeeMeetings.ToString();
                AutreMeetingsCard.Text = AutreMeetings.ToString();
                RecommandationMeetingsCard.Text = RecommandationMeetings.ToString();

                connection.Close();
            }
        }

    }
}