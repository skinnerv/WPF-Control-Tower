using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Windows;

namespace ControlTower01.Models
{
    public class SimulationAirCraft
    {
        //Deklaracja delegata do definicji zdarzenia Aktualizacja Tablicy Przylotow
        public delegate void TablicaPrzylotowAktualizacjaHander();
        //Deklaracja zdarzenia na podstawie powyższego delegata
        public event TablicaPrzylotowAktualizacjaHander TablicaPrzylotowAktualizacjaEvent;

        //Deklaracja delegata do definicji zdarzenia Aktualizacja Tablicy Przylotow Remove
        public delegate void TablicaPrzylotowAktualizacjaRemoveHander();
        //Deklaracja zdarzenia na podstawie powyższego delegata
        public event TablicaPrzylotowAktualizacjaRemoveHander TablicaPrzylotowAktualizacjaRemoveEvent;

        //Deklaracja delegata do definicji zdarzenia Aktualizacja Tablicy Odlotow
        public delegate void TablicaOdlotowAktualizacjaHander();
        //Deklaracja zdarzenia na podstawie powyższego delegata
        public event TablicaOdlotowAktualizacjaHander TablicaOdlotowAktualizacjaEvent;

        //Deklaracja delegata definicji zdarzenia AktualizujParkingWejscie
        public delegate void AktualizujTerminalWejscieHander();
        //Deklaracja zdarzenia na podstawie powyższego delegata
        public event AktualizujTerminalWejscieHander AktualizujTerminalWejscieEvent;

        //Deklaracja delegata definicji zdarzenia AktualizujTerminalWyjscie
        public delegate void AktualizujTerminalWyjscieHander();
        //Deklaracja zdarzenia na podstawie powyższego delegata
        public event AktualizujTerminalWyjscieHander AktualizujTerminalWyjscieEvent;

        private int AirCraftId { get; set; }
        private string NameModel { get; set; }
        private string NameAirLine { get; set; }
        private int PassengerCapacity { get; set; }
        private string Rejs { get; set; }
        private int Flaga { get; set; } //określa aktualne zachowanie obiektu
        private int Predkosc { get; set; }
        private string Kierunek { get; set; }

        private enum WarunkiTakNie { Tak, Nie}
        private enum KrokiPostepowania { Przylot, TerminalRozpakowac, Postoj, TerminalZapakowac, Wylot };
        private enum NazwyLotnisk { Gdansk, Warszawa, Krakow, Londyn, Leeds, Manchester, Berlin, Praga, Wieden } // jest w sumie 9 lotnisk ale tylko 8 docelowych
        private static Random random = new Random(Guid.NewGuid().GetHashCode());

        MainWindow mainWindow = null;

        public SimulationAirCraft(int _AirCraftId, string _NameModel, string _NameAirLine, int _PassangerCapacity, MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.AirCraftId = _AirCraftId;
            this.NameModel = _NameModel;
            this.NameAirLine = _NameAirLine;
            this.PassengerCapacity = _PassangerCapacity;
            Trace.WriteLine("Samolot " + AirCraftId.ToString() + " " + NameModel + " ISTNIEJE!!");
        }

