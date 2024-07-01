using System;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;
using SQLite;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Microsoft.Maui.Controls;   

namespace PruebaApp
{
    public partial class MainPage : ContentPage
    {
        private SQLiteAsyncConnection _database;
        private ObservableCollection<Joke> _jokes;


        int count = 0;

        public MainPage()
        {
            InitializeComponent();

            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "jokes.db");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Joke>().Wait();

            _jokes = new ObservableCollection<Joke>();
            JokeListView.ItemsSource = _jokes;

        }

        private async void OnGetJokeClicked(object sender, EventArgs e)
        {
            var jokeText = await FetchJokeAsync();
            var random = new Random();
            var code = "ML" + random.Next(1000, 2000);
            var newJoke = new Joke { JokeText = jokeText, Code = code };

            await _database.InsertAsync(newJoke);
            _jokes.Add(newJoke);
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
    }
    public class Joke
    {
        public int ID { get; set; }
        public string JokeText { get; set; }
        public string Code { get; set; }
    }
    public class JokeResponse
    {
        public string Value { get; set; }
    }
}

    


