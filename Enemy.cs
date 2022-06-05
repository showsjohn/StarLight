using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


public class Enemy : SpriteGameObject, IExplodable
{
    protected IMoveBehavior moveBehavior;
    private Texture2D debug;
    public event Action<Vector2> ShipExploded;
    public int health;
    private List<IExplosionHandler> handlers;

    public Enemy(string spriteName, Vector2 startPosition) : base(spriteName)
    {
        localPosition = startPosition;
        velocity = new Vector2(0,100);
        SetOriginToCenter();
        debug = ExtendedGame.AssetManager.LoadSprite("debug");
        health = 30;
        handlers = new List<IExplosionHandler>();
    }

    public void AddExplosionHandler(IExplosionHandler newHandler) => handlers.Add(newHandler);  

    private void TellHandlersAboutExplosion()
    {
        foreach(IExplosionHandler handler in handlers)
        {
            handler.HandleShipExploded();
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            var explosion = new Explosion(this);
            ShipExploded(localPosition);
            ExtendedGame.score += 100;
            ExtendedGame.gameManager.enemies.Remove(this);
        }
    }

    public void SetMoveBehavior(IMoveBehavior behavior) => moveBehavior = behavior;

    public void Move() => moveBehavior.Move(this, deltaTime);


    public override void Update(GameTime gameTime)
    {
        moveBehavior.Update(gameTime);
        
        localPosition.Y = (int)localPosition.Y;
    }
            
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(sprite, GlobalPosition, null, Color.White, 0f, origin, 1f, SpriteEffects.None, 1);
    }
}