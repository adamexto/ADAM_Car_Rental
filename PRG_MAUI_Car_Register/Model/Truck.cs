namespace PRG_MAUI_Car_Register.Model
{
    internal class Truck : Vehicle
    {
        public override string Type => "Truck";

        public override string Describe()
        {
            return $"Lastbil: {RegistrationNumber} – {Manufacturer} {VehicleModel} ({YearModel})";
        }
    }
}
