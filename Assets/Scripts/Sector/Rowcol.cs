using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Rowcol
{
    public int row;
    public int column;

    public Rowcol(int r, int c)
    {
        row = r;
        column = c;
    }

    public static bool operator ==(Rowcol a, Rowcol b)
    {
        return a.row == b.row && a.column == b.column;
    }

    public static bool operator !=(Rowcol a, Rowcol b)
    {
        return !(a == b);
    }
    public override bool Equals(object obj)
    {
        if (!(obj is Rowcol))
            return false;

        return this == (Rowcol)obj;
    }
}