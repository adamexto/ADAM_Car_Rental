using PRG_MAUI_Car_Register.Model;
using System.Diagnostics;

namespace PRG_MAUI_Car_Register
{
    public partial class MainPage : ContentPage
    {
        List<Vehicle> vehicleList = new List<Vehicle>();

        public MainPage()
        {
            InitializeComponent();
            VehiclePicker.SelectedIndex = 0;
        }

        private void OnRegisterClicked(object sender, EventArgs e)
        {
            try
            {
                Vehicle? _vehicle = null;

                if (VehiclePicker.SelectedIndex == -1)
                    return;

                if (VehiclePicker.SelectedIndex == 0)
                {
                    _vehicle = new Car();
                }
                else if (VehiclePicker.SelectedIndex == 1)
                {
                    _vehicle = new Truck();
                }
                else if (VehiclePicker.SelectedIndex == 2)
                {
                    _vehicle = new MC();
                }

                _vehicle.RegistrationNumber = entryRegistrationNumber.Text;
                _vehicle.Manufacturer = entryManufacturer.Text;
                _vehicle.VehicleModel = entryModel.Text;
                _vehicle.YearModel = entryYearModel.Text;


                vehicleList.Add(_vehicle);
                listViewVehicles.ItemsSource = null;
                listViewVehicles.ItemsSource = vehicleList;

                entryRegistrationNumber.Text = string.Empty;
                entryManufacturer.Text = string.Empty;
                entryModel.Text = string.Empty;
                entryYearModel.Text = string.Empty;
            }
            catch (ArgumentException ex)
            {
                DisplayAlert("Fel", ex.Message, "OK");
            }
        }
        private void OnSearchClicked(object sender, EventArgs e)
        {
            string searchTerm = entrySearchRegistrationNumber.Text?.ToLower();

            if (string.IsNullOrEmpty(searchTerm))
            {
                entrySearchRegistrationNumber.Text = "Ange ett registreringsnummer för att söka.";
                return;
            }

            var foundVehicle = vehicleList.FirstOrDefault(v => v.RegistrationNumber?.ToLower() == searchTerm);

            if (foundVehicle != null)
            {
                labelSearchResult.Text = $"Fordon hittat:\n" +
                                         $"Registreringsnummer: {foundVehicle.RegistrationNumber}\n" +
                                         $"Tillverkare: {foundVehicle.Manufacturer}\n" +
                                         $"Modell: {foundVehicle.VehicleModel}";
            }
            else
            {
                labelSearchResult.Text = "Inget fordon hittades med det registreringsnumret.";
            }
        }

    }
}
