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
        //SIMULATION SETTINGS
        public static int cell_x = 200;
        public static int cell_y = 150;
        public static int cell_size = 7;
        public static int max_entity_count;
        public static int x_size;
        public static int y_size;
        public static Random rnd = new Random();

        public static bool is_entity_energy_infinite = false; // if true the energy of the entities can be as large as possible
        public static int max_entity_energy = 100; // this variable is used if the maximum amount of energy of the entity is not infinite


        //ENTITIES SETTINGS
        public static int standart_energy = 100;
        public static int organics_after_dying = 10;
        public static int energy_for_staying = 1;
        public static int energy_for_moving = 3;
        public static int energy_for_rotating = 1;
        public static int energy_for_photosynthesis = 9;
        public static int energy_for_reproduction = 60;
        public static int bite_power = 100;
        public static int organics_bite_power = 5;


        //CELLS SETTINGS
        public static bool is_organics_infinite = false; // if true the organics of the cell can be as large as possible
        public static int max_organics = 100; // this variable is used if the maximum amount of organics of the cell is not infinite


        //CONSOLE&LOG SETTINGS
        public static Color default_color = Color.Black;
        public static ConsoleColor default_console_color = ConsoleColor.White;
        public static ConsoleColor success_console_color = ConsoleColor.Green;
        public static ConsoleColor warning_console_color = ConsoleColor.Yellow;
        public static ConsoleColor error_console_color = ConsoleColor.Red;


        //WINDOW SETTINGS
        public static int free_space_width = 450;
        public static int free_space_height = 100;


        //LOG SETTINGS
        public static bool log_simulation_turn = false; // if true, will log simulation turn after every cycle


        //OTHER
        public static int TurnWait = 100;
        public static int max_TurnWait = 10000;
        public static int min_TurnWait = 0;
        public static int change_TurnWait = 100;
        public static bool fixed_window = true; // if true you can chage window position
        public static bool is_simulation_on_pause = false; // it true simulation on pause

        public static List<int> rendering_mode = new List<int>
        {
            0, //entity_color
            1, //organics
        };

        public static void ChangeTurnWait(bool flag)
        {
            if (!flag)
            {
                if (TurnWait > min_TurnWait) TurnWait -= change_TurnWait;
            }
            else
            {
                if (TurnWait < max_TurnWait) TurnWait += change_TurnWait;
            }
        }

        public static void ChangeRenderingMode(int n)
        {
            for (int i = 0; i < rendering_mode.Count; i++) rendering_mode[i] = 0;
            rendering_mode[n] = 1;
        }

        static Settings()
        {
            max_entity_count = cell_x * cell_y;
            x_size = cell_x * cell_size + cell_x + 1;
            y_size = cell_y * cell_size + cell_y + 1;
        }
    }
}
