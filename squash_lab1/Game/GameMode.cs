using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
    class GameMode : ProgramState
    {
        private static int WINDOW_WIDTH = SquashGame.WINDOW_WIDTH;
        private static int WINDOW_HEIGHT = SquashGame.WINDOW_HEIGHT;

        public Paddle paddle = new Paddle(new Rectangle(0, 550, 120, 20));
        Ball ball = new Ball(new Rectangle(800, 400, 15, 15));
        Goal goal = new Goal(new Rectangle(WINDOW_WIDTH / 2 - 200, 0, 400, 50));
        Wall wall_left = new Wall(new Rectangle(WINDOW_WIDTH / 2 - 250, 0, 50, 150));
        Wall wall_right = new Wall(new Rectangle(WINDOW_WIDTH / 2 + 200, 0, 50, 150));
        public Paddle paddle_opponent = new Paddle(new Rectangle(50, 550, 120, 20));

        // Invisible walls that are outside of screen
        Wall frame_left = new Wall(new Rectangle(-200, 0, 200, WINDOW_HEIGHT));
        Wall frame_up = new Wall(new Rectangle(-200, -200, WINDOW_WIDTH + 400, 200));
        Wall frame_right = new Wall(new Rectangle(WINDOW_WIDTH, 0, 200, WINDOW_HEIGHT));
        LoseArea frame_down = new LoseArea(new Rectangle(-200, WINDOW_HEIGHT, WINDOW_WIDTH + 400, 200));

        Texture2D paddle_tex;
        Texture2D paddle_2_tex;
        Texture2D ball_tex;
        Texture2D wall_tex;
        Texture2D goal_tex;

        Texture2D scoreboard_tex;
        Rectangle scoreboard_rect = new Rectangle(WINDOW_WIDTH / 2 - 400 / 2, WINDOW_HEIGHT / 2 - 250 / 2, 400, 250);
        Texture2D retry_tex;
        Rectangle retry_rect = new Rectangle(WINDOW_WIDTH / 2 - 400 / 2 + 25, WINDOW_HEIGHT / 2 - 250 / 2 + 79, 354, 71);
        Texture2D quit_tex;
        Rectangle quit_rect = new Rectangle(WINDOW_WIDTH / 2 - 400 / 2 + 25, WINDOW_HEIGHT / 2 - 250 / 2 + 150, 354, 71);

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
        public int lifes = 3;

        public int enemy_score = 0;
        public int enemy_lifes = 3;

        // scoreboard buttons
        int hover = 0;
        bool mouseClick = false;

        private SpriteFont font;

        public bool player_turn = true;

        private bool end_game = false;

        public void Enter()
        {
            ball.game = this;

            player_turn = true;
            if (!Collision.ListOfObjectsWithCollision.Contains(paddle))
                Collision.ListOfObjectsWithCollision.Add(paddle);

            if (Collision.ListOfObjectsWithCollision.Contains(paddle_opponent))
                Collision.ListOfObjectsWithCollision.Remove(paddle_opponent);

            score = 0;
            lifes = 3;

            enemy_score = 0;
            enemy_lifes = 3;


            ball.position.X = 800;
            ball.position.Y = 400;

            end_game = false;
        }

        public override void LoadTextures(ContentManager content)
        {
            paddle_tex = content.Load<Texture2D>("Textures/paddle");
            paddle_2_tex = content.Load<Texture2D>("Textures/paddle_2");
            ball_tex = content.Load<Texture2D>("Textures/ball");
            goal_tex = content.Load<Texture2D>("Textures/goal1");
            wall_tex = content.Load<Texture2D>("Textures/wall1");
            scoreboard_tex = content.Load<Texture2D>("Textures/scoreboard");
            retry_tex = content.Load<Texture2D>("Textures/Menu/play_again_text");
            quit_tex = content.Load<Texture2D>("Textures/Menu/back_to_menu_text");

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

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(paddle_2_tex, paddle_opponent.position, player_turn ? Color.White * 0.5f : Color.White);
            spriteBatch.Draw(paddle_tex, paddle.position, !player_turn ? Color.White * 0.5f : Color.White);
            spriteBatch.Draw(wall_tex, wall_left.position, Color.White);
            spriteBatch.Draw(wall_tex, wall_right.position, Color.White);
            spriteBatch.Draw(goal_tex, goal.position, Color.White);
            spriteBatch.Draw(ball_tex, ball.position, Color.White);
            spriteBatch.DrawString(font, "Score  " + score, new Vector2(30, 50), Color.White);
            spriteBatch.DrawString(font, "Lifes  " + lifes, new Vector2(WINDOW_WIDTH - 150, 50), Color.White);

            if (end_game)
            {
                spriteBatch.Draw(scoreboard_tex, scoreboard_rect, Color.White);
                spriteBatch.DrawString(font, "Your score : " + score, new Vector2(scoreboard_rect.X + 20, scoreboard_rect.Y + 20), Color.White);
                spriteBatch.DrawString(font, "Opponent score : " + enemy_score, new Vector2(scoreboard_rect.X + 20, scoreboard_rect.Y + 50), Color.White);
                spriteBatch.Draw(retry_tex, retry_rect, hover == 1 ? Color.White : Color.White * 0.60f);
                spriteBatch.Draw(quit_tex, quit_rect, hover == 2 ? Color.White : Color.White * 0.60f);
            }
        }

        public override void Update(double delta, SquashGame game)
        {
            if (!end_game)
            {
                game.HideCursor();
                // paddle x position is same as mouse x
                paddle.Move_to_x(game.mouseState.X);
                // TODO: clip mouse cursor to the middle of the screen 

                ball.Update_position((float)delta);

                // moving opponent

                if (!player_turn)
                {
                    float enemy_velocity = 0.35f;
                    int reaction_height = 0;
                    switch (MainMenu.difficulty)
                    {
                        case Difficulty.EASY:
                            reaction_height = 300;
                            break;
                        case Difficulty.MEDIUM:
                            reaction_height = 200;
                            break;
                        case Difficulty.HARD:
                            reaction_height = 100;
                            break;
                    }

                    if (ball.position.Y > reaction_height)
                    {
                        int direction = 1;

                        if (paddle_opponent.position.Center.X < ball.position.Center.X)
                            direction = 1;
                        else
                            direction = -1;
                        paddle_opponent.Move_to_x((int)(paddle_opponent.position.X + enemy_velocity * delta * direction));
                    }
                }

                // if you score a goal
                if (ball.isGoal)
                {
                    if (!player_turn)
                        score++;
                    else
                        enemy_score++;

                    ball.isGoal = false;
                    game_coin.Play(volume, pitch, pan);
                }

                // if you failed

                if (ball.isFail)
                {
                    ball.isFail = false;

                    if (player_turn)
                    {
                        if (lifes >= 1)
                            lifes--;
                    }
                    else
                    {
                        if (enemy_lifes >= 1)
                            enemy_lifes--;
                    }

                    if (enemy_lifes < 1 || lifes < 1)
                    {
                        //gameover
                        game_over_arcade.Play(volume, pitch, pan);
                        end_game = true;
                        ball.position.X = 10000;
                    }
                    else
                        fail.Play(volume - 0.5f, pitch, pan);

                    player_turn = true;
                    if (!Collision.ListOfObjectsWithCollision.Contains(paddle))
                        Collision.ListOfObjectsWithCollision.Add(paddle);

                    if (Collision.ListOfObjectsWithCollision.Contains(paddle_opponent))
                        Collision.ListOfObjectsWithCollision.Remove(paddle_opponent);
                }

                // when ball hits a paddle
                if (ball.isInPaddle)
                {
                    shot.Play(volume, pitch, pan);
                }
            }
            else
            {
                game.ShowCursor();
                // show scoreboard

                int mouse_x = game.mouseState.X;
                int mouse_y = game.mouseState.Y;

                if (retry_rect.Contains(mouse_x, mouse_y))
                {
                    hover = 1;
                }
                else if (quit_rect.Contains(mouse_x, mouse_y))
                {
                    hover = 2;
                }
                else
                    hover = 0;

                // mouse clicking

                if (game.mouseState.LeftButton == ButtonState.Pressed)
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
                            game.ShowMainMenu();
                            break;
                        default:
                            // whoops
                            break;

                    }


                }
            }

        }
    }
}