        public void Play()
        {
            //Przeprowadz test
            Trace.WriteLine("Samolot "+ AirCraftId.ToString() + " "+ NameModel + " w grze z własnym wątkiem");
            //losuj od czego ma się zacząć działanie w symulacji
            int start = random.Next(1,4); //losuję jedną z trzech możliwości

            int dzialanie = 0; //aktualne dzialanie Obiektu - Samolotu
            if (start == 1) { dzialanie = (int)KrokiPostepowania.Postoj; }
            else if (start == 2) { dzialanie = (int)KrokiPostepowania.TerminalZapakowac; }
            else { dzialanie = (int)KrokiPostepowania.Przylot; }

            //Uzupełnienie danych do podjęcia pierwszego dzialania
            if (dzialanie == (int)KrokiPostepowania.Postoj)
            {
                Rejs = AirCraftId.ToString() + NameAirLine;
            }
            else if (dzialanie == (int)KrokiPostepowania.TerminalZapakowac)
            {
                Rejs = AirCraftId.ToString() + NameAirLine;
            }
            else //Przylot
            {
                Rejs = AirCraftId.ToString() + NameAirLine;
            }

            //Pętla życia dla Samolotu
            int licznikPętli = 0;
            bool dzialaj = true;
            while (dzialaj)
            {
                if (dzialanie == (int)KrokiPostepowania.Przylot)
                {
                    Rejs = AirCraftId.ToString() + NameAirLine + random.Next(0, 1000).ToString();
                    Przylot();
                }
                else if (dzialanie == (int)KrokiPostepowania.TerminalRozpakowac)
                {
                    TerminalRozpakowac();
                }
                else if (dzialanie == (int)KrokiPostepowania.Postoj)
                {
                    Postoj();
                }
                else if (dzialanie == (int)KrokiPostepowania.TerminalZapakowac)
                {
                    Rejs = AirCraftId.ToString() + NameAirLine + random.Next(0, 1000).ToString();
                    TerminalZapakowac();
                }
                else
                {
                    Wylot();
                    licznikPętli++;
                }
                dzialanie++;
                if (dzialanie == 5) { dzialanie = 0; } //ustawia wartość początkową dla pętli
                if (licznikPętli == 3) { dzialaj = false; } //kończy dzialanie dla pętli
            }

        }
        public void Przylot()
        {
            //Losuj kierunek z jakiego przyleci samolot
            int kier = random.Next(1,9);
            switch (kier)
            {
                case (int)NazwyLotnisk.Berlin: Kierunek = "Berlin";
                    break;
                case (int)NazwyLotnisk.Krakow:
                    Kierunek = "Kraków";
                    break;
                case (int)NazwyLotnisk.Leeds:
                    Kierunek = "Leeds";
                    break;
                case (int)NazwyLotnisk.Londyn:
                    Kierunek = "Londyn";
                    break;
                case (int)NazwyLotnisk.Manchester:
                    Kierunek = "Manchester";
                    break;
                case (int)NazwyLotnisk.Praga:
                    Kierunek = "Praga";
                    break;
                case (int)NazwyLotnisk.Warszawa:
                    Kierunek = "Warszawa";
                    break;
                case (int)NazwyLotnisk.Wieden:
                    Kierunek = "Wieden";
                    break;
                default:
                    Kierunek = "W siną dal xD";
                    break;
            }
            ///
            /// Teraz niech sobie tak leci przez jakiś czas który zostanie wylosowany
            /// 
            int czekaj = random.Next(10, 11); // losuję z przedziału od 10s do 60s
            //Aktualizacja tablicy przylotów i dodanie lotu
            AktualizacjaTablicyPrzylotow();
            Trace.WriteLine("SSS " + AirCraftId.ToString() + " " + NameModel + " PRZYLOT Z " + Kierunek + " za: " + czekaj.ToString() + "s");
            Thread.Sleep(czekaj*1000); //czas podawany w milisekundach

            //Losuj czy zajdzie opóźnienie
            int opoznienie = random.Next(0, 2); //losuje z przedzialu <0,1>

            //TYMCZASOWO WYCIETE OPOZNIENIE DLA PRZYLOTOW
            //if (opoznienie == (int)WarunkiTakNie.Tak)
            //{
            //    int opczas = random.Next(10, 11); //przedzial od <10s 30s>
            //    //Aktualizuje tablicę przylotów o podane opóźnienie
            //    AktualizacjaTablicyPrzylotow(opczas);
            //    Trace.WriteLine("SSS " + AirCraftId.ToString() + " " + NameModel + " PRZYLOT Z " + Kierunek + " opóźnienie: " + opczas.ToString() + "s");
            //    Thread.Sleep(opczas * 1000); //czas podany w milisekundach
            //}

            //Lądowanie
            Trace.WriteLine("SSS " + AirCraftId.ToString() + " " + NameModel + " PRZYLOT Z " + Kierunek + " LĄDOWANIE");
            AktualizacjaTablicyPrzylotow(true); // Aktualizacja o wylądowaniu
        }
        public void TerminalRozpakowac()
        {
            //Aktualizuj terminal o dodatkowy samolot 
            AktualizujTerminalWejscie();
            //Posiedz trochę w terminalu
            int czasWterminalu = random.Next(10, 11); // przedział między 30s a 2 minutami
            Trace.WriteLine("SSS " + AirCraftId.ToString() + " " + NameModel + " PRZYLOT Z " + Kierunek + " W TERMINALU: " + czasWterminalu.ToString() + "s");
            Thread.Sleep(czasWterminalu*1000); //czas spania w terminalu w milisekundach

            //Aktualizuj terminal o wyjsciu z niego
            AktualizujTerminalWyjscie();
            Trace.WriteLine("SSS " + AirCraftId.ToString() + " " + NameModel + " PRZYLOT Z " + Kierunek + " WYJSCIE Z TERMINALA");
        }
        public void Postoj()
        {
            //Udaj się na spoczynek

            //AktualizujParkingWejscie
            AktualizujParkingWejscie();
            //Posiedz trochę w parkingu
            int czaswParkingu = random.Next(10, 11); // przedział między 30s a 2 minutami
            Trace.WriteLine("SSS " + AirCraftId.ToString() + " " + NameModel + " PRZYLOT Z " + Kierunek + " NA PARKINGU: " + czaswParkingu.ToString() + "s");
            Thread.Sleep(czaswParkingu * 1000); //czas spania w terminalu w milisekundach
            //Wyjsciez parkingu
            AktualizujParkingWyjscie();
            Trace.WriteLine("SSS " + AirCraftId.ToString() + " " + NameModel + " WYJSCIE Z PARKINGU");
        }
        public void TerminalZapakowac()
        {
            //Wybranie kierunku do wylotu
            //Losuj kierunek do jakiego wyleci samolot
            int kier = random.Next(1, 9);
            switch (kier)
            {
                case (int)NazwyLotnisk.Berlin:
                    Kierunek = "Berlin";
                    break;
                case (int)NazwyLotnisk.Krakow:
                    Kierunek = "Kraków";
                    break;
                case (int)NazwyLotnisk.Leeds:
                    Kierunek = "Leeds";
                    break;
                case (int)NazwyLotnisk.Londyn:
                    Kierunek = "Londyn";
                    break;
                case (int)NazwyLotnisk.Manchester:
                    Kierunek = "Manchester";
                    break;
                case (int)NazwyLotnisk.Praga:
                    Kierunek = "Praga";
                    break;
                case (int)NazwyLotnisk.Warszawa:
                    Kierunek = "Warszawa";
                    break;
                case (int)NazwyLotnisk.Wieden:
                    Kierunek = "Wieden";
                    break;
                default:
                    Kierunek = "W siną dal xD";
                    break;
            }
            //Aktualizuj tablice odlotow
            AktualizacjaTablicyOdlotow();
            //Aktualizuj terminal o dodatkowy samolot 
            AktualizujTerminalWejscie();
            //Posiedz trochę w terminalu
            int czasWterminalu = random.Next(10, 11); // przedział między 30s a 2 minutami
            Trace.WriteLine("SSS " + AirCraftId.ToString() + " " + NameModel + " ODLOT DO " + Kierunek + " NA TERMINALU: " + czasWterminalu.ToString() + "s");
            Thread.Sleep(czasWterminalu * 1000); //czas spania w terminalu w milisekundach
            //Aktualizuj terminal o wyjsciu z niego
            AktualizujTerminalWyjscie();
            Trace.WriteLine("SSS " + AirCraftId.ToString() + " " + NameModel + " ODLOT DO " + Kierunek + " WYJSCIE Z TERMINALA");
        }
        public void Wylot()
        {
            //Aktualizacja tablicy odlotów o odlocie
            AktualizacjaTablicyOdlotow(true);
            //Niech sobie tak leci przez pewien czas
            int czaswPowietrzu = random.Next(10, 11); // przedział między 30s a 2 minutami
            Trace.WriteLine("SSS " + AirCraftId.ToString() + " " + NameModel + " ODLOT DO " + Kierunek + " ODLECIAŁ CZAS W POWIETRZU: " + czaswPowietrzu.ToString() + "s");
            Thread.Sleep(czaswPowietrzu * 1000); //czas spania w terminalu w milisekundach
            //teraz zaczyna się nowy cykl
            Trace.WriteLine("QQQSSS " + AirCraftId.ToString() + " " + NameModel + " NOWY CYKL");
        }
        public void AktualizacjaTablicyPrzylotow()
        {
            //nalezy dodac nowy przylot nazwy godziny kierunki ale bez opoznieni
            mainWindow.concurrentBagTablicaPrzylotow.Add(new MainWindow.TablicaPrzylotow()
            {
                Rejs = this.Rejs,
                Kierunek = this.Kierunek,
                Linia = this.NameAirLine,
                CzasRozkladu = TimeSpan.Parse("00"),
                Opoznienie = TimeSpan.Parse("00"),
                Uwagi = "--"
            });
            TablicaPrzylotowAktualizacjaEvent();

        }
        public void AktualizacjaTablicyPrzylotow(int opoznienie)
        {
            //nalezy dodac nowy przylot nazwy godziny kierunki ale bez opoznieni
            mainWindow.concurrentBagTablicaPrzylotow.Add(new MainWindow.TablicaPrzylotow()
            {
                Rejs = this.Rejs,
                Kierunek = this.Kierunek,
                Linia = this.NameAirLine,
                CzasRozkladu = TimeSpan.Parse("00"),
                Opoznienie = TimeSpan.Parse("00:20"),
                Uwagi = "OPÓŹNIONY"
            });
            TablicaPrzylotowAktualizacjaEvent();

            //Dodać usuwanie elementu z tablicy przylotów który jest opóźniony
        }
        public void AktualizacjaTablicyPrzylotow(bool wyladowal)
        {
            //nalezy zaktualizowac tablicę przylotow i dodać wpis "WYLĄDOWAŁ"

            //należy zaktualizować bazę danych o przylot na lotnisko
        }
        public void AktualizujTerminalWejscie()
        {
            mainWindow.concurrentBagTerminalAdd.Add(Rejs);
            AktualizujTerminalWejscieEvent();
        }
        public void AktualizujTerminalWyjscie()
        {
            mainWindow.concurrentBagTerminalRemove.Add(Rejs);
            AktualizujTerminalWyjscieEvent();
        }
        public void AktualizujParkingWejscie()
        {
            
        }
        public void AktualizujParkingWyjscie()
        {

        }
        public void AktualizujParkingWyjscie(bool wyjscie)
        {

        }
        public void AktualizacjaTablicyOdlotow()
        {
            mainWindow.concurrentBagTablicaOdlotow.Add(new MainWindow.TablicaPrzylotow()
            {
                Rejs = this.Rejs,
                Kierunek = this.Kierunek,
                Linia = this.NameAirLine,
                CzasRozkladu = TimeSpan.Parse("00"),
                Opoznienie = TimeSpan.Parse("00"),
                Uwagi = "--"
            });
            TablicaOdlotowAktualizacjaEvent();
        }
        public void AktualizacjaTablicyOdlotow (bool odlecial)
        {
            mainWindow.concurrentBagTablicaPrzylotowRemove.Add(new MainWindow.TablicaPrzylotow()
            {
                Rejs = this.Rejs,
                Kierunek = this.Kierunek,
                Linia = this.NameAirLine,
                CzasRozkladu = TimeSpan.Parse("00"),
                Opoznienie = TimeSpan.Parse("00"),
                Uwagi = "--"
            });
            TablicaPrzylotowAktualizacjaRemoveEvent();
        }
    }
}
