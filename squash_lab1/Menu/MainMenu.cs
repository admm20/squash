using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace squash_lab1
{
    public enum Difficulty
    {
        EASY,
        MEDIUM,
        HARD
    }

    class MainMenu : ProgramState
    {
        Texture2D background_tex;
        Texture2D quit_tex;
        Texture2D start_tex;
        Texture2D easy_tex;
        Texture2D medium_tex;
        Texture2D hard_tex;

        Rectangle start_rect = new Rectangle(500, 150, 354, 71);
        Rectangle diff_rect = new Rectangle(500, 250, 354, 71);
        Rectangle quit_rect = new Rectangle(500, 350, 354, 71);

        public static Difficulty difficulty = Difficulty.EASY;

        // which element in menu is highlighted (0 - none)
        int hover = 0;

        bool mouseClick = false;

        public override void LoadTextures(ContentManager content)
        {
            background_tex = content.Load<Texture2D>("Textures/Menu/background-clear");
            quit_tex = content.Load<Texture2D>("Textures/Menu/quit-text");
            start_tex = content.Load<Texture2D>("Textures/Menu/start-game-text");
            easy_tex = content.Load<Texture2D>("Textures/Menu/easy-text");
            medium_tex = content.Load<Texture2D>("Textures/Menu/medium-text");
            hard_tex = content.Load<Texture2D>("Textures/Menu/hard-text");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background_tex, new Rectangle(0, 0, 900, 600), Color.White);

            spriteBatch.Draw(start_tex, start_rect, hover == 1 ? Color.White : Color.White * 0.30f);

            switch (difficulty)
            {
                case Difficulty.EASY:
                    spriteBatch.Draw(easy_tex, diff_rect, hover == 2 ? Color.White : Color.White * 0.30f);
                    break;
                case Difficulty.MEDIUM:
                    spriteBatch.Draw(medium_tex, diff_rect, hover == 2 ? Color.White : Color.White * 0.30f);
                    break;
                case Difficulty.HARD:
                    spriteBatch.Draw(hard_tex, diff_rect, hover == 2 ? Color.White : Color.White * 0.30f);
                    break;
            }

            spriteBatch.Draw(quit_tex, quit_rect, hover == 3 ? Color.White : Color.White * 0.30f);
        }

        public override void Update(double delta, SquashGame game)
        {
            // TODO: use this only once, when entering main menu
            game.ShowCursor();

            int mouse_x = game.mouseState.X;
            int mouse_y = game.mouseState.Y;

            if (start_rect.Contains(mouse_x, mouse_y))
            {
                hover = 1;
            }
            else if (diff_rect.Contains(mouse_x, mouse_y))
            {
                hover = 2;
            }
            else if (quit_rect.Contains(mouse_x, mouse_y))
            {
                hover = 3;
            }
            else
                hover = 0;

            // mouse clicking

            if(game.mouseState.LeftButton == ButtonState.Pressed)
            {
                mouseClick = true;
            }
            else if (mouseClick == true)
            {
                mouseClick = false;

                // Left mouse button click detected:

                switch (hover)
                {
                    case 1:
                        game.ShowGameMode();
                        break;
                    case 2:
                        // it is possible to cast enum into int!
                        int current_diff = (int)difficulty;
                        if(mouse_x < diff_rect.Center.X && current_diff > 0)
                        {
                            difficulty = (Difficulty)(current_diff - 1);
                        }
                        else if(mouse_x > diff_rect.Center.X && current_diff < 2)
                        {
                            difficulty = (Difficulty)(current_diff + 1);
                        }


                        break;
                    case 3:
                        game.Exit();
                        break;
                    default:
                        // whoops
                        break;
                   
                }
                

            }

        }
    }
}
