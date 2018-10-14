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
        
        bool isInPaddle = false;

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
                    }

                    if (gameObject.type.Equals(GameObjectType.GOAL))
                    {
                        // PUNKT DLA GRYFFINDORU
                        Console.WriteLine("Masz punkt");
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
