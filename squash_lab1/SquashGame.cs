using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Runtime.InteropServices;

namespace squash_lab1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class SquashGame : Game
    {

        public static int WINDOW_WIDTH = 900;
        public static int WINDOW_HEIGHT = 600;


        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Paddle paddle = new Paddle(new Rectangle(0, 550, 120, 20));
        //Ball ball = new Ball(new Rectangle(485, 400, 15, 15));
        Ball ball = new Ball(new Rectangle(700, 400, 15, 15));
        Goal goal = new Goal(new Rectangle(WINDOW_WIDTH / 2 - 200, 0, 400, 50));
        Wall wall_left = new Wall(new Rectangle(WINDOW_WIDTH / 2 - 250, 0, 50, 150));
        Wall wall_right = new Wall(new Rectangle(WINDOW_WIDTH / 2 + 200, 0, 50, 150));

        // Invisible walls that are outside of screen
        Wall frame_left = new Wall(new Rectangle(-200, 0, 200, WINDOW_HEIGHT));
        Wall frame_up = new Wall(new Rectangle(-200, -200, WINDOW_WIDTH + 400, 200));
        Wall frame_right = new Wall(new Rectangle(WINDOW_WIDTH, 0, 200, WINDOW_HEIGHT));
        LoseArea frame_down = new LoseArea(new Rectangle(-200, WINDOW_HEIGHT, WINDOW_WIDTH + 400, 200));

        Texture2D paddle_tex;
        Texture2D ball_tex;
        Texture2D wall_tex;
        Texture2D goal_tex;


        // used to keep cursor inside window
        [DllImport("user32.dll")]
        static extern void ClipCursor(ref Rectangle rect);

        public SquashGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;  // width of game window
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;   // height
            graphics.ApplyChanges();

            // change update frequency
            base.TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 60.0);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            

            paddle_tex = Content.Load<Texture2D>("Textures/paddle");
            ball_tex = Content.Load<Texture2D>("Textures/ball");
            goal_tex = Content.Load<Texture2D>("Textures/goal");
            wall_tex = Content.Load<Texture2D>("Textures/wall");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
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

            // paddle x position is same as mouse x
            paddle.Move_to_x(Mouse.GetState().Position.X);
            // TODO: clip mouse cursor to the middle of the screen 

            ball.Update_position(gameTime.ElapsedGameTime.Milliseconds);


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.Draw(paddle_tex, paddle.position, Color.White);
            spriteBatch.Draw(wall_tex, wall_left.position, Color.White);
            spriteBatch.Draw(wall_tex, wall_right.position, Color.White);
            spriteBatch.Draw(goal_tex, goal.position, Color.White);
            spriteBatch.Draw(ball_tex, ball.position, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
