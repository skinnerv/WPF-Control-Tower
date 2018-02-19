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
using ControlTower01.DAL;
using ControlTower01.Models;
using System.Diagnostics;
using System.Data.Entity;
using ControlTower01.Pages;
using System.Collections.Concurrent;

namespace ControlTower01
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static List<SimulationAirCraft> LSimAirCraft;
        static List<Task<int>> TasksAirCrafts;
        ControlTowerContext db = new ControlTowerContext();
        public ConcurrentBag<TablicaPrzylotow> concurrentBagTablicaPrzylotow = new ConcurrentBag<TablicaPrzylotow>();
        public ConcurrentBag<TablicaPrzylotow> concurrentBagTablicaPrzylotowRemove = new ConcurrentBag<TablicaPrzylotow>();

        public ConcurrentBag<TablicaPrzylotow> concurrentBagTablicaOdlotow = new ConcurrentBag<TablicaPrzylotow>();
        public ConcurrentBag<string> concurrentBagTerminalAdd = new ConcurrentBag<string>();
        public ConcurrentBag<string> concurrentBagTerminalRemove = new ConcurrentBag<string>();

        public List<TablicaPrzylotow> listaPrzylotowAdd = new List<TablicaPrzylotow>();
        public List<TablicaPrzylotow> listaPrzylotowRemove = new List<TablicaPrzylotow>();

        public List<TablicaPrzylotow> listaOdlotow = new List<TablicaPrzylotow>();
        public List<TablicaPrzylotow> listaOdlotowRemove = new List<TablicaPrzylotow>();

        TerminalTablica tt;
        Terminal terminal;

        Przyloty prz;

        public MainWindow()
        {
            InitializeComponent();
            tt = new TerminalTablica()
            {
                Gate1 = "----", Gate2 = "----", Gate3 = "----", Gate4 = "----", Gate5 = "----", Gate6 = "----", Gate7 = "----", Gate8 = "----", Gate9 = "----"
            };
            DataGridTerminal.Items.Add(tt);
            //DataGridTerminal.Items.Refresh();
            this.terminal = new Terminal(tt, this);

            prz = new Przyloty(this);

            //db.LoginUsers.Add(new LoginUser() {Name = "admin", Password = "admin" });
            //db.LoginUsers.Add(new LoginUser() {Name = "user", Password = ""});
            //db.SaveChanges();
            //this.Close();
            //Trace.WriteLine("Tworzenie bazy");
            //db.AirCrafts.Add(new AirCraft() { NameModel = "Embraer", NameAirLine = "PLL LOT", PassengerCapacity = 112 });
            //Trace.WriteLine("Dodano linię do bazy");
            //db.SaveChanges();

            //Tworzenie testowych danych do obrazowania wyświetlania
            //var listaPrzylotow = new List<TablicaPrzylotow>()
            //{
            //    new TablicaPrzylotow() { Rejs = "AB 01", Kierunek = "Warszawa", Linia = "PLL LOT", CzasRozkladu = TimeSpan.Parse("01:32:00"), Opoznienie = TimeSpan.Parse("00"), Uwagi = "--" },
            //    new TablicaPrzylotow() { Rejs = "AB 02", Kierunek = "Gdańsk", Linia = "PLL LOT", CzasRozkladu = TimeSpan.Parse("01:32:00"), Opoznienie = TimeSpan.Parse("00:20"), Uwagi = "--" },
            //    new TablicaPrzylotow() { Rejs = "AB 03", Kierunek = "Szczecin", Linia = "PLL LOT", CzasRozkladu = TimeSpan.Parse("01:32:00"), Opoznienie = TimeSpan.Parse("00"), Uwagi = "--" },
            //    new TablicaPrzylotow() { Rejs = "AB 04", Kierunek = "Bydgoszcz", Linia = "PLL LOT", CzasRozkladu = TimeSpan.Parse("01:32:00"), Opoznienie = TimeSpan.Parse("1:00"), Uwagi = "--" }
            //};
            //listaPrzylotow.ForEach(s => DataGridXAML1.Items.Add(s)); //dodanie danych do tablicy
        }
        public class TablicaPrzylotow // dataGridXAML1
        {
            public string Rejs { get; set; }
            public string Kierunek { get; set; }
            public string Linia { get; set; }
            public TimeSpan CzasRozkladu { get; set; }
            public TimeSpan Opoznienie { get; set; }
            public string Uwagi { get; set; }
        }
        //public class TerminalTablica //DataGridTerminal
        //{
        //    public string Gate1 { get; set; }
        //    public string Gate2 { get; set; }
        //    public string Gate3 { get; set; }
        //    public string Gate4 { get; set; }
        //    public string Gate5 { get; set; }
        //    public string Gate6 { get; set; }
        //    public string Gate7 { get; set; }
        //    public string Gate8 { get; set; }
        //    public string Gate9 { get; set; }
        //}
        //public class Terminal 
        //{
        //    //MiejsceWterminalu mwt = null;
        //    List<MiejsceWterminalu> miejsceWTerminalu = new List<MiejsceWterminalu>()
        //    {
        //        new MiejsceWterminalu (){NrGate = 1, Zajetosc = false, Miejsce = "----" },
        //        new MiejsceWterminalu (){NrGate = 2, Zajetosc = false, Miejsce = "----" },
        //        new MiejsceWterminalu (){NrGate = 3, Zajetosc = false, Miejsce = "----" },
        //        new MiejsceWterminalu (){NrGate = 4, Zajetosc = false, Miejsce = "----" },
        //        new MiejsceWterminalu (){NrGate = 5, Zajetosc = false, Miejsce = "----" },
        //        new MiejsceWterminalu (){NrGate = 6, Zajetosc = false, Miejsce = "----" },
        //        new MiejsceWterminalu (){NrGate = 7, Zajetosc = false, Miejsce = "----" },
        //        new MiejsceWterminalu (){NrGate = 8, Zajetosc = false, Miejsce = "----" },
        //        new MiejsceWterminalu (){NrGate = 9, Zajetosc = false, Miejsce = "----" },
        //    };

        //    TerminalTablica terminalTablica = null;
        //    MainWindow mainWindow = null;

        //    public Terminal(TerminalTablica terminalTablica, MainWindow mainWindow)
        //    {
        //        this.terminalTablica = terminalTablica;
        //        this.mainWindow = mainWindow;
        //    }

        //    public void DodajDOTerminala (string Rejs)
        //    {
        //        //Sprawdź następne wolne miejsce
        //        int wolnynumer = 0;
        //        foreach (var item in miejsceWTerminalu)
        //        {
        //            if (item.Zajetosc == false)
        //            {
        //                wolnynumer = item.NrGate;
        //                item.Zajetosc = true;
        //                item.Miejsce = Rejs;
        //                break;
        //            }
        //        }
        //        //Aktualizuj Tablicę terminalu
        //        if (wolnynumer == 1) { terminalTablica.Gate1 = Rejs; }
        //        else if (wolnynumer == 2) { terminalTablica.Gate2 = Rejs; }
        //        else if (wolnynumer == 3) { terminalTablica.Gate3 = Rejs; }
        //        else if (wolnynumer == 4) { terminalTablica.Gate4 = Rejs; }
        //        else if (wolnynumer == 5) { terminalTablica.Gate5 = Rejs; }
        //        else if (wolnynumer == 6) { terminalTablica.Gate6 = Rejs; }
        //        else if (wolnynumer == 7) { terminalTablica.Gate7 = Rejs; }
        //        else if (wolnynumer == 8) { terminalTablica.Gate8 = Rejs; }
        //        else  { terminalTablica.Gate9 = Rejs; }

        //        mainWindow.DataGridTerminal.Items.Clear();
        //        mainWindow.DataGridTerminal.Items.Add(terminalTablica);
        //    }
        //    public void UsunZterminala(string Rejs)
        //    {
        //        //Sprawdź miejsce do usuniecia
        //        int miejscedousuniecia = 0;
        //        foreach (var item in miejsceWTerminalu)
        //        {
        //            if (item.Miejsce == Rejs)
        //            {
        //                miejscedousuniecia = item.NrGate;
        //                item.Zajetosc = false;
        //                item.Miejsce = "----";
        //                break;
        //            }
        //        }
        //        string puste = "----";
        //        //Aktualizuj Tablicę terminalu
        //        if (miejscedousuniecia == 1) { terminalTablica.Gate1 = puste; }
        //        else if (miejscedousuniecia == 2) { terminalTablica.Gate2 = puste; }
        //        else if (miejscedousuniecia == 3) { terminalTablica.Gate3 = puste; }
        //        else if (miejscedousuniecia == 4) { terminalTablica.Gate4 = puste; }
        //        else if (miejscedousuniecia == 5) { terminalTablica.Gate5 = puste; }
        //        else if (miejscedousuniecia == 6) { terminalTablica.Gate6 = puste; }
        //        else if (miejscedousuniecia == 7) { terminalTablica.Gate7 = puste; }
        //        else if (miejscedousuniecia == 8) { terminalTablica.Gate8 = puste; }
        //        else { terminalTablica.Gate9 = puste; }

        //        mainWindow.DataGridTerminal.Items.Clear();
        //        mainWindow.DataGridTerminal.Items.Add(terminalTablica);
        //    }
        //}
        //public class MiejsceWterminalu
        //{
        //    public int NrGate { get; set; }
        //    public string Miejsce { get; set; }
        //    public bool Zajetosc { get; set; }
        //}

        private void DataGridXAML1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void StartControlTower_Clicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Uruchomiłeś symulację ruchu lotniska. Miłej zabawy ;)");
            //LoginPage login = new LoginPage();
            ///this.Content = login;
            ///STARTING SIMULATION
            StartSimulation();
        }

        private void StopControlTower_Clicked(object sender, RoutedEventArgs e)
        {
            //TasksAirCrafts.ForEach(t => t.());
        }
        /// <summary>
        /// Uruchamianie symulacji
        /// </summary>
        private void StartSimulation()
        {
            //Stworz liste obiektow do symulacji
            AddAirCraftToList();
            //Stworz listę taskow dla samolotow (lista osobnych wątków)
            AddListTaskAirCraftTask();
            //Odpal taski
            RunAllTask();
        }
        /// <summary>
        /// Pobranie Samolotow z bazy a następnie dodanie ich do listy aktywnych
        /// </summary>
        private void AddAirCraftToList()
        {
            int LSamWBazie = db.AirCrafts.Count();
            var query = from q in db.AirCrafts
                        orderby q.AirCraftId
                        select q;
            LSimAirCraft = new List<SimulationAirCraft>();
            //query.ForEach(q => LSimAirCraft.Add(new SimulationAirCraft(q.AirCraftId, q.NameModel, q.NameAirLine, q.PassengerCapacity)));
            foreach (var q in query)
            {
                LSimAirCraft.Add(new SimulationAirCraft(q.AirCraftId, q.NameModel, q.NameAirLine, q.PassengerCapacity, this));
                //break; //tymczasowe do testowania dodaje tylko jeden rekord
            }
            //podpięcie do zdarzenia AktualizacjaTablicyPrzylotow metody delegata dla zaktualizowania tablicy przylotow
            LSimAirCraft.ForEach(lsim => lsim.TablicaPrzylotowAktualizacjaEvent += new SimulationAirCraft.TablicaPrzylotowAktualizacjaHander(AtualizacjaTablicyPrzylotowGraber));
            LSimAirCraft.ForEach(lsim => lsim.TablicaPrzylotowAktualizacjaRemoveEvent += new SimulationAirCraft.TablicaPrzylotowAktualizacjaRemoveHander(AktualizacjaTablicyPrzylotowGraberRemove));

            LSimAirCraft.ForEach(lsim => lsim.TablicaOdlotowAktualizacjaEvent += new SimulationAirCraft.TablicaOdlotowAktualizacjaHander(AtualizacjaTablicyOdlotowGraber));
            LSimAirCraft.ForEach(lsim => lsim.AktualizujTerminalWejscieEvent += new SimulationAirCraft.AktualizujTerminalWejscieHander(AktualizacjaTerminalaAddGraber));
            LSimAirCraft.ForEach(lsim => lsim.AktualizujTerminalWyjscieEvent += new SimulationAirCraft.AktualizujTerminalWyjscieHander(AktualizacjaTerminalaRemoveGraber));

            //LSimAirCraft[0].TablicaPrzylotowAktualizacjaEvent += new SimulationAirCraft.TablicaPrzylotowAktualizacjaHander(AtualizacjaTablicyPrzylotow);
            Trace.WriteLine("Czekam");

        }
        /// <summary>
        /// Utworzenie zadań dla każdego Samolotu a nasepnie dodanie go do listy TaskAirCrafts
        /// </summary>
        private void AddListTaskAirCraftTask()
        {
            TasksAirCrafts = new List<Task<int>>();
            //int i = 1;
            LSimAirCraft.ForEach(lsim =>
                TasksAirCrafts.Add(new Task<int>((j) =>
                {
                    lsim.Play();
                    return 1; //zwracany przykladowy int
                }, lsim)) //parametr ktory jest tam przekazywany
            );
            
        }
        /// <summary>
        /// Proces uruchomienia każdego z zadania i następnie oczekiwanie na ich zakończenie
        /// </summary>
        private void RunAllTask()
        {
            Trace.WriteLine("Uruchamiam taski");
            TasksAirCrafts.ForEach(t => t.Start());
            Trace.WriteLine("Taski uruchomione");

            //TasksAirCrafts.ForEach(t => t.Wait());
            //Trace.WriteLine("Taski zakończone");
        }
        private void AtualizacjaTablicyPrzylotowGraber()
        {
            
            while (!concurrentBagTablicaPrzylotow.IsEmpty)
            {
                if (concurrentBagTablicaPrzylotow.TryTake(out TablicaPrzylotow result))
                {
                    listaPrzylotowAdd.Add(result);
                }
            }
            //AktualizacjaTestEvent();
            AktualizujTablicePrzylotowAdd();
            //listaPrzylotow.Add(concurrentBagTablicaPrzylotow.TryTake());
        }
        private void AktualizujTablicePrzylotowAdd()
        {
            //DataGridXAML1.Dispatcher.Invoke(new Action(() => 
            //{
            //    listaPrzylotowAdd.ForEach(s => DataGridXAML1.Items.Add(s));
            //}));
            //listaPrzylotowAdd.Clear(); //czyszczenie listy - zapobiega ponownemu ładowaniu tych samych połączeń

            prz.DodajDoTablicy();
            Trace.WriteLine("ZAKTUALIZOWANO TABLICĘ PRZYLOTOW!!!!!!!!!!!!!");
        }
        public void AktualizacjaTablicyPrzylotowGraberRemove()
        {
            while (!concurrentBagTablicaPrzylotowRemove.IsEmpty)
            {
                if (concurrentBagTablicaPrzylotowRemove.TryTake(out TablicaPrzylotow result))
                {
                    listaPrzylotowRemove.Add(result);
                }
            }
            //AktualizacjaTestEvent();
            AktualizujTablicePrzylotowRemove();
        }
        public void AktualizujTablicePrzylotowRemove()
        {
            prz.UsunZTablicy();
        }
        private void AtualizacjaTablicyOdlotowGraber()
        {

            while (!concurrentBagTablicaOdlotow.IsEmpty)
            {
                if (concurrentBagTablicaOdlotow.TryTake(out TablicaPrzylotow result))
                {
                    listaOdlotow.Add(result);
                }
            }
            //AktualizacjaTestEvent();
            AktualizujTabliceOdlotowAdd();
            //listaPrzylotow.Add(concurrentBagTablicaPrzylotow.TryTake());
        }
        private void AktualizujTabliceOdlotowAdd()
        {
            DataGridXAML2.Dispatcher.Invoke(new Action(() =>
            {
                listaOdlotow.ForEach(s => DataGridXAML2.Items.Add(s));
            }));
            listaOdlotow.Clear(); //czyszczenie listy - zapobiega ponownemu ładowaniu tych samych połączeń
            Trace.WriteLine("ZAKTUALIZOWANO TABLICĘ ODLOTOW!!!!!!!!!!!!!");
        }
        public void AktualizacjaTerminalaAddGraber()
        {
            string result = "";
            while (!concurrentBagTerminalAdd.IsEmpty)
            {
                if (concurrentBagTerminalAdd.TryTake(out result))
                {
                    Trace.WriteLine(result);
                    //to do: stworzyć nową listę dla stringow i zastanowic się na umieszczniem tego w terminalu
                    //listaOdlotow.Add(result);
                }
            }
            //AktualizacjaTestEvent();
            AktualizujTerminalAdd(result);
        }
        public void AktualizujTerminalAdd(string rejs)
        {
            terminal.DodajDOTerminala(rejs);
        }
        public void AktualizacjaTerminalaRemoveGraber()
        {
            string result = "";
            while (!concurrentBagTerminalRemove.IsEmpty)
            {
                if (concurrentBagTerminalRemove.TryTake(out result))
                {
                    Trace.WriteLine(result);
                    //to do: stworzyć nową listę dla stringow i zastanowic się na umieszczniem tego w terminalu
                    //listaOdlotow.Add(result);
                }
            }
            //AktualizacjaTestEvent();
            AktualizujTerminalRemove(result);
        }
        public void AktualizujTerminalRemove(string rejs)
        {
            terminal.UsunZterminala(rejs);
        }

        private void ZapisControlTower_Clicked(object sender, RoutedEventArgs e)
        {

        }

        private void btnPanelAdmina_Click(object sender, RoutedEventArgs e)
        {
            PanelAdminaWindow panel = new PanelAdminaWindow(this);
            panel.Show();
        }
    }

    ///
    ///
    ///
    public class TablicaPrzylotow1 // dataGridXAML1
    {
        public string Rejs { get; set; }
        public string Kierunek { get; set; }
        public string Linia { get; set; }
        public TimeSpan CzasRozkladu { get; set; }
        public TimeSpan Opoznienie { get; set; }
        public string Uwagi { get; set; }
    }
    public class Przyloty
    {
        TablicaPrzylotow1 tablicaPrzylotow = null;
        MainWindow mainWindow = null;

        int licznikDodanych = 0;
        List<ZajetePrzyloty> zajetePrzyloty = new List<ZajetePrzyloty>();
        public Przyloty(TablicaPrzylotow1 tablicaPrzylotow, MainWindow mainWindow)
        {
            this.tablicaPrzylotow = tablicaPrzylotow;
            this.mainWindow = mainWindow;
        }
        public Przyloty(MainWindow mainWindow)
        {
            //this.tablicaPrzylotow = tablicaPrzylotow;
            this.mainWindow = mainWindow;
        }
        public void DodajDoTablicy()
        {
            var obiektdododania = mainWindow.listaPrzylotowAdd[0];
            //int obiektnr = zajetePrzyloty.Count();
            zajetePrzyloty.Add(new ZajetePrzyloty() {tabNr = licznikDodanych, Rejs = obiektdododania.Rejs, CzasRozkladu = obiektdododania.CzasRozkladu, Kierunek = obiektdododania.Kierunek, Linia = obiektdododania.Linia, Opoznienie = obiektdododania.Opoznienie, Uwagi = obiektdododania.Uwagi });
            //Dodaj do tablicy przylotów
            var dodawanyobiekt = new TablicaPrzylotow1() {Rejs = obiektdododania.Rejs, Kierunek = obiektdododania.Kierunek, Linia = obiektdododania.Linia, CzasRozkladu = obiektdododania.CzasRozkladu, Opoznienie = obiektdododania.Opoznienie, Uwagi = obiektdododania.Uwagi };

            mainWindow.DataGridXAML1.Dispatcher.Invoke(new Action(() =>
             {
                 mainWindow.DataGridXAML1.Items.Add(dodawanyobiekt);
             }));
            mainWindow.listaPrzylotowAdd.Clear();
            
        }
        public void UsunZTablicy()
        {
            var obiektdousuniecia = mainWindow.listaPrzylotowRemove[0];
            //1 sprawdz który nr jest do wywalenia
            int ktoryto = 0;
            foreach (var item in zajetePrzyloty)
            {
                if (item.Rejs == obiektdousuniecia.Rejs)
                {
                    ktoryto = zajetePrzyloty.IndexOf(item);
                    break;
                }
            }
            //usuwanie obiektu z listy
            if (zajetePrzyloty.Count !=0)
            {
                zajetePrzyloty.RemoveAt(ktoryto);
                //aktualizowanie listy przylotow
                mainWindow.DataGridXAML1.Dispatcher.Invoke(new Action(() =>
                {
                    mainWindow.DataGridXAML1.Items.Clear();
                    zajetePrzyloty.ForEach(item => mainWindow.DataGridXAML1.Items.Add(item));
                }));
                mainWindow.listaPrzylotowRemove.Clear();
            }
            

        }
    }
    public class ZajetePrzyloty
    {
        public int tabNr { get; set; }
        public string Rejs { get; set; }
        public string Kierunek { get; set; }
        public string Linia { get; set; }
        public TimeSpan CzasRozkladu { get; set; }
        public TimeSpan Opoznienie { get; set; }
        public string Uwagi { get; set; }
    }
    /// <summary>
    /// Do tablicy odlotów
    /// </summary>
    /// 
    public class TablicaOdlotow // dataGridXAML2
    {
        public string Rejs { get; set; }
        public string Kierunek { get; set; }
        public string Linia { get; set; }
        public TimeSpan CzasRozkladu { get; set; }
        public TimeSpan Opoznienie { get; set; }
        public string Uwagi { get; set; }
    }
    public class Odloty
    {
        TablicaOdlotow tablicaOdlotow = null;
        MainWindow mainWindow = null;

        int licznikDodanych = 0;
        List<ZajeteOdloty> zajeteOdloty = new List<ZajeteOdloty>();
        public Odloty(TablicaOdlotow tablicaOdlotow, MainWindow mainWindow)
        {
            this.tablicaOdlotow = tablicaOdlotow;
            this.mainWindow = mainWindow;
        }
        public Odloty(MainWindow mainWindow)
        {
            //this.tablicaPrzylotow = tablicaPrzylotow;
            this.mainWindow = mainWindow;
        }
        public void DodajDoTablicy()
        {
            var obiektdododania = mainWindow.listaOdlotow[0];
            //int obiektnr = zajetePrzyloty.Count();
            zajeteOdloty.Add(new ZajeteOdloty() { tabNr = licznikDodanych, Rejs = obiektdododania.Rejs, CzasRozkladu = obiektdododania.CzasRozkladu, Kierunek = obiektdododania.Kierunek, Linia = obiektdododania.Linia, Opoznienie = obiektdododania.Opoznienie, Uwagi = obiektdododania.Uwagi });
            //Dodaj do tablicy przylotów
            var dodawanyobiekt = new TablicaOdlotow() { Rejs = obiektdododania.Rejs, Kierunek = obiektdododania.Kierunek, Linia = obiektdododania.Linia, CzasRozkladu = obiektdododania.CzasRozkladu, Opoznienie = obiektdododania.Opoznienie, Uwagi = obiektdododania.Uwagi };

            mainWindow.DataGridXAML2.Dispatcher.Invoke(new Action(() =>
            {
                mainWindow.DataGridXAML2.Items.Add(dodawanyobiekt);
            }));
            mainWindow.listaOdlotow.Clear();

        }
        public void UsunZTablicy()
        {
            var obiektdousuniecia = mainWindow.listaOdlotowRemove[0];
            //1 sprawdz który nr jest do wywalenia
            int ktoryto = 0;
            foreach (var item in zajeteOdloty)
            {
                if (item.Rejs == obiektdousuniecia.Rejs)
                {
                    ktoryto = zajeteOdloty.IndexOf(item);
                    break;
                }
            }
            //usuwanie obiektu z listy
            if (zajeteOdloty.Count != 0)
            {
                zajeteOdloty.RemoveAt(ktoryto);
                //aktualizowanie listy przylotow
                mainWindow.DataGridXAML2.Dispatcher.Invoke(new Action(() =>
                {
                    mainWindow.DataGridXAML2.Items.Clear();
                    zajeteOdloty.ForEach(item => mainWindow.DataGridXAML2.Items.Add(item));
                }));
                mainWindow.listaOdlotowRemove.Clear();
            }


        }
    }
    public class ZajeteOdloty
    {
        public int tabNr { get; set; }
        public string Rejs { get; set; }
        public string Kierunek { get; set; }
        public string Linia { get; set; }
        public TimeSpan CzasRozkladu { get; set; }
        public TimeSpan Opoznienie { get; set; }
        public string Uwagi { get; set; }
    }
    /// <summary>
    /// Do edycji terminala
    /// </summary>
    public class TerminalTablica //DataGridTerminal
    {
        public string Gate1 { get; set; }
        public string Gate2 { get; set; }
        public string Gate3 { get; set; }
        public string Gate4 { get; set; }
        public string Gate5 { get; set; }
        public string Gate6 { get; set; }
        public string Gate7 { get; set; }
        public string Gate8 { get; set; }
        public string Gate9 { get; set; }
    }
    public class Terminal
    {
        //MiejsceWterminalu mwt = null;
        List<MiejsceWterminalu> miejsceWTerminalu = new List<MiejsceWterminalu>()
            {
                new MiejsceWterminalu (){NrGate = 1, Zajetosc = false, Miejsce = "----" },
                new MiejsceWterminalu (){NrGate = 2, Zajetosc = false, Miejsce = "----" },
                new MiejsceWterminalu (){NrGate = 3, Zajetosc = false, Miejsce = "----" },
                new MiejsceWterminalu (){NrGate = 4, Zajetosc = false, Miejsce = "----" },
                new MiejsceWterminalu (){NrGate = 5, Zajetosc = false, Miejsce = "----" },
                new MiejsceWterminalu (){NrGate = 6, Zajetosc = false, Miejsce = "----" },
                new MiejsceWterminalu (){NrGate = 7, Zajetosc = false, Miejsce = "----" },
                new MiejsceWterminalu (){NrGate = 8, Zajetosc = false, Miejsce = "----" },
                new MiejsceWterminalu (){NrGate = 9, Zajetosc = false, Miejsce = "----" },
            };

        TerminalTablica terminalTablica = null;
        MainWindow mainWindow = null;

        public Terminal(TerminalTablica terminalTablica, MainWindow mainWindow)
        {
            this.terminalTablica = terminalTablica;
            this.mainWindow = mainWindow;
        }

        public void DodajDOTerminala(string Rejs)
        {
            //Sprawdź następne wolne miejsce
            int wolnynumer = 0;
            foreach (var item in miejsceWTerminalu)
            {
                if (item.Zajetosc == false)
                {
                    wolnynumer = item.NrGate;
                    item.Zajetosc = true;
                    item.Miejsce = Rejs;
                    break;
                }
            }
            //Aktualizuj Tablicę terminalu
            if (wolnynumer == 1) { terminalTablica.Gate1 = Rejs; }
            else if (wolnynumer == 2) { terminalTablica.Gate2 = Rejs; }
            else if (wolnynumer == 3) { terminalTablica.Gate3 = Rejs; }
            else if (wolnynumer == 4) { terminalTablica.Gate4 = Rejs; }
            else if (wolnynumer == 5) { terminalTablica.Gate5 = Rejs; }
            else if (wolnynumer == 6) { terminalTablica.Gate6 = Rejs; }
            else if (wolnynumer == 7) { terminalTablica.Gate7 = Rejs; }
            else if (wolnynumer == 8) { terminalTablica.Gate8 = Rejs; }
            else { terminalTablica.Gate9 = Rejs; }

            mainWindow.DataGridTerminal.Dispatcher.Invoke(new Action(() =>
            {
                mainWindow.DataGridTerminal.Items.Clear();
                mainWindow.DataGridTerminal.Items.Add(terminalTablica);
            }));

            //mainWindow.DataGridTerminal.Items.Clear();
            //mainWindow.DataGridTerminal.Items.Add(terminalTablica);
        }
        public void UsunZterminala(string Rejs)
        {
            //Sprawdź miejsce do usuniecia
            int miejscedousuniecia = 0;
            foreach (var item in miejsceWTerminalu)
            {
                if (item.Miejsce == Rejs)
                {
                    miejscedousuniecia = item.NrGate;
                    item.Zajetosc = false;
                    item.Miejsce = "----";
                    break;
                }
            }
            string puste = "----";
            //Aktualizuj Tablicę terminalu
            if (miejscedousuniecia == 1) { terminalTablica.Gate1 = puste; }
            else if (miejscedousuniecia == 2) { terminalTablica.Gate2 = puste; }
            else if (miejscedousuniecia == 3) { terminalTablica.Gate3 = puste; }
            else if (miejscedousuniecia == 4) { terminalTablica.Gate4 = puste; }
            else if (miejscedousuniecia == 5) { terminalTablica.Gate5 = puste; }
            else if (miejscedousuniecia == 6) { terminalTablica.Gate6 = puste; }
            else if (miejscedousuniecia == 7) { terminalTablica.Gate7 = puste; }
            else if (miejscedousuniecia == 8) { terminalTablica.Gate8 = puste; }
            else { terminalTablica.Gate9 = puste; }

            //mainWindow.DataGridTerminal.Items.Clear();
            //mainWindow.DataGridTerminal.Items.Add(terminalTablica);

            mainWindow.DataGridTerminal.Dispatcher.Invoke(new Action(() =>
            {
                mainWindow.DataGridTerminal.Items.Clear();
                mainWindow.DataGridTerminal.Items.Add(terminalTablica);
            }));
        }
    }
    public class MiejsceWterminalu
    {
        public int NrGate { get; set; }
        public string Miejsce { get; set; }
        public bool Zajetosc { get; set; }
    }
}
