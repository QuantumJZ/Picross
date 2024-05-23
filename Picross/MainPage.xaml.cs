
namespace Picross
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            for (int i = 1; i < 11; i++)
            {
                Label label = new Label();
                label.Text = "1 1 1 1 2";
                label.HorizontalTextAlignment = TextAlignment.End;
                label.VerticalTextAlignment = TextAlignment.Center;
                label.Margin = new Thickness(0, 0, 10, 0);
                label.FontSize = 15;
                label.CharacterSpacing = 3;
                gameBoard.Add(label, 0, i);

                label = new Label();
                label.Text = "1 1 1 1 2";
                label.HorizontalTextAlignment = TextAlignment.Center;
                label.VerticalTextAlignment = TextAlignment.End;
                label.WidthRequest = 20;
                label.Margin = new Thickness(0, 0, 0, 10);
                label.FontSize = 15;
                gameBoard.Add(label, i, 0);

                for(int j = 1; j < 11; j++)
                {
                    Button button = new Button();
                    button.BackgroundColor = Color.FromArgb("#FFFFFF");
                    button.BorderColor = Color.FromArgb("#000000");
                    button.BorderWidth = 1;
                    button.CornerRadius = 0;
                    PointerGestureRecognizer pgr = new PointerGestureRecognizer();
                    pgr.PointerEntered += OnPointerEntered;
                    pgr.PointerExited += OnPointerExited;
                    button.GestureRecognizers.Add(pgr);
                    gameBoard.Add(button, i, j);
                }
            }
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
