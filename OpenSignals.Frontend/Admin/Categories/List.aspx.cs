using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Jayrock.Json;
using OpenSignals.Framework.Categories;
using OpenSignals.Framework.Core.Base;
using OpenSignals.Framework.Core.Utility;

namespace OpenSignals.Frontend.Admin.Categories
{
    public partial class List : BaseAdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BuildPage();
            }
        }

        private void BuildPage()
        {
            tblList.Rows.Add(this.CreateListHeader(true, new string[] { "ID", "Nome", "Stato" }));

            CategoryManager cm = new CategoryManager();
            List<Category> items = cm.GetCategories();

            for (int i = 0; i < items.Count; i++)
            {
                TableRow tr = CreateTableRow();
                tr.Cells.Add(CreateTableCell(items[i].CategoryID.ToString()));
                tr.Cells.Add(CreateTableCell(items[i].Name));

                if (items[i].Status)
                        tr.Cells.Add(CreateTableCell("Attiva"));
                else
                        tr.Cells.Add(CreateTableCell("Non attiva"));

                List<Control> buttons = new List<Control>();
                buttons.Add(CreateImageLink("Edit.aspx?ID=" + items[i].CategoryID, "Modifica", ImageButtons.Edit));
                buttons.Add(CreateImageButton(JsUtils.CreateJsFunction("performAction", true, new JsonObject(new string[] { "action", "argument" }, new string[] { "delete", items[i].CategoryID.ToString() })),
                    "Elimina", ImageButtons.Delete));
                tr.Controls.Add(CreateCommandsCell(buttons));

                tblList.Rows.Add(tr);
            }
        }

        protected void lnkAction_Click(object sender, EventArgs e)
        {
            JsonObject action = GetAction();

            CategoryManager cm = new CategoryManager();

            switch (action["action"].ToString())
            {
                case "delete":
                    cm.Delete(int.Parse(action["argument"].ToString()));
                    Alert("Segnalazione cancellata");
                    break;
            }

            BuildPage();
        }
    }
}