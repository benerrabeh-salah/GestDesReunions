using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using GestDesReunions.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using PdfSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;



namespace GestDesReunions.Filtrage
{
    public partial class FiltrerParId : System.Web.UI.Page
    {
        AdoNetHelper dbHelper = new AdoNetHelper("cnxStrGestionReunion");

        //chargement de la division
        private void chargerDivision()
        {
            if (DivisionFilter.Items.Count == 0)
            {
                using (DataTable dt2 = new DataTable())
                {
                    string qry = "select Id, nom_division from DivisionTbl";
                    SqlDataAdapter sda = new SqlDataAdapter(qry, dbHelper.Connection);
                    sda.Fill(dt2);
                    DivisionFilter.DataSource = dt2;
                    DivisionFilter.DataTextField = "nom_division";
                    DivisionFilter.DataValueField = "nom_division";
                    DivisionFilter.DataBind();
                }

                DivisionFilter.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Selectionner la division --", "0"));
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
            }

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable data = new DataTable();
                string query = "SELECT Id, FORMAT(date_reunion, 'dd/MM/yyyy') AS date_reunion, sujet_reunion, division FROM ReunionTbl WHERE";
                //Id filter
                if (txtIdFilter.Visible)
                {
                    int parsedValue;
                    if (string.IsNullOrEmpty(txtIdFilter.Text))
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Merci de saisir ID Réunion !!', 'warning')", true);
                        errorMessage.Text = "Merci de saisir ID Réunion !!";
                        txtIdFilter.Focus();
                    }
                    else if (!int.TryParse(txtIdFilter.Text, out parsedValue))
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Merci de saisir une valeur numérique pour ID Réunion !!', 'warning')", true);
                        errorMessage.Text = "Merci de saisir une valeur numérique pour ID Réunion !!";
                        return;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(txtIdFilter.Text))
                            query += $" Id = '{txtIdFilter.Text}' and supprimer is null AND";

                        query = query.TrimEnd(" AND".ToCharArray());

