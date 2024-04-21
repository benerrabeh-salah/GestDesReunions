using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestDesReunions.Filtrage
{
	public partial class CRviewer : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			DataTable reportData = Session["ReportData"] as DataTable;
            if (!IsPostBack)
            {
                ReportDocument rd = new ReportDocument();
                rd.Load(Server.MapPath("../Filtrage/ReportFilter/CrystalReportFilter.rpt"));
                rd.SetDataSource(reportData);

                // Boucle à travers tous les champs texte du rapport
                foreach (ReportObject reportObject in rd.ReportDefinition.ReportObjects)
                {
                    if (reportObject is TextObject)
                    {
                        TextObject textObject = (TextObject)reportObject;

                        // Vérifiez si le texte contient des caractères arabes
                        bool isArabicText = ContainsArabicCharacters(textObject.Text);

                        // Ajustez l'alignement en conséquence
                        textObject.ObjectFormat.HorizontalAlignment = isArabicText ? Alignment.RightAlign : Alignment.LeftAlign;
                    }
                }

                // Assignez le rapport au CrystalReportViewer
                CrystalReportViewer1.ReportSource = rd;

                // Exportez le rapport vers le format PDF et envoyez-le en réponse
                rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "les Reunions");
            }
        }

        private bool ContainsArabicCharacters(string text)
        {
            foreach (char c in text)
            {
                // Caractères arabes Unicode
                if (c >= '\u0600' && c <= '\u06FF')
                {
                    return true;
                }
            }
            return false;
        }
    }
}