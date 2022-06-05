using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


internal class GameObjectList : GameObject, IEnumerable<GameObject>
{
    public List<GameObject> children;

    public GameObjectList()
    {
        children = new List<GameObject>();
    }

    public IEnumerator<GameObject> GetEnumerator()
    {
        foreach (var child in children.ToArray()) yield return child;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void AddChild(GameObject child)
    {
        child.Parent = this;
        children.Add(child);
    }

    public void RemoveChild(GameObject child)
    {
        child.Parent = this;
        children.Remove(child);
    }

    public override void Reset()
    {
        foreach (var child in children) child.Reset();
    }

    public override void InputHandler()
    {
        base.InputHandler();
        foreach (var child in children) child.InputHandler();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        for (var i = 0; i < children.Count; i++) children[i].Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        foreach (var child in children) child.Draw(gameTime, spriteBatch);
    }
}