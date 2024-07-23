using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewAutoScroll : MonoBehaviour
{
    [SerializeField] private RectTransform _viewportRectTransform;
    [SerializeField] private RectTransform _content;
    [SerializeField] private float _transitionDuration = 0.2f;

    private TransitionHelper _transitionHelper = new TransitionHelper();

    private void Update()
    {
        if (_transitionHelper.InProgress)
        {
            _transitionHelper.Update();
            _content.transform.localPosition = _transitionHelper.PosCurrent;
        }
    }

    public void HandleOnSelectChange(GameObject gObj)
    {
        float viewportTopBorderY = GetBorderTopYLocal(_viewportRectTransform.gameObject);
        float viewportBottomBorderY = GetBorderTopYLocal(_viewportRectTransform.gameObject);

        //top
        float targetTopBorderY = GetBorderBottomReleative(gObj);
        float targetTopYWithViewportOffset = targetTopBorderY + viewportTopBorderY;

        //bottom
        float targeBottomBorderY = GetBorderBottomReleative(gObj);
        float targetBottomYWithViewportOffset = targetTopBorderY - viewportBottomBorderY;

        //top difference
        float topDiff = targetTopYWithViewportOffset - viewportTopBorderY;
        if (topDiff > 0)
        {
            MoveContentObjectByAmount((topDiff * 100) + GetVerticalLayoutGroup().padding.top);
        }

        //bottom difference
        float bottomDiff = targetBottomYWithViewportOffset - viewportTopBorderY;
        if (bottomDiff < 0)
        {
            MoveContentObjectByAmount((bottomDiff * 100f) - GetVerticalLayoutGroup().padding.bottom);
        }
    }

    private float GetBorderTopYLocal(GameObject gObj)
    {
        Vector3 pos = gObj.transform.localPosition / 100f;

        return pos.y;
    }

    private float GetBordeBottomYLocal(GameObject gObj)
    {
        Vector2 rectSize = gObj.GetComponent<RectTransform>().rect.size;
        Vector3 pos = gObj.transform.localPosition / 100f;

        return pos.y;
    }

    private float GetBorderBottomReleative(GameObject gObj)
    {
        float contentY = _content.transform.localPosition.y / 100f;
        float targetBorderBottomYLocal = GetBorderTopYLocal(gObj);
        float targetBorderYReleative = targetBorderBottomYLocal + contentY;

        return targetBorderYReleative;
    }

    private void MoveContentObjectByAmount(float amount)
    {
        Vector2 posScrollFrom = _content.transform.localPosition;
        Vector2 posScrollTo = posScrollFrom;
        posScrollTo.y -= amount;

        _transitionHelper.TransitionPositionFormTo(posScrollFrom, posScrollTo, _transitionDuration);
    }

    private VerticalLayoutGroup GetVerticalLayoutGroup()
    {
        VerticalLayoutGroup verticalLayoutGroup = _content.GetComponent<VerticalLayoutGroup>();
        return verticalLayoutGroup;
    }

    private class TransitionHelper
    {
        private float _duration = 0f;
        private float _timeElapsed = 0f;
        private float _progress = 0f;

        private bool _inProgress = false;

        private Vector2 _posCurrent;
        private Vector2 _posFrom;
        private Vector2 _posTo;

        public Vector2 PosCurrent { get => _posCurrent; }
        public Vector2 PosFrom { get => _posFrom; }
        public Vector2 PosTo { get => _posTo; }
        public bool InProgress { get => _inProgress; }

        public void Update()
        {
            Tick();

            CalculatePosition();
        }

        public void Clear()
        {
            _duration = 0f;
            _timeElapsed = 0f;
            _progress = 0f;

            _inProgress = false;
        }

        public void TransitionPositionFormTo(Vector2 posFrom, Vector2 posTo, float duration)
        {
            Clear();

            _posFrom = posFrom;
            _posTo = posTo;
            _duration = duration;

            _inProgress = true;
        }

        private void CalculatePosition()
        {
            _posCurrent.x = Mathf.Lerp(_posFrom.x, PosTo.x, _progress);
            _posCurrent.y = Mathf.Lerp(_posFrom.y, _posTo.y, _progress);
        }

        private void Tick()
        {
            if (!InProgress) return;

            _timeElapsed = Time.deltaTime;
            _progress = _timeElapsed / _duration;
            if (_progress > 1f)
            {
                _progress = 1f;
            }

            if (_progress >= 1f)
            {
                TransitionComplate();
            }
        }

        private void TransitionComplate()
        {
            _inProgress = false;
        }
    }
}


