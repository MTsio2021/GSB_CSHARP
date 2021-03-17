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
           
            myCom = myConnection.reqExec("Select * from testfichefrais where mois='" + date1 + "'");

            dt = new DataTable();
            dt.Load(myCom.ExecuteReader());

            dataGridView1.DataSource = dt;

            myConnection.closeConnection();
        }

        private void InitializeTimer()
        {
            timer1.Interval = 86400000;
            timer1.Tick += new EventHandler(timer1_Tick);

            timer1.Enabled = true;

        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            try {



                GestionDate date = new GestionDate();
                myConnection = ConnexionSql.getInstance("localhost", "gsb-v1", "root", "");
                dateVerif = Convert.ToInt32(date.dateJour().Substring(0, 2));
              

                if (dateVerif >= 1 && dateVerif <= 10)
                {

                    myConnection.openConnection();

                    myCom = myConnection.reqExec("update testfichefrais set idEtat ='CL' where idEtat ='CR' and mois = '" + date.moisPrecedent()+"'");
                    myCom.ExecuteNonQuery();

                    myDate = new GestionDate();
                    date1 = myDate.moisPrecedent();

                    myCom = myConnection.reqExec("Select * from testfichefrais where mois='" + date1 + "'");
                    dt = new DataTable();
                    dt.Load(myCom.ExecuteReader());

                    dataGridView1.DataSource = dt;

                    myConnection.closeConnection();
                }
                else if (dateVerif >= 15 && dateVerif <= 31)
                {
                    myConnection.openConnection();

                    myCom = myConnection.reqExec("update testfichefrais set idEtat ='MP' where idEtat ='RB' and mois = '" + date.moisPrecedent() + "'");
                    myCom.ExecuteNonQuery();

                    myDate = new GestionDate();
                    date1 = myDate.moisPrecedent();

                    myCom = myConnection.reqExec("Select * from testfichefrais where mois='" + date1 + "'");
                    dt = new DataTable();
                    dt.Load(myCom.ExecuteReader());

                    dataGridView1.DataSource = dt;

                    myConnection.closeConnection();
                }

            }
            catch (Exception emp)
            {
                MessageBox.Show(emp.Message);
            }
            

        }
    }
}
