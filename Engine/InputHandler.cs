using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


internal class InputHandler
{
    private KeyboardState currentKeyboardState, previousKeyboardState;
    private MouseState currentMouseState, previousMouseState;

    public InputHandler() { }
    

    public void Update(GameTime gameTime)
    {
        previousMouseState = currentMouseState;
        previousKeyboardState = currentKeyboardState;
        currentMouseState = Mouse.GetState();
        currentKeyboardState = Keyboard.GetState();
    }

    /// <summary>Checks if a key is currently pressed</summary>
    public bool IsKeyDown(Keys k)
    {
        return currentKeyboardState.IsKeyDown(k);
    }

    /// <summary>Checks if no key is currently being pressed</summary>
    public bool NoKeyPressed()
    {
        return currentKeyboardState.GetPressedKeys() == null;
    }

    public bool KeyWasPressed(Keys key)
    {
        return currentKeyboardState.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key);
    }

    public Point MousePosition()
    {
        return currentMouseState.Position;
    }
}