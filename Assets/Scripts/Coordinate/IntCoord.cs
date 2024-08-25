using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntCoord
{
    public int x;
    public int y;
    public IntCoord(int _x,int _y){
        x = _x;
        y = _y;
    }
     public IntCoord(Vector2 _position){
        x = Mathf.RoundToInt(_position.x);
        y = Mathf.RoundToInt(_position.y);
    }
}
