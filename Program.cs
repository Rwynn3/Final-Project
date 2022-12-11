using System;
using System.CodeDom;
using System.Collections;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace TimeClock_Console
{
    internal class Program
    {

        public static readonly string adminUser = "admin";
        public static readonly string adminPass = "admin123";



        public static ArrayList usernames = new ArrayList();
        public static ArrayList passwords = new ArrayList();
        public static ArrayList empID = new ArrayList();
        public static ArrayList payrate = new ArrayList();






        public static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public static string usernamePath = desktopPath + "\\usernames.txt";
        public static string passwordPath = desktopPath + "\\passwords.txt";
        public static string empIDPath = desktopPath + "\\empID.txt";
        public static string payratePath = desktopPath + "\\payRate.txt";


        static void Main(string[] args)
        {




            if (!File.Exists(usernamePath) && !File.Exists(passwordPath) && !File.Exists(empIDPath) && !File.Exists(payratePath))
            {
                File.Create(usernamePath);
                File.Create(passwordPath);
                File.Create(empIDPath);
                File.Create(payratePath);

                Environment.Exit(0);
            }
            else
            {

                StreamReader reader = new StreamReader(usernamePath);
                ArrayList newuser = new ArrayList();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    newuser.Add(line);
                }
                reader.Close();


                for (int i = 0; i < newuser.Count; i++)
                {
                    usernames.Add(newuser[i]);
                }

                StreamReader reader2 = new StreamReader(passwordPath);
                ArrayList newpass = new ArrayList();
                while ((line = reader2.ReadLine()) != null)
                {
                    newpass.Add(line);
                }
                reader2.Close();


                for (int i = 0; i < newpass.Count; i++)
                {
                    passwords.Add(newpass[i]);
                }
                StreamReader reader3 = new StreamReader(empIDPath);
                ArrayList newED = new ArrayList();
                while ((line = reader3.ReadLine()) != null)
                {
                    newED.Add(line);
                }
                reader3.Close();
                for (int i = 0; i < newED.Count; i++)
                {
                    empID.Add(newED[i]);
                }
                StreamReader reader4 = new StreamReader(payratePath);
                ArrayList newpay = new ArrayList();
                while ((line = reader4.ReadLine()) != null)
                {
                    newpay.Add(line);
                }
                reader4.Close();


                for (int i = 0; i < newpay.Count; i++)
                {
                    payrate.Add(newpay[i]);
                }
            }

            bool go = true;


            while (go)
            {
                Console.WriteLine("Welcome to TimeClock");
                Console.WriteLine("Please Enter your your Username");
                string username = Console.ReadLine().Trim();
                Console.WriteLine("Please Enter your Password");
                string password = Console.ReadLine().Trim();

                if (username == adminUser && password == adminPass)
                {
                    adminMenu();
                }
                else if (usernames.Contains(username) && passwords.Contains(password))
                {
                    int index = usernames.IndexOf(username);
                    empMenu(index);
                }
                else
                {
                    Console.WriteLine("The Username or Password is incorrect");
                }




            }



        }


        public static void empMenu(int index)
        {
            bool go = true;

            string emID = (string)empID[index];

            while (go)
            {
                Console.WriteLine("What would you like to do? \n" +
                    "1) Clock In \n" +
                    "2) Clock Out \n" +
                    "3) View Check Stubs \n" +
                    "4) Logout");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Your are going to clock in");
                        GetClockIn(emID);
                        break;

                    case "2":
                        Console.WriteLine("Your are going to clock out");
                        GetClockOut(emID);
                        break;

                    case "3":
                        Console.WriteLine("Your are going to view check stubs");
                        viewCheckStubs(emID);
                        break;

                    case "4":
                        Console.WriteLine("You are going to Logout");
                        go = false;
                        break;

                    default:
                        Console.WriteLine("This is not a correct choice");
                        break;


                }


            }
        }

        // This is a test for git

        public static void GetClockIn(string emID)
        {
            ArrayList clockIN = new ArrayList();
            ArrayList clockOUT = new ArrayList();

            if (File.Exists(desktopPath + "\\EmID-" + emID + ".txt"))
            {
                StreamReader reader = new StreamReader(desktopPath + "\\EmID-" + emID + ".txt");

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    clockIN.Add(line);
                }
                reader.Close();

                if (File.Exists(desktopPath + "\\EmID-out" + emID + ".txt"))
                {

                    StreamReader reader2 = new StreamReader(desktopPath + "\\EmID-out" + emID + ".txt");

                    string line2;
                    while ((line2 = reader2.ReadLine()) != null)
                    {
                        clockOUT.Add(line2);
                    }
                    reader2.Close();

                }


            }

            if (clockIN.Count < clockOUT.Count || clockIN.Count - clockOUT.Count > 1)
            {
                Console.WriteLine("Error Clocking IN");
            }
            else
            {
                DateTime time = DateTime.Now;

                string time1 = time.ToString("HH:mm:ss");
                clockIN.Add(time1);
                Console.WriteLine("You have clocked IN at: " + time1);



                TextWriter writer = new StreamWriter(desktopPath + "\\EmID-" + emID + ".txt");
                for (int i = 0; i < clockIN.Count; i++)
                {
                    writer.WriteLine(clockIN[i]);

                }
                writer.Close();


            }
           ;

        }

        public static void GetClockOut(string emID)
        {

            ArrayList clockIN = new ArrayList();
            ArrayList clockOUT = new ArrayList();

            if (File.Exists(desktopPath + "\\EmID-" + emID + ".txt"))
            {
                StreamReader reader = new StreamReader(desktopPath + "\\EmID-" + emID + ".txt");

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    clockIN.Add(line);
                }
                reader.Close();

                if (File.Exists(desktopPath + "\\EmID-out" + emID + ".txt"))
                {

                    StreamReader reader2 = new StreamReader(desktopPath + "\\EmID-out" + emID + ".txt");

                    string line2;
                    while ((line2 = reader2.ReadLine()) != null)
                    {
                        clockOUT.Add(line2);
                    }
                    reader2.Close();

                }


            }
            if (clockOUT.Count >= clockIN.Count)
            {
                Console.WriteLine("Error you have not clocked in");
            }
            else
            {

                DateTime time = DateTime.Now;

                string time1 = time.ToString("HH:mm:ss");

                clockOUT.Add(time1);
                Console.WriteLine("You have clocked OUT at: " + time1);



                TextWriter writer = new StreamWriter(desktopPath + "\\EmID-out" + emID + ".txt");
                for (int i = 0; i < clockOUT.Count; i++)
                {
                    writer.WriteLine(clockOUT[i]);

                }
                writer.Close();


            }


        }

        public static void viewCheckStubs(string emID)
        {


            if (!File.Exists(desktopPath + "\\EmID-out" + emID + ".txt"))
            {
                Console.WriteLine("Error no clock in data");
            }
            else
            {
                ArrayList clockIN = new ArrayList();
                ArrayList clockOUT = new ArrayList();

                if (File.Exists(desktopPath + "\\EmID-" + emID + ".txt"))
                {
                    StreamReader reader = new StreamReader(desktopPath + "\\EmID-" + emID + ".txt");

                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        clockIN.Add(line);
                    }
                    reader.Close();

                    if (File.Exists(desktopPath + "\\EmID-out" + emID + ".txt"))
                    {

                        StreamReader reader2 = new StreamReader(desktopPath + "\\EmID-out" + emID + ".txt");

                        string line2;
                        while ((line2 = reader2.ReadLine()) != null)
                        {
                            clockOUT.Add(line2);
                        }
                        reader2.Close();

                    }




                    Console.WriteLine("Clock IN:       Clock Out:");

                    Console.WriteLine(clockOUT.ToString());

                    for (int i = 0; i < clockOUT.Count; i++)
                    {
                        Console.WriteLine(clockIN[i] + "          " + clockOUT[i]);
                    }


                }




            }


        }
        public static void adminMenu()
        {
            bool go = true;
            Console.WriteLine("Welcome Admin");
            while (go)
            {

                Console.WriteLine("What would you like to do? \n" +
                    "1) Edit/Remove Employee \n" +
                    "2) Add Employee \n" +
                    "3) Edit Employee Times \n" +
                    "4) Logout");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("We are going to edit an Employee");
                        editEmp();
                        break;

                    case "2":
                        Console.WriteLine("We are going to add a employee");
                        addEmp();
                        break;

                    case "3":
                        Console.WriteLine("We are going to Edit Employee Times");

                        Console.WriteLine();

                        Console.WriteLine("What is the employee id that you would like to edit");
                        string id = Console.ReadLine();

                        if (!empID.Contains(id))
                        {
                            Console.WriteLine("Error there is no Id of that value");
                        }
                        else
                        {
                            editTimes(id);
                        }

                        break;

                    case "4":
                        Console.WriteLine("we will logout");
                        go = false;
                        break;

                    default:
                        Console.WriteLine("This is not a correct Response");
                        break;
                }

            }


        }



        public static void addEmp()
        {
            Console.WriteLine("What is the Employee username");
            string username = Console.ReadLine();

            if (usernames.Contains(username))
            {

                Console.Clear();
                Console.WriteLine("This username already exists");
                return;
            }

            Console.WriteLine("What is the Employee Password");
            string password = Console.ReadLine();

            if (passwords.Contains(password))
            {
                Console.Clear();
                Console.WriteLine("Error this password is already in use");
                return;
            }

            Console.WriteLine("What is the Employee Id");
            string emID = Console.ReadLine();

            if (empID.Contains(emID))
            {
                Console.Clear();
                Console.WriteLine("Error this employee id is already being used");
                return;
            }

            Console.WriteLine("What is the Employee Payrate");
            string newRate = Console.ReadLine();

            double pay = Convert.ToDouble(newRate);

            usernames.Add(username);
            TextWriter writer = new StreamWriter(usernamePath);
            for (int i = 0; i < usernames.Count; i++)
            {
                writer.WriteLine(usernames[i]);
            }

            writer.Close();
            passwords.Add(password);
            TextWriter writer2 = new StreamWriter(passwordPath);
            for (int i = 0; i < passwords.Count; i++)
            {
                writer2.WriteLine(passwords[i]);
            }
            writer2.Close();
            empID.Add(emID);
            TextWriter writer3 = new StreamWriter(empIDPath);

            for (int i = 0; i < empID.Count; i++)
            {
                writer3.WriteLine(empID[i]);
            }
            writer3.Close();
            payrate.Add(pay);
            TextWriter writer4 = new StreamWriter(payratePath);
            for (int i = 0; i < payrate.Count; i++)
            {
                writer4.WriteLine(payrate[i]);
            }
            writer4.Close();

        }



        public static void editEmp()
        {

            bool go = true;

            while (go)
            {
                Console.WriteLine("Enter the employee ID that you would like to edit");
                string emID = Console.ReadLine();

                int index = empID.IndexOf(emID);

                if (index == -1)
                {
                    Console.WriteLine("This employee ID does not exist");
                    return;
                }
                else
                {
                    Console.WriteLine("What would you like to edit about this employee? \n" +
                        "1) Edit username of employee \n" +
                        "2) Edit password of employee \n" +
                        "3) Edit payrate of employee");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {

                        case "1":
                            Console.WriteLine("We will edit employee username:");
                            userNameEdit(index);
                            go = false;
                            break;

                        case "2":
                            Console.WriteLine("We will edit the password of the employee");
                            passwordEdit(index);
                            go = false;
                            break;
                        case "3":
                            Console.WriteLine("We will edit the payrate of the Employee");
                            payrateEdit(index);
                            go = false;
                            break;
                        default:
                            Console.WriteLine("This is not a choice");
                            break;
                    }
                }


            }




        }

        public static void userNameEdit(int index)
        {

            Console.WriteLine("What is the new username of the employee");

            string userName = Console.ReadLine();

            if (usernames.Contains(userName))
            {
                Console.WriteLine("Error this username has already been used");
            }
            else if (!usernames.Contains(userName))
            {
                usernames.RemoveAt(index);
                usernames.Add(userName);
            }

            TextWriter writer = new StreamWriter(usernamePath);
            for (int i = 0; i < usernames.Count; i++)
            {
                writer.WriteLine(usernames[i]);
            }
            writer.Close();



        }

        public static void passwordEdit(int index)
        {
            Console.WriteLine("What is the new password for the employee");

            string password = Console.ReadLine();

            passwords.RemoveAt(index);
            passwords.Add(password);

            TextWriter writer = new StreamWriter(passwordPath);
            for (int i = 0; i < passwords.Count; i++)
            {
                writer.WriteLine(passwords[i]);
            }
            writer.Close();
        }

        public static void payrateEdit(int index)
        {
            Console.WriteLine("What is the new payrate for the employee");

            string pay = Console.ReadLine();

            double newPay = Convert.ToDouble(pay);

            payrate.RemoveAt(index);
            payrate.Add(newPay);

            TextWriter writer = new StreamWriter(payratePath);
            for (int i = 0; i < payrate.Count; i++)
            {
                writer.WriteLine(payrate[i]);
            }
            writer.Close();
        }

        public static void editTimes(string emID)
        {
            if (!File.Exists(desktopPath + "\\EmID-out" + emID + ".txt"))
            {
                Console.WriteLine("Error no clock in data");
            }
            else
            {
                ArrayList clockIN = new ArrayList();
                ArrayList clockOUT = new ArrayList();

                if (File.Exists(desktopPath + "\\EmID-" + emID + ".txt"))
                {
                    StreamReader reader = new StreamReader(desktopPath + "\\EmID-" + emID + ".txt");

                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        clockIN.Add(line);
                    }
                    reader.Close();

                    if (File.Exists(desktopPath + "\\EmID-out" + emID + ".txt"))
                    {

                        StreamReader reader2 = new StreamReader(desktopPath + "\\EmID-out" + emID + ".txt");

                        string line2;
                        while ((line2 = reader2.ReadLine()) != null)
                        {
                            clockOUT.Add(line2);
                        }
                        reader2.Close();


                        Console.WriteLine("Do you want to edit a clock in time or clock out time");
                        Console.WriteLine("1 for Clock IN, 2 for Clock OUT");

                        string choice = Console.ReadLine().Trim();

                        switch (choice)
                        {
                            case "1":
                                for (int i = 0; i < clockIN.Count; i++)
                                {
                                    Console.WriteLine(clockIN[i]);
                                }
                                Console.WriteLine("Enter the time you want to change exactly");
                                string time = Console.ReadLine().Trim();

                                if (!clockIN.Contains(time))
                                {
                                    Console.WriteLine("Error this is not a valid time");
                                    break;
                                }

                               

                                Console.WriteLine("Enter the New time following the same format as above using 24 hr time");
                                string newtime = Console.ReadLine().Trim();

                                if(newtime.Length != 8)
                                {
                                    Console.WriteLine("Error this is not in the correct format");
                                    break;
                                }
                                else
                                {
                                    int index = clockIN.IndexOf(time);
                                    clockIN.RemoveAt(index);
                                    clockIN.Insert(index, newtime);

                                    TextWriter writer = new StreamWriter(desktopPath + "\\EmID-" + emID + ".txt");
                                    for (int i = 0; i < clockIN.Count; i++)
                                    {
                                        writer.WriteLine(clockIN[i]);

                                    }
                                    writer.Close();
                                }
                               



                                break;

                            case "2":

                                for (int i = 0; i < clockOUT.Count; i++)
                                {
                                    Console.WriteLine(clockIN[i]);
                                }
                                Console.WriteLine("Enter the time you want to change exactly");

                                string time2 = Console.ReadLine().Trim();

                                if (!clockOUT.Contains(time2))
                                {
                                    Console.WriteLine("Error this is not a valid time");
                                    break;
                                }


                                Console.WriteLine("What is the new Time you would like to enter following the same format as above");

                                string newtime2 = Console.ReadLine().Trim();

                                if (newtime2.Length != 8)
                                {
                                    Console.WriteLine("Error this is not in the correct format");
                                    break;
                                }
                                else
                                {

                                    int index2 = clockOUT.IndexOf(time2);
                                    clockOUT.RemoveAt(index2);
                                    clockOUT.Insert(index2, newtime2);

                                    TextWriter writer2 = new StreamWriter(desktopPath + "\\EmID-out" + emID + ".txt");
                                    for (int i = 0; i < clockOUT.Count; i++)
                                    {
                                        writer2.WriteLine(clockOUT[i]);

                                    }
                                    writer2.Close();

                                }
                               

                                break;

                            default:
                                Console.WriteLine("Error this is not a choice");
                                break;
                        }
                    }




                }
            }
        }
    }
}

     
