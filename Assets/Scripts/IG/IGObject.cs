using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace IGMain
{
  

    public class IGObject : MonoBehaviour
    {
        /// <summary>
        /// ¾²·Á³ª...?
        /// </summary>
        public int Index { get; set; } = 0;

        public void SetPos(Vector2 pos)
        {
            this.transform.position = pos;
        }
        public void SetIndex(int index)
        {
            Index = index;
        }
    }
}
