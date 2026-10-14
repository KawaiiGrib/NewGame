using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    static class MessageHelper
    {
        static public string getString(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            return Console.ReadLine();
        }

        static public int getInt(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            return Convert.ToInt32(MessageHelper.getString(message));
        }

        static public double getDouble(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            return Convert.ToDouble(MessageHelper.getString(message));
        }

        static public bool getBool(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            return Convert.ToBoolean(MessageHelper.getString(message));
        }
        static public void showText(string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.Write(text);

            Console.WriteLine();
        }
    }
}
