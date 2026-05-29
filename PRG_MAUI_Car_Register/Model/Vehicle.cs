using System.Text.RegularExpressions;

namespace PRG_MAUI_Car_Register.Model
{
    public abstract class Vehicle
    {
        // Medlemsvariabler
        public string registrationNumber { get; set; }
        public string manufacturer { get; set; }
        public string vehicleModel { get; set; }
        public string yearModel { get; set; }
        public abstract string Type { get; }

        // Get-Set för att hålla variablerna privata, och för att validera inkommande värden från UI (user interface, användargränssnittet)

        public virtual string Describe()
        {
            return $"{Type}: {RegistrationNumber} – {Manufacturer} {VehicleModel} ({YearModel})";
        }

        public string RegistrationNumber
        {
            get { return registrationNumber; }

            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length != 6)
                    throw new ArgumentException("Registreringsnummer måste vara exakt 6 tecken.");

                for (int i = 0; i < 3; i++)
                    if (!char.IsLetter(value[i]))
                        throw new ArgumentException("De första tre tecknen måste vara bokstäver.");

                for (int i = 3; i < 5; i++)
                    if (!char.IsDigit(value[i]))
                        throw new ArgumentException("Tecken 4 och 5 måste vara siffror.");

                if (!char.IsLetterOrDigit(value[5]))
                    throw new ArgumentException("Sista tecknet måste vara bokstav eller siffra.");

                registrationNumber = value.ToUpper();
            }
        }

        public string VehicleModel
        {
            get { return vehicleModel; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Glöm inte att skriva in fordons model");

                if (!Regex.IsMatch(value, @"^[a-zA-Z0-9]+$"))
                    throw new ArgumentException("Model får bara innehålla bokstäver, siffror och mellanslag");

                vehicleModel = value;
            }
        }

        public string Manufacturer
        {
            get { return manufacturer; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Glöm inte att skriva in tillverkaren");

                if (!Regex.IsMatch(value, @"^[a-zA-Z0-9]+$"))
                    throw new ArgumentException("Tillverkare får bara innehålla bokstäver, siffror och mellanslag");

                manufacturer = value;
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

                yearModel = value;
            }
        }
    }
}
