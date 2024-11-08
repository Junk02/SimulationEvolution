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
       //simulation settings
        public static int cell_x = 200;
        public static int cell_y = 150;
        public static int cell_size = 7;
        public static int max_entity_count;
        public static int x_size;
        public static int y_size;
        public static Random rnd = new Random();

        public static bool is_entity_energy_infinite = true; // if true the energy of the entities can be as large as possible
            public static int max_entity_energy = 100; // this variable is used if the maximum amount of energy of the entity is not infinite


        //entities settings
        public static int standart_energy = 100;
        public static int organics_after_dying = 10;
        public static int energy_for_staying = 5;
        public static int energy_for_moving = -4;
        public static int energy_for_rotating = 5;
        public static int energy_for_photosynthesis = 1;


        //cells settings
        public static bool is_organics_infinite = false; // if true the organics of the cell can be as large as possible
            public static int max_organics = 100; // this variable is used if the maximum amount of organics of the cell is not infinite


        //console&log settings
        public static Color default_color = Color.Black;
        public static ConsoleColor default_console_color = ConsoleColor.White;
        public static ConsoleColor success_console_color = ConsoleColor.Green;
        public static ConsoleColor warning_console_color = ConsoleColor.Yellow;
        public static ConsoleColor error_console_color = ConsoleColor.Red;


        //window settings
        public static int free_space_width = 450;
        public static int free_space_height = 100;


        //log settings
        public static bool log_simulation_turn = false; // if true, will log simulation turn after every cycle


        //other
        public static int TurnWait = 0;
        public static bool fixed_window = true;

        public enum rendering_mode
        {
            entity_color = 1,
            organics = 0,
        }

        static Settings()
        {
            max_entity_count = cell_x * cell_y;
            x_size = cell_x * cell_size + cell_x + 1;
            y_size = cell_y * cell_size + cell_y + 1;
        }
    }
}
