using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GSB_CSHARP;

namespace UnitTestGsb
{
    [TestClass]
   
    public class UnitTest1
    {
        GestionDate date = new GestionDate();
        [TestMethod]

       
        //TEST UNITAIRE
        public void TestMethodPrecedent()
        {

            Assert.AreEqual("202102", date.moisPrecedent(), "Le mois précédent n'est pas le bon");
            
        }

        public void TestMethodSuivant()
        {
            Assert.AreEqual("202104", date.moisSuivant(), "Le mois suivant n'est pas le bon");
        }

        public void TestMethodCourant()
        {
            Assert.AreEqual("202103", date.moisCourant(), "Le mois courant n'est pas le bon");
        }
    }
}
