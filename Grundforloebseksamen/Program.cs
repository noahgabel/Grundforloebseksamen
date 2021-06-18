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
                //Ask for CPR and readline
                Console.Write("Skriv CPR nummer xxxxxxxxxx (Sammensat) ->");
                string CPR = Console.ReadLine();

                //Ask for height and readline
                Console.Write("Skriv din højde (Centimeter) ->");
                double Height = Convert.ToInt32(Console.ReadLine());

                //Ask for weight and readline
                Console.Write("Skriv din vægt (Kilogram) ->");
                double Weight = Convert.ToInt32(Console.ReadLine());

                //Which do you choose (Menu)
                Console.Write("Hvad vælger du? (Bigmac, BMI, Kcal, Exit)");
                menuChoose = Console.ReadLine();

                //Switch case and to lower the value.
                switch (menuChoose.ToLower())
                {
                    //In case of bigmac then run bigmac
                    case "bigmac":
                        bigMacBeregner(Weight);
                        break;
                    //In case of bmi then run bmi
                    case "bmi":
                        bmiBeregner(Height, Weight);
                        break;
                    //In case of kcal then run kcal
                    case "kcal":
                        kcalBeregner(CPR, Height, Weight);
                        break;
                    //The "default" the exit value
                    default:
                        Console.WriteLine("Du lukker nu programmet, tryk på en tast for at afslutte...");
                        break;
                }
                //Stop the loop if exit
            } while (menuChoose != "exit");
            //Keepalive
            Console.ReadKey();

            //The burgerCalculater
            void bigMacBeregner(double Weight)
            {
                //Set the default value for a bigmac
                int bigMacKalorier = 503;
                //Ask for how many burgers the user would like and read the input
                Console.Write("Hvor mange BigMac's vil du gerne spise? ->");
                int numberOfBigmacs = Convert.ToInt32(Console.ReadLine());
                //do the calculations
                int overAllKalorierBigMacs = numberOfBigmacs * bigMacKalorier;
                //do some more calculations
                double totalKMNeeded = overAllKalorierBigMacs / Weight;
                //Output the result to the user
                Console.WriteLine("Du skal løbe {0} km", totalKMNeeded);

            }

            //The bmiCalculator
            void bmiBeregner(double Height, double Weight)
            {
                //Convert to meters instead of CM
                double HeightInMeter = Height * 0.01;

                //Use math.pow to calculate the "potens"
                double bmi = Weight / Math.Pow(HeightInMeter, 2);
                //output the result
                Console.WriteLine("Din bmi er på " + bmi);
            }

            //The kcalCalculator
            void kcalBeregner(string CPR, double Height, double Weight)
            {
                //Get the last of the Cpr
                string lastIntOfCpr = CPR.Substring(9);
                //use modulus to see their gender
                int genderResult = Convert.ToInt32(lastIntOfCpr) % 2;
                //Get the six first characters to be used in the age calc
                string sixFirstCifre = CPR.Substring(0, 6);
                //Init the Datatime
                DateTime birth;
                //Get the Culruteinfo library
                CultureInfo country = CultureInfo.InvariantCulture;

                //Set Country to da-DK which is Danish-Denmark
                country = new CultureInfo("da-DK");
                //Use parseexact to calc the birth date, I've used the ddmmyy because the social security number is "ddMMyy" - "****"
                birth = DateTime.ParseExact(sixFirstCifre, "ddMMyy", country);

                //Set variable now to now
                DateTime now = DateTime.Now;
                //Calc the Timespan between now and the birth
                TimeSpan timespan = now - birth;
                //Then divide the timespan in days with 365
                int age = timespan.Days / 365;
                //If the gender result is 0 you are a women.
                if (genderResult == 0)
                {
                    //Do the basalstofskifte calculation
                    double basalStofSkifte = ((Weight * 42) + (Height * 26.3) - (20.7 * age) - 676.2) / 4.2;

                    //Kalories pr day needed
                    double kalorierPrDag = (1.65) * (basalStofSkifte);

                    Console.WriteLine("Du har brug {0} Kalorier", kalorierPrDag);

                }
                //else a man
                else
                {
                    //Do the basalstofskifte calculation
                    double basalStofSkifte = ((Weight * 42) + (Height * 26.3) - (20.7 * age) + 12) / 4.2;

                    //Kalories pr day needed
                    double kalorierPrDag = (1.65) * (basalStofSkifte);

                    Console.WriteLine("Du har brug {0} Kalorier", kalorierPrDag);
                }
            }
        }
    }
}
