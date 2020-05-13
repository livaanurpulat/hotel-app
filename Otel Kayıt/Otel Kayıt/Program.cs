/****************************************************************************
**                         SAKARYA ÜNİVERSİTESİ
**              BİLGİSAYAR VE BİLİŞİM BİLİMLERİ FAKÜLTESİ
**               BİLİŞİM SİSTEMLERİ MÜHENDİSLİĞİ BÖLÜMÜ
**                  NESNEYE DAYALI PROGRAMLAMA DERSİ
**                       2019-2020 BAHAR DÖNEMİ
**
**                    PROJE NUMARASI.........: 01
**                    ÖĞRENCİ ADI............:Liva Nur PULAT
**                    ÖĞRENCİ NUMARASI.......:B191200371
**                    DERSİN ALINDIĞI GRUP...: A
****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otel_Kayıt
{
    static class Program
    {
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //uygulama ilk olarak FrmAdmin adlı form ile açılır.
            Application.Run(new FrmAdmin());
        }
    }
}
