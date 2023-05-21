using System.Windows.Forms;

namespace FrontDamka
{
    class Program
    {
        public static void Main()
        {
            bool settingsOk;
            Damka theDamka = new Damka(out settingsOk);

            if(settingsOk)
            {
                theDamka.ShowDialog();
            }
            else
            {
                MessageBox.Show("Invalid Settings, Please Restart The App And Enter Correct Settings.");
            }
        }
    }
}
