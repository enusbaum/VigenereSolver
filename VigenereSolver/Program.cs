using System;
using System.IO;
using log4net;
using log4net.Config;
using VigenereSolver.Library.Analysis;
using VigenereSolver.Library.Attack;
using VigenereSolver.Library.Cipher;
using VigenereSolver.Library.Helpers;

namespace VigenereSolver.Console
{
    class Program
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Program).Namespace);
        private static readonly Vigenere VigenereEngine = new Vigenere();

        static Program()
        {
            //Setup log4net
            var log4netconfig = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, typeof(Program).Namespace + ".log4net.xml");
            XmlConfigurator.ConfigureAndWatch(new FileInfo(log4netconfig));
        }

        static void Main(string[] args)
        {

            //Example Enciphered Text
            const string cipherText = "Ar op chmrivv tfh ofvx nyneiksfnv cxsvu dc ysxjvv zszg di lgqg rhoagg kltl M'xv fxwr vlvgari fzxj mp dc farf vzxj wkegx. \"Olgeiowv afy ywin cmdw gtzxbumbzrz sra frx,\" zi vfpw ei, \"llwm jiovquwv vyem spn klx hiqgpx ar vyml ostch aszge'x ash vyi tvzcextyiu kltl cql'zx zef.\"";

            //Example of how to execute a known plaintext attack
            _log.Info("-------- KNOWN PLAINTEXT ATTACK --------");
            var possibleKeys = KnownPlaintextAttack.AttackWithKnownPlaintext(cipherText, "the");
            foreach (var key in possibleKeys)
            {
                _log.InfoFormat("Possible Key: {0}", key);
                _log.InfoFormat(VigenereEngine.Decipher(cipherText, key));

            }

            //Example of how to execute a Kasiski/Babbage Analysis & Attack
            _log.Info("-------- KASISKI ATTACK --------");
            var likelyKeyLengths = Kasiski.KasiskiExamination(cipherText);
            foreach (var keyLength in likelyKeyLengths)
            {
                _log.InfoFormat("Attempting hack with key length {0} ({1} possible keys)", keyLength, Math.Pow(Convert.ToDouble(keyLength), Convert.ToDouble(Constants.NumMostFreqLetters)));
                var foundKey = KasiskiAttack.AttackWithKeyLength(cipherText, keyLength);

                if (!string.IsNullOrEmpty(foundKey))
                {
                    _log.InfoFormat("Decrypted Text: {0}", new Vigenere().Decipher(cipherText, foundKey));
                    System.Console.ReadKey();
                    return;
                }
            }
        }
    }
}
