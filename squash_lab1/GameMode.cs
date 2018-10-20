using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace squash_lab1
{
    class GameMode
    {
        private static int WINDOW_WIDTH = SquashGame.WINDOW_WIDTH;
        private static int WINDOW_HEIGHT = SquashGame.WINDOW_HEIGHT;

        Paddle paddle = new Paddle(new Rectangle(0, 550, 120, 20));
        Ball ball = new Ball(new Rectangle(800, 400, 15, 15));
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

        //Sounds
        SoundEffect ball_tick;
        SoundEffect game_coin;
        SoundEffect game_over;
        SoundEffect game_over_arcade;
        SoundEffect shot_bump;
        SoundEffect shot;
        SoundEffect fail;

        //Volume
        float volume = 1.0f;
        float pitch = 0.0f;
        float pan = 0.0f;


        // Score, lives, font(fontfamily = Code - it can be configurate in file .spritefont)
        public int score = 0;
        public int lives = 3;
        private SpriteFont font;

        public void LoadTextures(ContentManager content)
        {
            paddle_tex = content.Load<Texture2D>("Textures/paddle");
            ball_tex = content.Load<Texture2D>("Textures/ball");
            //goal_tex = Content.Load<Texture2D>("Textures/goal");
            goal_tex = content.Load<Texture2D>("Textures/goal1");
            //wall_tex = Content.Load<Texture2D>("Textures/wall");
            wall_tex = content.Load<Texture2D>("Textures/wall1");

            font = content.Load<SpriteFont>("Fonts/Score");

            //sounds
            ball_tick = content.Load<SoundEffect>("Sounds/ball-tick");
            game_coin = content.Load<SoundEffect>("Sounds/game-coin");
            game_over = content.Load<SoundEffect>("Sounds/game-over");
            game_over_arcade = content.Load<SoundEffect>("Sounds/game-over-arcade");
            shot = content.Load<SoundEffect>("Sounds/shot");
            shot_bump = content.Load<SoundEffect>("Sounds/phone-bump");
            fail = content.Load<SoundEffect>("Sounds/fail");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(paddle_tex, paddle.position, Color.White);
            spriteBatch.Draw(wall_tex, wall_left.position, Color.White);
            spriteBatch.Draw(wall_tex, wall_right.position, Color.White);
            spriteBatch.Draw(goal_tex, goal.position, Color.White);
            spriteBatch.Draw(ball_tex, ball.position, Color.White);
            spriteBatch.DrawString(font, "Score  " + score, new Vector2(30, 50), Color.White);
            spriteBatch.DrawString(font, "Lifes  " + lives, new Vector2(WINDOW_WIDTH - 150, 50), Color.White);
        }

        public void Update(int delta, int mouse_x)
        {
            // paddle x position is same as mouse x
            paddle.Move_to_x(mouse_x);
            // TODO: clip mouse cursor to the middle of the screen 

            ball.Update_position(delta);

            // if you score a goal
            if (ball.isGoal)
            {
                score++;
                ball.isGoal = false;
                game_coin.Play(volume, pitch, pan);
            }

            // if you failed

            if (ball.isFail)
            {
                ball.isFail = false;
                if (lives >= 1)
                {
                    lives--;
                    fail.Play(volume - 0.5f, pitch, pan);
                }
                else
                {
                    //gameover
                    score = 0;
                    lives = 3;
                    //game_over.Play(volume, pitch, pan);
                    game_over_arcade.Play(volume, pitch, pan);

                }
            }

            //if you hit a paddle
            if (ball.isInPaddle)
            {
                shot.Play(volume, pitch, pan);
            }
        }

        public GameMode()
        {
        }


    }
}
