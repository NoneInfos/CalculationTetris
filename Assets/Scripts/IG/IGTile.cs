using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IGMain;
using UnityEngine.UI;
using TMPro;

namespace IGMain
{
    /*
    
    IGTile?? ???????? ?????? ???? ?????? ???? ?????? ???????? ???? ?????????? ?????? ????????

    ?????????? ???? ???? ?????????? ???????? ?? ?????????? ???????

     */

    


    /// <summary>
    /// 
    /// </summary>
    public class IGTile : IGObject
    {
        [SerializeField] TextMeshPro TXT_Index;

        [SerializeField] SpriteRenderer _spriteRenderer;

        private IGObjectData _objectData;

        private float radius = 36f;

        public bool IsColide { get; set; } = false;

        public bool IsPlaceBlock { get; set; } = false;

        public void SetUI()
        {
#if UNITY_EDITOR
            TXT_Index.enabled = true;
            TXT_Index.text = Index.ToString();
#else
            TXT_Index.enabled = false;
#endif
        }

        private void Update()
        {
            if (IsColide)
            {
            }
            else
            {

            }

            _spriteRenderer.color = IsColide ? Color.red : Color.black;


            IsColide = false;
        }



        public void SetCollide(bool isCollide)
        {
            IsColide = isCollide;
        }

    }
}

