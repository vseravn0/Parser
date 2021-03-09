using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Kursach
{
    /// <summary>
    /// Логика взаимодействия для BDView.xaml
    /// </summary>
    public partial class BDView : Window
    {
        GameContext db = new GameContext();
        public BDView()
        {
            
            InitializeComponent();
            gameGrid.ItemsSource = db.Games.ToList();
 
        }

        private void bsave_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }

        private void bxz_Click(object sender, RoutedEventArgs e)
        {
            db.Games.Add(new Game());
            db.SaveChanges();
            gameGrid.ItemsSource = db.Games.ToList();
        }

        private void bupdate_Click(object sender, RoutedEventArgs e)
        {
            gameGrid.ItemsSource = db.Games.ToList();
        }

        private void bdelete_Click(object sender, RoutedEventArgs e)
        {
            var b = gameGrid.SelectedItem;
            Game game = (Game)b;
            var ga = game.Id;
            var deletegame = db.Games.Find(ga);
            db.Games.Remove(deletegame);
            db.SaveChanges();
            gameGrid.ItemsSource = db.Games.ToList();
        }
    }
}
