using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace statki
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] start= null;
        string button1Tag = null;
        string button2Tag = null;
        List<Ships> shipsList = new List<Ships>()
        {
            new Ships(){count=1, length = 4},
            new Ships(){count=2, length = 3},
            new Ships(){count=3, length = 2},
            new Ships(){count=4, length = 1},
        };
        List<Ships> shipsInGame = new List<Ships>();
        void howMuchShips()
        {
            
            countOfShips.Content = $"Liczba statków:\n";
            foreach (var ship in shipsList)
            {
                countOfShips.Content += $"Długość: {ship.length} liczba: {ship.count}\n";
            }
            if(!shipsList.Any(x => x.count > 0))
            {
                Button btn = new Button() {Content = "Rozpocznij grę", Height = 100 };
                panel.Children.Clear();
                panel.Children.Add(btn);
            }
        }
        void checkOthers(string pos)
        {
            if(shipsInGame.Any(x => x.posittionA == pos))
            {
                MessageBox.Show("Nie");
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            howMuchShips();
            for(int i =0; i<10; i++)
            {
                for(int j=0; j<10; j++)
                {
                    var btn = new Button() {Margin = new Thickness(3), Tag = i+","+j, Background = Brushes.Transparent};
                    btn.Click += Btn_Click;
                    grid1.Children.Add(btn);
                    Grid.SetRow(btn, i);
                    Grid.SetColumn(btn, j);
                }
            }
        }
        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button1Tag != null)
                button2Tag = button.Tag.ToString();
            else
                button1Tag = button.Tag.ToString();
            string[] values = button.Tag.ToString().Split(",");
            void reset()
            {
                grid1.Children.Cast<Button>().First(x => x.Tag.ToString() == values[0] + "," + values[1]).Background = Brushes.Transparent;
                grid1.Children.Cast<Button>().First(x => x.Tag.ToString() == start[0] + "," + start[1]).Background = Brushes.Transparent;
                button1Tag = null;
                button2Tag = null;
            }
            if(start is null)
            {
                button.Background = Brushes.Green;
                start = values;
            }
            else
            {
                button.Background = Brushes.Red;
                if (values[0] != start[0] && values[1] == start[1])
                {
                    int a = Convert.ToInt32(values[0]);
                    int b = Convert.ToInt32(start[0]);

                    int count=0;
                    try
                    {
                        count = shipsList.First(x => x.length == (a - b + 1)).count;
                    }
                    catch
                    {
                        reset();
                    }
                    if (count > 0)
                    {
                        if (a < b && b - a < 4)
                        {
                            
                            shipsList.First(x => x.length == (a - b + 1)).count--;
                            while (a != b + 1)
                            {
                                if(shipsInGame.Any(x=>x.posittionA == a.ToString() + "," + values[1]  || x.posittionB == a.ToString() + "," + values[1]))
                                {
                                    reset();
                                }
                                    
                                grid1.Children.Cast<Button>().First(x => x.Tag.ToString() == a.ToString() + "," + values[1]).Background = Brushes.Blue;
                                a++;
                            }
                            shipsInGame.Add(new Ships() { posittionA=button1Tag, posittionB=button2Tag });
                            button1Tag = null;
                            button2Tag = null;
                            
                        }
                        else if (a > b && a - b < 4)
                        {
                            
                            shipsList.First(x => x.length == (a - b + 1)).count--;
                            while (b != a + 1)
                            {
                                if (shipsInGame.Any(x => x.posittionA == b.ToString() + "," + values[1] || x.posittionB == b.ToString() + "," + values[1]))
                                {
                                    reset();
                                }
                                grid1.Children.Cast<Button>().First(x => x.Tag.ToString() == b.ToString() + "," + values[1]).Background = Brushes.Blue;
                                b++;
                            }
                            shipsInGame.Add(new Ships() { posittionA = button1Tag, posittionB = button2Tag });
                            button1Tag = null;
                            button2Tag = null;
                            
                        }
                        else
                            reset();
                    }
                    else
                        reset();
                }
                else if (values[0] == start[0] && values[1] != start[1])
                {

                    int a = Convert.ToInt32(values[1]);
                    int b = Convert.ToInt32(start[1]);
                    int count = shipsList.First(x => x.length == (a - b + 1)).count;
                    if (count > 0)
                    {
                        if (a < b && b - a < 4)
                        {
                            
                            shipsList.First(x => x.length == (a - b + 1)).count--;
                            while (a != b + 1)
                            {
                                if (shipsInGame.Any(x => x.posittionA == a.ToString() + "," + values[1] || x.posittionB == a.ToString() + "," + values[1]))
                                {
                                    reset();
                                }
                                grid1.Children.Cast<Button>().First(x => x.Tag.ToString() == values[0] + "," + a.ToString()).Background = Brushes.Blue;
                                a++;
                            }
                            shipsInGame.Add(new Ships() { posittionA = button1Tag, posittionB = button2Tag });
                            button1Tag = null;
                            button2Tag = null;
                        }

                        else if (a > b && a - b < 4)
                        {
                            
                            shipsList.First(x => x.length == (a - b + 1)).count--;
                            while (b != a + 1)
                            {
                                if (shipsInGame.Any(x => x.posittionA == b.ToString() + "," + values[1] || x.posittionB == b.ToString() + "," + values[1]))
                                {
                                    reset();
                                }
                                grid1.Children.Cast<Button>().First(x => x.Tag.ToString() == values[0] + "," + b.ToString()).Background = Brushes.Blue;
                                b++;
                            }
                            shipsInGame.Add(new Ships() { posittionA = button1Tag, posittionB = button2Tag });
                            button1Tag = null;
                            button2Tag = null;
                        }
                        else
                            reset();
                    }
                    else
                        reset();
                }
                else if (values[0] == start[0] && values[1] == start[1] && shipsList.First(x => x.length == 1).count > 0)
                {
                    
                    shipsInGame.Add(new Ships() { posittionA = button1Tag, posittionB = button2Tag });
                    button1Tag = null;
                    button2Tag = null;
                    shipsList.First(x => x.length == 1).count--;

                    grid1.Children.Cast<Button>().First(x => x.Tag.ToString() == values[0] + "," + values[1]).Background = Brushes.Blue;
                    
                }
                else
                    reset();
                start = null;
            }
            howMuchShips();
        }
    }
}
