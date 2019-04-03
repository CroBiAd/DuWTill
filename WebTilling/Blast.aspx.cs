using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Bio;
using Bio.IO;
using Bio.Web.Blast;
using System.Web.UI.DataVisualization.Charting;
using System.Configuration;

namespace WebTilling {
    public partial class Blast:System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            Response.Cache.SetNoStore();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoServerCaching();
            //Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            Response.Expires = 0;
            Response.AppendHeader("Pragma", "no-cache");
            //Response.Cache.AppendCacheExtension("must-revalidate, proxy-revalidate, post-check=0, pre-check=0");
            //if (!Context.User.Identity.IsAuthenticated)
            //    Response.Redirect("~/Account/Login");
        }
        protected void txtBlast_PreRender1(object sender, EventArgs e) {
            if (Session["blast"] != null)
                txtBlast.Text = Session["blast"].ToString();
        }
        protected void butBLAST_Click(object sender, EventArgs e) {
            //string baseDir = @"C:\blast-2.6.0+\";
            string baseDir = ConfigurationManager.AppSettings["NCBIBLAST"];
            string file = baseDir + @"temp\test" + DateTime.Now.ToString("yyyyMMddhhmmss");
            string ls = Environment.NewLine.Normalize();
            if (!txtBlast.Text.StartsWith(">"))
                txtBlast.Text = @">No definition line" + ls + txtBlast.Text;
            using (MemoryStream mstrm = new MemoryStream(Encoding.UTF8.GetBytes(txtBlast.Text))) {
                ISequenceParser parser = SequenceParsers.Fasta;
                ISequenceFormatter formatter = SequenceFormatters.Fasta;
                try {
                    Sequence seq = (Sequence)parser.ParseOne(mstrm);
                    formatter.Format(seq, file + ".in");
                    txtBlast.Text = @">" + seq.ID + ls + seq.ConvertToString(0);
                    ErrorMessage.Text = "";
                } catch (Exception ex) {
                    ErrorMessage.Text = ex.Message;
                    txtRes.Text = "";
                    return;
                }
                parser.Close();
                formatter.Close();
            }
            Session["blast"] = txtBlast.Text;
            using (Process blast = new Process()) {
                blast.StartInfo.FileName = baseDir + @"bin\blastn.exe";
                blast.StartInfo.UseShellExecute = false;
                blast.StartInfo.Arguments = @"-task blastn -db " + baseDir + @"db\durum_exome -evalue 0.1 -outfmt 11 -query " + file + ".in -out " + file + ".asn -max_target_seqs 5";
                blast.Start();
                blast.WaitForExit();
            }
            if (File.Exists(file + ".asn")) {
                using (Process form = new Process()) {
                    form.StartInfo.FileName = baseDir + @"bin\blast_formatter.exe";
                    form.StartInfo.UseShellExecute = false;
                    form.StartInfo.Arguments = @"-archive " + file + ".asn" + @" -outfmt 5 -out " + file + ".xml";
                    form.Start();
                    form.WaitForExit();
                    form.StartInfo.Arguments = @"-archive " + file + ".asn" + @" -outfmt 0 -out " + file + ".txt";
                    form.Start();
                    form.WaitForExit();
                }
                using (StreamReader sr = new StreamReader(file + ".xml")) {
                    IBlastParser blastParser = new BlastXmlParser();
                    List<BlastResult> blastres = blastParser.Parse(sr.BaseStream).ToList();
                    ChartArea area = chartBlast.ChartAreas.FindByName("ChartArea1");
                    if (blastres.Count > 0) {
                        BlastXmlMetadata meta = blastres[0].Metadata;
                        chartBlast.Titles.FindByName("blastTitle").Text = meta.QueryDefinition;
                        area.AxisY.Maximum = Math.Floor(meta.QueryLength + 5.0);
                        Series series = chartBlast.Series["Series1"];
                        series.Points.Clear();
                        int i = 0;
                        List<BlastHit> blasts = new List<BlastHit>();
                        chartBlast.Height = 30 * blastres[0].Records[0].Hits.Count + 50;
                        //foreach (BlastSearchRecord br in blastres[0].Records) {
                        foreach (Hit hit in blastres[0].Records[0].Hits) {
                            //int m = hit.Def.IndexOf(' ');
                            //string contig = hit.Def.Substring(0, m);
                            string contig = hit.Def;
                            for (int j=0; j<hit.Hsps.Count; j++) {
                                Hsp hsp = hit.Hsps[j];
                                int k = series.Points.AddXY(i, hsp.QueryStart, hsp.QueryEnd);
                                if (j < 1)
                                    series.Points[k].Label = contig;
                                //series.Points[k].Label = hit.Def;
                                BlastHit bhit = new BlastHit();
                                bhit.seqID = Convert.ToInt64(hit.Accession);
                                bhit.Contig = contig;
                                //bhit.Descr = hit.Def.Substring(m + 1);
                                bhit.Descr = hit.Def;
                                bhit.Score = hsp.BitScore.ToString("N2");
                                bhit.Evalue = hsp.EValue.ToString("E2");
                                bhit.HitStart = hsp.HitStart.ToString();
                                bhit.HitEnd = hsp.HitEnd.ToString();
                                bhit.Align = hsp.AlignmentLength.ToString();
                                bhit.Frame = hsp.QueryFrame>0?"+":"-";
                                bhit.Frame += @"/";
                                bhit.Frame += hsp.HitFrame > 0 ? "+" : "-";
                                blasts.Add(bhit);
                            }
                            i++;
                            //}
                        }
                        gridBLAST.DataSource = blasts;
                    } else {
                        gridBLAST.DataSource = null;
                        chartBlast.Height = 1;
                    }
                    gridBLAST.DataBind();
                }
                using (StreamReader sr = new StreamReader(file + ".txt", Encoding.UTF8, true)) {
                    txtRes.Text = sr.ReadToEnd();
                }
            } else {
                txtRes.Text = "Nothing found.";
            }
        }
        protected void gridBLAST_SelectedIndexChanged(object sender, EventArgs e) {
            GridViewRow gr = gridBLAST.SelectedRow;
            Session["hitCont"] = gr.Cells[2].Text;
            Session["hitStart"] = gr.Cells[6].Text;
            Session["hitEnd"] = gr.Cells[7].Text;
            Session["hitMut"] = "";
            Response.Redirect("~/Search.aspx");
        }
    }
    public class BlastHit {
        public long seqID { get; set; }
        public string Contig { get; set; }
        public string Descr { get; set; }
        public string Score { get; set; }
        public string Evalue { get; set; }
        public string Align { get; set; }
        public string HitStart { get; set; }
        public string HitEnd { get; set; }
        public string Frame { get; set; }
    }
}