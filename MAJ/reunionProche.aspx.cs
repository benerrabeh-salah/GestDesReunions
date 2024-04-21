using GestDesReunions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestDesReunions.MAJ
{
	public partial class reunionProche : System.Web.UI.Page
	{
        AdoNetHelper dbHelper = new AdoNetHelper("cnxStrGestionReunion");

        protected void Page_Load(object sender, EventArgs e)
		{
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Login/Login.aspx");
            }

            FillTable();
        }


        protected void FillTable()
        {
            try
            {
                string query = "SELECT id, FORMAT(proch_reunion, 'dd/MM/yyyy') AS proch_reunion, sujet_reunion, division FROM ReunionTbl where DATEDIFF(day, GETDATE() , proch_reunion) > 0 AND DATEDIFF(day, GETDATE() , proch_reunion) < 8 and supprimer is null";
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

    }
}