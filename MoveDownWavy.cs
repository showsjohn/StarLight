using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

    
    public class MoveDownWavy: IMoveBehavior
    {
        GameTime gameTime = new GameTime();
        float deltaTime;
       public void Move(Enemy obj, float deltaTime)
        {
            obj.localPosition.X += (float)(Math.Sin(obj.localPosition.Y / 15) * 2);
            obj.localPosition.Y += obj.velocity.Y * this.deltaTime;
        }

        public void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }

    public class MoveDownStraight : IMoveBehavior
    {
        GameTime gameTime = new GameTime();
        float deltaTime;
        public void Move(Enemy obj, float deltaTime)
        {
            obj.localPosition.X += obj.velocity.X * this.deltaTime; ;
            obj.localPosition.Y += obj.velocity.Y * this.deltaTime;
        }

        public void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }

