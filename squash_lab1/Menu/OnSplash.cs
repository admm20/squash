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
    class OnSplash : ProgramState
    {

        private Texture2D splashArt = null;

        public override void LoadTextures(ContentManager content)
        {
            splashArt = content.Load<Texture2D>("Textures/Menu/background");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(splashArt, new Rectangle(0, 0, 900, 600), Color.White);
        }

        public override void Update(double delta, SquashGame game)
        {
            game.ShowCursor();
            // if any key is pressed
            if(game.keyboardState.GetPressedKeys().Length > 0 ||
                game.mouseState.LeftButton == ButtonState.Pressed)
            {
                game.ShowMainMenu();
            }
        }
        
    }
}
