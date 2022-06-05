using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class GameObject
{
    public Vector2 localPosition;
    public Vector2 velocity;
    protected bool isVisible;
    public float deltaTime;
    public GameObject Parent { get; set; }

    public Vector2 GlobalPosition
    {
        get
        {
            if (Parent == null) return localPosition;
            return localPosition + Parent.GlobalPosition;
        }
    }

    public GameObject()
    {
        localPosition = Vector2.Zero;
        velocity = Vector2.Zero;
        isVisible = true;
    }

    public virtual void InputHandler(){}

    public virtual void Update(GameTime gameTime)
    {
        deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;
        localPosition += velocity * (float) gameTime.ElapsedGameTime.TotalSeconds;
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch){}

    public virtual void Reset() => velocity = Vector2.Zero;

}