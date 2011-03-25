using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenSignals.Framework.Core.Base;
using OpenSignals.Framework.Signals;

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
                buttons.Add(CreateImageButton("#", "Modifica", ImageButtons.Edit));
                buttons.Add(CreateImageButton("#", "Elimina", ImageButtons.Delete));
                buttons.Add(CreateImageButton("#", "Approva", ImageButtons.Approve));
                buttons.Add(CreateImageButton("#", "Rifiuta", ImageButtons.Reject));
                tr.Controls.Add(CreateCommandsCell(buttons));

                tblList.Rows.Add(tr);
            }

            tblList.Rows.Add(CreatePaginationRow(totalRecords, 10, 7));
        }
    }
}