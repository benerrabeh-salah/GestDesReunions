using GestDesReunions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestDesReunions
{
    public partial class SiteMaster : MasterPage
    {
        private readonly AdoNetHelper dbHelper = new AdoNetHelper("cnxStrGestionReunion");
        protected void Page_Load(object sender, EventArgs e)
        {
            /* debu authonticate et authorization*/
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            if (Session["id"] == null)
            {
                Response.Redirect("~/Login/Login.aspx");
            }
            else
            {
                userValue.Text = "Bonjour " + Session["id"].ToString();

                if (Session["isAdmin"].ToString() == "True")
                {
                    adminId.Visible = true;
                }

                if (Session["input_"].ToString() == "True")
                {
                    ajoutId.Visible = true;
                }

                if (Session["edit_"].ToString() == "True")
                {
                    modifId.Visible = true;
                }

                if (Session["consult_"].ToString() == "True")
                {
                    consultId.Visible = true;
                    listReunionsId.Visible = true;
                }
            }

            /* fin authonticate et authorization*/

            /*
            string QRY = "update ReunionTbl set statut = 'Clôturer' where DATEDIFF(day, '" + DateTime.Now + "', proch_reunion) < 0 ";

            dbHelper.GetDataTable(QRY);
            */

           
        }

        protected void LinkBtnDeconnexion_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut(); // Clear authentication cookie
            Session.Abandon(); // Abandon session
            Response.Cookies.Clear(); // Clear any existing cookies
            Response.Redirect("~/Login/Login.aspx"); // Redirect to login page
        }
    }
}