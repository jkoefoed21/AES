using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace encryption
{
    class consoleTester
    {
        public static readonly string filePath = "C:\\Users\\Jack Koefoed\\Pictures\\Camera Roll\\EncryptionTest.mp4";
        public static void main(String[] args)
        {
            
            String password = "Jack Koefoed";
            //AES.encryptionMain(password, filePath);
            //Console.ReadKey();
            GC.Collect();
            Console.WriteLine();
            AES.decryptionMain(password, filePath);
            Console.ReadKey();
        }
    }
}
