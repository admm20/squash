using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace squash_lab1
{
    class Goal : Collision
    {
        public Goal(Rectangle position)
        {
            base.position = position;
            type = GameObjectType.GOAL;
            ListOfObjectsWithCollision.Add(this);

            side_ratio = (double)position.Width / (double)position.Height;
        }
        
    }
}
