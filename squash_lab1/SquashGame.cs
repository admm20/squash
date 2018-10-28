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

        public KeyboardState keyboardState;
        public MouseState mouseState;
        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GameMode gameMode = new GameMode();
        Splash splash = new Splash();
        MainMenu menu = new MainMenu();

        ProgramState currentState = null;
        
        bool clipCursor = false;

        // used to keep cursor inside window
        [DllImport("user32.dll")]
        static extern void ClipCursor(ref Rectangle rect);

        public void ShowSplash()
        {
            currentState = splash;
        }

        public void ShowMainMenu()
        {
            currentState = menu;
        }

        public void ShowGameMode()
        {
            currentState = gameMode;
            gameMode.Enter();
        }

        public void ShowCursor()
        {
            this.IsMouseVisible = true;
            clipCursor = false;
        }

        public void HideCursor()
        {
            this.IsMouseVisible = false;
            clipCursor = true;
        }

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
            base.TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 120.0);

            // start game with splash screen
            currentState = splash;


            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            splash.LoadTextures(Content);
            gameMode.LoadTextures(Content);
            menu.LoadTextures(Content);


        }
        
        protected override void UnloadContent()
        {

        }
        
        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            // hide cursor if game window is active
            if (IsActive && clipCursor)
            {
                Rectangle rect = Window.ClientBounds;
                rect.Width += rect.X;
                rect.Height += rect.Y;

                ClipCursor(ref rect);
            }


            currentState.Update(gameTime.ElapsedGameTime.Milliseconds, this);



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            currentState.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
