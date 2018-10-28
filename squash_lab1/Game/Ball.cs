using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace squash_lab1
{
    struct Velocity
    {
        public double x_direction;
        public double y_direction;

        public void Set_velocity(double x, double y)
        {
            x_direction = x;
            y_direction = y;
        }
    }

    class Ball : Collision
    {
        public Velocity speed;
        
        public bool isInPaddle = false;
        public bool isGoal = false;
        public bool isFail = false;

        public GameMode game;

        public Ball(Rectangle position)
        {
            
            base.position = position;
            type = GameObjectType.BALL;

            speed.Set_velocity(0.3, -0.3);
            
        }

        public void Update_position(float delta)
        {
            int old_x = position.X;
            int old_y = position.Y;

            position.X += (int)(speed.x_direction * delta);
            position.Y += (int)(speed.y_direction * delta);
            

            foreach(Collision gameObject in ListOfObjectsWithCollision)
            {

                if (Collided(gameObject) && !isInPaddle){

                    position.X = old_x;
                    position.Y = old_y;

                    // calculate distance between object and ball
                    double dist_x = Math.Abs(position.Center.X - gameObject.position.Center.X);
                    double dist_y = Math.Abs(position.Center.Y - gameObject.position.Center.Y);

                    double ratio = dist_x / dist_y;

                    if (ratio < gameObject.side_ratio && !isInPaddle)
                    {
                        speed.y_direction *= -1;

                        // odbijanie pilki od paletki ma byc podobne do tego, jak jest w arkanoidach:
                        if (gameObject.type.Equals(GameObjectType.PADDLE))
                        {
                            double x1 = 0.3, y1 = -0.3;
                            if (dist_x > 40)
                            {
                                x1 = 0.384513829233677;
                                y1 = -0.179301743237636;

                            }
                            
                            if (position.Center.X < gameObject.position.Center.X - 10) // ball is on left side
                            {
                                speed.x_direction = -x1;
                                speed.y_direction = y1;
                            }
                            else if (position.Center.X > gameObject.position.Center.X + 10)
                            {
                                speed.x_direction = x1;
                                speed.y_direction = y1;
                            }
                            else // around middle of paddle
                            {
                                speed.y_direction = y1;
                                if (speed.x_direction < 0)
                                    speed.x_direction = -0.3;
                                else
                                    speed.x_direction = 0.3;
                                   

                            }

                        }

                        // jeżeli piłka uderzyła w paletke gracza, to usuń go z listy obiektów kolizyjnych
                        // i dodaj przeciwnika do niej przeciwnika (i vice versa)
                        if (gameObject.type.Equals(GameObjectType.PADDLE))
                        {
                            game.player_turn = !game.player_turn;
                            if (game.player_turn)
                            {
                                ListOfObjectsWithCollision.Add(game.paddle);
                                ListOfObjectsWithCollision.Remove(game.paddle_opponent);
                            }
                            else
                            {
                                ListOfObjectsWithCollision.Add(game.paddle_opponent);
                                ListOfObjectsWithCollision.Remove(game.paddle);

                            }

                        }
                    }
                    else
                    {
                        // specyficzny przypadek, gdzie paletka uderza piłkę bokiem 
                        if (gameObject.type.Equals(GameObjectType.PADDLE))
                        {
                            Paddle paddle = (Paddle)gameObject;
                            if (speed.x_direction > 0)
                            {
                                if (paddle.lastMove.Equals(LastMove.LEFT))
                                    speed.x_direction *= -1;
                            }
                            else
                                if (paddle.lastMove.Equals(LastMove.RIGHT))
                                speed.x_direction *= -1;
                        }
                        else
                            speed.x_direction *= -1;
                    }

                    if (gameObject.type.Equals(GameObjectType.PADDLE))
                    {                 
                        isInPaddle = true;
                    }

                    if (gameObject.type.Equals(GameObjectType.LOSE_AREA))
                    {
                        // PRZEGRALES
                        position.X = 600;
                        position.Y = 400;
                        speed.x_direction = 0.3;
                        speed.y_direction = -0.3;

                        isFail = true;
                    }

                    if (gameObject.type.Equals(GameObjectType.GOAL))
                    {
                        // PUNKT 
                        //Console.WriteLine("Masz punkt");
                        isGoal = true;                 
                    }

                    break;
                }
                else if (gameObject.type.Equals(GameObjectType.PADDLE) && !Collided(gameObject)){
                    isInPaddle = false;
                }
            }
            
        }
        
        
    }
}
