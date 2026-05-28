namespace PRG_MAUI_Car_Register.Model
{
    internal class Car : Vehicle
    {
        public override string Type => "Car";

        public override string Describe()
        {
            return $"Bil: {RegistrationNumber} – {Manufacturer} {VehicleModel} ({YearModel})";
        }
    }
}