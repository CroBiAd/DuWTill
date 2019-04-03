using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebTilling {
    public partial class Search:System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            Response.Cache.SetNoStore();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoServerCaching();
            Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            Response.Expires = 0;
            Response.AppendHeader("Pragma", "no-cache");
            SQLiteDataMutations.ConnectionString = "data source=" + Server.MapPath(ConfigurationManager.AppSettings["DefaultSQLiteDB"]) + ";Version=3;Pooling=True;Max Pool Size=100;Read Only=True;";
            SQLiteDataMutations.SelectCommand = "SELECT mutID, mutant, ctg, position, mutType, mutAllele, parentAllele, zygosity, depth, chr, flankSeq FROM Mutations ";
            SQLiteDataMutations.SelectCommand += "WHERE CASE WHEN length(@cont)>0 OR length(@mut)>0 THEN (";
            SQLiteDataMutations.SelectCommand += "CASE WHEN length(@cont)>0 THEN ctg=@cont AND position>=@posstart AND (CASE WHEN @posend>0 THEN position<=@posend ELSE 1 END) ELSE 1 END ";
            SQLiteDataMutations.SelectCommand += "AND CASE WHEN length(@mut)>0 THEN mutant LIKE '%'||@mut||'%' ELSE 1 END) END ORDER BY depth DESC";
        }
        protected void txtContig_PreRender(object sender, EventArgs e) {
            if (Session["hitCont"] != null)
                txtContig.Text = Session["hitCont"].ToString();
        }
        protected void txtMut_PreRender(object sender, EventArgs e) {
            if (Session["hitMut"] != null)
                txtMut.Text = Session["hitMut"].ToString();
        }
        protected void txtStart_PreRender(object sender, EventArgs e) {
            if (Session["hitStart"] != null)
                txtStart.Text = Session["hitStart"].ToString();
        }
        protected void txtEnd_PreRender(object sender, EventArgs e) {
            if (Session["hitEnd"] != null)
                txtEnd.Text = Session["hitEnd"].ToString();
        }
        protected void butSearch_Click(object sender, EventArgs e) {
            if (Page.IsValid) {
                Session["hitCont"] = txtContig.Text;
                Session["hitMut"] = txtMut.Text;
                Session["hitStart"] = txtStart.Text;
                Session["hitEnd"] = txtEnd.Text;
            }
        }
        protected void gridMutations_DataBound(object sender, EventArgs e) {
            int n = gridMutations.Rows.Count;
            if (n > 0) {
                labRecords.Text = n.ToString() + " record(s) found.";
                labRecords.CssClass = "lead text-success";
            } else {
                labRecords.Text = "There are no records to display.";
                labRecords.CssClass = "lead text-danger";
            }
        }
        protected void gridMutations_RowDataBound(object sender, GridViewRowEventArgs e) {
            GridViewRow r = e.Row;
            if (r.RowType == DataControlRowType.Header) {
                r.Cells[0].ToolTip = "Mutation number, a.k.a. internal ID.";
                r.Cells[1].ToolTip = "Individual plant.";
                r.Cells[3].ToolTip = "Mutation position on a reference contig.";
                r.Cells[5].ToolTip = "Parent Allele.";
                r.Cells[6].ToolTip = "Mutant Allele.";
                r.Cells[7].ToolTip = "Number of reads at the mutation position.";
            } else if (r.RowType == DataControlRowType.DataRow) {
                TextBox txt = (TextBox)r.FindControl("txtSeq");
                DataRowView v = (DataRowView)r.DataItem;
                txt.Text = @">#" + v["mutID"] + ": " + v["mutant"].ToString() + " " + v["mutType"] + " at position " + v["position"].ToString() + " on " + v["ctg"] + Environment.NewLine.Normalize() + v["flankSeq"];
            }
        }
    }
}
