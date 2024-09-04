namespace Module02Exercise01.View;

public partial class StartPage : ContentPage
{
    public StartPage()
    {
        InitializeComponent();
    }
    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;

        if (username == "student" && password == "123")
        {
            (Application.Current as App)?.GoOnline();
        }
        else
        {
            await DisplayAlert("Login Failed", "Wrong Credentials", "OK");
        }
    }

}