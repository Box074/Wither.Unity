using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HKLabColliderControl : MonoBehaviour
{
    public bool HitTop = false;
    public bool HitDown = false;
    public bool HitLeft = false;
    public bool HitRight = false;
    public BoxCollider2D col;

    private void OnEnable()
    {
        HitTop = HitDown = HitLeft = HitRight = false;
    }
    private void FixedUpdate()
    {
        float mx = col.bounds.max.x;
        float my = col.bounds.max.y;
        float minx = col.bounds.min.x;
        float miny = col.bounds.min.y;
        float cx = col.bounds.center.x;
        float cy = col.bounds.center.y;
        HitTop = 
            Physics2D.Raycast(new Vector2(minx, my), Vector2.up, 0.75f, 1 << 8) ||
            Physics2D.Raycast(new Vector2(cx, my), Vector2.up, 0.75f, 1 << 8) ||
            Physics2D.Raycast(new Vector2(mx, my), Vector2.up, 0.75f, 1 << 8);
        HitLeft =
            Physics2D.Raycast(new Vector2(minx, my), Vector2.left, 0.75f, 1 << 8) ||
            Physics2D.Raycast(new Vector2(minx, cy), Vector2.left, 0.75f, 1 << 8) ||
            Physics2D.Raycast(new Vector2(minx, miny), Vector2.left, 0.75f, 1 << 8);
        HitDown =
            Physics2D.Raycast(new Vector2(minx, miny), Vector2.down, 0.75f, 1 << 8) ||
            Physics2D.Raycast(new Vector2(cx, miny), Vector2.down, 0.75f, 1 << 8) ||
            Physics2D.Raycast(new Vector2(mx, miny), Vector2.down, 0.75f, 1 << 8);
        HitRight =
            Physics2D.Raycast(new Vector2(mx, my), Vector2.right, 0.75f, 1 << 8) ||
            Physics2D.Raycast(new Vector2(mx, cy), Vector2.right, 0.75f, 1 << 8) ||
            Physics2D.Raycast(new Vector2(mx, miny), Vector2.right, 0.75f, 1 << 8);
    }

}
