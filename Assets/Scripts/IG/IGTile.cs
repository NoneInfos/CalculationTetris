using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IGMain;
using UnityEngine.UI;
using TMPro;

namespace IGMain
{
    /*
    
    IGTile�� �����͸� ������ ��� ���� ���� ��带 ���� ���� ������Ʈ�� ������ �ؾ��ϳ�

    ��ϳ�忡 �浹 üũ ������Ʈ�� �������� �� ���ư��°� �ƴұ�?

     */

    enum TileState
    {
        None,       //�� ����
        Stable,     //��� ���� ����
        UnStable,   //��� ����� ��
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

