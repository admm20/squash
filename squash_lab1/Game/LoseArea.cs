using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace squash_lab1
{
    class LoseArea : Wall
    {

        public LoseArea(Rectangle position) : base(position)
        {
            type = GameObjectType.LOSE_AREA;
        }

    }
}
