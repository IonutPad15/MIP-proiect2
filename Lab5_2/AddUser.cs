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
    public partial class AddUser : Form
    {
        private delegate void MyDelegate(AddUser ad);
        private delegate void EnableDelegate(Button btn);
        private void Enable(Button btn)
        {
            btn.Enabled = true;
        }
        private void CloseForm(AddUser ad)
        {
            ad.DialogResult = DialogResult.OK;
            ad.Close();
        }
        public AddUser()
        {
            InitializeComponent();
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
        }

        private void btnAdaugare_Click(object sender, EventArgs e)
        {
            btnAdaugare.Enabled = false;
            Task.Run(() => Add(this));
        }
        private async Task Add(AddUser ad)
        {
            await Task.Run(
                async () =>
                {
                using (UtilizatorDbContext udb = new UtilizatorDbContext())
                {
                    Utilizator u = new Utilizator {
                        Nume = this.txtNume.Text,
                        Prenume = this.txtPrenume.Text,
                        Parola = this.txtParola.Text,
                        Username = this.txtUser.Text
                    };
                        bool verif = (from p in udb.Utilizatori
                                      where p.Username.Equals(u.Username)
                                      select u.Username).Count() > 0;
                        if (verif == true)
                        {
                            MessageBox.Show(null, "Username-ul exista deja!", "Eroare");
                            if (btnAdaugare.InvokeRequired)
                            {
                                object obj = new object();
                                obj = btnAdaugare;
                                btnAdaugare.BeginInvoke(new EnableDelegate(Enable), obj);
                            }
                            else
                            {
                                Enable(btnAdaugare);
                            }
                        }
                        else
                        {
                            try
                            {
                                udb.Utilizatori.Add(u);
                                udb.SaveChanges();
                                MessageBox.Show(null, "Utilizator creat", "Succes");
                            }
                            catch (InvalidOperationException ex)
                            {
                                Trace.WriteLineIf(Form1.sw.Enabled, DateTime.Now.ToString("MM\\/dd\\/yyyy h\\:mm:ss::fff tt") + "    " + ex.StackTrace);
                            }
                            catch (Exception ex)
                            {
                                Trace.WriteLineIf(Form1.sw.Enabled, DateTime.Now.ToString("MM\\/dd\\/yyyy h\\:mm:ss::fff tt") + "    " + ex.StackTrace);
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
                }
            );
        }
    }
}
