using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimulationEvolutionForms.Logging;

namespace SimulationEvolutionForms
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
        public static Random rnd;
        public static bool is_spawn_checker = false;

        public static bool is_entity_energy_infinite = false; // if true the energy of the entities can be as large as possible
        public static int max_entity_energy = 1000; // this variable is used if the maximum amount of energy of the entity is not infinite

        //MUTATION SETTINGS
        public static float mutation_chance = 0.00f;
        public static float neuron_mutation_chance = 0.5f; // these two should make 1f in sum
        public static float connection_mutation_chance = 0.5f; // these two should make 1f in sum
        public static int min_weights_mutate_count = 50; // min amount of weights to mutate
        public static int max_weights_mutate_count = 100; // max amount of weights to mutate
        public static bool can_weights_mutate_in_different_layers = false;


        //ENTITIES SETTINGS
        public static int standart_energy = 100;
        public static int organics_after_dying = 10;
        public static int energy_for_staying = 100;
        public static int energy_for_moving = 5;
        public static int energy_for_rotating = 5;
        public const  int energy_for_photosynthesis = 0;
        public static int energy_for_reproduction = 50;
        public static int bite_power = 100;
        public static int organics_bite_power = 50;
        public static int max_age = 100;
        
        //LAYERS SETTINGS
        public const int defaulf_neurons_input_layer_quantity = 5;
        public const int defaulf_neurons_hidden_layer_quantity = 5;
        public const int defaulf_neurons_output_layer_quantity = 5;

        //WEIGHTS SETTINGS
        public const float min_weight_size = -1f;
        public const float max_weight_size = 1f;

        //NEURAL_NETWORK SETTINGS
        public const int default_layers_quantity = 5;

        //PHOTOSYNTHESIS SETTINGS
        public const int waves_count = 50;
        public const int start_wave_position = 50;
        public const int wave_size = 1;
        public static int start_wave_value = 50;
        public static int max_wave_value;
        public const int delta_wave_value = -1;


        //CELLS SETTINGS
        public static bool is_organics_infinite = false; // if true the organics of the cell can be as large as possible
        public static int max_organics = 100; // this variable is used if the maximum amount of organics of the cell is not infinite
        public static bool can_organics_kill = false;
        public static int organics_limit_value = 50;
        public static int organics_damage = 100;


        //CONSOLE&LOG SETTINGS
        //public static Color default_color = Color.Black;
        public static Color default_console_color = Color.Black;
        public static Color success_console_color = Color.Green;
        public static Color warning_console_color = Color.Orange;
        public static Color error_console_color = Color.Red;
        public static Color selected_entity_color = Color.Blue;


        //WINDOW SETTINGS
        public static int free_space_width = 450;
        public static int free_space_height = 100;
        public static IntPtr font;


        //LOG SETTINGS
        public static bool log_simulation_turn = false; // if true, will log simulation turn after every cycle


        //OTHER
        public static int TurnWait = 0;
        public static int max_TurnWait = 10000;
        public static int min_TurnWait = 0;
        public static int change_TurnWait = 100;
        public static int entity_to_spawn_by_click = 1000;
        public static bool fixed_window = true; // if true you can chage window position
        public static bool is_simulation_on_pause = false; // it true simulation on pause
        public static int mouse_square_size = 1;
        public static int relative_difference = 1;


        //INTERFACE SETTINGS
        public static Color simulation_color = Color.White;
        public static Color panel_color = Color.White;
        public static Color lines_color = Color.Black;
        public static bool is_night_theme = false;

        public static List<int> rendering_mode = new List<int>
        {
            1, //entity_color
            0, //organics
            0, //eat_color
            0, //energy_color
            0, //waves
        };

        public static List<int> mouse_mode = new List<int>
        {
            1, //overview
            0, //spawn
            0, //erase
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

        public static void SwitchTheme()
        {
            if (simulation_color == Color.White)
            {
                simulation_color = Color.Black;
                lines_color = Color.White;         
            }
        }

        public static void ChangeRenderingMode(int n)
        {
            for (int i = 0; i < rendering_mode.Count; i++) rendering_mode[i] = 0;
            rendering_mode[n] = 1;
        }

        public static void ChangeMouseMode(int n)
        {
            for (int i = 0; i < mouse_mode.Count; i++) mouse_mode[i] = 0;
            mouse_mode[n] = 1;
        }

        public static List<string> input_neuron_variants = new List<string>() // possible variants for neurons in input layer
        {
            "x", "y", "energ", "visio", "visio"
        };

        public static List<string> hidden_neuron_variants = new List<string>() // possible variants for neurons in hidden layer
        {
            "relu", "line", "tanh", "rand"
        };

        public static List<string> output_neuron_variants = new List<string>() // possible variants for neurons in output layer
        {
            "move", "bite", "rotr", "rotl", "produ", "recyc", "photo", "rota", "rote"
        };

        static Settings()
        {
            rnd = new Random();
            max_entity_count = cell_x * cell_y;
            x_size = cell_x * cell_size + cell_x + 1;
            y_size = cell_y * cell_size + cell_y + 1;
            max_wave_value = (delta_wave_value >= 0) ? start_wave_value + delta_wave_value * (waves_count - 1) : start_wave_value; //TODO MATH REGRESSION PROGRESSION I DON'T KNOW HOW TO NAME IT RIGHT
            Log("max_wave_value - ", max_wave_value);
            Log("Seed - ", rnd.GetHashCode());
        }

        /* Buttons
         * [A]      fixed window
         * [S]      delete all entities
         * [Z]      delete all
         * [D]      generate entities
         * [V]      spawn entities by mouse
         * 
         * [SPACE]  pause simulation
         * [DOWN]   reduce TurnWait
         * [UP]     increase TurnWait
         * [1]      change RenderingMode to standart
         * [2]      change RenderingMode to organics
         * [3]      change RenderingMode to eat_color
         * [4]      change RenderingMode to energy_color
         * [5]      change RenderingMode to photosynthesis_waves
         */
    }
}
