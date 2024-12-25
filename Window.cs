using SDL2;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static SDL2.SDL;
using static SDL2.SDL_ttf;
using static SimulationEvolution.Settings;
using static SimulationEvolution.Logging;
using static System.Net.Mime.MediaTypeNames;

namespace SimulationEvolution
{
    internal class Window
    {
        Simulation sim;

        public IntPtr window;
        public IntPtr renderer;
        public bool running;

        private string name;
        public int width { get; private set; }
        public int height { get; private set; }

        int x1 = 0, y1 = 0; // REMOVE AFTER CHECK



        public void Cycle() // method which check events, draw graphics, make simulation turn, etc.
        {
            while (running)
            {
                while (SDL_PollEvent(out SDL_Event e) == 1)
                {
                    if (e.type == SDL_EventType.SDL_QUIT) // close window event
                    {
                        running = false;
                    }

                    else if (e.type == SDL_EventType.SDL_KEYDOWN) // group of keydown events
                    {
                        if (e.key.keysym.sym == SDL_Keycode.SDLK_a) // fixed window event [A]
                        {
                            fixed_window = !fixed_window;
                        }
                        else if (e.key.keysym.sym == SDL_Keycode.SDLK_s) // delete all entities event [S]
                        {
                            sim.DeleteAllEntities();
                        }
                        else if (e.key.keysym.sym == SDL_Keycode.SDLK_d) // generate entities event [D]
                        {
                            sim.GenerateEntities(1000);
                        }
                        else if (e.key.keysym.sym == SDL_Keycode.SDLK_SPACE) // pause simulation event [SPACE]
                        {
                            is_simulation_on_pause = !is_simulation_on_pause;
                        }
                        else if (e.key.keysym.sym == SDL_Keycode.SDLK_DOWN) // reduce TurnWait event [DOWN]
                        {
                            ChangeTurnWait(false);
                        }
                        else if (e.key.keysym.sym == SDL_Keycode.SDLK_UP) // increase TurnWait event [UP]
                        {
                            ChangeTurnWait(true);
                        }
                        else if (e.key.keysym.sym == SDL_Keycode.SDLK_1) // change RenderingMode event [1]
                        {
                            ChangeRenderingMode(0);
                        }
                        else if (e.key.keysym.sym == SDL_Keycode.SDLK_2) // change RenderingMode event [2]
                        {
                            ChangeRenderingMode(1);
                        }
                    }

                    else if (e.type == SDL_EventType.SDL_MOUSEBUTTONDOWN) // check mouse position event
                    {
                        //Log(message_color.def, e.motion.x, e.motion.y);
                        int x_pos = e.motion.x, y_pos = e.motion.y;
                        int x_ind = (e.motion.x - 1) / 8, y_ind = (e.motion.y - 1) / 8;
                        Log($"{x_pos} : {y_pos}");
                        if (x_pos >= cell_x * (cell_size + 1) + 1 || y_pos > cell_y * (cell_size + 1) + 1 ||
                           (x_pos - 1) % 8 == 7 || (y_pos - 1) % 8 == 7) Log("Not found", message_color.warn);
                        else
                        {
                            Log((sim.map[x_ind, y_ind].IsFree()).ToString());
                            if (!sim.map[x_ind, y_ind].IsFree())
                            {
                                Log($"Energy: {sim.map[x_ind, y_ind].entity.energy}\nColor: {sim.map[x_ind, y_ind].entity.color}" +
                                    $"\nRotation: {sim.map[x_ind, y_ind].entity.rotation}", message_color.suc);
                            }
                            else
                            {
                                Log($"Organics: {sim.map[x_ind, y_ind].organics}");
                            }
                        }
                    }
                }


                SetColor(0, 0, 0, 255); // setting black color

                Clear(); // clearing window

                SetColor(Color.White);

                // drawing extreme lines

                DrawLine(x_size, 0, x_size, y_size);
                DrawLine(0, y_size, x_size, y_size);

                if (!is_simulation_on_pause) sim.MakeTurn(); // make simulation turn
                sim.DrawEntities(); // draw all entities




                // TEXT TEST

                Text population_text = new Text(255, 255, 255, $"Population: {sim.entity_count}", renderer, x_size + 10, 0);
                Text time_text = new Text(255, 255, 255, $"Time: {sim.GetSimulationTurn()}", renderer, x_size + 10, 20);

                population_text.Render();
                time_text.Render();





                Present(); // present all



                



                if (fixed_window) SetWindowPos(1, 31); // check if window is fixed and move it

                Thread.Sleep(TurnWait);
            }
        }

        public void Clear()
        {
            SDL_RenderClear(renderer);
        }

        public void SetColor(byte r, byte g, byte b, byte a = 255)
        {
            SDL_SetRenderDrawColor(renderer, r, g, b, a);
        }

        public void SetColor(Color color, byte a = 255)
        {
            SDL_SetRenderDrawColor(renderer, color.R, color.G, color.B, a);
        }

        public void DrawLine(int x1, int y1, int x2, int y2)
        {
            SDL_RenderDrawLine(renderer, x1, y1, x2, y2);
        }

        public void DrawRectangle(int x, int y, int w, int h)
        {
            SDL_Rect rect = new SDL_Rect { x = x, y = y, w = w, h = h };
            SDL_RenderFillRect(renderer, ref rect);
        }

        public void DrawRectangle(int x, int y, int s)
        {
            SDL_Rect rect = new SDL_Rect { x = x, y = y, w = s, h = s };
            SDL_RenderFillRect(renderer, ref rect);
        }

        public void Present()
        {
            SDL_RenderPresent(renderer);
        }

        public void SetWindowPos(int x, int y)
        {
            SDL.SDL_SetWindowPosition(window, x, y);
        }

        public void GetWindowPos(ref int x, ref int y)
        {
            SDL.SDL_GetWindowPosition(window, out x, out y);
        }

        public Window(string name, int pos_x, int pos_y)
        {
            this.name = name;
            this.width = cell_x * (cell_size + 1) + 2 + free_space_width;
            this.height = cell_y * (cell_size + 1) + 2 + free_space_height;
            if (pos_x == -1 && pos_y == -1) pos_x = pos_y = SDL_WINDOWPOS_UNDEFINED;
            window = SDL_CreateWindow(name, pos_x, pos_y, width, height, SDL_WindowFlags.SDL_WINDOW_SHOWN);

            if (window == IntPtr.Zero) Console.WriteLine($"There was an issue creating the window. {SDL_GetError()}");

            renderer = SDL_CreateRenderer(window,
                                        -1,
                                        SDL_RendererFlags.SDL_RENDERER_ACCELERATED |
                                        SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

            if (renderer == IntPtr.Zero) Console.WriteLine($"There was an issue creating the renderer. {SDL_GetError()}");

            TTF_Init();

            running = true;

            sim = new Simulation(this);

            Console.WriteLine(x_size + " --- " + y_size);
            Console.WriteLine("Maximum number of entities: " + max_entity_count);
            Console.WriteLine('\n');



            font = TTF_OpenFont("D:/ProgrammingProjects/SImulationEvolution/fonts/arial_bolditalicmt.ttf", 20);
            if (font == IntPtr.Zero)
            {
                Console.WriteLine("Ошибка загрузки шрифта: " + TTF_GetError());
                return;
            }


            Cycle();
        }

        ~Window()
        {
            SDL_DestroyRenderer(renderer);
            SDL_DestroyWindow(window);
            SDL_Quit();
        }
    }
}
