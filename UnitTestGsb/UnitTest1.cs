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
        public void TestMethod1()
        {

            Assert.AreEqual("202102", date.moisPrecedent(), "Erreur");
            Assert.AreEqual("202104", date.moisSuivant(), "Erreur");
        }
    }
}
