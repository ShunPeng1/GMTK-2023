using _Scripts.Cards;
using UnityEngine;
using UnityUtilities;

namespace _Scripts.Managers
{
    public class InputManager : SingletonMonoBehaviour<InputManager>
    {
        
        private Vector3 _mousePosition;
        private int _currentColor = 0;
        
        [Header("Drag Objects")]
        [SerializeField] private Transform _mapParent;
        private bool _isDragging = false;
        private Vector3 _cardOffset;
        private BaseCard _draggingCard;
        private CardPlaceRegion _lastCardPlaceRegion;
        private CardPlaceHolder _lastCardPlaceHolder;
        
        [Header("Spin Object")] 
        [SerializeField] private float _spinDuration = 0.25f;
        private bool _isSpinning = false;
        
        private void Update()
        {
            UpdateMousePosition();
            
            if (Input.GetMouseButtonDown(0))
            {
               StartDragMouse();
            }

            if (Input.GetMouseButton(0))
            {
                DragMouse();
                
            }

            if (Input.GetMouseButtonUp(0))
            {
                EndDragMouse();
            }
        }

        private void UpdateMousePosition()
        {
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _mousePosition = new Vector3(worldMousePosition.x, worldMousePosition.y, 0);
        }

        private TResult CastMouseFindFirst<TResult>()
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(_mousePosition, Vector2.zero);

            foreach (var hit in hits)
            {
                var result = hit.transform.gameObject.GetComponent<TResult>();
                if ( result != null) return result;
            }

            return default;
        }

        private void StartDragMouse()
        {
            _draggingCard = CastMouseFindFirst<BaseCard>();
            _lastCardPlaceHolder = CastMouseFindFirst<CardPlaceHolder>();
            _lastCardPlaceRegion = CastMouseFindFirst<CardPlaceRegion>();

            if (_draggingCard == null) return;
            
            _cardOffset = _draggingCard.transform.position - _mousePosition;
            _isDragging = true;

            if (_lastCardPlaceHolder == null) return;
            _lastCardPlaceRegion.TakeOutTemporary(_lastCardPlaceHolder);

        }

        private void DragMouse()
        {
            if (!_isDragging) return; 
            
            _draggingCard.transform.position = _mousePosition + _cardOffset;
            
        }

        private void EndDragMouse()
        {
            if (!_isDragging) return;
            
            var placeRegion = CastMouseFindFirst<CardPlaceRegion>();
            var placeHolder = CastMouseFindFirst<CardPlaceHolder>();
            
            _draggingCard.transform.position = _mousePosition + _cardOffset;
            
            if (placeHolder == null)
            {
                if (_lastCardPlaceRegion != null) _lastCardPlaceRegion.ReAddTemporary(_draggingCard);
            }
            else
            {
                placeRegion.AddCard(_draggingCard, placeHolder);

                if (_lastCardPlaceHolder != null)
                {
                    _lastCardPlaceRegion.RemoveTemporary(_draggingCard);
                }
            }
            
            _draggingCard = null;
            _lastCardPlaceHolder = null;
            _lastCardPlaceRegion = null;

        }
        
        
    }
}