namespace Picross
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnPointerEntered(object sender, PointerEventArgs e)
        {
            Button button = (Button)sender;
            button.BackgroundColor = Color.FromArgb("#F7D358");
            button.BorderColor = Color.FromArgb("#FF8000");
        }

        private void OnPointerExited(object sender, PointerEventArgs e)
        {
            Button button = (Button)sender;
            button.BackgroundColor = Color.FromArgb("#FFFFFF");
            button.BorderColor = Color.FromArgb("#000000");
        }

        private void GameButtonEntered(object sender, PointerEventArgs e)
        {
            Button button = (Button)sender;
            button.BackgroundColor = Color.FromArgb("#92def7");
            button.BorderColor = Color.FromArgb("#01A9DB");
        }

        private void GameButtonExited(object sender, PointerEventArgs e)
        {
            Button button = (Button)sender;
            button.BackgroundColor = Color.FromArgb("#FFFFFF");
            button.BorderColor = Color.FromArgb("#000000");
        }
    }
}
