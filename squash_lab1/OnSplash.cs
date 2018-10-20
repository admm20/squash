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
    class OnSplash
    {
        private SquashGame game = null;

        private Texture2D splashArt = null;

        public void Update()
        {
            if (game.keyboardState.IsKeyDown(Keys.Enter))
            {
                game.state = ProgramState.MAIN_MENU;
            }
        }

        public void LoadTextures(ContentManager content)
        {
            splashArt = content.Load<Texture2D>("Textures/Menu/background");
        }

        public void DrawSplash(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(splashArt, new Rectangle(0, 0, 900, 600), Color.White);
        }

        public OnSplash(SquashGame game)
        {
            this.game = game;
        }
    }
}
