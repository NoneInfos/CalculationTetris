using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IGMain;

public class IGInputController : ControllerBase
{
    private IGGameController _gameController;


    private Vector3 _dragStartPosition;
    private Vector3 _dragCurrentPosition;
    private IGBlock _selectedBlock;
    private Camera _mainCamera;
    private bool _isDragging = false;

    // 다른 컨트롤러에서 참조할 수 있도록 델리게이트 이벤트 정의
    public delegate void BlockSelectedHandler(IGBlock block);
    public delegate void BlockDraggedHandler(Vector3 position);
    public delegate void BlockReleasedHandler(Vector3 position);

    public event BlockSelectedHandler OnBlockSelected;
    public event BlockDraggedHandler OnBlockDragged;
    public event BlockReleasedHandler OnBlockReleased;

    public override void InitializeController()
    {
        _mainCamera = Camera.main;

        // 게임 컨트롤러 참조 가져오기
        _gameController = GetComponentInParent<IGGameController>();
        if (_gameController == null)
        {
            Debug.LogError("Game Controller reference not found!");
        }
    }

    public override void UpdateController()
    {
        HandleInputs();
    }

    private void HandleInputs()
    {
        // 터치 및 마우스 입력 모두 처리 (모바일 & PC 호환)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            HandleTouchInput(touch);
        }
        else
        {
            HandleMouseInput();
        }
    }

    private void HandleTouchInput(Touch touch)
    {
        Vector3 touchPosition = _mainCamera.ScreenToWorldPoint(touch.position);
        touchPosition.z = 0;

        switch (touch.phase)
        {
            case TouchPhase.Began:
                HandleInputBegan(touchPosition);
                break;
            case TouchPhase.Moved:
                HandleInputMoved(touchPosition);
                break;
            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                HandleInputEnded(touchPosition);
                break;
        }
    }

    private void HandleMouseInput()
    {
        Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        if (Input.GetMouseButtonDown(0))
        {
            HandleInputBegan(mousePosition);
        }
        else if (Input.GetMouseButton(0) && _isDragging)
        {
            HandleInputMoved(mousePosition);
        }
        else if (Input.GetMouseButtonUp(0) && _isDragging)
        {
            HandleInputEnded(mousePosition);
        }
    }

    private void HandleInputBegan(Vector3 position)
    {
        _dragStartPosition = position;
        _isDragging = false;

        // 레이캐스트로 블록 선택 확인
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero);
        if (hit.collider != null)
        {
            IGBlock block = hit.collider.GetComponent<IGBlock>();
            if (block != null)
            {
                _selectedBlock = block;
                _isDragging = true;
                OnBlockSelected?.Invoke(_selectedBlock);
                
                // 사운드 재생
                AudioManager.Instance.Play("BlockPickup");
            }
        }
    }

    private void HandleInputMoved(Vector3 position)
    {
        if (!_isDragging || _selectedBlock == null)
            return;

        _dragCurrentPosition = position;
        _selectedBlock.transform.position = position;

        // 블록이 이동할 때마다 충돌 체크
        CheckBlockCollision(_selectedBlock, position);

        OnBlockDragged?.Invoke(position);
    }

    private void HandleInputEnded(Vector3 position)
    {
        if (!_isDragging || _selectedBlock == null)
            return;

        _isDragging = false;
        OnBlockReleased?.Invoke(position);
        
        // 사운드 재생
        AudioManager.Instance.Play("BlockPlace");
        
        _selectedBlock = null;
    }

    public override void ClearController()
    {
        _selectedBlock = null;
        _isDragging = false;
    }

    public override void FinalizeController()
    {
        // 이벤트 구독 해제 등 정리 작업
        OnBlockSelected = null;
        OnBlockDragged = null;
        OnBlockReleased = null;
    }

    private void CheckBlockCollision(IGBlock block, Vector3 position)
    {
        Vector2Int gridPos = block.WorldToGridPosition(position);

        // 게임 컨트롤러를 통해 충돌 체크 요청
        bool canPlace = _gameController.CheckBlockPlacement(block, gridPos);

        block.SetCollisionState(!canPlace);
    }
}