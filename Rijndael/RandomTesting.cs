using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace encryption
{
    /// <summary>
    /// This is just a class that I drew up to test various stuff. Currently it is testing the difference between encrypt
    /// and encrypt single
    /// </summary>
    class RandomTesting
    {
        public static byte[] key = { 0x26, 0xA1, 0xB4, 0xC3, 0x04, 0x57, 0x66, 0x97, 0xD8, 0x09, 0x0A, 0xBB, 0xDC, 0xAD, 0x2E, 0x76 };
        public static byte[] toEncrypt = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
        public static BitMatrix[] schedule = AES.getKeySchedule(key);
        public static BitMatrix matrix = new BitMatrix(AES.GF_TABLE, AES.SUB_TABLE, toEncrypt, 0);
        public static BitMatrix iv = new BitMatrix(AES.GF_TABLE, AES.SUB_TABLE, new byte[16], 0);
        public static void Main(String[] args)
        {
            int m = 1000000;
            for (int ii = 0; ii < m; ii++)
            {
                AES.encryptSingle(schedule, matrix);
            }
            Stopwatch s = new Stopwatch();
            s.Start();
            for (int ii=0; ii<m; ii++)
            {
                AES.encryptSingle(schedule, matrix);
            }
            Console.WriteLine(s.ElapsedMilliseconds);
            Console.ReadKey();
            s.Restart();
            //for (int ii = 0; ii < m; ii++)
            {
                //AES.encrypt(schedule, toEncrypt, iv);
            }
            //Console.WriteLine(s.ElapsedMilliseconds);
            Console.ReadKey();
        }
    }
}
