

namespace Picross
{
    public partial class MainPage : ContentPage
    {
        bool isDragging = false;
        int mistakes = 0;
        HashSet<Button> buttons = new HashSet<Button>();
        HashSet<Button> completed = new HashSet<Button>();
        Random rand = new Random();
        List<List<bool>> grid = new List<List<bool>>();
        List<Label> rows = new List<Label>();
        List<Label> cols = new List<Label>();
        public MainPage()
        {
            InitializeComponent();
            setGrid();
            for (int i = 1; i < 11; i++)
            {
                Label label = new Label();
                int count = 0;
                for (int j = 0; j < 10; j++)
                {
                    if (grid[j][i - 1])
                    {
                        count++;
                    }
                    else
                    {
                        if (count != 0)
                        {
                            label.Text += count.ToString() + " ";
                            count = 0;
                        }
                    }
                }
                if (count != 0)
                {
                    label.Text += count.ToString() + " ";
                }
                if (label.Text != null)
                {
                    label.Text.TrimEnd();
                }
                label.HorizontalTextAlignment = TextAlignment.End;
                label.VerticalTextAlignment = TextAlignment.Center;
                label.Margin = new Thickness(0, 0, 10, 0);
                label.FontSize = 15;
                label.CharacterSpacing = 3;
                gameBoard.Add(label, 0, i);
                rows.Add(label);

                label = new Label();
                count = 0;
                for (int j = 0; j < 10; j++)
                {
                    if (grid[i-1][j])
                    {
                        count++;
                    }
                    else
                    {
                        if (count != 0)
                        {
                            label.Text += count.ToString() + " ";
                            count = 0;
                        }
                    }
                }
                if (count != 0)
                {
                    label.Text += count.ToString() + " ";
                }
                if (label.Text != null)
                {
                    label.Text.TrimEnd();
                }
                label.HorizontalTextAlignment = TextAlignment.Center;
                label.VerticalTextAlignment = TextAlignment.End;
                label.WidthRequest = 20;
                label.Margin = new Thickness(0, 0, 0, 10);
                label.FontSize = 15;
                label.CharacterSpacing = 3;
                gameBoard.Add(label, i, 0);
                cols.Add(label);

                for(int j = 1; j < 11; j++)
                {
                    Button button = new Button();
                    button.BackgroundColor = Color.FromArgb("#FFFFFF");
                    button.BorderColor = Color.FromArgb("#000000");
                    button.BorderWidth = 1;
                    button.CornerRadius = 0;
                    button.StyleId = (i-1).ToString() + (j-1).ToString();
                    button.Text = "";
                    button.TextColor = Colors.Red;
                    button.FontSize = 60;
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
        // Check Game Completed
        // Add gif for completed correct game

        private void resetGrid(object sender, EventArgs e)
        {
            setGrid();
            buttons.Clear();
            mistakes = 0;
            mistakesCounter.Text = "0";
            progressPercent.Text = "0.0%";
            for (int i = 0; i < 10; i++)
            {
                Label label = rows[i];
                label.Text = "";
                int count = 0;
                for (int j = 0; j < 10; j++)
                {
                    if (grid[j][i])
                    {
                        count++;
                    }
                    else
                    {
                        if (count != 0)
                        {
                            label.Text += count.ToString() + " ";
                            count = 0;
                        }
                    }
                }
                if(count != 0)
                {
                    label.Text += count.ToString() + " ";
                }
                if (label.Text != null)
                {
                    label.Text.TrimEnd();
                }
                label = cols[i];
                label.Text = "";
                count = 0;
                for (int j = 0; j < 10; j++)
                {
                    if (grid[i][j])
                    {
                        count++;
                    }
                    else
                    {
                        if (count != 0)
                        {
                            label.Text += count.ToString() + " ";
                            count = 0;
                        }
                    }
                }
                if (count != 0)
                {
                    label.Text += count.ToString() + " ";
                }
                if (label.Text != null)
                {
                    label.Text.TrimEnd();
                }
            }
            foreach(Button button in completed)
            {
                button.BackgroundColor = Color.FromArgb("#FFFFFF");
                button.BorderColor = Color.FromArgb("#000000");
                button.Text = "";
            }
            completed.Clear();
        }

        private void setGrid()
        {
            grid.Clear();
            for (int i = 0; i < 10; i++)
            {
                List<bool> row = new List<bool>();
                for (int j = 0; j < 10; j++)
                {
                    row.Add(rand.Next(2) == 1);
                }
                grid.Add(row);
            }
        }

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
                    int row = int.Parse(button.StyleId[0].ToString());
                    int col = int.Parse(button.StyleId[1].ToString());
                    if (grid[row][col])
                    {
                        button.Text = "x";
                        button.BackgroundColor = Color.FromArgb("#92def7");
                        button.BorderColor = Color.FromArgb("#01A9DB");
                        mistakes++;
                        mistakesCounter.Text = mistakes.ToString();
                    }
                    else
                    {
                        button.BackgroundColor = Color.FromArgb("#DDD");
                        button.BorderColor = Color.FromArgb("#000");
                    }
                    completed.Add(button);
                    progressPercent.Text = (completed.Count()).ToString() + ".0%";
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
                int row = int.Parse(button.StyleId[0].ToString());
                int col = int.Parse(button.StyleId[1].ToString());
                if (grid[row][col])
                {
                    button.BackgroundColor = Color.FromArgb("#92def7");
                    button.BorderColor = Color.FromArgb("#01A9DB");
                }
                else
                {
                    button.Text = "x";
                    button.BackgroundColor = Color.FromArgb("#DDD");
                    button.BorderColor = Color.FromArgb("#000");
                    mistakes++;
                    mistakesCounter.Text = mistakes.ToString();
                }
                completed.Add(button);
                progressPercent.Text = (completed.Count()).ToString() + ".0%";
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
