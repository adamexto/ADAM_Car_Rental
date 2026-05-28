namespace PRG_MAUI_Car_Register.Model
{
    internal class MC : Vehicle
    {
        public override string Type => "MC";

        public override string Describe()
        {
            return $"Motorcykel: {RegistrationNumber} – {Manufacturer} {VehicleModel} ({YearModel})";
        }
    }
}
