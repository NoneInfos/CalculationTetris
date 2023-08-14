using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace IGMain
{
    public class IGTile : MonoBehaviour
    {
        private int width { get; } = 76;
        private int height { get; } = 76;

        private float x = 0;
        private float y = 0;

        private int Index { get; set; } = 0;

        public void SetPosX(float x)
        {
            this.x = x;
        }
        public void SetPosY(float y)
        {
            this.y = y;
        }

        public void SetPos(Vector2 pos)
        {
            this.x = pos.x;
            this.y = pos.y;
        }

        public Vector2 GetPos() { return new Vector2(this.x, this.y); }
        public float GetPosX() { return this.x; }
        public float GetPosY() { return this.y; }
    }

}

