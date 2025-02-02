using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static SimulationEvolution.Settings;
using static SDL2.SDL;

namespace SimulationEvolution
{
    internal static class Logging
    {
        public enum message_color
        {
            def = 0,
            suc = 1,
            warn = 2,
            err = 3,
        }


        public static void Log(string message, message_color color = message_color.def)
        {
            ChangeForegroundColor(color);
            Console.WriteLine(message);
            Console.ForegroundColor = default_console_color;
        }

        public static void Log(message_color color, params Object[] args)
        {
            ChangeForegroundColor(color);
            for (int i = 0; i < args.Length; i++)
            {
                Console.Write(args[i] + " ");
            }
            Console.WriteLine();
            Console.ForegroundColor = default_console_color;
        }

        public static void Log(params Object[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                Console.Write(args[i] + " ");
            }
            Console.WriteLine();
        }

        private static void ChangeForegroundColor(message_color color)
        {
            if (((int)color) == 0) Console.ForegroundColor = default_console_color;
            else if (((int)color) == 1) Console.ForegroundColor = success_console_color;
            else if (((int)color) == 2) Console.ForegroundColor = warning_console_color;
            else if (((int)color) == 3) Console.ForegroundColor = error_console_color;
        }

        public static void Box(string title, string message)
        {
            SDL_ShowSimpleMessageBox(SDL_MessageBoxFlags.SDL_MESSAGEBOX_ERROR, title, message, IntPtr.Zero);
        }
    }
}
