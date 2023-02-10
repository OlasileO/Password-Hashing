using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using static System.Console;
using Microsoft.Identity.Client;

namespace Password_Hashing
{
    public class Program
    {

        static void Main() 
        {
            void Promot()
            {
                Clear();
                WriteLine("[R] Register [L] Login");

                while (true)
                {
                    var input = ReadLine().ToUpper()[0];
                    switch (input)
                    {
                        case 'R': Register(); break;
                        case 'L': Login(); break;
                        default:
                            break;
                    }
                }
            }
            void Login()
            {
                Clear();
                WriteLine("======Login=====");
                Write("UserName ");
                var name = ReadLine();
                Write("Password ");
                var password = ReadLine();
                using UserDb db = new UserDb();
                var userFound = db.Users.Any(u => u.UserName == name);
                if (userFound)
                {
                    var loginuser = db.Users.FirstOrDefault(u => u.UserName == name);
                    if (HashPassword($"{password}{loginuser.salt}") == loginuser.Password)
                    {
                        Clear();
                        ForegroundColor = ConsoleColor.Blue;
                        WriteLine("Login in Successfully");
                        ReadLine();
                    }
                    else
                    {
                        Clear();
                        ForegroundColor = ConsoleColor.Red;
                        WriteLine("Login in Failed");
                        ReadLine();
                    }
                }
            }
            void Register()
            {
                Clear();
                WriteLine("====Register====");
                Write("UserName ");
                var name = ReadLine();
                Write("Password ");
                var password = ReadLine();

                using UserDb db = new UserDb();
                var salt = DateTime.Now.ToString();
                var hashPw = HashPassword($"{password}{salt}");
                db.Users.Add(new User { UserName = name, Password = hashPw, salt = salt });
                db.SaveChanges();

                while (true)
                {
                    Clear();
                    WriteLine("Registration Complete");
                    WriteLine("[B] back");
                    if (ReadKey().Key == ConsoleKey.B)
                    {
                        Promot();
                    }
                }

            }

            string HashPassword(string password)
            {
                SHA256 hash = SHA256.Create();
                var passwordbyte = Encoding.Default.GetBytes(password);
                var hashedpassword = hash.ComputeHash(passwordbyte);
                return Convert.ToHexString(hashedpassword);
            }


            Promot();
        }
    }
}
