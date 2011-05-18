using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenSignals.Framework.Core.Base;
using OpenSignals.Framework.Signals;
using OpenSignals.Framework.Core.Utility;
using Jayrock.Json;

namespace OpenSignals.Frontend.Admin.Signals
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
            tblList.Rows.Add(this.CreateListHeader(true, new string[] { "ID", "Oggetto", "Creata il", "Città", "Indirizzo", "Categoria", "Stato" }));

            SignalManager sm = new SignalManager();
            int totalRecords = 0;
            List<Signal> items = sm.Search(string.Empty, string.Empty, string.Empty, -1, -2, 0, out totalRecords);

            for (int i = 0; i < items.Count; i++)
            {
                TableRow tr = CreateTableRow();
                tr.Cells.Add(CreateTableCell(items[i].SignalID.ToString()));
                tr.Cells.Add(CreateTableCell(items[i].Subject));
                tr.Cells.Add(CreateTableCell(items[i].CreationDate.ToShortDateString()));
                tr.Cells.Add(CreateTableCell(items[i].City));
                tr.Cells.Add(CreateTableCell(items[i].Address));
                tr.Cells.Add(CreateTableCell(items[i].CategoryName));
                switch (items[i].Status)
                {
                    case Signal.SignalStatus.Approved:
                        tr.Cells.Add(CreateTableCell("Approvato"));
                        break;
                    case Signal.SignalStatus.NotApproved:
                        tr.Cells.Add(CreateTableCell("Non approvato"));
                        break;
                    case Signal.SignalStatus.Resolved:
                        tr.Cells.Add(CreateTableCell("Risolto"));
                        break;
                }

                List<Control> buttons = new List<Control>();
                buttons.Add(CreateImageLink("Edit.aspx?ID=" + items[i].SignalID, "Modifica", ImageButtons.Edit));
                buttons.Add(CreateImageButton(JsUtils.CreateJsFunction("performAction", true, new JsonObject(new string[]{"action","argument"}, new string[]{"delete", items[i].SignalID.ToString()})),
                    "Elimina", ImageButtons.Delete));
                buttons.Add(CreateImageButton(JsUtils.CreateJsFunction("performAction", true, new JsonObject(new string[]{"action","argument"}, new string[]{"approve", items[i].SignalID.ToString()})), 
                    "Approva", ImageButtons.Approve));
                buttons.Add(CreateImageButton(JsUtils.CreateJsFunction("performAction", true, new JsonObject(new string[] { "action", "argument" }, new string[] { "reject", items[i].SignalID.ToString() })),
                    "Rifiuta", ImageButtons.Reject));
                tr.Controls.Add(CreateCommandsCell(buttons));

                tblList.Rows.Add(tr);
            }

            tblList.Rows.Add(CreatePaginationRow(totalRecords, 10, 7));
        }

        protected void lnkAction_Click(object sender, EventArgs e)
        {
            JsonObject action = GetAction();

            SignalManager sm = new SignalManager();
            switch (action["action"].ToString())
            {
                case "delete":
                    sm.Delete(int.Parse(action["argument"].ToString()));
                    Alert("Segnalazione cancellata");
                    break;
                case "approve":
                    //sm.ResolveSignal(int.Parse(action["argument"].ToString()), string.Empty);
                    Alert("Segnalazione approvata");
                    break;
                case "reject":
                    //sm.RejectSignal(int.Parse(action["argument"].ToString()), string.Empty);
                    Alert("Segnalazione rifiutata");
                    break;
            }

            BuildPage();
        }
    }
}