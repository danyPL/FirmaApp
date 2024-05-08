using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using FirmaApp.Models;

namespace FirmaApp.Scripts
{
    internal class DataBaseManagment
    {
        public List<Note> Notes = new();
        public List<Worker> Workers = new();
        public List<Role> Roles = new();
        public MySqlConnectionStringBuilder builder = new()

        {
            Server = "localhost",
            Database = "workgroup",
            UserID = "root",
            Password = "",
        };
        public MySqlConnection conn;

        public DataBaseManagment()
        {
            conn = new MySqlConnection(builder.ConnectionString);
        }

        public void ShowWorkers()
        {
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT id_worker, name, surname, role_name FROM workers JOIN role ON role.id_role = workers.id_role;", conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Worker worker = new Worker
                    {
                        id_worker = rdr.GetInt32("id_worker"),
                        name = rdr.GetString("name"),
                        surname = rdr.GetString("surname"),
                        role_name = rdr.GetString("role_name")
                    };
                    Workers.Add(worker);
                }

                rdr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public Worker ShowWorker(int id)
        {
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand($"SELECT id_worker, name, surname, login, password, age, hire_date, is_working, role_name FROM workers JOIN role ON role.id_role = workers.id_role WHERE id_worker = {id};", conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                Worker worker = null;

                if (rdr.Read())
                {
                    worker = new Worker
                    {
                        id_worker = rdr.GetInt32("id_worker"),
                        name = rdr.GetString("name"),
                        surname = rdr.GetString("surname"),
                        login = rdr.GetString("login"),
                        password = rdr.GetString("password"),
                        age = rdr.GetInt32("age"),
                        hire_date = rdr.GetDateTime("hire_date"),
                        is_working = rdr.GetBoolean("is_working"),
                        role_name = rdr.GetString("role_name")
                    };
                }

                rdr.Close();
                conn.Close();

                return worker;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<Stats> ShowStats()
        {
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand($"SELECT role.role_name AS stanowisko, COUNT(workers.id_worker) AS liczba_osob FROM role LEFT JOIN workers ON role.id_role = workers.id_role WHERE workers.is_working = TRUE GROUP BY role.role_name;", conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                List<Stats> statsList = new List<Stats>();

                while (rdr.Read())
                {
                    Stats stats = new Stats
                    {
                        Stanowisko = rdr.GetString("stanowisko"),
                        LiczbaOsob = rdr.GetInt32("liczba_osob")
                    };
                    statsList.Add(stats);
                }

                rdr.Close();
                conn.Close();

                return statsList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void AddWorker(Worker worker)
        {
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand($"INSERT INTO workers (name, surname, login, password, id_role, age, hire_date, is_working) VALUES ('{worker.name}', '{worker.surname}', '{worker.login}', '{worker.password}', {worker.id_role}, {worker.age}, '{worker.hire_date.ToString("yyyy-MM-dd")}', {worker.is_working})", conn);
                cmd.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void RemoveWorker(int id)
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"DELETE FROM workers WHERE id_worker = {id};", conn);
                cmd.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdatePassword(int id, int length = 7)
        {
            try
            {
                string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
                Random rd = new Random();
                char[] chars = new char[length];
                for (int i = 0; i < length; i++)
                {
                    chars[i] = validChars[rd.Next(0, validChars.Length)];
                }
                string updatedpassword = new string(chars);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"UPDATE workers SET password = '{updatedpassword}' WHERE id_worker = {id}", conn);
                cmd.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AddNote_Worker(int id, string content)
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"INSERT INTO note (id_worker, content) VALUES ({id}, '{content}')", conn);
                cmd.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void RemoveNote_Worker(int id)
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"DELETE FROM note WHERE id_note = {id};", conn);
                cmd.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<Note_worker> ShowNote_Worker(int id)
        {
            List<Note_worker> notes = new List<Note_worker>();

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand($"SELECT id_note, name AS WorkerName, content, added_at FROM note JOIN workers ON note.id_worker = workers.id_worker WHERE workers.id_worker = {id}", conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Note_worker note = new Note_worker
                    {
                        id_note = rdr.GetInt32("id_note"),
                        WorkerName = rdr.GetString("WorkerName"),
                        Content = rdr.GetString("content"),
                        AddedAt = rdr.GetDateTime("added_at")
                    };
                    notes.Add(note);
                }

                rdr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return notes;
        }
    }
}
