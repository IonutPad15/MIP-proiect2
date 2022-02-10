using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;
using System.IO;
namespace Lab5_2
{
    public partial class SaleProdus : Form
    {
        private Produs produs;
        private delegate void MyDelegate(SaleProdus MySale);
        private void CloseForm(SaleProdus MySale)
        {
            MySale.DialogResult = DialogResult.OK;
            MySale.Close();
        }
        public SaleProdus(Produs p)
        {
            InitializeComponent();
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            this.produs = p;
            this.lblProdus.Text = p.Denumire;
            this.lblCantitate.Text = p.Cantitate.ToString();
        }
     
        private void btnSale_Click(object sender, EventArgs e)
        {
            btnSale.Enabled = false;
            Task.Run(() => Sale(this));
            
        }
        private async Task Sale(SaleProdus sp)
        {
            await Task.Run(
                async () =>
                {
                    using (MagazinDbContext mdb = new MagazinDbContext())
                    {
                        var res = mdb.Produse.SingleOrDefault(p => p.Id == produs.Id);
                        if (res != null)
                        {
                            int cantitate = 0;
                            try
                            {
                                cantitate = int.Parse(txtCantitate.Text);
                            }
                            catch(FormatException ex)
                            {
                                Trace.WriteLineIf(Form1.sw.Enabled, DateTime.Now.ToString("MM\\/dd\\/yyyy h\\:mm:ss::fff tt") + "    " + ex.StackTrace);
                            }
                            if (cantitate > 0)
                            {
                                res.Cantitate -= cantitate;
                                using (VanzariDbContext vdb = new VanzariDbContext())
                                {
                                    IstoricVanzari v = new IstoricVanzari
                                    {
                                        Id_user = UserForm.user,
                                        Cantitate = cantitate,
                                        Denumire = res.Denumire
                                    };
                                    try
                                    {
                                        vdb.Vanzari.Add(v);
                                        vdb.SaveChanges();
                                        Trace.WriteLineIf(Form1.sw.Enabled, DateTime.Now.ToString("MM\\/dd\\/yyyy h\\:mm:ss::fff tt") + "    " + "Sale added");
                                    }
                                    catch (InvalidOperationException ex)
                                    {
                                        Trace.WriteLineIf(Form1.sw.Enabled, DateTime.Now.ToString("MM\\/dd\\/yyyy h\\:mm:ss::fff tt") + "    " + ex.StackTrace);
                                    }
                                    catch (Exception ex)
                                    {
                                        Trace.WriteLineIf(Form1.sw.Enabled, DateTime.Now.ToString("MM\\/dd\\/yyyy h\\:mm:ss::fff tt") + "    " + ex.StackTrace);
                                    }
                                }
                            }
                            if (res.Cantitate <= 0)
                            {
                                try
                                {
                                    mdb.Produse.Remove(res);
                                    Trace.WriteLineIf(Form1.sw.Enabled, DateTime.Now.ToString("MM\\/dd\\/yyyy h\\:mm:ss::fff tt") + "    " + "Produs sters");
                                }
                                catch(Exception ex)
                                {
                                    Trace.WriteLineIf(Form1.sw.Enabled, DateTime.Now.ToString("MM\\/dd\\/yyyy h\\:mm:ss::fff tt") + "    " + ex.StackTrace);
                                }
                            }
                            try
                            {
                                mdb.SaveChanges();
                            }
                            catch (InvalidOperationException ex)
                            {
                                Trace.WriteLineIf(Form1.sw.Enabled, DateTime.Now.ToString("MM\\/dd\\/yyyy h\\:mm:ss::fff tt") + "    " + ex.StackTrace);
                            }
                            catch (Exception ex)
                            {
                                Trace.WriteLineIf(Form1.sw.Enabled, DateTime.Now.ToString("MM\\/dd\\/yyyy h\\:mm:ss::fff tt") + "    " + ex.StackTrace);
                            }
                            if (sp.InvokeRequired)
                            {
                                object obj = new object();
                                obj = sp;
                                sp.BeginInvoke(new MyDelegate(CloseForm), obj);
                            }
                            else
                            {
                                CloseForm(sp);
                            }
                        }
                    }
                    
                }
                );
        }
    }
}
