﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace squash_lab1
{


    abstract class ProgramState
    {
        public abstract void LoadTextures(ContentManager content);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Update(double delta, SquashGame game);
        
    }
}
