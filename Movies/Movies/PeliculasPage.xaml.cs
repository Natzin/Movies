/*TASK Peliculas Page*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace Movies
{
    //=================================================================================================================

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PeliculasPage : ContentPage
    {
        //-------------------------------------------------------------------------------------------------------------
        //                                                  //Variable de retorno de token en login
        public String Access
        {
            get;
            set;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public PeliculasPage()
        {
            InitializeComponent();

            this.Title = "Peliculas";

            //                                              //Eliminamos boton de retroceso
            NavigationPage.SetHasBackButton(this, false);
            this.lvMovies.ItemTapped += LvMovies_ItemTapped;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        protected override void OnAppearing()
        {
            base.OnAppearing();

            var ListaPeliculas = new ObservableCollection<Movie>();

            NetworkConect ncMovieList = new NetworkConect();

            //                                              //Enviamos el token obtenido en el login para iniciar el
            //                                              //      webservice en NetworkConect
            ncMovieList.Access = this.Access;
            ncMovieList.MoviesList((Movies) =>
            {
                //                                          //Asignamos los datos obtenidos del webservice a la
                //                                          //      variable de collecion
                foreach (Movie Peli in Movies)
                {
                    ListaPeliculas.Add(Peli);
                }
            },
            () =>
            {
                DisplayAlert("Error", "Ocurrió un error al consumir el servicio Web", "Ok");
            });

            //                                              //Mostramos los datos obtenidos en la lista
            this.lvMovies.ItemsSource = ListaPeliculas;
            this.lvMovies.RowHeight = 75;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        private void LvMovies_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //                                              //Convertimos el elemento que se selecciono en un objeto
            //                                              //      Movie para enviarlo al crear la pagina
            Movie movPelicula = (Movie)e.Item;

            DatailPage dtpDatelles = new DatailPage();
            dtpDatelles.movDatos = movPelicula;
            Navigation.PushAsync(dtpDatelles, true);
        }

        //-------------------------------------------------------------------------------------------------------------
    }

    //=================================================================================================================
}
/*END-TASK*/