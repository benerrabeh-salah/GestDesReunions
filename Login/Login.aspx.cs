using GestDesReunions.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestDesReunions.Login
{
    public partial class Login : System.Web.UI.Page
    {
        AdoNetHelper dbHelper = new AdoNetHelper("cnxStrGestionReunion");


        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            string query = @" select * from UserTbl where username = '" + UserName.Value + "' and password = '" + UserPasswordTb.Value + "'";
            //query = string.Format(query, UserName.Value, UserPasswordTb.Value);
            DataTable dt = dbHelper.GetDataTable(query);

            if (dt.Rows.Count == 0)
            {
                infoMessage.InnerText = "Le nom d'utilisateur ou mot de passe est incorrecte!!";

                Session["id"] = null;
                Session["isAdmin"] = null;
                Session["input_"] = null;
                Session["edit_"] = null;
                Session["delete_"] = null;
                Session["consult_"] = null;
                Session["print_"] = null;
                Session["instruction_"] = null;
            }
            else if (dt.Rows[0]["isActive"].ToString() == "False")
            {
                infoMessage.InnerText = "Votre compte est désactivé !!";

                Session["id"] = null;
                Session["isAdmin"] = null;
                Session["input_"] = null;
                Session["edit_"] = null;
                Session["delete_"] = null;
                Session["consult_"] = null;
                Session["print_"] = null;
                Session["instruction_"] = null;
            }
            else
            {
                Session["id"] = dt.Rows[0]["username"].ToString();
                Session["isAdmin"] = dt.Rows[0]["isAdmin"].ToString();
                Session["input_"] = dt.Rows[0]["input_"].ToString();
                Session["edit_"] = dt.Rows[0]["edit_"].ToString();
                Session["delete_"] = dt.Rows[0]["delete_"].ToString();
                Session["consult_"] = dt.Rows[0]["consult_"].ToString();
                Session["print_"] = dt.Rows[0]["print_"].ToString();
                Session["instruction_"] = dt.Rows[0]["instruction_"].ToString();
                FormsAuthentication.RedirectFromLoginPage(dt.Rows[0]["username"].ToString(), false);
                
            }

            //UserName.Value.Equals("Admin") && UserPasswordTb.Value.Equals("Admin")
        }
    }
}