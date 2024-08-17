namespace SumaCrud10195594
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDbService _dbService;
        private int _editResultadoId;

        public MainPage(LocalDbService dbService)
        {
            InitializeComponent();
            _dbService = dbService;
            Task.Run(async () => Listview.ItemsSource = await _dbService.GetResultado());
        }

        private async void sumarBtn_Clicked(object sender, EventArgs e)
        {
           
            if (_editResultadoId == 0)
            {
                int total, num1, num2;

                num1 = Convert.ToInt32(Entryprimernumero.Text);
                num2 = Convert.ToInt32(Entrysegundonumero.Text);

                total= num1 + num2;
                labelresultado.Text = total.ToString() ;

                await _dbService.Create(new Resultado
                {
                    Numero1 = Entryprimernumero.Text,
                    Numero2 = Entrysegundonumero.Text,
                    Suma1 = total.ToString()
                });
                Listview.ItemsSource = await _dbService.GetResultado();
            }
            else
            {
                int total, num1, num2;

                num1 = Convert.ToInt32(Entryprimernumero.Text);
                num2 = Convert.ToInt32(Entrysegundonumero.Text);

                total = num1 + num2;
                labelresultado.Text = total.ToString();

                await _dbService.Update(new Resultado
                {
                    Id = _editResultadoId,
                    Numero1 = Entryprimernumero.Text,
                    Numero2 = Entrysegundonumero.Text,
                    Suma1 = labelresultado.Text
                });
                _editResultadoId = 0;
            }
            Entryprimernumero.Text = string.Empty;
            Entrysegundonumero.Text = string.Empty;
            labelresultado.Text = string.Empty;

            Listview.ItemsSource = await _dbService.GetResultado();
        }

        private async void Listview_ItemTapped(object sender, ItemTappedEventArgs e)
        { 
            var resultado = (Resultado)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

            switch (action)
            {
                case "Edit":
                    _editResultadoId = resultado.Id;
                    Entryprimernumero.Text = resultado.Numero1;
                    Entrysegundonumero.Text = resultado.Numero2;
                    labelresultado.Text = resultado.Suma1;
                    break;

                case "Delete":
                    await _dbService.Delete(resultado);
                    Listview.ItemsSource = await _dbService.GetResultado();
                    break;
            }
        }

    }
    }

