using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

public interface IExplodable
    {
    public event Action<Vector2> ShipExploded;
    }

