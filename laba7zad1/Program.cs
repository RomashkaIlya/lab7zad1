using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
//using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Application
{
    class MainClass
    {
        public enum Type { Д, К, М, Б, А, Неизвестен }

        struct Film
        {
            public string title;
            public string name;
            public int year;
            public Type type;

            public Film fromString(String str)
            {
                String[] tmp = str.Split(';');
                Film hd = new Film();
                hd.title = tmp[0];
                hd.name = tmp[1];
                hd.year = Int32.Parse(tmp[2]);
                hd.type = (Type)Int32.Parse(tmp[3]);
                return hd;
            }
            public string ToString()
            {
                return String.Format("{0},{1},{2},{3}",
                    this.title
                    , this.name.ToString()
                        , this.year
                            , this.type.ToString()
                    );


            }

          




            public void DisplayInfo()
            {
                Console.WriteLine($"{title,-20} {name,-20} {year,-20} {type,-21}");
            }
        }
        private static void printTableSystem(List<Film> list)
        {

            //Console.WriteLine($"{title,-20} {name,-20} {year,-20} {type,-21}");
            if (list.Count == 0)
            {
                Console.WriteLine("Записей нет");
                return;
            }
            foreach (Film hd in list)
            {
                Console.Write(String.Format("{0:20} {1:20} {2:20} {3:20} \n", hd.title.PadRight(20, ' '),
                    ("" + hd.name).PadRight(20, ' '), ("" + hd.year).PadRight(20, ' '), ("" + hd.type).PadRight(20, ' ')));
            }
        }
        private void PrintTableWithSort(List<Film> list)
        { 
            //List<Film> tmplist = new List<Film>();
            //tmplist.Clear();
            //tmplist.AddRange(ViborSort())

        }

        static Film[] ViborSort(Film[] mas,String field = "name" ,bool directoin = true)
        {

            for (int i = 0; i < mas.Length - 1; i++)
            {
                if (directoin )
                {
                    //поиск минимального числа
                    int min = i;
                    for (int j = i + 1; j < mas.Length; j++)
                    {
                        switch (field)
                        {
                            default:
                            case "name":
                                if (mas[j].name[0] < mas[min].name[0])
                                {
                                    min = j;
                                }
                        break;
                            case "title":
                                if (mas[j].title[0] < mas[min].title[0])
                                {
                                    min = j;
                                }
                                break;

                            case "year":
                                if (mas[j].year < mas[min].year)
                                {
                                    min = j;
                                }
                                break;

                            //case "type":
                            //    if (mas[j].type < mas[min].type)
                            //    {
                            //        min = j;
                            //    }
                            //    break;
                        }
                    }
                    //обмен элементов
                    Film temp = mas[min];
                    mas[min] = mas[i];
                    mas[i] = temp;
                }
                if (!directoin) 
                {
                    //поиск минимального числа
                    int max = i;
                    for (int j = i + 1; j < mas.Length; j++)
                    {
                        switch (field)
                        {
                            default:
                            case "name":
                                if (mas[j].name[0] > mas[max].name[0])
                                {
                                    max = j;
                                }
                                break;
                            case "title":
                                if (mas[j].title[0] > mas[max].title[0])
                                {
                                    max = j;
                                }
                                break;

                            case "year":
                                if (mas[j].year > mas[max].year)
                                {
                                    max = j;
                                }
                                break;

                            //case "type":
                            //    if (mas[j].type > mas[max].type)
                            //    {
                            //        max = j;
                            //    }
                            //    break;
                        }
                    }
                    //обмен элементов
                    Film temp = mas[max];
                    mas[max] = mas[i];
                    mas[i] = temp;
                }
            }
            return mas;
        }
        struct Log
        {
            public string title;
            public DateTime time;
            public string operation;

            public void DisplayLog()
            {
                Console.WriteLine($"{time,-20} {operation,-20} {title,-20}");
            }
        }
        //


        //
        //List<Film> Table = new List<Film>();
        public static void Main(string[] args)
        {




            //Film The_big_Lebowski;
            //The_big_Lebowski.title = "Большой Лебовски";
            //The_big_Lebowski.name = "Коэн  И., Коэн Дж.";
            //The_big_Lebowski.year = 1998;
            //The_big_Lebowski.type = Type.К;

            //Film Hercules;
            //Hercules.title = "Геркулес";
            //Hercules.name = "Маскер Дж.";
            //Hercules.year = 1997;
            //Hercules.type = Type.А;

            //Film Notting_hill;
            //Notting_hill.title = "Ноттинг-хилл";
            //Notting_hill.name = "Мишелл Р.";
            //Notting_hill.year = 1999;
            //Notting_hill.type = Type.М;

            var Table = new List<Film>();
            //Table.Add(The_big_Lebowski);
            //Table.Add(Hercules);
            //Table.Add(Notting_hill);

            var Log = new List<Log>();
            DateTime time_1 = DateTime.Now;
            DateTime time_2 = DateTime.Now;
            TimeSpan timeInterval_1 = time_2 - time_1;

            string Menu = "\n1 – Просмотр таблицы \n2 – Добавить запись \n3 – Удалить запись \n4 – Обновить запись \n5 – Поиск записей \n6 – Просмотреть лог \n7 – Выход \n8 - Сортировка\n";
            bool optionError = true;

            do
            {
                Console.WriteLine(Menu);
                int Option = Convert.ToInt32(Console.ReadLine());
                switch (Option)
                {
                    case 1: // Просмотр таблицы
                        printTableSystem(Table);
                        //for (int i = 0; i < Table.Count; i++)
                        //{
                        //    Table[i].DisplayInfo();
                        //}
                        break;

                    case 2: // Добавить запись
                        {
                            Console.WriteLine("Введите название фильма: ");
                            string title = Console.ReadLine();

                            Console.WriteLine("Введите режиссера фильма: ");
                            string name = Console.ReadLine();
                            if (name == string.Empty)
                            {
                                name = "Неизвестно";
                            }

                            Console.WriteLine("Введите год премьеры фильма: ");
                            int year = 0;
                            bool yearError = false;
                            do
                            {
                                int choiceYear = Convert.ToInt32(Console.ReadLine());
                                if (choiceYear > 1895 && choiceYear < 2021)
                                {
                                    year = choiceYear;
                                    yearError = false;
                                }
                                else
                                {
                                    Console.WriteLine("Введите правильный год премьеры!");
                                    yearError = true;
                                }
                            }
                            while (yearError == true);

                            Console.WriteLine("Введите жанр фильма: (Д - драма, К - комедия, М - мелодрама, Б - боевик, А - мультфильм)");
                            var type = Type.Неизвестен;
                            bool typeError = false;
                            do // public enum Type { Д, К, М, Б, А }
                            {
                                string choiceType = Console.ReadLine();

                                if (choiceType == "Д")
                                {
                                    type = Type.Д;
                                    typeError = false;
                                }
                                else if (choiceType == "К" || choiceType == "K")
                                {
                                    type = Type.К;
                                    typeError = false;
                                }
                                else if (choiceType == "М" || choiceType == "M")
                                {
                                    type = Type.М;
                                    typeError = false;
                                }
                                else if (choiceType == "Б")
                                {
                                    type = Type.Б;
                                    typeError = false;
                                }
                                else if (choiceType == "А" || choiceType == "A")
                                {
                                    type = Type.А;
                                    typeError = false;
                                }
                                else
                                {
                                    Console.WriteLine("Введите правильний жанр фильма! (Д - драма, К - комедия, М - мелодрама, Б - боевик, А - мультфильм)");
                                    typeError = true;
                                }
                            }
                            while (typeError == true);

                            Film newFilm;
                            newFilm.title = title;
                            newFilm.name = name;
                            newFilm.year = year;
                            newFilm.type = type;
                            Table.Add(newFilm);

                            Log newLog;
                            newLog.title = title;
                            newLog.time = DateTime.Now;
                            newLog.operation = "Запись добавлена!";
                            Log.Add(newLog);

                            time_1 = DateTime.Now;
                            TimeSpan timeInterval_2 = time_1 - time_2;
                            if (timeInterval_1 < timeInterval_2)
                            {
                                timeInterval_1 = timeInterval_2;
                            }
                            time_2 = newLog.time;
                        }
                        break;

                    case 3: // Удалить запись
                        {
                            Console.Write("Введите номер записи: ");

                            bool deleteError = false;
                            do // (1, 2, 3, 4, 5, 6, 7)
                            {
                                deleteError = false;

                                int choiceNumberDelete = Convert.ToInt32(Console.ReadLine());
                                if (choiceNumberDelete > 0 && choiceNumberDelete < Table.Count)
                                {
                                    Log newDelete;
                                    newDelete.title = Table[choiceNumberDelete - 1].title;
                                    newDelete.time = DateTime.Now;
                                    newDelete.operation = "Запись удалена!";
                                    Log.Add(newDelete);
                                    Table.RemoveAt(choiceNumberDelete - 1);

                                    time_1 = DateTime.Now;
                                    TimeSpan timeInterval_2 = time_1 - time_2;
                                    if (timeInterval_1 < timeInterval_2)
                                    {
                                        timeInterval_1 = timeInterval_2;
                                    }
                                    time_2 = newDelete.time;

                                }
                                else
                                {
                                    Console.WriteLine("Введите правильный номер! (1, 2, 3, 4, 5, 6, 7)");
                                    deleteError = true;
                                }
                            }
                            while (deleteError == true);
                        }
                        break;

                    case 4: // Обновить запись
                        {
                            Console.Write("Введите номер записи: ");

                            bool changeError = false;
                            do // (1, 2, 3, 4, 5, 6, 7)
                            {
                                int choiceNumberChange = Convert.ToInt32(Console.ReadLine());
                                if (choiceNumberChange > 0 && choiceNumberChange < Table.Count)
                                {
                                    string oldTitle = Table[choiceNumberChange - 1].title;
                                    Console.WriteLine("Введите новое название фильма: ");
                                    string title = Console.ReadLine();
                                    if (title == String.Empty)
                                    {
                                        title = oldTitle;
                                    }

                                    string oldName = Table[choiceNumberChange - 1].name;
                                    Console.WriteLine("Введите нового режессера фильма: ");
                                    string name = Console.ReadLine();
                                    if (name == String.Empty)
                                    {
                                        name = oldName;
                                    }

                                    int oldYear = Table[choiceNumberChange - 1].year;
                                    Console.WriteLine("Введите новый год премьеры фильма: ");
                                    int year = Convert.ToInt32(Console.ReadLine());
                                    if (year > 1895 && year < 2021)
                                    {
                                        changeError = false;
                                    }
                                    else if (year == 0)
                                    {
                                        year = oldYear;
                                        changeError = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Введите правильный год премьеры!");
                                        changeError = true;
                                    }

                                    var oldType = Table[choiceNumberChange - 1].type;
                                    Console.WriteLine("Введите новый жанр фильма: (Д - драма, К - комедия, М - мелодрама, Б - боевик, А - мультфильм)");
                                    var type = Type.Неизвестен;

                                    bool typeError = false;
                                    do // { Д, К, М, Б, А }
                                    {
                                        string choiceType = Console.ReadLine();
                                        if (choiceType == "Д")
                                        {
                                            type = Type.Д;
                                            typeError = false;
                                        }
                                        else if (choiceType == "К" || choiceType == "K")
                                        {
                                            type = Type.К;
                                            typeError = false;
                                        }
                                        else if (choiceType == "М" || choiceType == "M")
                                        {
                                            type = Type.М;
                                            typeError = false;
                                        }
                                        else if (choiceType == "Б")
                                        {
                                            type = Type.Б;
                                            typeError = false;
                                        }
                                        else if (choiceType == "А" || choiceType == "A")
                                        {
                                            type = Type.А;
                                            typeError = false;
                                        }
                                        else if (choiceType == String.Empty)
                                        {
                                            type = oldType;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Введите правильний жанр фильма! (Д - драма, К - комедия, М - мелодрама, Б - боевик, А - мультфильм)");
                                            typeError = true;
                                        }
                                    }
                                    while (typeError == true);
                                }
                                else
                                {
                                    Console.WriteLine("Введите правильный номер! (1, 2, 3, 4, 5, 6, 7)");
                                }
                            }
                            while (changeError == true);
                        }
                        break;

                    case 5: // Поиск записей
                        {
                            Console.WriteLine("Введите жанр фильма: (Д - драма, К - комедия, М - мелодрама, Б - боевик, А - мультфильм)");

                            bool seatchError = false;
                            do // { Д, К, М, Б, А }
                            {
                                Char choiceNumberSearch = Convert.ToChar(Console.ReadLine());
                                if (choiceNumberSearch == 'Д')
                                {
                                    var records = Table.FindAll(i => i.type == Type.Д);
                                    foreach (var record in records)
                                    {
                                        record.DisplayInfo();
                                    }
                                    seatchError = false;
                                }
                                else if (choiceNumberSearch == 'К' || choiceNumberSearch == 'K')
                                {
                                    var records = Table.FindAll(i => i.type == Type.К);
                                    foreach (var record in records)
                                    {
                                        record.DisplayInfo();
                                    }
                                    seatchError = false;
                                }
                                else if (choiceNumberSearch == 'М' || choiceNumberSearch == 'M')
                                {
                                    var records = Table.FindAll(i => i.type == Type.М);
                                    foreach (var record in records)
                                    {
                                        record.DisplayInfo();
                                    }
                                    seatchError = false;
                                }
                                else if (choiceNumberSearch == 'Б')
                                {
                                    var records = Table.FindAll(i => i.type == Type.Б);
                                    foreach (var record in records)
                                    {
                                        record.DisplayInfo();
                                    }
                                    seatchError = false;
                                }
                                else if (choiceNumberSearch == 'А' || choiceNumberSearch == 'A')
                                {
                                    var records = Table.FindAll(i => i.type == Type.А);
                                    foreach (var record in records)
                                    {
                                        record.DisplayInfo();
                                    }
                                    seatchError = false;
                                }
                                else
                                {
                                    Console.WriteLine("Введите правильный жанр фильма: (Д - драма, К - комедия, М - мелодрама, Б - боевик, А - мультфильм)");
                                    seatchError = true;
                                }
                            }
                            while (seatchError == true);
                        }
                        break;

                    case 6: // Просмотреть лог
                        {
                            for (int i = 0; i < Log.Count; i++)
                            {
                                Log[i].DisplayLog();
                            }
                            Console.WriteLine();
                            Console.WriteLine(timeInterval_1 + " - Самый долгий период бездействия пользователя");
                        }
                        break;

                    case 7: // Выход
                        {

                            int type = 0;

                            switch (type) //save
                            {
                                default:
                                //case 0:
                                //    StringBuilder tmpText = new StringBuilder();
                                //    foreach (Film hd in Table)
                                //    {
                                //        tmpText.Append(hd.ToString());
                                //    }
                                //    File.WriteAllText(@"d:\promapirovanie\Lab5Zad1\lab_text.dat", tmpText.ToString());
                                //    break;


                                case 1:
                                    using (BinaryWriter writer = new BinaryWriter(File.Open(@"d:\promapirovanie\Lab5Zad1\lab_binary.dat", FileMode.OpenOrCreate)))
                                    {
                                        foreach (Film hd in Table)
                                            writer.Write(hd.ToString());

                                    }
                                    break;


                            }
                            switch (type)//load
                            {
                                default:
                                //case 0:
                                //    String pathText = @"d:\promapirovanie\Lab5Zad1\lab_text.dat";
                                //    if (!File.Exists(pathText))
                                //        return;

                                //    String[] str = File.ReadAllLines(pathText);
                                //    Table.Clear();
                                //    foreach (String row in str)
                                //    {
                                //        Film hd = new Film();
                                //        Console.WriteLine($"test=>{row}");

                                //        Table.Add(hd.fromString(row));
                                //    }

                                //    break;

                                case 1:
                                    String pathBinary = @"d:\promapirovanie\Lab5Zad1\lab_binary.dat";
                                    if (!File.Exists(pathBinary))
                                        return;

                                    using (BinaryReader reader = new BinaryReader(File.Open(pathBinary, FileMode.Open)))
                                    {
                                        while (reader.PeekChar() > -1)
                                        {
                                            string row = reader.ReadString();

                                            Film hd = new Film();
                                            Table.Add(hd.fromString(row));
                                        }


                                    }


                                    break;

                            }
                            
                        
                           
                            
                            optionError = false;
                        }
                        break;
                    case 8: 
                        {
                            Console.WriteLine("выберите тип соритровки:\n1 - name,Выбором <");
                            Console.WriteLine("\n 2 - name,Выбором >");
                            Console.WriteLine("\n 3 - title,Выбором <");
                            Console.WriteLine("\n 4 - title,Выбором >");
                            Console.WriteLine("\n 5 - year,Выбором <");
                            Console.WriteLine("\n 6 - year,Выбором >");
                            Console.WriteLine("\n 10 - не сортировать");
                            int selecttype = 1;
                            do
                            {
                                try
                                {

                                    selecttype = Int32.Parse(Console.ReadLine());
                                }
                                catch { selecttype = 0; }

                                if (selecttype > 0 && selecttype <= 6 || selecttype == 10)
                                    break;
                                else { Console.WriteLine("ошибочный выбор"); }

                            }
                            while (true);

                            List<Film> tmplist = new List<Film>();
                            tmplist.Clear();

                            switch (selecttype)
                            {
                                case 1:  tmplist.AddRange(ViborSort(Table.ToArray(),"name",true));
                                        break;
                                case 2:
                                    tmplist.AddRange(ViborSort(Table.ToArray(),"name",false));
                                    break;
                                case 3:
                                    tmplist.AddRange(ViborSort(Table.ToArray(),"title",true));
                                    break;

                                case 4:
                                    tmplist.AddRange(ViborSort(Table.ToArray(),"title",false));
                                    break;
                                case 5:
                                    tmplist.AddRange(ViborSort(Table.ToArray(), "year", true));
                                    break;
                                case 6:
                                    tmplist.AddRange(ViborSort(Table.ToArray(), "year", false));
                                    break;
                                case 10:
                                    break;
                            }
                            
                            printTableSystem(tmplist);
                            
                            
                        }
                        break;


                        default:
                        Console.WriteLine("Введите правильную команду!");
                        optionError = true;
                        break;




                }
            }
            while (optionError);

        }


    }
}