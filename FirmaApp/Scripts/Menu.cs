using FirmaApp.Models;
using Org.BouncyCastle.Tls.Crypto.Impl.BC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirmaApp.Scripts
{
    internal class Menu
    {
        bool repeat = true;
        public Menu() {
            DataBaseManagment db = new();

            while (repeat)
            {
                Console.Clear();
                Console.WriteLine("Witaj w aplikacji do zarządzania firmą!\n" +
                    "Wybierz akcje ktora chcesz wykonac:\n" +
                    "1. Wyswietl Pracownikow\n" +
                    "2. Zobacz statystyki firmy\n" +
                    "3. Dodaj pracownika\n" +
                    "4.Wyjdz");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ShowWorkers(db);
                        break;
                    case "2":

                        ShowStats(db);
                        break;
                        case "3":
                        AddWorker(db);
                        break;
                    case "4":
                        repeat = false;
                        break;

                }
            }
        
        }
        public void ShowStats(DataBaseManagment db)
        {
            List<Stats> statsList = db.ShowStats();
            if (statsList != null)
            {
                foreach (Stats stat in statsList)
                {
                    Console.WriteLine($"{stat.Stanowisko} - {stat.LiczbaOsob}");
                }
            }
            else
            {
                Console.WriteLine("Brak danych do wyświetlenia.");
            }
            Console.ReadKey();
        }

        public void AddWorker(DataBaseManagment db)
        {
            Console.WriteLine("Podaj imie pracownika");
            string name = Console.ReadLine();
            Console.WriteLine("Podaj nazwisko pracownika");
            string surname = Console.ReadLine();
            Console.WriteLine("Podaj login uzytkownika");
            string login = Console.ReadLine();
            Console.WriteLine("Podaj haslo uzytkownika");
            string pass = Console.ReadLine();
            Console.WriteLine("Podaj id roli");
            int id_role = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Podaj wiek pracownika");
            int age = Convert.ToInt32(Console.ReadLine());
            
            Worker w = new Worker
            {
                id_worker = db.Workers.Count + 1,
                name = name,
                surname = surname,
                login = login,
                password = pass,
                id_role = id_role,
                age = age,
                hire_date = DateTime.Now,
                is_working = true
            };
            db.AddWorker(w);
            Console.ReadKey();
        }
        public void ShowWorkers(DataBaseManagment db) {
            Console.Clear();
            bool repeat2 = true;
            
            db.ShowWorkers();
            while (repeat2)
            {

                Console.Clear();
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("             Pracownicy                 ");
                Console.WriteLine("----------------------------------------");
                foreach (Worker w in db.Workers)
                {
                    Console.WriteLine($"{w.id_worker}. {w.name} {w.surname} / {w.role_name}");
                }
                Console.WriteLine("Wybierz akcje uzytkownika uzywajac jego id\n" +
                    "1. Szczegoly uzytkownika\n" +
                    "2. Usun danego uzytkownika\n" +
                    "3. Zaaktualizuj haslo uzytkownika\n" +
                    "4. Dodaj notatke uzytkownikowi\n" +
                    "5. Wyswietl notatki o uzytkowniku\n" +
                    "6. Usun notatke uzytkownika\n" +
                    "7. Cofnij");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Podaj id uzytkownika");
                        int id = Convert.ToInt32(Console.ReadLine());
                        Worker w = db.ShowWorker(id);
                        Console.WriteLine($"ID: {w.id_worker}\n" +
                            $"{w.name} {w.surname} ({w.age} lat)\n" +
                            $"Login: {w.login}\n" +
                            $"Zatrudninoy: {w.hire_date} na stanowisku {w.role_name}");
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.WriteLine("Podaj id uzytkownika do usuniecia");
                        int id2 = Convert.ToInt32(Console.ReadLine());
                        db.RemoveWorker(id2);
                        Console.ReadKey();
                        break;
                    case "3":
                        Console.WriteLine("Podaj id uzytkownika");
                        int id3 = Convert.ToInt32(Console.ReadLine());
                        db.UpdatePassword(id3);
                        Console.ReadKey();
                        break;
                    case "4":
                        Console.WriteLine("Podaj id uzytkownika");
                        int id4 = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Podaj tresc notatki");
                        string content = Console.ReadLine();
                        db.AddNote_Worker(id4, content);
                        Console.ReadKey();
                        break;
                    case "5":
                        Console.WriteLine("Podaj id uzytkownika");
                        int id5 = Convert.ToInt32(Console.ReadLine());
                        List<Note_worker> notes = db.ShowNote_Worker(id5);
                        Console.WriteLine("Notki o pracowniku");
                        foreach(Note_worker note in notes)
                        {
                            Console.WriteLine($"{note.id_note}. {note.WorkerName} / {note.AddedAt} - {note.Content}");
                        }
                        Console.ReadKey();

                        break;
                    case "6":
                        Console.WriteLine("Podaj id uzytkownika");
                        int id6 = Convert.ToInt32(Console.ReadLine());
                        db.RemoveNote_Worker(id6);
                        Console.ReadKey();
                        break;
                    case "7":
                        repeat2 = false;
                        Console.ReadKey();
                        break;
                    
                }
            }

            Console.ReadKey();
        }

    }
}
