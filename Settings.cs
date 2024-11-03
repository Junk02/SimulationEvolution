using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationEvolution
{
    internal static class Settings
    {
        public static int cell_x = 110;
        public static int cell_y = 95;
        public static int cell_size = 7;
        public static int max_entity_count;
        public static int x_size;
        public static int y_size;

        //entities settings
        public static int genom_size = 64;
        public static int standart_energy = 100;
        public static int energy_for_staying = 10;

        public static Color default_color = Color.Black;
        public static ConsoleColor default_console_color = ConsoleColor.White;
        public static ConsoleColor success_console_color = ConsoleColor.Green;
        public static ConsoleColor warning_console_color = ConsoleColor.Yellow;
        public static ConsoleColor error_console_color = ConsoleColor.Red;


        public static bool fixed_window = true;

        static Settings()
        {
            max_entity_count = cell_x * cell_y;
            x_size = cell_x * cell_size + cell_x + 1;
            y_size = cell_y * cell_size + cell_y + 1;
        }
    }
}
