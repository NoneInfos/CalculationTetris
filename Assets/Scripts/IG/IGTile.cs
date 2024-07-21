using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IGMain;
using UnityEngine.UI;
using TMPro;

namespace IGMain
{
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

        public void HighlightTileAsEmpty()
        {

        }

        public void HighlightTileAsColide()
        {

        }

        public void ResetTile()
        {
            IsPlaceBlock = false;
            // 타일의 시각적 상태를 초기화
            _spriteRenderer.color = Color.white; // 또는 기본 색상으로 설정
                                                 // 필요한 경우 다른 속성들도 초기화
        }

    }
}

