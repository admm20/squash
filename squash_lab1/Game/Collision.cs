using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace squash_lab1
{
    class Collision : GameObject
    {
        public static List<GameObject> ListOfObjectsWithCollision = new List<GameObject>();
        
        public double side_ratio = 1.0;
        

        public Collision()
        {
        }
        
        public bool Collided(Collision collider)
        {
            if (position.Intersects(collider.position))
                return true;
            return false;
        }

        public bool Collided(GameObject gameObject)
        {
            if (position.Intersects(gameObject.position))
                return true;
            return false;
        }

        public bool Collided(Rectangle rectangle)
        {
            if (this.position.Intersects(rectangle))
                return true;
            return false;
        }

    }
}
