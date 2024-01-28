using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IGMain;
using UnityEngine.UI;
using TMPro;

namespace IGMain
{
    /*
    
    IGTile에 데이터를 가지고 블록 노드랑 보드 노드를 만들어서 각자 업데이트를 돌리게 해야하나

    블록노드에 충돌 체크 업데이트가 쓸데없이 다 돌아가는건 아닐까?

     */

    enum TileState
    {
        None,       //빈 상태
        Stable,     //블록 결합 이후
        UnStable,   //블록 사라질 때
    }


    /// <summary>
    /// 
    /// </summary>
    public class IGTile : IGObject
    {
        [SerializeField] TextMeshProUGUI TXT_Index;

        [SerializeField] SpriteRenderer _spriteRenderer;

        private IGObjectData _objectData;

        private float radius = 36f;

        private bool _isCollide = false;

        public void SetUI()
        {
            TXT_Index.text = Index.ToString();
        }

        private void Update()
        {
            if (_isCollide)
            {
            }
            else
            {

            }

            _spriteRenderer.color = _isCollide ? Color.red : Color.black;



            _isCollide = false;
        }



        public void SetCollide(bool isCollide)
        {
            _isCollide = isCollide;
            //_spriteRenderer.color = isCollide ? Color.red : Color.black;
        }

    }
}

