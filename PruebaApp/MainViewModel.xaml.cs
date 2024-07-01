using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PruebaApp
{
public partial class MainViewModel : INotifyPropertyChanged
{
	public ObservableCollection<Joke> Joke { get; set; }
	public MainViewModel()
	{
		Joke = new ObservableCollection<Joke>();
	}

	public event PropertyChangedEventHandler PropertyChanged;
	protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
  }
}