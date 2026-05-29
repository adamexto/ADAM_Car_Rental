using PRG_MAUI_Car_Register.Model;
using PRG_MAUI_Car_Register.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace PRG_MAUI_Car_Register.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private string? _registrationNumber;
        public string? RegistrationNumber
        {
            get => _registrationNumber;
            set
            {
                _registrationNumber = value;
                OnChanged(nameof(RegistrationNumber));
            }
        }

        private string? _manufacturer;
        public string? Manufacturer
        {
            get => _manufacturer;
            set
            {
                _manufacturer = value;
                OnChanged(nameof(Manufacturer));
            }
        }

        private string? _vehicleModel;
        public string? VehicleModel
        {
            get => _vehicleModel;
            set
            {
                _vehicleModel = value;
                OnChanged(nameof(VehicleModel));
            }
        }

        private string? _yearModel;
        public string? YearModel
        {
            get => _yearModel;
            set
            {
                _yearModel = value;
                OnChanged(nameof(YearModel));
            }
        }

        private string? _selectedVehicleType;
        public string? SelectedVehicleType
        {
            get => _selectedVehicleType;
            set
            {
                _selectedVehicleType = value;
                OnChanged(nameof(SelectedVehicleType));
            }
        }

        private string? _searchTerm;
        public string? SearchTerm
        {
            get => _searchTerm;
            set
            {
                _searchTerm = value;
                OnChanged(nameof(SearchTerm));
            }
        }

        private string? _searchResult;
        public string? SearchResult
        {
            get => _searchResult;
            set
            {
                _searchResult = value;
                OnChanged(nameof(SearchResult));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public List<string> VehicleTypes { get; } = ["Car", "MC", "Truck"];

        public ObservableCollection<Vehicle> AllVehicles { get; } = [];

        public IEnumerable<Vehicle> Cars => AllVehicles.Where(v => v is Car);
        public IEnumerable<Vehicle> MCs => AllVehicles.Where(v => v is MC);
        public IEnumerable<Vehicle> Trucks => AllVehicles.Where(v => v is Truck);

        public ICommand RegisterCommand { get; }
        public ICommand SearchCommand { get; }

        public MainPageViewModel()
        {
            RegisterCommand = new Command(async () => await Register());
            SearchCommand = new Command(Search);
        }

        private async Task Register()
        {
            try
            {
                Vehicle v = SelectedVehicleType switch
                {
                    "Car" => new Car(),
                    "MC" => new MC(),
                    "Truck" => new Truck(),
                    _ => throw new Exception("Välj en fordonstyp.")
                };

                v.RegistrationNumber = RegistrationNumber;
                v.Manufacturer = Manufacturer;
                v.VehicleModel = VehicleModel;
                v.YearModel = YearModel;

                AllVehicles.Add(v);

                OnChanged(nameof(Cars));
                OnChanged(nameof(MCs));
                OnChanged(nameof(Trucks));

            }
            catch (Exception ex)
            {
                await Application.Current.Windows[0].Page.DisplayAlert("Fel", ex.Message, "OK");
            }

            await SaveAll();

            ClearInputs();
        }

        private void Search()
        {
            var found = AllVehicles.FirstOrDefault(v =>
                v.RegistrationNumber.Equals(SearchTerm, StringComparison.OrdinalIgnoreCase));

            if (found == null)
            {
                SearchResult = "Inget fordon hittades.";
            }
            else
            {
                SearchResult =
                    $"Typ: {found.GetType().Name}\n" +
                    $"RegNr: {found.RegistrationNumber}\n" +
                    $"Tillverkare: {found.Manufacturer}\n" +
                    $"Modell: {found.VehicleModel}\n" +
                    $"Årsmodell: {found.YearModel}";
            }
        }

        public async Task SaveAll()
        {
            await DataService.SaveAsync(AllVehicles.ToList());
        }

        public async Task LoadAll()
        {
            var list = await DataService.LoadAsync();

            AllVehicles.Clear();

            foreach (var v in list)
                AllVehicles.Add(v);

            OnChanged(nameof(Cars));
            OnChanged(nameof(MCs));
            OnChanged(nameof(Trucks));
        }

        private void ClearInputs()
        {
            RegistrationNumber = string.Empty;
            Manufacturer = string.Empty;
            VehicleModel = string.Empty;
            YearModel = string.Empty;
        }
    }
}

