using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SDL2.SDL;
using static System.Net.Mime.MediaTypeNames;
using static SDL2.SDL_ttf;
using static SimulationEvolution.Settings;
using static SimulationEvolution.Logging;

namespace SimulationEvolution
{
    internal class Text
    {
        public SDL_Color color;
        public IntPtr textSurface;
        public IntPtr textTexture;
        public SDL_Rect textRect;
        public uint forma;
        public int acss;
        public IntPtr renderer;
        public string text;
        public int text_x;
        public int text_y;

        public Text(int r, int g, int b, string text, IntPtr renderer, int text_x, int text_y)
        {
            this.renderer = renderer;
            this.text = text;
            this.text_x = text_x;
            this.text_y = text_y;
            color = new SDL_Color { r = 255, g = 255, b = 255, a = 255 };
            textRect.x = text_x;
            textRect.y = text_y;

            IntPtr textSurface = TTF_RenderText_Solid(font, text, color);
            IntPtr textTexture = SDL_CreateTextureFromSurface(renderer, textSurface);
            SDL_QueryTexture(textTexture, out forma, out acss, out textRect.w, out textRect.h);
            SDL_RenderCopy(renderer, textTexture, IntPtr.Zero, ref textRect);

            SDL_FreeSurface(textSurface);
            SDL_DestroyTexture(textTexture);
        }

        public void ChangeTextPosition(int x, int y)
        {
            textRect.x = x;
            textRect.y = y;
        }

        public void Render()
        {
            IntPtr textSurface = TTF_RenderText_Solid(font, text, color);
            IntPtr textTexture = SDL_CreateTextureFromSurface(renderer, textSurface);
            SDL_QueryTexture(textTexture, out forma, out acss, out textRect.w, out textRect.h);
            SDL_RenderCopy(renderer, textTexture, IntPtr.Zero, ref textRect);

            Destroy();
        }

        private void Destroy()
        {
            SDL_FreeSurface(textSurface);
            SDL_DestroyTexture(textTexture);
        }
    }
}
