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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace Kursach
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            listbox1.Visibility = Visibility.Hidden;
            label1.Visibility = Visibility.Hidden;
            labelerror.Visibility = Visibility.Hidden;

        }

        private void StackPanel_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            string InfoFromList = Convert.ToString(listbox1.SelectedItem);
            string[] ArrayIFI = InfoFromList.Split('.');
            if (ArrayIFI[0] == "")
            {
                labelerror.Visibility = Visibility.Visible;
            }
            else
            {
                labelerror.Visibility = Visibility.Hidden;
                string InfoFromBD = WorkBD.FindItem(Convert.ToInt32(ArrayIFI[0]));
                string[] IFBD = InfoFromBD.Split('|');
                string KolGenres =  WorkBD.OutputAllGenres();
                string[] KG = KolGenres.Split('|');
                string KolDev = WorkBD.OutputAllDevelopers();
                string[] KL = KolDev.Split('|');
                string KolGame = WorkBD.OutputALlGames();
                string[] CK = KolGame.Split('$');
                string LastGame = WorkBD.FindItem(KL.Length-1);
                string[] LG = LastGame.Split('|');
                COMFormatter com = new COMFormatter(@"D:\shablon.doc");
                com.Replace("[Name]", IFBD[0]); //подставляем название игры
                com.Replace("[Link]", IFBD[1]); //ссылку
                com.Replace("[Ganre]", IFBD[2]); //жанры
                com.Replace("[Dev]", IFBD[3]); //разработчиков
                com.Replace("Allgame", Convert.ToString(CK.Length - 1)); //кол-во игр
                com.Replace("Allgenre", Convert.ToString(KG.Length - 1));//кол-во жанров
                com.Replace("Alldev", Convert.ToString(KL.Length - 1));//кол-во разработчиков
                com.Replace("Lastgame", LG[0]);
                com.CreateTable();
                com.Close();
            }
        }

        public delegate void InvokeDelegate();
        Thread thread;
        private void StackPanel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            label1.Visibility = Visibility.Visible;
            listbox1.Visibility = Visibility.Hidden;
            thread = new Thread(new ThreadStart(delegate () { Parse.ParseandSave(); }));
            thread.Start();
        }

        private void StackPanel_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            listbox1.Items.Clear();
            label1.Visibility = Visibility.Hidden;
            listbox1.Visibility = Visibility.Visible;
            if (thread != null)
                thread.Abort();
            using (GameContext db = new GameContext())
            {
                var games = db.Games;
                foreach (Game u in games)
                {
                    string text = u.Id + ". " + u.Name;
                    listbox1.Items.Add(text);
                }
            }
        }


        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string AllGenres = WorkBD.OutputAllGenres();
            string[] AD = AllGenres.Split('|');
            string GameFromGenre = WorkBD.GenresFromGame();
            string[] GFG = GameFromGenre.Split(',');
            int[] ArrayY = new int[GFG.Length];
            string[] ArrayX = new string[AD.Length];
            List<int> AXY = new List<int>();
            List<string> AXX = new List<string>();
            for (int i = 0; i < AD.Length - 1; i++)
            {
                ArrayY[i] = Convert.ToInt32(GFG[i]);
                ArrayX[i] = AD[i];
                AXY.Add(Convert.ToInt32(GFG[i]));
                AXX.Add(AD[i]);
            }
            ExcelWork.ExcelGraph(AXX, AXY);
        }


        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            //label1.Visibility = Visibility.Hidden;
        }

        private void StackPanel_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            BDView bdw = new BDView();
            bdw.Show();
        }
        private void LV_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == true)
            {
                tt_parse.Visibility = Visibility.Collapsed;
                tt_setting.Visibility = Visibility.Collapsed;
                tt_excel.Visibility = Visibility.Collapsed;
                tt_word.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_parse.Visibility = Visibility.Visible;
                tt_setting.Visibility = Visibility.Visible;
                tt_excel.Visibility = Visibility.Visible;
                tt_word.Visibility = Visibility.Visible;
            }
        }

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {
            img_bg.Opacity = 1;
        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {
            img_bg.Opacity = 0.3;
        }

        private void bg_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

       
    }
}
