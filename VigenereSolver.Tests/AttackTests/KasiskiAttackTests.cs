using Microsoft.VisualStudio.TestTools.UnitTesting;
using VigenereSolver.Library.Attack;

namespace VigenereSolver.Tests.AttackTests
{
    [TestClass]
    public class KasiskiAttackTests
    {
        [TestMethod]
        public void AttackWithKeyLength_Test()
        {
            var expected =
                KasiskiAttack.AttackWithKeyLength(
                    "Llkj ml s xgjx llvkek mg fg lwxv ajvr mwwvzrz llg Mmzwrgii Vatjvv mgsnj", 6);

            Assert.AreEqual("SECRET", expected);
        }
    }
}
