using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static SimulationEvolutionForms.Settings;
using static SimulationEvolutionForms.Form1;
using System.Windows.Forms;
using System.Drawing;

namespace SimulationEvolutionForms
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

        public static void Log(message_color color, params Object[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                RichTextBoxExtensions.AppendText(logs, args[i] + " ", GetForegroundColor(color));
            }
            logs.AppendText(Environment.NewLine);
            logs.ScrollToCaret();
        }

        public static void Log(params Object[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                RichTextBoxExtensions.AppendText(logs, args[i] + " ", default_console_color);
            }
            logs.AppendText(Environment.NewLine);
            logs.ScrollToCaret();
        }

        private static Color GetForegroundColor(message_color color)
        {
            if (((int)color) == 1) return success_console_color;
            else if (((int)color) == 2) return warning_console_color;
            else if (((int)color) == 3) return error_console_color;
            return default_console_color;
        }
    }
}