                        data = dbHelper.GetDataTable(query);
                    }
                }

                //division filter
                if (DivisionFilter.Visible)
                {
                    if (DivisionFilter.SelectedValue == "0")
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Merci de choisir une division !!', 'warning')", true);
                        errorMessage.Text = "Merci de choisir une division !!";
                        DivisionFilter.Focus();
                    }
                    else
                    {
                        string str = DivisionFilter.SelectedItem.ToString();
                        if (!string.IsNullOrEmpty(str))
                            query += $" division = '{str}' and supprimer is null AND";

                        query = query.TrimEnd(" AND".ToCharArray());

                        data = dbHelper.GetDataTable(query);
                    }

                }
                //date réunion filter
                if (txtDateFilter1.Visible)
                {
                    if (string.IsNullOrEmpty(txtDateFilter1.Text))
                    {
                        txtDateFilter1.Focus();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Merci de choisir une date !!', 'warning')", true);
                        errorMessage.Text = "Merci de choisir une date !!";
                    }
                    else
                    {

                        if (!string.IsNullOrEmpty(txtDateFilter2.Text))
                        {
                            //if (!string.IsNullOrEmpty(txtDateFilter1.Text) && !string.IsNullOrEmpty(txtDateFilter2.Text))
                            query += $" date_reunion between '" + txtDateFilter1.Text + "' and '" + txtDateFilter2.Text + "' and supprimer is null AND";

                            query = query.TrimEnd(" AND".ToCharArray());

                            data = dbHelper.GetDataTable(query);
                        }

                        else
                        {
                            //if (!string.IsNullOrEmpty(txtDateFilter1.Text))
                            query += $" date_reunion = '" + txtDateFilter1.Text + "' and supprimer is null AND";

                            query = query.TrimEnd(" AND".ToCharArray());

                            data = dbHelper.GetDataTable(query);
                        }
                    }
                }

                //date prochaine réunion filter
                if (txtDateFilter3.Visible)
                {
                    if (string.IsNullOrEmpty(txtDateFilter3.Text))
                    {
                        txtDateFilter3.Focus();
                        errorMessage.Text = "Merci de choisir une date !!";
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Merci de choisir une date !!', 'warning')", true);
                    }
                    else if (!string.IsNullOrEmpty(txtDateFilter4.Text))
                    {
                        if (!string.IsNullOrEmpty(txtDateFilter3.Text) && !string.IsNullOrEmpty(txtDateFilter4.Text))
                            query += $" proch_reunion between '" + txtDateFilter3.Text + "' and '" + txtDateFilter4.Text + "' and supprimer is null AND";

                        query = query.TrimEnd(" AND".ToCharArray());

                        data = dbHelper.GetDataTable(query);
                    }
                    else
                    {
                        string str = txtDateFilter3.Text;
                        if (!string.IsNullOrEmpty(str))
                            query += $" proch_reunion = '{str}' and supprimer is null AND";

                        query = query.TrimEnd(" AND".ToCharArray());

                        data = dbHelper.GetDataTable(query);
                    }

                }

                //secteur filter
                if (DSceteur.Visible)
                {
                    if (DSceteur.SelectedValue == "0")
                    {
                        errorMessage.Text = "Merci de choisir un secteur !!";
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Merci de choisir un secteur !!', 'warning')", true);
                        DSceteur.Focus();
                    }
                    else
                    {
                        string str = DSceteur.SelectedItem.ToString();
                        if (!string.IsNullOrEmpty(str))
                            query += $" secteur Like '%{str}%' and supprimer is null AND";

                        query = query.TrimEnd(" AND".ToCharArray());

                        data = dbHelper.GetDataTable(query);
                    }

                }

                //partenaire filter
                if (DPartenaire.Visible)
                {
                    if (DPartenaire.SelectedValue == "0")
                    {
                        errorMessage.Text = "Merci de choisir un partenaire !!";
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Merci de choisir un partenaire !!', 'warning')", true);
                        DPartenaire.Focus();
                    }
                    else
                    {
                        string str = DPartenaire.SelectedItem.ToString();
                        if (!string.IsNullOrEmpty(str))
                            query += $"  partenaire like '%{str}%' and supprimer is null AND";

                        query = query.TrimEnd(" AND".ToCharArray());

                        data = dbHelper.GetDataTable(query);
                    }

                }
                if (DCadre.Visible)
                {
                    if (DCadre.SelectedValue == "0")
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Erreur!', 'Merci de choisir le Cadre !!', 'warning')", true);
                        DPartenaire.Focus();
                    }
                    else
                    {
                        string strObjet = txtObjet.Text;
                        string strId = txtIdCader.Text;
                        string strCadre = DCadre.SelectedValue.ToString();
                        if (!string.IsNullOrEmpty(strCadre))
                            query += $"  cadre like '{strCadre}' and supprimer is null AND";

                        if (!string.IsNullOrEmpty(strObjet))
                            query += $"  objet like '{strObjet}' and supprimer is null AND";

                        if (!string.IsNullOrEmpty(strId))
                            query += $"  idcadre like '{strId}' and supprimer is null AND";

                        query = query.TrimEnd(" AND".ToCharArray());

                        data = dbHelper.GetDataTable(query);
                    }

                }
                // Créer dynamiquement les colonnes du GridView
                if (data != null && data.Rows.Count > 0)
                {
                    errorMessage.Text = "";
                    btnExportExcel.Enabled = true;
                    btnExportPdf.Enabled = true;
                    ReunionRepeater.DataSource = data;
                    ReunionRepeater.DataBind();
                }
            }
            catch (Exception ex)
            {

                lblmessage.Text = ex.Message;
            }


        }

        private void ResetControlVisibility()
        {
            ReunionRepeater.DataSource = null;
            ReunionRepeater.DataBind();
            txtIdFilter.Visible = false;
            txtDateFilter1.Visible = false;
            txtDateFilter2.Visible = false;
            lbl1.Visible = false;
            lbl2.Visible = false;
            DivisionFilter.Visible = false;
            txtDateFilter3.Visible = false;
            txtDateFilter4.Visible = false;
            lbl3.Visible = false;
            lbl4.Visible = false;
            DSceteur.Visible = false;
            DPartenaire.Visible = false;
            DCadre.Visible = false;
            txtObjet.Visible = false;
            txtIdCader.Visible = false;

        }

        protected void Dsearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetControlVisibility();

            errorMessage.Text = "";
            btnExportExcel.Enabled = false;
            btnExportPdf.Enabled = false;

            if (Dsearch.SelectedValue == "1")
            {
                txtIdFilter.Visible = true;
            }
            if (Dsearch.SelectedValue == "2")
            {
                DivisionFilter.Visible = true;
            }
            if (Dsearch.SelectedValue == "3")
            {
                lbl1.Visible = true;
                lbl2.Visible = true;
                txtDateFilter1.Visible = true;
                txtDateFilter2.Visible = true;
            }
            if (Dsearch.SelectedValue == "4")
            {
                lbl3.Visible = true;
                lbl4.Visible = true;
                txtDateFilter3.Visible = true;
                txtDateFilter4.Visible = true;
            }
            if (Dsearch.SelectedValue == "5")
            {
                DSceteur.Visible = true;
                string query = "select Id, nom_secteur from SecteurTbl";
                DSceteur.DataSource = dbHelper.GetDataTable(query);
                DSceteur.DataTextField = "nom_secteur";
                DSceteur.DataValueField = "Id";
                DSceteur.DataBind();
                DSceteur.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Séléctionner le secteur --", "0"));
            }
            if (Dsearch.SelectedValue == "6")
            {
                DPartenaire.Visible = true;
                string query = "select Id, nom_partenaire from PartenaireTbl";
                DPartenaire.DataSource = dbHelper.GetDataTable(query);
                DPartenaire.DataTextField = "nom_partenaire";
                DPartenaire.DataValueField = "Id";
                DPartenaire.DataBind();
                DPartenaire.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Séléctionner le Partenaire --", "0"));
            }
            if (Dsearch.SelectedValue == "7")
            {
                DCadre.Visible = true;
                txtObjet.Visible = true;
                txtIdCader.Visible = true;
            }

        }

        protected void btnExportPdf_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if any records are selected
                bool exportSelectedRecords = false;
                List<int> selectedRecordIds = new List<int>();

                foreach (RepeaterItem item in ReunionRepeater.Items)
                {
                    CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                    if (chkSelect.Checked)
                    {
                        exportSelectedRecords = true;
                        // Get the ID of the selected record
                        int recordId;
                        if (int.TryParse(chkSelect.Attributes["data-id"], out recordId))
                        {
                            selectedRecordIds.Add(recordId);
                        }
                    }
                }

                // Fetch data based on the export mode
                DataTable data = exportSelectedRecords ? GetSelectedRecordsDataPDF(selectedRecordIds) : GetFilteredDataPDF();

                // Once you have your data, proceed with PDF generation
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (Document document = new Document(iTextSharp.text.PageSize.A4, 10f, 10f, 40f, 40f))
                    {
                        PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                        document.Open();

                        // Add content to the PDF document based on the retrieved data
                        AddContentToPdf(document, data);

                        // Close the document
                        document.Close();
                    }

                    // Send the generated PDF as response to the client
                    SendPdfResponse(memoryStream, "Reunions.pdf");
                }
            }
            catch (Exception ex)
            {
                lblmessage.Text = ex.Message;
            }
        }





        // Method to add content to the PDF document
        private void AddContentToPdf(Document document, DataTable data)
        {
            // Create a font for headers
            Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.BLUE);

            // Create a font for table headers
            Font tableHeaderFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.WHITE);

            // Create a font for table cells
            Font cellFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK);

            // Define an array to hold the relative widths of each column
            float[] columnWidths = new float[data.Columns.Count];

            // Adjust the width for specific columns (adjust these values as needed)
            columnWidths[0] = 2f; // Width for the first column
            columnWidths[1] = 5f; // Width for the second column
            columnWidths[2] = 5f;
            columnWidths[3] = 5f;
            //columnWidths[4] = 5f;

            // Create the PdfPTable with the defined column widths
            PdfPTable table = new PdfPTable(columnWidths);
            table.WidthPercentage = 100; // Set table width to 100% of page width
            table.DefaultCell.BackgroundColor = BaseColor.LIGHT_GRAY; // Set default cell background color

            // Create a table to hold the images and header text
            PdfPTable imageTable = new PdfPTable(3); // Three columns: one for each image and one for the header text
            imageTable.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            imageTable.WidthPercentage = 100;
            imageTable.HorizontalAlignment = Element.ALIGN_CENTER;

            /***/

            // Create a font for table cells using the Arabic font
            BaseFont arabicBaseFont = BaseFont.CreateFont(Server.MapPath("~/Fonts/arial.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font arabicFont = new Font(arabicBaseFont, 10, Font.NORMAL);

            // Create a font for the header text using the Arabic font
            BaseFont arabicBaseFont2 = BaseFont.CreateFont(Server.MapPath("~/Fonts/arial.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font arabicFont2 = new Font(arabicBaseFont2, 16, Font.BOLD, BaseColor.BLACK);

            // Create a paragraph for the header text
            Paragraph headerText = new Paragraph("المملكة المغربية وزارة الداخلية عمالة إقليم أزيــــلال", arabicFont2);
            headerText.Alignment = Element.ALIGN_CENTER;

            // Create a PdfPCell for the header text
            PdfPCell headerCell = new PdfPCell(headerText);
            headerCell.Border = PdfPCell.NO_BORDER;
            headerCell.Colspan = 3; // Span the header text cell across all three columns
            headerCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL; // Set text direction to right-to-left
            headerCell.HorizontalAlignment = Element.ALIGN_CENTER; // Center align the header cell content
            headerCell.VerticalAlignment = Element.ALIGN_TOP; // Align at the top
            imageTable.AddCell(headerCell);

            /***/

            // Create a cell for the header text
            //PdfPCell headerCell = new PdfPCell(new Phrase("المملكة المغربية وزارة الداخلية عمالة إقليم أزيــــلال")); // Replace "Header Text" with your actual header text
            //headerCell.Border = PdfPCell.NO_BORDER;
            //headerCell.Colspan = 3; // Span the header text cell across all three columns
            //headerCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL; // Set text direction to right-to-left
            //headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //headerCell.VerticalAlignment = Element.ALIGN_TOP; // Align at the top
            //imageTable.AddCell(headerCell);

            // Create a cell for the left image
            iTextSharp.text.Image leftImage = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Asset/images/logo_azilal.png"));
            leftImage.ScaleAbsolute(70f, 70f); // Adjust size as needed
            PdfPCell leftCell = new PdfPCell(leftImage);
            leftCell.Border = PdfPCell.NO_BORDER;
            leftCell.HorizontalAlignment = Element.ALIGN_LEFT;
            leftCell.VerticalAlignment = Element.ALIGN_TOP; // Align at the top
            imageTable.AddCell(leftCell);

            // Create an empty cell to occupy the space of the header text
            PdfPCell emptyCell = new PdfPCell();
            emptyCell.Border = PdfPCell.NO_BORDER;
            emptyCell.Colspan = 1; // Span one column
            imageTable.AddCell(emptyCell);

            // Create a cell for the right image
            iTextSharp.text.Image rightImage = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Asset/images/logo_azilal1.png"));
            rightImage.ScaleAbsolute(70f, 70f); // Adjust size as needed
            PdfPCell rightCell = new PdfPCell(rightImage);
            rightCell.Border = PdfPCell.NO_BORDER;
            rightCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            rightCell.VerticalAlignment = Element.ALIGN_TOP; // Align at the top
            imageTable.AddCell(rightCell);

            // Add the image table to the document
            document.Add(imageTable);


            //// Create a font for table cells using the Arabic font
            //BaseFont arabicBaseFont = BaseFont.CreateFont(Server.MapPath("~/Fonts/arial.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            //Font arabicFont = new Font(arabicBaseFont, 10, Font.NORMAL);

            //// Create a font for the header text using the Arabic font
            //BaseFont arabicBaseFont2 = BaseFont.CreateFont(Server.MapPath("~/Fonts/arial.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            //Font arabicFont2 = new Font(arabicBaseFont2, 16, Font.BOLD, BaseColor.BLACK);




            // Add empty line after the header text
            document.Add(new Paragraph("\n"));

            // Add table headers
            foreach (DataColumn column in data.Columns)
            {
                PdfPCell tableHeaderCell = new PdfPCell(new Phrase(column.ColumnName, arabicFont));
                tableHeaderCell.BackgroundColor = BaseColor.LIGHT_GRAY; // Set header cell background color
                tableHeaderCell.HorizontalAlignment = Element.ALIGN_CENTER; // Center align header text
                table.AddCell(tableHeaderCell);
            }

            // Add table rows
            foreach (DataRow row in data.Rows)
            {
                foreach (object item in row.ItemArray)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(item.ToString(), arabicFont)); // Use the Arabic font
                    cell.HorizontalAlignment = Element.ALIGN_CENTER; // Center align the content within the cell
                    cell.RunDirection = PdfWriter.RUN_DIRECTION_RTL; // Set text direction to right-to-left
                    table.AddCell(cell);
                }
            }
            // Add the table to the document
            document.Add(table);
        }

        // Method to send the generated PDF as response to the client
        private void SendPdfResponse(MemoryStream memoryStream, string fileName)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.BinaryWrite(memoryStream.ToArray());
            HttpContext.Current.Response.End();
            HttpContext.Current.Response.Close();
        }

        private DataTable GetFilteredDataPDF()
        {
            // Construct the initial query
            string query = "SELECT FORMAT(date_reunion, 'dd/MM/yyyy') AS [Date Réunion], sujet_reunion AS [Objet Réunion], etat_avancement AS [Etat Evancement], recommendation AS [Recommandation] FROM ReunionTbl WHERE 1=1";

            // Apply filters if they are visible and valid
            if (txtIdFilter.Visible && int.TryParse(txtIdFilter.Text, out int idFilter))
                query += $" AND Id = {idFilter}";

            if (DivisionFilter.Visible && DivisionFilter.SelectedValue != "0")
                query += $" AND division = '{DivisionFilter.SelectedValue}'";

            if (txtDateFilter1.Visible && !string.IsNullOrEmpty(txtDateFilter1.Text))
                query += $" AND date_reunion = '{txtDateFilter1.Text}'";

            if (txtDateFilter2.Visible && !string.IsNullOrEmpty(txtDateFilter2.Text))
                query += $" AND date_reunion <= '{txtDateFilter2.Text}'";

            if (txtDateFilter3.Visible && !string.IsNullOrEmpty(txtDateFilter3.Text))
                query += $" AND proch_reunion = '{txtDateFilter3.Text}'";

            if (txtDateFilter4.Visible && !string.IsNullOrEmpty(txtDateFilter4.Text))
                query += $" AND proch_reunion <= '{txtDateFilter4.Text}'";

            if (DSceteur.Visible && DSceteur.SelectedValue != "0")
                query += $" AND secteur like '%{DSceteur.SelectedValue}%'";

            if (DPartenaire.Visible && DPartenaire.SelectedValue != "0")
                query += $" AND partenaire like '%{DPartenaire.SelectedValue}%'";

            if (DCadre.Visible && DCadre.SelectedValue != "0")
            {
                query += $" AND cadre like '{DCadre.SelectedValue}'";
                if (!string.IsNullOrEmpty(txtObjet.Text))
                    query += $" AND objet like '{txtObjet.Text}'";
                if (!string.IsNullOrEmpty(txtIdCader.Text))
                    query += $" AND idcadre like '{txtIdCader.Text}'";
            }

            // Check if the "supprimer" field is null
            query += " AND supprimer IS NULL";

            // Remove unnecessary 'AND' at the end of the query
            query = query.Replace("WHERE 1=1 AND", "WHERE").Replace("WHERE 1=1", "");

            // Fetch data from the database
            return dbHelper.GetDataTable(query);
        }

        private DataTable GetSelectedRecordsDataPDF(List<int> selectedRecordIds)
        {
            // Implement logic to fetch data for the selected records based on their IDs
            // For example:
            string idList = string.Join(",", selectedRecordIds);
            string query = $"SELECT FORMAT(date_reunion, 'dd/MM/yyyy') AS [Date Réunion], sujet_reunion AS [Objet Réunion], etat_avancement AS [Etat Evancement], recommendation AS [Recommandation] FROM ReunionTbl WHERE Id IN ({idList})";
            return dbHelper.GetDataTable(query);
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

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            // Check if any records are selected
            bool exportSelectedRecords = false;
            List<int> selectedRecordIds = new List<int>();

            foreach (RepeaterItem item in ReunionRepeater.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect.Checked)
                {
                    exportSelectedRecords = true;
                    // Get the ID of the selected record
                    int recordId;
                    if (int.TryParse(chkSelect.Attributes["data-id"], out recordId))
                    {
                        selectedRecordIds.Add(recordId);
                    }
                }
            }

            // Export selected records or all filtered records based on the flag
            if (exportSelectedRecords)
            {
                ExportSelectedRecordsToExcel(selectedRecordIds);
            }
            else
            {
                ExportAllFilteredRecordsToExcel();
            }
        }

        private void ExportSelectedRecordsToExcel(List<int> selectedRecordIds)
        {
            DataTable selectedData = GetSelectedRecordsData(selectedRecordIds);
            ExportDataTableToExcel(selectedData);
        }



        private void ExportAllFilteredRecordsToExcel()
        {
            DataTable filteredData = GetFilteredData();
            ExportDataTableToExcel(filteredData);
        }

        private DataTable GetSelectedRecordsData(List<int> selectedRecordIds)
        {
            // Implement logic to fetch data for selected records based on their IDs
            // Example:
            string query = $"SELECT FORMAT(date_reunion, 'dd/MM/yyyy') AS [Date Réunion], sujet_reunion AS [Objet Réunion], division AS [Division], etat_avancement AS [Etat Evancement], recommendation AS [Recommandation], FORMAT(proch_reunion, 'dd/MM/yyyy') AS [Prochaine Réunion], cadre AS [Cadre], objet AS [Objet], cout_cadre AS [Coût Cadre], secteur AS [Secteur], partenaire AS [Partenaire], statut AS [Statut] FROM ReunionTbl WHERE Id IN ({string.Join(",", selectedRecordIds)})";
            DataTable data = dbHelper.GetDataTable(query);
            return data;

            // For demonstration, returning an empty DataTable
            //return new DataTable();
        }

        private DataTable GetFilteredData()
        {
            // Construct the initial query
            string query = "SELECT FORMAT(date_reunion, 'dd/MM/yyyy') AS [Date Réunion], sujet_reunion AS [Objet Réunion], division AS [Division], etat_avancement AS [Etat Evancement], recommendation AS [Recommandation], FORMAT(proch_reunion, 'dd/MM/yyyy') AS [Prochaine Réunion], cadre AS [Cadre], objet AS [Objet], cout_cadre AS [Coût Cadre], secteur AS [Secteur], partenaire AS [Partenaire], statut AS [Statut] FROM ReunionTbl WHERE 1=1";

            // Apply filters if they are visible and valid
            if (txtIdFilter.Visible && int.TryParse(txtIdFilter.Text, out int idFilter))
                query += $" AND Id = {idFilter}";

            if (DivisionFilter.Visible && DivisionFilter.SelectedValue != "0")
                query += $" AND division = '{DivisionFilter.SelectedValue}'";

            if (txtDateFilter1.Visible && !string.IsNullOrEmpty(txtDateFilter1.Text))
                query += $" AND date_reunion = '{txtDateFilter1.Text}'";

            if (txtDateFilter2.Visible && !string.IsNullOrEmpty(txtDateFilter2.Text))
                query += $" AND date_reunion <= '{txtDateFilter2.Text}'";

            if (txtDateFilter3.Visible && !string.IsNullOrEmpty(txtDateFilter3.Text))
                query += $" AND proch_reunion = '{txtDateFilter3.Text}'";

            if (txtDateFilter4.Visible && !string.IsNullOrEmpty(txtDateFilter4.Text))
                query += $" AND proch_reunion <= '{txtDateFilter4.Text}'";

            if (DSceteur.Visible && DSceteur.SelectedValue != "0")
                query += $" AND secteur = '{DSceteur.SelectedValue}'";

            if (DPartenaire.Visible && DPartenaire.SelectedValue != "0")
                query += $" AND partenaire like '{DPartenaire.SelectedItem.ToString()}'";

            if (DCadre.Visible && DCadre.SelectedValue != "0")
            {
                query += $" AND cadre like '{DCadre.SelectedValue}'";
                if (!string.IsNullOrEmpty(txtObjet.Text))
                    query += $" AND objet like '{txtObjet.Text}'";
                if (!string.IsNullOrEmpty(txtIdCader.Text))
                    query += $" AND idcadre like '{txtIdCader.Text}'";
            }

            // Check if the "supprimer" field is null
            query += " AND supprimer IS NULL";

            // Remove unnecessary 'AND' at the end of the query
            query = query.Replace("WHERE 1=1 AND", "WHERE").Replace("WHERE 1=1", "");

            // Fetch data from the database
            return dbHelper.GetDataTable(query);
        }

        private void ExportDataTableToExcel(DataTable dataTable)
        {
            // Create a new Excel package
            using (ExcelPackage package = new ExcelPackage())
            {
                // Add a new worksheet to the Excel package
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Reunion Data");

                // Add column headers to the Excel worksheet
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1].Value = dataTable.Columns[i].ColumnName;
                }

                // Add data rows to the Excel worksheet
                for (int row = 0; row < dataTable.Rows.Count; row++)
                {
                    for (int col = 0; col < dataTable.Columns.Count; col++)
                    {
                        worksheet.Cells[row + 2, col + 1].Value = dataTable.Rows[row][col];
                    }
                }

                // Prepare the response for file download
                byte[] fileBytes = package.GetAsByteArray();
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", $"attachment;filename=ReunionData_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
                Response.BinaryWrite(fileBytes);
                Response.Flush();
                Response.End();
            }
        }

        protected void Details_Click(object sender, EventArgs e)
        {
            Button btnDetails = (Button)sender;
            string reunionId = btnDetails.CommandArgument;

            Response.Redirect($"~/MAJ/DetailsReunion.aspx?reunionId={reunionId}");
        }
    }
}