using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class ItemsButton : MonoBehaviour, ISelectHandler, IPointerClickHandler, ISubmitHandler
{
    [SerializeField] private TextMeshProUGUI _itemsName;

    [SerializeField] private ItemButtonEvent _onSelectEvent;
    [SerializeField] private ItemButtonEvent _onSubmitEvent;
    [SerializeField] private ItemButtonEvent _onClickEvent;

    public ItemButtonEvent OnSelectEvent { get => _onSelectEvent; set => _onSelectEvent = value; }
    public ItemButtonEvent OnSubmitEvent { get => _onSubmitEvent; set => _onSubmitEvent = value; }
    public ItemButtonEvent OnClickEvent { get => _onClickEvent; set => _onClickEvent = value; }
    public string ItemsNameValue { get => _itemsName.text; set => _itemsName.text = value; }

    public void OnPointerClick(PointerEventData eventData)
    {
        _onClickEvent.Invoke(this);
    }

    public void OnSelect(BaseEventData eventData)
    {
        _onSelectEvent.Invoke(this);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        _onSelectEvent?.Invoke(this);
    }

    public void OptainSelectionFocus()
    {
        EventSystem.current.SetSelectedGameObject(this.gameObject);

        _onSelectEvent.Invoke(this);
    }
}

[System.Serializable]
public class ItemButtonEvent : UnityEvent<ItemsButton> { }
