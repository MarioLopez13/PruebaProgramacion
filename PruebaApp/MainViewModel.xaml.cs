using Newtonsoft.Json;
using SQLite;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace PruebaApp
{
public partial class MainViewModel : INotifyPropertyChanged
{
	public ObservableCollection<Joke> Joke { get; set; }
        private SQLiteAsyncConnection _database;
        private ObservableCollection<Joke> _jokes;
        private async void OnGetJokeClicked(object sender, EventArgs e)
        {
            var jokeText = await FetchJokeAsync();
            var random = new Random();
            var code = "ML" + random.Next(1000, 2000);
            var newJoke = new Joke { JokeText = jokeText, Code = code };

            await _database.InsertAsync(newJoke);
            _jokes.Add(newJoke);
        }

        public MainViewModel()
	{

		Joke = new ObservableCollection<Joke>();
	}
        private async Task<string> FetchJokeAsync()
        {
            using (var client = new HttpClient())
            {
                var responce = await client.GetStringAsync("https://api.chucknorris.io/jokes/random");
                var jokeObject = JsonConvert.DeserializeObject<JokeResponse>(responce);
                return jokeObject.Value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
	protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
  }
}