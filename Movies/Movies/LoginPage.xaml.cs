/*TASK Login Page*/
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
    public partial class LoginPage : ContentPage
    {
        //-------------------------------------------------------------------------------------------------------------

        public LoginPage()
        {
            InitializeComponent();

            this.Title = "Login";

            //                                              //Validacion de entradas vacios en el login
            this.enUser.TextChanged += TextChanged;
            this.enPass.TextChanged += TextChanged;

            this.btnLogin.Clicked += BtnLogin_Clicked;
            this.btnRegister.Clicked += BtnRegister_Clicked;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        private void BtnRegister_Clicked(
            object sender,
            EventArgs e
            )
        {
            this.Navigation.PushAsync(new RegisterPage(), true);
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        protected override void OnAppearing()
        {
            base.OnAppearing();

            //                                              //Se deshabilitan botones an inicio aparecer el formulario
            //                                              //      para evitar que el usuario envie textos vacios
            this.btnLogin.IsEnabled = false;

            this.enPass.Text = String.Empty;
            this.enUser.Text = String.Empty;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        private void TextChanged(
            object sender,
            TextChangedEventArgs e
            )
        {
            if (
                //                                          //Evitamos que se envien entradas vacias
                this.enUser.Text != String.Empty &&
                this.enPass.Text != String.Empty
                )
            {
                this.btnLogin.IsEnabled = true;
            }
            else
            {
                this.btnLogin.IsEnabled = false;
            }
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        private void BtnLogin_Clicked(
            object sender,
            EventArgs e
            )
        {
            String Token;
            String strUser = this.enUser.Text;
            String strPass = this.enPass.Text;
            NetworkConect.Login(strUser, strPass, out Token);

            //                                              //Validamos que el nombre de usuario y contraseña sean
            //                                              //      correctas
            if (
                Token != String.Empty
                )
            {
                this.enUser.Text = String.Empty;
                this.enPass.Text = String.Empty;

                PeliculasPage peliculasPage = new PeliculasPage();
                //                                          //Enviamos el token de acceso obtenido para la conexion
                peliculasPage.Access = Token;
                //                                          //Pasamos a DataPage
                this.Navigation.PushAsync(peliculasPage, true);
            }
            else
            {
                DisplayAlert("Alert", "Usuario y/o contraseña incorrecta", "Accept");

                enUser.Text = String.Empty;
                enPass.Text = String.Empty;

                //                                          //Se dehabilita el btnLogin y el enPass para evitar el
                //                                          //      envio de vacios en caso de error
                this.btnLogin.IsEnabled = false;
            }
        }

        //-------------------------------------------------------------------------------------------------------------
    }
    //=================================================================================================================
}
/*END-TASK*/
