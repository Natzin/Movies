/*TASK Detail Page*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Movies
{
    //=================================================================================================================

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DatailPage : ContentPage
    {

        //-------------------------------------------------------------------------------------------------------------
        //                                                  //Variable que contiene de datos de la Movie a mostrar en
        //                                                  //      la page
        public Movie movDatos
        {
            get;
            set;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public DatailPage(
            )
        {
            InitializeComponent();
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        protected override void OnAppearing(
            )
        {
            base.OnAppearing();

            this.Title = movDatos.title;
            this.lbCategory.Text = lbCategory.Text + this.movDatos.category;
            this.lbDescription.Text = lbDescription.Text + this.movDatos.description;
            this.lbRanking.Text = lbRanking.Text + this.movDatos.rating;
            this.imImage.Source = this.movDatos.image;
        }

        //-------------------------------------------------------------------------------------------------------------
    }

    //=================================================================================================================
}
/*END-TASK*/
