using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Runtime.InteropServices;

namespace squash_lab1
{

    //public enum ProgramState
    //{
    //    SPLASH,
    //    MAIN_MENU,
    //    GAME
    //}

    public class SquashGame : Game
    {

        public static int WINDOW_WIDTH = 900;
        public static int WINDOW_HEIGHT = 600;

        public ProgramState state = ProgramState.SPLASH;

        public KeyboardState keyboardState;


        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GameMode gameMode = new GameMode();
        OnSplash splash = null;
        MainMenu menu = new MainMenu();



        // used to keep cursor inside window
        [DllImport("user32.dll")]
        static extern void ClipCursor(ref Rectangle rect);

        public SquashGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;  // width of game window
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;   // height
            graphics.ApplyChanges();

            // change update frequency
            base.TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 60.0);

            splash = new  OnSplash(this);
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            splash.LoadTextures(Content);
            gameMode.LoadTextures(Content);


        }
        
        protected override void UnloadContent()
        {

        }
        
        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            // hide cursor if game window is active
            if (IsActive)
            {
                Rectangle rect = Window.ClientBounds;
                rect.Width += rect.X;
                rect.Height += rect.Y;

                ClipCursor(ref rect);
            }


            switch (state)
            {
                case ProgramState.SPLASH:
                    splash.Update();
                    break;
                case ProgramState.MAIN_MENU:

                    break;
                case ProgramState.GAME:
                    gameMode.Update(gameTime.ElapsedGameTime.Milliseconds, Mouse.GetState().Position.X);
                    break;
                default:
                    // ups
                    break;
            }



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            switch (state)
            {
                case ProgramState.SPLASH:
                    splash.DrawSplash(spriteBatch);
                    break;
                case ProgramState.MAIN_MENU:

                    break;
                case ProgramState.GAME:
                    gameMode.Draw(spriteBatch);
                    break;
                default:
                    // ups
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
