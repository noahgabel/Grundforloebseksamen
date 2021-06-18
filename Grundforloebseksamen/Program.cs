using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Grundforloebseksamen
{
    class Program
    {
        static void Main(string[] args)
        {
            string menuChoose;

            do
            {
                Console.Write("Skriv CPR nummer xxxxxxxxxx (Sammensat) ->");
                string CPR = Console.ReadLine();

                Console.Write("Skriv din højde (Centimeter) ->");
                double Height = Convert.ToInt32(Console.ReadLine());

                Console.Write("Skriv din vægt (Kilogram) ->");
                double Weight = Convert.ToInt32(Console.ReadLine());

                Console.Write("Hvad vælger du? (Bigmac, BMI, Kcal, Exit)");
                menuChoose = Console.ReadLine();

                switch (menuChoose.ToLower())
                {
                    case "bigmac":
                        bigMacBeregner(Weight);
                        break;
                    case "bmi":
                        bmiBeregner(Height, Weight);
                        break;
                    case "kcal":
                        kcalBeregner(CPR, Height, Weight);
                        break;
                    default:
                        Console.WriteLine("Du lukker nu programmet, tryk på en tast for at afslutte...");
                        break;
                }

            } while (menuChoose != "exit");

            Console.ReadKey();

            void bigMacBeregner(double Weight)
            {
                int bigMacKalorier = 503;

                Console.Write("Hvor mange BigMac's vil du gerne spise? ->");
                int numberOfBigmacs = Convert.ToInt32(Console.ReadLine());

                int overAllKalorierBigMacs = numberOfBigmacs * bigMacKalorier;

                double totalKMNeeded = overAllKalorierBigMacs / Weight;

                Console.WriteLine("Du skal løbe {0} km", totalKMNeeded);

            }


            void bmiBeregner(double Height, double Weight)
            {
                double HeightInMeter = Height * 0.01;

                Console.WriteLine(HeightInMeter);

                double bmi = Weight / Math.Pow(HeightInMeter, 2);

                Console.WriteLine("Din bmi er på " + bmi);
            }


            void kcalBeregner(string CPR, double Height, double Weight)
            {
                string lastIntOfCpr = CPR.Substring(9);

                int genderResult = Convert.ToInt32(lastIntOfCpr) % 2;

                string sixFirstCifre = CPR.Substring(0, 6);

                DateTime birth;
                CultureInfo country = CultureInfo.InvariantCulture;
                country = new CultureInfo("da-DK");

                birth = DateTime.ParseExact(sixFirstCifre, "ddMMyy", country);

                DateTime now = DateTime.Now;

                TimeSpan timespan = now - birth;

                int age = timespan.Days / 365;

                if (genderResult == 0)
                {
                    double basalStofSkifte = ((Weight * 42) + (Height * 26.3) - (20.7 * age) - 676.2) / 4.2;

                    double kalorierPrDag = (1.65) * (basalStofSkifte);

                    Console.WriteLine("Du har brug {0} Kalorier", kalorierPrDag);

                }
                else
                {
                    double basalStofSkifte = ((Weight * 42) + (Height * 26.3) - (20.7 * age) + 12) / 4.2;

                    double kalorierPrDag = (1.65) * (basalStofSkifte);

                    Console.WriteLine("Du har brug {0} Kalorier", kalorierPrDag);
                }
            }
        }
    }
}
