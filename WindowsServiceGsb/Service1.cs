using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using GSB_CSHARP;
using MySql.Data.MySqlClient;

namespace WindowsServiceGsb
{
    public partial class Service1 : ServiceBase
    {
        private Timer timer1 = new Timer();
        private MySqlCommand myCom;
        private ConnexionSql myConnection;
        private int dateVerif;

        public Service1()
        {
            InitializeComponent();
            InitializeTimer();
        }

        protected override void OnStart(string[] args)
        {
            InitializeTimer();

        }

        protected override void OnStop()
        {
        }

        private void InitializeTimer()
        {
            timer1.Interval = 10000;
            timer1.Elapsed += timer1_Tick;

            timer1.Enabled = true;

        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            try
            {



                GestionDate date = new GestionDate();
                myConnection = ConnexionSql.getInstance("localhost", "gsb-v1", "root", "");
                dateVerif = Convert.ToInt32(date.dateJour().Substring(0, 2));


                if (dateVerif >= 1 && dateVerif <= 10)
                {

                    myConnection.openConnection();

                    myCom = myConnection.reqExec("update testfichefrais set idEtat ='CL' where idEtat ='CR' and mois = '" + date.moisPrecedent() + "'");
                    myCom.ExecuteNonQuery();

                   


                    myConnection.closeConnection();
                }
                else if (dateVerif >= 15 && dateVerif <= 31)
                {
                    myConnection.openConnection();

                    myCom = myConnection.reqExec("update testfichefrais set idEtat ='MP' where idEtat ='RB' and mois = '" + date.moisPrecedent() + "'");
                    myCom.ExecuteNonQuery();

                    


                    myConnection.closeConnection();
                }

            }
            catch (Exception emp)
            {
                throw(emp);
            }


        }
    }
}
