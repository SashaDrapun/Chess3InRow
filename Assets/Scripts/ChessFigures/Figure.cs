using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using System.Drawing;

public abstract class Figure
{
    public int XPosition { get; set; }
    public int YPosition { get; set; }

    public abstract List<Point> WhereCanMove();
}
