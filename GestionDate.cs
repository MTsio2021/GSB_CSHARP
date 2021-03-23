using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSB_CSHARP
{
    public class GestionDate
    {

        DateTime ajd = DateTime.Now;

        /// <summary>
        /// Retourne la date du jour
        /// </summary>
        
        public String dateJour()
        {
            String asString = ajd.ToString("dd/MM/yyyy");
            return asString;
        }

        /// <summary>
        /// Retourne le mois précédent par rapport à la date d'aujourd'hui
        /// </summary>
        
        public String moisPrecedent()
        {
            ajd = ajd.AddMonths(-1);
            String asString = ajd.ToString("yyyyMM");

            return asString;
        }

        /// <summary>
        /// Retourne le mois courant 
        /// </summary>
       
        public String moisCourant()
        {

            String asString = ajd.ToString("yyyyMM");

            return asString;
        }

        /// <summary>
        /// Retourne le mois suivant en fonction de la date d'aujourd'hui
        /// </summary>
        
        public String moisSuivant()
        {
            ajd = ajd.AddMonths(+1);
            String asString = ajd.ToString("yyyyMM");

            return asString;
        }
    }
}
