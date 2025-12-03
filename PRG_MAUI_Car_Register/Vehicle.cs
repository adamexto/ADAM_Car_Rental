using System.Text.RegularExpressions;
namespace PRG_MAUI_Car_Register
{
    class Vehicle
    {
        // Medlemsvariabler
        public enum Type { Bil, MC, Lastbil };
        private Type vehicleType;
        private string registrationNumber = string.Empty;
        private string manufacturer = string.Empty;
        private string vehicleModel = string.Empty;
        private string yearModel = string.Empty;


        // Konstruktor (en metod med samma namn som klassen, som returnerar ett objekt)
        public Vehicle(Type vehicleType) // en konstruktor kan, men måste inte, ta parametrar
        {
            this.vehicleType = vehicleType;


        }

        // Get-Set för att hålla variablerna privata, och för att validera inkommande värden från UI (user interface, användargränssnittet)
        public string RegistrationNumber
        {
            get { return registrationNumber; }

            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    if (value.Length == 6)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (!char.IsLetter(value[i]))
                                throw new ArgumentException("Inkorret registreringsnummer: De första tre tecknen måste vara bokstäver.");
                        }

                        for (int i = 3; i < 6; i++)
                        {
                            if (i < 5)
                            {
                                if (!char.IsDigit(value[i]))
                                    throw new ArgumentException("Inkorret registreringsnummer: Det fjärde och femte tecknet måste vara siffror.");
                            }
                            else
                            {
                                if (!char.IsDigit(value[i]) && !char.IsLetter(value[i]))
                                    throw new ArgumentException("Inkorret registreringsnummer: Det sjätte tecknet måste vara en siffra eller en bokstav.");
                            }
                        }
                    }
                }
                else
                {
                    throw new ArgumentException("Ett registreringsnummer måste bestå av exakt 6 tecken, med tre bokstäver följt av två siffror och en siffra eller bokstav.");
                }

                registrationNumber = value.ToUpper();
            }
        }


        // Fordonstyp tas in från dropdown-menyn, och behöver därför inte valideras
        public Type VehicleType
        {
            get { return vehicleType; }
            set { this.vehicleType = value; }
        }

        //TODO Tillverkare ska valideras, sparas i objektet och visas i UI
        public string VehicleModel
        {
            get { return vehicleModel; }
            set
            {
                
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Glöm inte att skriva in fordons model");

                if (!Regex.IsMatch(value, @"^[a-zA-Z0-9]+$"))
                    throw new ArgumentException("Model får bara innehålla bokstäver, siffror och mellanslag");

                this.vehicleModel = value;
            }
        }

        //TODO Modell ska valideras, sparas i objektet och visas i UI
        public string Manufacturer
        {
            get { return manufacturer; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Glöm inte att skriva in tillverkaren");

                if (!Regex.IsMatch(value, @"^[a-zA-Z0-9]+$"))
                    throw new ArgumentException("Tillverkare får bara innehålla bokstäver, siffror och mellanslag");

                this.manufacturer = value;
            }
        }

        public string YearModel
        {
            get { return yearModel; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Glöm inte att skriva in Årsmodell");

                if (!int.TryParse(value, out int parsedYear))
                    throw new ArgumentException("Bara nummer får skrivas");

                if (!Regex.IsMatch(value, @"^[0-9]+$"))
                    throw new ArgumentException("Inga onördiga tecken");

                int minYear = 1895;
                int maxYear = DateTime.Now.Year + 1;
                if (parsedYear < minYear || parsedYear > maxYear)
                    throw new ArgumentException($"Årsmodell måste vara mellan {minYear} och {maxYear}.");

                this.yearModel = value;
            }
        }

        //TODO Att spara årsmodell ska möjliggöras, ska valideras, sparas i objektet och visas i UI

        // Klassens eventuella övriga metoder brukar finnas här, här en override av ToString()

        //TODO Modifiera overriden på ToString() så att allt visas som önskat i UIs listBox
        public override string ToString()
        {
            return this.registrationNumber + "\t" + this.vehicleType + "\t" + this.vehicleModel + "\t" + this.manufacturer + "\t" + this.yearModel;
        }
    }
}
