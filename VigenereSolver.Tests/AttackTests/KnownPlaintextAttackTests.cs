using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using VigenereSolver.Library.Attack;

namespace VigenereSolver.Tests.AttackTests
{
    [TestClass]
    public class KnownPlaintextAttackTests
    {
        [TestMethod]
        public void AttackWithKnownPlaintext_Test()
        {
            var actual =
                KnownPlaintextAttack.AttackWithKnownPlaintext(
                    "Llkj ml s xgjx llvkek mg fg lwxv ajvr mwwvzrz llg Mmzwrgii Vatjvv mgsnj", "testing");

            var expected = new List<string> {"SECRET"};

            CollectionAssert.AreEqual(expected, actual);
        }

    }
}
