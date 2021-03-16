using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GSB_CSHARP
{
    public partial class Form1 : Form
    {
        private MySqlCommand myCom;
        private ConnexionSql myConnection;
        private GestionDate myDate;
        private DataTable dt;
        private string date1;
        private int dateVerif;
        public Form1()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            myConnection = ConnexionSql.getInstance("localhost", "gsb-v1", "root", "");
            myConnection.openConnection();
            myDate = new GestionDate();

            date1 = myDate.moisCourant();
            lb1.Text = date1;
            myCom = myConnection.reqExec("Select * from testfichefrais where mois='" + date1 + "'");

            dt = new DataTable();
            dt.Load(myCom.ExecuteReader());

            dataGridView1.DataSource = dt;
        }

        private void InitializeTimer()
        {
            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(timer1_Tick);

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            try {

                timer1.Start();

                GestionDate date = new GestionDate();
                myConnection = ConnexionSql.getInstance("localhost", "gsb-v1", "root", "");
                dateVerif = Convert.ToInt32(date.dateJour().Substring(0, 1));

                if (dateVerif >= 1 && dateVerif <= 20)
                {

                    myConnection.openConnection();

                    myCom = myConnection.reqExec("update testfichefrais set idEtat ='CL' where idEtat ='CR'");
                    myCom.ExecuteNonQuery();

                    myDate = new GestionDate();
                    date1 = myDate.moisCourant();

                    myCom = myConnection.reqExec("Select * from testfichefrais where mois='" + date1 + "'");
                    dt = new DataTable();
                    dt.Load(myCom.ExecuteReader());

                    dataGridView1.DataSource = dt;
                }

            }
            catch (Exception emp)
            {
                MessageBox.Show(emp.Message);
            }
            

        }
    }
}
