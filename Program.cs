using System;
using System.IO;


namespace L5
{
    abstract public class SubwayStation
    {
        public static int arS = Sizing() - 1;
        public string[] Title = new string[arS];
        public int[] Year = new int[arS];

        public string Heading;
        public static int Sizing()
        {
            var fileStream = new FileStream(@"C:\Users\User\source\repos\L5\text.txt", FileMode.Open);
            using var f = new StreamReader(fileStream);
            string mains = f.ReadToEnd();
            string[] lines = mains.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            int size = lines.Length;
            f.Close();
            return size;
        }
    }
    public class Hour : SubwayStation
    {
        public int[] Passengers = new int[arS];
        public string[] Comment = new string[arS];
        public Hour()
        {

        }
        public Hour(string title, int year, int passengers, string comment)
        {
            if (title.Length == 0) throw new System.Exception("Title is not found.");
            if (year < 0) throw new System.Exception("Year of opening is incorrect.");
            if (passengers < 0) throw new System.Exception("Number of passengers per hour is incorrect.");
            if (comment.Length == 0) throw new System.Exception("Comment is not found.");
        }
        public void Exiting()
        {
            var fileStream = new FileStream(@"C:\Users\User\source\repos\L5\text.txt", FileMode.Truncate);
            using var f = new StreamWriter(fileStream);
            string ex = "";
            for (int b = 0; b < Title.Length; b++)
            {
                if (Title[b].Length > ex.Length) ex = Title[b];
            }
            for (int b = 0; b < Title.Length; b++)
            {
                if (Title[b].Length != ex.Length)
                {
                    int dif = ex.Length - Title[b].Length;
                    for (int a = 0; a < dif; a++) Title[b] += " ";
                }
            }
            Console.WriteLine(Heading);
            f.WriteLine(Heading);
            for (int i = 0; i < Title.Length; i++)
            {

                if (i == Passengers.Length - 1)
                {
                    f.Write(Title[i] + "\t\t\t\t" + Year[i] + "\t\t\t\t" + Passengers[i] + "\t\t\t\t" + Comment[i]);
                }
                else f.WriteLine(Title[i] + "\t\t\t\t" + Year[i] + "\t\t\t\t" + Passengers[i] + "\t\t\t\t" + Comment[i]);
                Console.WriteLine(Title[i] + "\t\t\t\t" + Year[i] + "\t\t\t\t" + Passengers[i] + "\t\t\t\t" + Comment[i]);
            }
            f.Close();
        }
        public void Opening()
        {
            var fileStream = new FileStream(@"C:\Users\User\source\repos\L5\text.txt", FileMode.Open);
            using var f = new StreamReader(fileStream);
            string mains = f.ReadToEnd();
            string[] lines = mains.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            Heading = lines[0];
            int count = lines.Length;
            int t = 0;
            for (int i = 1; i < count; i++)
            {
                if (lines[i].Length > t) t = lines[i].Length;

            }
            char[,] temps = new char[t, count - 1];
            for (int i = 1; i < count; i++)
            {
                char[] tem = lines[i].ToCharArray();
                for (int j = 0; j < lines[i].Length; j++)
                {
                    temps[j, i - 1] = tem[j];
                }
            }
            bool start = false;
            bool go = false;
            bool yes = false;
            string tit = "";
            string yea = "";
            string pas = "";
            string com = "";
            for (int i = 1; i < count; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (!start)
                    {

                        if (temps[j, i - 1] == '\t')
                        {
                            start = true;
                            Title[i - 1] = tit;
                            tit = "";
                            j += 3;
                        }
                        else if (!(char.IsControl(temps[j, i - 1]))) tit = tit + temps[j, i - 1];
                    }
                    else if (!go)
                    {
                        if (temps[j, i - 1] == '\t')
                        {
                            go = true;
                            Year[i - 1] = Int32.Parse(yea);
                            yea = "";
                            j += 3;
                        }
                        if (char.IsNumber(temps[j, i - 1])) yea = yea + temps[j, i - 1];
                    }
                    else if (!yes)
                    {
                        if (temps[j, i - 1] == '\t')
                        {
                            yes = true;
                            Passengers[i - 1] = Int32.Parse(pas);
                            pas = "";
                            j += 3;
                        }
                        if (char.IsNumber(temps[j, i - 1])) pas = pas + temps[j, i - 1];
                    }
                    else
                    {
                        com = com + temps[j, i - 1];
                        if (j == lines[i].Length - 1)
                        {
                            Comment[i - 1] = com;
                            com = "";
                            start = false;
                            go = false;
                            yes = false;
                            break;
                        }
                    }
                }
            }
            string ex = "";
            for (int b = 0; b < Title.Length; b++)
            {
                if (Title[b].Length > ex.Length) ex = Title[b];
            }
            for (int b = 0; b < Title.Length; b++)
            {
                if (Title[b].Length != ex.Length)
                {
                    int dif = ex.Length - Title[b].Length;
                    for (int a = 0; a < dif; a++) Title[b] += " ";
                }
            }
            Console.WriteLine(Heading);
            for (int i = 0; i < Title.Length; i++)
            {
                Console.WriteLine(Title[i] + "\t\t\t\t" + Year[i] + "\t\t\t\t" + Passengers[i] + "\t\t\t\t" + Comment[i]);
            }
        }
        public void Adding(string title, int year, int passengers, string comment)
        {
            var fileStream = new FileStream(@"C:\Users\User\source\repos\L5\text.txt", FileMode.Truncate);
            using var f = new StreamWriter(fileStream);
            Array.Resize(ref Title, Title.Length + 1);
            Array.Resize(ref Year, Year.Length + 1);
            Array.Resize(ref Passengers, Passengers.Length + 1);
            Array.Resize(ref Comment, Comment.Length + 1);
            Title[Title.Length - 1] = title;
            Year[Year.Length - 1] = year;
            Passengers[Passengers.Length - 1] = passengers;
            Comment[Comment.Length - 1] = comment;
            Console.WriteLine(Heading);
            f.WriteLine(Heading);
            for (int i = 0; i < Passengers.Length; i++)
            {
                if (i == Passengers.Length - 1)
                {
                    f.Write(Title[i] + "\t\t\t\t" + Year[i] + "\t\t\t\t" + Passengers[i] + "\t\t\t\t" + Comment[i]);
                }
                else f.WriteLine(Title[i] + "\t\t\t\t" + Year[i] + "\t\t\t\t" + Passengers[i] + "\t\t\t\t" + Comment[i]);
                Console.WriteLine(Title[i] + "\t\t\t\t" + Year[i] + "\t\t\t\t" + Passengers[i] + "\t\t\t\t" + Comment[i]);
            }
            f.Close();
        }
        public void Searching(int q)
        {
            if (q >= 0)
            {
                Console.WriteLine("____________________________________________________________________________________________________________________________________\n");
                Console.WriteLine(Title[q] + "\t\t\t\t" + Year[q] + "\t\t\t\t" + Passengers[q] + "\t\t\t\t" + Comment[q]);
                Console.WriteLine("____________________________________________________________________________________________________________________________________\n");
            }
            else Console.WriteLine(Heading);
        }
        public void Editing(int q, string title, int year, int passengers, string comment)
        {
            var fileStream = new FileStream(@"C:\Users\User\source\repos\L5\text.txt", FileMode.Truncate);
            using var f = new StreamWriter(fileStream);
            if (title.Length == 0) throw new System.Exception("Title is not found.");
            if (year < 0) throw new System.Exception("Year of opening is incorrect.");
            if (passengers < 0) throw new System.Exception("Number of passengers per hour is incorrect.");
            if (comment.Length == 0) throw new System.Exception("Comment is not found.");
            Title[q] = title;
            Year[q] = year;
            Passengers[q] = passengers;
            Comment[q] = comment;
            Console.WriteLine(Heading);
            f.WriteLine(Heading);
            for (int i = 0; i < Passengers.Length; i++)
            {
                if (i == Passengers.Length - 1)
                {
                    f.Write(Title[i] + "\t\t\t\t" + Year[i] + "\t\t\t\t" + Passengers[i] + "\t\t\t\t" + Comment[i]);
                }
                else f.WriteLine(Title[i] + "\t\t\t\t" + Year[i] + "\t\t\t\t" + Passengers[i] + "\t\t\t\t" + Comment[i]);
                Console.WriteLine(Title[i] + "\t\t\t\t" + Year[i] + "\t\t\t\t" + Passengers[i] + "\t\t\t\t" + Comment[i]);
            }
            f.Close();
        }
        public void Deleting(int q)
        {
            var fileStream = new FileStream(@"C:\Users\User\source\repos\L5\text.txt", FileMode.Truncate);
            using var f = new StreamWriter(fileStream);
            string[] a = Title;
            int[] ar = Year;
            int[] arr = Passengers;
            string[] arrr = Comment;
            bool start = false;
            for (int i = 0; i < Passengers.Length; i++)
            {
                if (i == q) start = true;
                if (i == Passengers.Length - 1)
                {
                    Array.Resize(ref a, a.Length - 1);
                    Array.Resize(ref ar, ar.Length - 1);
                    Array.Resize(ref arr, arr.Length - 1);
                    Array.Resize(ref arrr, arrr.Length - 1);
                    Title = a;
                    Year = ar;
                    Passengers = arr;
                    Comment = arrr;
                    break;
                }
                if (start)
                {
                    a[i] = Title[i + 1];
                    ar[i] = Year[i + 1];
                    arr[i] = Passengers[i + 1];
                    arrr[i] = Comment[i + 1];
                }
            }
            Console.WriteLine(Heading);
            f.WriteLine(Heading);
            for (int i = 0; i < Passengers.Length; i++)
            {
                if (i == Passengers.Length - 1)
                {
                    f.Write(Title[i] + "\t\t\t\t" + Year[i] + "\t\t\t\t" + Passengers[i] + "\t\t\t\t" + Comment[i]);
                }
                else f.WriteLine(Title[i] + "\t\t\t\t" + Year[i] + "\t\t\t\t" + Passengers[i] + "\t\t\t\t" + Comment[i]);
                Console.WriteLine(Title[i] + "\t\t\t\t" + Year[i] + "\t\t\t\t" + Passengers[i] + "\t\t\t\t" + Comment[i]);
            }
            f.Close();
        }
        public int TotalPass()
        {
            int totals = 0;
            for (int i = 0; i < Passengers.Length; i++)
            {
                totals += Passengers[i];
            }
            return totals;
        }
        public void MajorHour()
        {
            int min = int.MaxValue;
            for (int i = 0; i < Passengers.Length; i++)
            {
                if (Passengers[i] < min)
                {
                    min = Passengers[i];
                }
            }
            for (int n = 0; n < Passengers.Length; n++)
            {
                if (min == Passengers[n])
                {
                    Console.WriteLine("____________________________________________________________________________________________________________________________________\n");
                    Console.WriteLine(Title[n] + "\t\t\t\t" + Year[n] + "\t\t\t\t" + Passengers[n] + "\t\t\t\t" + Comment[n]);
                    Console.WriteLine("____________________________________________________________________________________________________________________________________\n");
                }
            }
        }
        public void WordsInCom()
        {
            int n = 0;
            string ex = "";
            for (int b = 0; b < Comment.Length; b++)
            {
                if (Comment[b].Length > ex.Length) ex = Comment[b];
            }
            for (int b = 0; b < Comment.Length; b++)
            {
                if (Comment[b].Length != ex.Length)
                {
                    int dif = ex.Length - Comment[b].Length;
                    for (int a = 0; a < dif; a++) Comment[b] += " ";
                }
            }
            char[,] temps = new char[ex.Length, Comment.Length];
            char[] tem = new char[ex.Length];
            for (int b = 0; b < Comment.Length; b++)
            {
                tem = Comment[b].ToCharArray();
                for (int a = 0; a < tem.Length; a++)
                {
                    temps[a, b] = tem[a];
                }
            }
            bool word = false;
            int[] w = new int[Comment.Length];
            int j = 0;
            int k = 0;
            for (j = 0; j < Comment.Length; j++)
            {
                for (k = 0; k < ex.Length; k++)
                {
                    if ((char.IsLetter(temps[k, j])) || (char.IsNumber(temps[k, j])) || (temps[k, j] == '\''))
                    {
                        word = true;
                        if (k == ex.Length - 1) w[j]++;
                    }
                    else
                    {
                        if (word) w[j]++;
                        word = false;
                    }
                }
            }
            int max = 0;
            int e = 0;
            for (j = 0; j < Comment.Length; j++)
            {
                if (w[j] > max) max = w[j];
            }
            for (j = 0; j < Comment.Length; j++)
            {
                if (w[j] == max)
                {
                    Console.WriteLine(Title[j] + "\t\t\t\t" + Year[j] + "\t\t\t\t" + Passengers[j] + "\t\t\t\t" + Comment[j]);
                    Console.WriteLine("____________________________________________________________________________________________________________________________________\n");
                }
            }
        }
    }
    class Program
    {
        static void Main()
        {
            Hour r = new Hour();
            string task = "";
            string func = "";
            string choice = "";
            string ti = "";
            string ye = "";
            string pa = "";
            string co = "";
            int yearo = -1;
            int pasperh = -1;
            do
            {
                Console.WriteLine("\nEnter 'o' to open the file and 'e' to exit.");
                task = Console.ReadLine();
                switch (task)
                {
                    case "e": r.Exiting(); break;
                    case "o":
                        {
                            r.Opening();
                            Console.WriteLine("\nThe total number of passengers per hour is " + r.TotalPass());
                            Console.WriteLine("\nThe station(s) that has(have) the minimal number of passengers: ");
                            r.MajorHour();
                            Console.WriteLine("\nThe station(s) that has(have) the maximum number of words in comment: ");
                            r.WordsInCom();
                            Console.WriteLine("\n");
                            do
                            {
                                Console.WriteLine("\n'a' to add new information, 's' to search for editing and deleting and 'e' to exit.");
                                func = Console.ReadLine();
                                switch (func)
                                {
                                    case "e": r.Exiting(); task = "e"; break;
                                    case "a":
                                        {
                                            do
                                            {
                                                Console.WriteLine("\nEnter the title: ");
                                                ti = Console.ReadLine();
                                            } while (ti.Length == 0);
                                            do
                                            {
                                                Console.WriteLine("\nEnter the year when station was opened: ");
                                                ye = Console.ReadLine();
                                                yearo = Int32.Parse(ye);
                                            } while (yearo < 0);
                                            do
                                            {
                                                Console.WriteLine("\nEnter the number of passengers per hour: ");
                                                pa = Console.ReadLine();
                                                pasperh = Int32.Parse(pa);
                                            } while (pasperh < 0);
                                            do
                                            {
                                                Console.WriteLine("\nEnter the comment: ");
                                                co = Console.ReadLine();
                                            } while (co.Length == 0);
                                            r.Adding(ti, yearo, pasperh, co);
                                            break;
                                        }
                                    case "s":
                                        {
                                            int strings = -1;
                                            string strins = "";
                                            do
                                            {
                                                Console.WriteLine("\nEnter the number of string you need: ");
                                                strins = Console.ReadLine();
                                                strings = Int32.Parse(strins);
                                            } while (strings < 1);
                                            strings = strings - 1;
                                            r.Searching(strings);
                                            do
                                            {
                                                Console.WriteLine("\nEnter 'e' to edit, 'd' to delete and 'r' to return.");
                                                choice = Console.ReadLine();
                                                switch (choice)
                                                {
                                                    case "r": break;
                                                    case "e":
                                                        {
                                                            string tt = "";
                                                            string yy = "";
                                                            string pp = "";
                                                            string cc = "";
                                                            int yeo = -1;
                                                            int paper = -1;
                                                            do
                                                            {
                                                                Console.WriteLine("\nEnter the title to replace the previous one: ");
                                                                tt = Console.ReadLine();
                                                            } while (tt.Length == 0);
                                                            do
                                                            {
                                                                Console.WriteLine("\nEnter the year when station was opened to replace the previous one: ");
                                                                yy = Console.ReadLine();
                                                                yeo = Int32.Parse(yy);
                                                            } while (yeo < 0);
                                                            do
                                                            {
                                                                Console.WriteLine("\nEnter the number of passengers per hour to replace the previous one: ");
                                                                pp = Console.ReadLine();
                                                                paper = Int32.Parse(pp);
                                                            } while (paper < 0);
                                                            do
                                                            {
                                                                Console.WriteLine("\nEnter the comment to replace the previous one: ");
                                                                cc = Console.ReadLine();
                                                            } while (cc.Length == 0);
                                                            r.Editing(strings, tt, yeo, paper, cc);
                                                            break;
                                                        }
                                                    case "d":
                                                        {
                                                            r.Deleting(strings);
                                                            choice = "r";
                                                            break;
                                                        }
                                                    default: Console.WriteLine("Try again. ('e' to edit, 'd' to delete and 'r' to return)"); break;
                                                }
                                            } while (choice != "r");
                                            break;
                                        }
                                    default: Console.WriteLine("Try again. ('a' to add, 's' to search and 'e' to exit)"); break;
                                }
                            } while (func != "e");
                            break;
                        }
                    default: Console.WriteLine("Try again. ('o' to open file and 'e' to exit)"); break;
                }
            } while (task != "e");
        }
    }
}

