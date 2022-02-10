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
    public partial class UserForm : Form
    {
        public static int user = 0;
        private delegate void MyDelegate(UserForm uf);
        private void CloseForm(UserForm uf)
        {
            uf.DialogResult = DialogResult.OK;
            uf.Close();
        }
        public UserForm()
        {
            InitializeComponent();
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            AddUser adduser = new AddUser();
            if (adduser.ShowDialog() == DialogResult.OK)
            {
                //LoadData();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Task.Run(() => Login(this));
        }
        private async Task Login(UserForm uf)
        {
            await Task.Run(
                async () =>
                {
                    using (UtilizatorDbContext udb = new UtilizatorDbContext())
                    {
                        string Parola = this.txtParola.Text;
                        string Username = this.txtUser.Text;

                        int vuser = (from p in udb.Utilizatori
                                     where p.Username.Equals(Username) && p.Parola.Equals(Parola)
                                     select p.Id).SingleOrDefault();
                        Console.WriteLine(vuser);
                        if (vuser==0)
                        {
                            MessageBox.Show(null, "Date de logare incorecte!", "Eroare");
                        }
                        else
                        {
                            user = vuser;
                            MessageBox.Show(null, "V-ati logat cu succes!", "Succes");

                            if (uf.InvokeRequired)
                            {
                                object obj = new object();
                                obj = uf;
                                uf.BeginInvoke(new MyDelegate(CloseForm), obj);
                            }
                            else
                            {
                                CloseForm(uf);
                            }
                        }
                    }
                }
            );
        }

        private void btnStergere_Click(object sender, EventArgs e)
        {
            if (user != 0)
            {
                DialogResult res = MessageBox.Show(null, "Sunteti sigur ca vreti sa stergeti contul?", "Atentie", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.Yes)
                {
                    Task.Run(() => DeleteAccount(this));
                }
            }
            else
            {
                MessageBox.Show("Nu sunteti logat!"); 
            }
        }
        private async Task DeleteAccount(UserForm uf)
        {
            await Task.Run(
                async () =>
                {
                using (UtilizatorDbContext mdb = new UtilizatorDbContext())
                    {
                        var res = mdb.Utilizatori.SingleOrDefault(p => p.Id == user);
                        if (res != null)
                        {
                            try
                            {
                                mdb.Utilizatori.Remove(res);
                                UserForm.user = 0;
                                mdb.SaveChanges();
                                Trace.WriteLineIf(Form1.sw.Enabled, DateTime.Now.ToString("MM\\/dd\\/yyyy h\\:mm:ss::fff tt") + "    " + "Utilizator sters");
                            }
                            catch (Exception ex)
                            {
                                Trace.WriteLineIf(Form1.sw.Enabled, DateTime.Now.ToString("MM\\/dd\\/yyyy h\\:mm:ss::fff tt") + "    " + ex.StackTrace);
                            }
                            if (uf.InvokeRequired)
                            {
                                object obj = new object();
                                obj = uf;
                                uf.BeginInvoke(new MyDelegate(CloseForm), obj);
                            }
                            else
                            {
                                CloseForm(uf);
                            }
                        }
                    }

                }
                );
        }
    }
}
