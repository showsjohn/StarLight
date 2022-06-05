using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

    public interface IMoveBehavior
    
    {
    void Move(Enemy obj, float deltaTime);
    void Update(GameTime gameTime);
    }

