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

        //Initialisation des variables / objets

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
        /// <summary>
        /// Chargement du formulair, connexion et remplissage des datatables et datagrid view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       
        private void Form1_Load_1(object sender, EventArgs e)
        {

            //Connection à la bdd
            myConnection = ConnexionSql.getInstance("localhost", "gsb-v1", "root", "");
            myConnection.openConnection();


            myDate = new GestionDate();


            //Selection des fiche du mois précédent en fonction de la date du jour
            date1 = myDate.moisPrecedent();
            lb1.Text = date1;
            myCom = myConnection.reqExec("Select * from testfichefrais where mois='" + date1 + "'");


            //Remplissage des datatables
            dt = new DataTable();
            dt.Load(myCom.ExecuteReader());


            //Remplissage du datagrid view
            dataGridView1.DataSource = dt;


            //Fermeture de la connection
            myConnection.closeConnection();
        }


        /// <summary>
        /// Création du timer
        /// </summary>
        private void InitializeTimer()
        {
            timer1.Interval = 5000;
            timer1.Tick += new EventHandler(timer1_Tick);

            timer1.Enabled = true;

        }
        /// <summary>
        /// Fonction modifiant les états en fonctions du timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {

            try {


                //Initialisation de l'objet de date + connection à la bdd

                GestionDate date = new GestionDate();
                myConnection = ConnexionSql.getInstance("localhost", "gsb-v1", "root", "");


                dateVerif = Convert.ToInt32(date.dateJour().Substring(0, 2));
               

                //Si la date du jour est entre le 1 et le 10 du mois alors l'état des fiches de frais du mois précédent change de CR à CL
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

                //Si la date du jour est entre le 10 et le 20 du mois alors l'état des fiches de frais du mois précédent change de RB à MP
                else if (dateVerif >= 20 && dateVerif <= 31)
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
