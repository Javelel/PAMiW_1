using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P06Shop.Shared.MovieRental
{
	public class Movie : INotifyPropertyChanged
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public int Year { get; set; }
		public string Director { get; set; }
		public int Rating { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
	}
}
