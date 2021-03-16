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
        public Form1()
        {
            InitializeComponent();
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
    }
}
