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
    public partial class Form1 : Form
    {
        public static BooleanSwitch sw;//public static pentru a fi apelat din toate formele
        FileStream file;
        TextWriterTraceListener traceListener;
        public Form1()
        {
          
            InitializeComponent();
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            LoadData();
            file = new FileStream("testLog.txt", FileMode.OpenOrCreate | FileMode.Append);
            traceListener = new TextWriterTraceListener(file);
            Trace.Listeners.Add(traceListener);
            Trace.AutoFlush = true;
            sw = new BooleanSwitch("Switch1", "Trace switch");
            Trace.WriteLineIf(sw.Enabled, DateTime.Now.ToString("MM\\/dd\\/yyyy h\\:mm:ss::fff tt") + "    " + "Form loaded");
        }
        private delegate void ModifyDelegate(DataGridView dgv, List<Produs2> list);
        private void Modify(DataGridView dgv, List<Produs2> list)
        {
            dgv.DataSource = list;
        }
        private delegate void ModifyDelegateVanzari(DataGridView dgv, List<IstoricVanzari> list);
        private void Modify(DataGridView dgv, List<IstoricVanzari> list)
        {
            dgv.DataSource = list;

        }
        private void LoadData()
        {
            /*using (MagazinDbContext mdb = new MagazinDbContext())
            {
                List<Produs2> produse = new List<Produs2>();
                var rez = from p in mdb.Produse
                          select new
                          {
                              p.Id,
                              p.Denumire,
                              p.Cantitate
                          };
                foreach (var r in rez)
                {
                    Produs2 p = new Produs2
                    {
                        Id = r.Id,
                        Cantitate = r.Cantitate,
                        Denumire = r.Denumire
                    };
                    produse.Add(p);
                }
                dgvProduse.DataSource = produse;
            }*/
            Task.Run(() => LoadDataTask(this.dgvProduse));
        }
        
        private async Task LoadDataTask(DataGridView dgv)
        {
            await Task.Run(
                async () =>
                {
                    using (MagazinDbContext mdb = new MagazinDbContext())
                    {
                        List<Produs2> produse = new List<Produs2>();
                        var rez = from p in mdb.Produse
                                  select new
                                  {
                                      p.Id,
                                      p.Denumire,
                                      p.Cantitate
                                  };
                        foreach (var r in rez)
                        {
                            Produs2 p = new Produs2
                            {
                                Id = r.Id,
                                Cantitate = r.Cantitate,
                                Denumire = r.Denumire
                            };
                            produse.Add(p);
                        }
                        if(dgv.InvokeRequired)
                        {
                            object[] obj = new object[2];
                            obj[0] = dgv;
                            obj[1] = produse;
                            dgv.Invoke(new ModifyDelegate(Modify), obj);
                        }
                        else
                        {
                            Modify(dgv, produse);
                        }
                    }
                }
                );
        }
        
        private void adaugareProduseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddProdus formAdd = new AddProdus();
            if (formAdd.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            /*using (MagazinDbContext mdb = new MagazinDbContext())
            {
                List<Produs2> produse = new List<Produs2>();

                var rez = from p in mdb.Produse
                          where p.Denumire.Contains(this.txtSearch.Text)
                          select new
                          {
                              p.Id,
                              p.Denumire,
                              p.Cantitate
                          };
                foreach (var r in rez)
                {
                    Produs2 p = new Produs2
                    {
                        Id = r.Id,
                        Cantitate = r.Cantitate,
                        Denumire = r.Denumire
                    };
                    produse.Add(p);
                }
                dgvProduse.DataSource = produse;
            }*/
            Task.Run(() => SearchDataTask(this.dgvProduse));
        }
        private async Task SearchDataTask(DataGridView dgv)
        {
            await Task.Run(
                async () =>
                {
                    using (MagazinDbContext mdb = new MagazinDbContext())
                    {
                        List<Produs2> produse = new List<Produs2>();

                        var rez = from p in mdb.Produse
                                  where p.Denumire.Contains(this.txtSearch.Text)
                                  select new
                                  {
                                      p.Id,
                                      p.Denumire,
                                      p.Cantitate
                                  };
                        foreach (var r in rez)
                        {
                            Produs2 p = new Produs2
                            {
                                Id = r.Id,
                                Cantitate = r.Cantitate,
                                Denumire = r.Denumire
                            };
                            produse.Add(p);
                        }
                        if (dgv.InvokeRequired)
                        {
                            object[] obj = new object[2];
                            obj[0] = dgv;
                            obj[1] = produse;
                            dgv.BeginInvoke(new ModifyDelegate(Modify), obj);
                        }
                        else
                        {
                            Modify(dgv, produse);
                        }
                    }
                }
                );
        }
        private void dgvProduse_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Produs p = new Produs();
            int index = e.RowIndex;

            p.Id = int.Parse(dgvProduse.Rows[index].Cells["Id"].Value.ToString());
            p.Denumire = dgvProduse.Rows[index].Cells["Denumire"].Value.ToString();
            p.Cantitate = int.Parse(dgvProduse.Rows[index].Cells["Cantitate"].Value.ToString());
            if (UserForm.user == 0)
            {
                MessageBox.Show("Nu sunteti logat!");
            }
            else
            {
                SaleProdus formSale = new SaleProdus(p);
                if (formSale.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }
        private void utilizatoriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserForm uf = new UserForm();
            uf.ShowDialog();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (UserForm.user != 0)
            {
                UserForm.user = 0;
                MessageBox.Show("Logged out");
            }
            else MessageBox.Show("You are not logged!");
            LoadData();
        }
        //daca un user e logat, afiseaza istoricul cumparaturilor sale
        //daca nu, arata istoricul vanzarilor la toti userii
        private void istoricVanzariToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*using (VanzariDbContext vdb = new VanzariDbContext())
            {

                if (UserForm.user != 0)
                {
                    List<Produs2> vanzari = new List<Produs2>();
                    var rez = from p in vdb.Vanzari
                              where p.Id_user == UserForm.user
                              select new
                              {
                                  p.Id,
                                  p.Denumire,
                                  p.Cantitate
                              };
                    foreach (var r in rez)
                    {
                        Produs2 p = new Produs2
                        {
                            Id = r.Id,
                            Cantitate = r.Cantitate,
                            Denumire = r.Denumire
                        };
                        vanzari.Add(p);
                    }
                    dgvProduse.DataSource = vanzari;
                }
                else
                {
                    List<IstoricVanzari> vanzari = new List<IstoricVanzari>();
                    var rez = from p in vdb.Vanzari
                              select p;
                    foreach (IstoricVanzari iv in rez)
                    {
                        vanzari.Add(iv);
                    }
                    dgvProduse.DataSource = vanzari;
                }
            }*/
            Task.Run(() => LoadVanzariTask(dgvProduse));
        }
        private async Task LoadVanzariTask(DataGridView dgv)
        {
            await Task.Run(
                async () =>
                {
                    using (VanzariDbContext vdb = new VanzariDbContext())
                    {
                        
                        if (UserForm.user != 0)
                        {
                            List<Produs2> vanzari = new List<Produs2>();
                            var rez = from p in vdb.Vanzari
                                      where p.Id_user == UserForm.user
                                      select new
                                      {
                                          p.Id,
                                          p.Denumire,
                                          p.Cantitate
                                      };
                            foreach (var r in rez)
                            {
                                Produs2 p = new Produs2
                                {
                                    Id = r.Id,
                                    Cantitate = r.Cantitate,
                                    Denumire = r.Denumire
                                };
                                vanzari.Add(p);
                            }
                            if (dgv.InvokeRequired)
                            {
                                object[] obj = new object[2];
                                obj[0] = dgv;
                                obj[1] = vanzari;
                                dgv.BeginInvoke(new ModifyDelegate(Modify), obj);
                            }
                            else
                            {
                                Modify(dgv, vanzari);
                            }
                        }
                        else
                        {
                            List<IstoricVanzari> vanzari = new List<IstoricVanzari>();
                            var rez = from p in vdb.Vanzari
                                      select p;
                            foreach(IstoricVanzari iv in rez)
                            {
                                vanzari.Add(iv);
                            }
                            if (dgv.InvokeRequired)
                            {
                                object[] obj = new object[2];
                                obj[0] = dgv;
                                obj[1] = vanzari;
                                dgv.BeginInvoke(new ModifyDelegateVanzari(Modify), obj);
                            }
                            else
                            {
                                Modify(dgv, vanzari);
                            }
                        }
                    }
                }
                );
        }
    }
    
}
public class Produs2
{
    public int Id { get; set; }
    public string Denumire { get; set; }
    public int Cantitate { get; set; }
}
