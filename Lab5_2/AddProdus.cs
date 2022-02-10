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
namespace Lab5_2
{
    public partial class AddProdus : Form
    {
        private delegate void MyDelegate(AddProdus MyAdd);
        private delegate void EnableDelegate(Button btn);
        public AddProdus()
        {
            InitializeComponent();
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
        }
        private void Enable(Button btn)
        {
            btn.Enabled = true;
        }
        private void CloseForm(AddProdus MyAdd)
        {
            MyAdd.DialogResult = DialogResult.OK;
            MyAdd.Close();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = false;
            Task.Run(() => Add(this));
        }
        private async Task Add(AddProdus ad)
        {
            await Task.Run(
                async() =>
                {
                    using (MagazinDbContext mdb = new MagazinDbContext())
                    {
                        try
                        {
                            Produs p = new Produs();
                            p.Denumire = this.txtDenumire.Text;
                            p.Descriere = this.txtDescriere.Text;
                            p.DataExpirare = this.dtpExpirare.Value;
                            p.Cantitate = int.Parse(this.txtCantitate.Text);
                            p.DataIntrare = DateTime.Now;

                            mdb.Produse.Add(p);
                            mdb.SaveChanges();
                            Trace.WriteLineIf(Form1.sw.Enabled, DateTime.Now.ToString("MM\\/dd\\/yyyy h\\:mm:ss::fff tt") + "    " + "Produs adaugat");
                        }
                        catch (InvalidOperationException ex)
                        {
                            Trace.WriteLineIf(Form1.sw.Enabled, DateTime.Now.ToString("MM\\/dd\\/yyyy h\\:mm:ss::fff tt") + "    " + ex.StackTrace);
                            //btnAdd.Enabled = true;
                        }
                        catch (Exception ex)
                        {
                            Trace.WriteLineIf(Form1.sw.Enabled, DateTime.Now.ToString("MM\\/dd\\/yyyy h\\:mm:ss::fff tt") + "    " + ex.StackTrace);
                            //btnAdd.Enabled = true;
                        }
                        //finally
                        {
                            if (btnAdd.InvokeRequired)
                            {
                                object obj = new object();
                                obj = btnAdd;
                                btnAdd.BeginInvoke(new EnableDelegate(Enable), obj);
                            }
                            else
                            {
                                Enable(btnAdd);
                            }
                        }
                        if (ad.InvokeRequired)
                        {
                            object obj = new object();
                            obj = ad;
                            ad.BeginInvoke(new MyDelegate(CloseForm), obj);
                        }
                        else
                        {
                            CloseForm(ad);
                        }
                    }
                }
            );
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
