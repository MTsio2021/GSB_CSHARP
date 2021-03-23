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
        /// <summary>
        /// Initialisation au lancement du service windows
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            InitializeTimer();

        }

        protected override void OnStop()
        {
        }
        /// <summary>
        /// Création du timer
        /// </summary>
        private void InitializeTimer()
        {
            timer1.Interval = 10000;
            timer1.Elapsed += timer1_Tick;

            timer1.Enabled = true;

        }

        /// <summary>
        /// Fonction qui permet la mise à jour des fiches de frais en fonctions de la date du jour et du mois de ces dernières
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {

            try
            {
                
                
                //Initialisation de l'objet date
                GestionDate date = new GestionDate();
                myConnection = ConnexionSql.getInstance("localhost", "gsb-v1", "root", "");
                dateVerif = Convert.ToInt32(date.dateJour().Substring(0, 2));

                //Si la date du jour est entre 1 et 10 on passe l'état des fiches du mois précédent à CL Si elle est à l'état CR 

                if (dateVerif >= 1 && dateVerif <= 10)
                {

                    myConnection.openConnection();

                    myCom = myConnection.reqExec("update testfichefrais set idEtat ='CL' where idEtat ='CR' and mois = '" + date.moisPrecedent() + "'");
                    myCom.ExecuteNonQuery();

                   


                    myConnection.closeConnection();
                }

                //Si la date du jour est entre 10 et 20 on passe l'état des fiches du mois précédent à MP Si elle est à l'état RB 
                else if (dateVerif >= 20 && dateVerif <= 31)
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
