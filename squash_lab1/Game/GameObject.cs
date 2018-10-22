using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace squash_lab1
{
    enum GameObjectType
    {
        PADDLE,
        WALL,
        GOAL,
        BALL,
        LOSE_AREA
    }

    class GameObject
    {
        public Rectangle position;
        public GameObjectType type;

        public GameObject()
        {
        }

    }
}
