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
using ControlTower01.DAL;

namespace ControlTower01
{
    /// <summary>
    /// Logika interakcji dla klasy PanelAdminaWindow.xaml
    /// </summary>
    public partial class PanelAdminaWindow : Window
    {
        bool btEdit = false;
        bool btAdd = false;
        bool btDelet = false;

        MainWindow mainWindow = null;
        public PanelAdminaWindow(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            InitializeComponent();
            Wyswietl();

        }
        private void Wyswietl()
        {
            using (var context = new ControlTowerContext())
            {
                var query =
                    from products in context.AirCrafts
                    orderby products.AirCraftId
                    select products;
                datagridSamoloty.ItemsSource = query.ToList();
            };
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            btEdit = false;
            btAdd = true;
            btDelet = false;

            btnDodaj.Background = Brushes.Green;
            btnEdytuj.Background = Brushes.White;
            btnUsun.Background = Brushes.White;
        }

        private void btnEdytuj_Click(object sender, RoutedEventArgs e)
        {
            btEdit = true;
            btAdd = false;
            btDelet = false;

            btnDodaj.Background = Brushes.White;
            btnEdytuj.Background = Brushes.Green;
            btnUsun.Background = Brushes.White;
        }

        private void btnUsun_Click(object sender, RoutedEventArgs e)
        {
            btEdit = false;
            btAdd = false;
            btDelet = true;
            btnDodaj.Background = Brushes.White;
            btnEdytuj.Background = Brushes.White;
            btnUsun.Background = Brushes.Green;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (btEdit == true)
            {
                using (var context = new ControlTowerContext())
                {
                    int id = Int32.Parse(txtboxId.Text);
                    string model = txtboxModel.Text;
                    string linia = txtboxLinia.Text;

                    var query =
                        from products in context.AirCrafts
                        where products.AirCraftId == id
                        select products;
                    foreach (Models.AirCraft prod in query)
                    {
                        prod.NameAirLine = linia;
                        prod.NameModel = model;
                        //prod.Nazwa = nazwa;
                    };
                    context.SaveChanges();
                    Wyswietl();
                }
            }
            else if (btAdd == true)
            {
                using (var context = new ControlTowerContext())
                {
                    string model = txtboxModel.Text;
                    string linia = txtboxLinia.Text;
                    context.AirCrafts.Add(new Models.AirCraft() { NameModel = model, NameAirLine = linia });
                    context.SaveChanges();
                };
                Wyswietl();
            }
            else if (btDelet == true)
            {
                using (var context = new ControlTowerContext())
                {
                    int id = Int32.Parse(txtboxId.Text);
                    //string model = txtboxModel.Text;
                    //string linia = txtboxLinia.Text;

                    var query =
                        from products in context.AirCrafts
                        where products.AirCraftId == id
                        select products;
                    foreach (var proc in query)
                    {
                        context.AirCrafts.Remove(proc);
                        //prod.Nazwa = nazwa;
                    };
                    context.SaveChanges();
                    Wyswietl();
                }
            }
        }
    }
}
