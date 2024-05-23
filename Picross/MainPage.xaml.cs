
using Microsoft.Maui.ApplicationModel.DataTransfer;

namespace Picross
{
    public partial class MainPage : ContentPage
    {
        bool isDragging = false;
        HashSet<Button> buttons = new HashSet<Button>();
        HashSet<Button> completed = new HashSet<Button>();
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
                    pgr.PointerPressed += OnDragOver;
                    pgr.PointerReleased += DragRelease;
                    button.GestureRecognizers.Add(pgr);
                    button.Clicked += ButtonClicked;
                    gameBoard.Add(button, i, j);
                }
            }
        }

        // TODO:
        // Add list of possible row layouts
        // Start new game by adding 10 rows and setting correct grid
        // Check if selected squares are labeled correctly
        // Add incorrect image to put over buttons
        // Check Game Completed
        // Add gif for completed correct game

        private void ButtonClicked(object sender, EventArgs e)
        {
            if (isDragging)
            {
                foreach (Button button in buttons)
                {
                    button.BackgroundColor = Color.FromArgb("#FFFFFF");
                    button.BorderColor = Color.FromArgb("#000000");
                }
                buttons.Clear();
                isDragging = false;
            }
            else
            {
                Button button = (Button)sender;
                if (!completed.Contains(button))
                {
                    button.BackgroundColor = Color.FromArgb("#DDD");
                    button.BorderColor = Color.FromArgb("#000");
                    completed.Add(button);
                }
            }
        }

        private void OnDragOver(object sender, PointerEventArgs e)
        {
            isDragging = true;
            buttons.Add((Button)sender);
        }

        private void DragRelease(object sender, PointerEventArgs e)
        {
            foreach(Button button in buttons)
            {
                button.BackgroundColor = Color.FromArgb("#92def7");
                button.BorderColor = Color.FromArgb("#01A9DB");
                completed.Add(button);
            }
            isDragging = false;
            buttons.Clear();
        }

        private void OnPointerEntered(object sender, PointerEventArgs e)
        {
            if (!completed.Contains((Button)sender))
            {
                Button button = (Button)sender;
                button.BackgroundColor = Color.FromArgb("#F7D358");
                button.BorderColor = Color.FromArgb("#FF8000");
                if (isDragging)
                {
                    buttons.Add(button);
                }
            }
        }

        private void OnPointerExited(object sender, PointerEventArgs e)
        {
            if (!isDragging && !completed.Contains((Button)sender))
            {
                Button button = (Button)sender;
                button.BackgroundColor = Color.FromArgb("#FFFFFF");
                button.BorderColor = Color.FromArgb("#000000");
            }
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
