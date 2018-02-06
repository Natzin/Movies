/*TASK Register Page*/
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
    public partial class RegisterPage : ContentPage
    {

        //-------------------------------------------------------------------------------------------------------------
        public RegisterPage()
        {
            InitializeComponent();

            this.Title = "Registro";

            //                                              //Validacion de entradas vacias
            this.enName.TextChanged += TextChanged;
            this.enUser.TextChanged += TextChanged;
            this.enTelphone.TextChanged += TextChanged;
            this.enMail.TextChanged += TextChanged;
            this.enPass.TextChanged += TextChanged;
            this.enConfirm.TextChanged += TextChanged;

            this.btnRegister.Clicked += BtnRegister_Clicked;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        private void BtnRegister_Clicked(object sender, EventArgs e)
        {
            if (
                //                                          //Validamos que el password coincida
                this.enPass.Text == this.enConfirm.Text
                )
            {
                //                                          //Asignamos los datos a la variable publica que se enviara
                //                                          //      al webservice
                UserData Registo = new UserData();
                Registo.username = this.enUser.Text;
                Registo.name = this.enName.Text;
                Registo.phone = this.enTelphone.Text;
                Registo.mail = this.enMail.Text;
                Registo.password = this.enPass.Text;

                String Token;
                //                                          //Metodo de carga de datos en webservice
                NetworkConect.Register(Registo, out Token);

                if (
                    Token != String.Empty
                    )
                {
                    DisplayAlert("Registro", "Registro exitoso, ya puedes iniciar sesion", "Accept");

                    //                                      //Retornamos al login de manera automatica
                    this.Navigation.PopAsync(true);
                }
                else
                {
                    DisplayAlert("Alert", "Usuario ya existente", "Accept");

                    this.Clean();
                }
            }
            else
            {
                DisplayAlert("Password Confirm", "El Password no coincide", "Accept");
                this.enConfirm.Text = String.Empty;
            }
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.Clean();
        }
        
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (
                //                                          //Validamos si todas las entradas tienen datos
                (this.enPass.Text != String.Empty) &&
                (this.enName.Text != String.Empty) &&
                (this.enUser.Text != String.Empty) &&
                (this.enMail.Text != String.Empty) &&
                (this.enTelphone.Text != String.Empty)&&
                (this.enConfirm.Text != String.Empty)
                )
            {
                this.btnRegister.IsEnabled = true;
            }
            else
            {
                this.btnRegister.IsEnabled = false;
            }
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public void Clean(
            //                                              //Limpia las entradas del registro
            )
        {
            this.btnRegister.IsEnabled = false;

            this.enPass.Text = String.Empty;
            this.enName.Text = String.Empty;
            this.enUser.Text = String.Empty;
            this.enMail.Text = String.Empty;
            this.enTelphone.Text = String.Empty;
            this.enConfirm.Text = String.Empty;
        }

        //-------------------------------------------------------------------------------------------------------------
    }
    //=================================================================================================================
}
/*END-TASK*/