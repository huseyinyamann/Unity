using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewSample : MonoBehaviour
{
    [SerializeField] private RectTransform _content;
    [SerializeField] private GameObject _prefabListItem;

    [Space(10)]
    [Header("Scroll view Events")]
    [SerializeField] private ItemButtonEvent _eventItemClicked;
    [SerializeField] private ItemButtonEvent _eventItemOnSelect;
    [SerializeField] private ItemButtonEvent _eventItemOnSubmit;

    [Space(10)]
    [Header("Default Selected Index")]
    [SerializeField] private int defaultSelectedIndex = 0;

    [Space(10)]
    [Header("For Testing Only")]
    [SerializeField] private int _testButtonCount = 1; //total test button

    private void Start()
    {
        if (_testButtonCount > 0)
        {
            TestCreateItems(_testButtonCount);
            UpdateAllButtonNavigationalReferences();
        }

        StartCoroutine(DelaySelectedChild(defaultSelectedIndex));
    }

    public void SelectChild(int index)
    {
        int childCount = _content.childCount;

        if (index >= childCount)
        {
            return;
        }

        GameObject childObject = _content.transform.GetChild(index).gameObject;
        ItemsButton item = childObject.GetComponent<ItemsButton>();
        item.OptainSelectionFocus();
    }

    public IEnumerator DelaySelectedChild(int index)
    {
        yield return new WaitForSeconds(1f);

        SelectChild(index);
    }

    private void UpdateAllButtonNavigationalReferences()
    {
        ItemsButton[] children = _content.GetComponentsInChildren<ItemsButton>();

        if (children.Length < 2)
        {
            return; //must have at least 2 children for navigation to work correctly
        }

        ItemsButton item;
        Navigation navigation;

        for (int i = 0; i < children.Length; i++)
        {
            item = children[i];

            navigation = item.gameObject.GetComponent<Button>().navigation;

            navigation.selectOnUp = GetNavigationUp(i, children.Length);
            navigation.selectOnDown = GetNavigationDown(i, children.Length);

            item.gameObject.GetComponent<Button>().navigation = navigation;
        }
    }

    private Selectable GetNavigationDown(int indexCurrent, int totalEntries)
    {
        ItemsButton item;
        if (indexCurrent == totalEntries - 1) //last item
        {
            item = _content.transform.GetChild(0).GetComponent<ItemsButton>();
        }
        else
        {
            item = _content.transform.GetChild(indexCurrent + 1).GetComponent<ItemsButton>();
        }

        return item.GetComponent<Selectable>();
    }

    private Selectable GetNavigationUp(int indexCurrent, int totalEntries)
    {
        ItemsButton item;

        if (indexCurrent == 0)
        {
            item = _content.transform.GetChild(totalEntries - 1).GetComponent<ItemsButton>(); //this will loop the navigation and optional
        }
        else
        {
            item = _content.transform.GetChild(indexCurrent - 1).GetComponent<ItemsButton>();
        }

        return item.GetComponent<Selectable>();
    }

    private void TestCreateItems(int count)
    {
        for (int i = 0; i < count; i++)
        {
            CreateItem($"Player {i}");
        }
    }

    private ItemsButton CreateItem(string v)
    {
        GameObject gObj;
        ItemsButton item;

        gObj = Instantiate(_prefabListItem, Vector3.zero, Quaternion.identity);
        gObj.transform.SetParent(_content.transform);
        gObj.transform.localScale = new Vector3(1, 1, 1);
        gObj.transform.localPosition = new Vector3();
        gObj.transform.localRotation = Quaternion.Euler(new Vector3());
        gObj.name = v;

        //set the apporopatite params
        item = gObj.GetComponent<ItemsButton>();
        item.ItemsNameValue = v;

        //add event listeners
        item.OnSelectEvent.AddListener((ItemButton) => { HandleEventItemOnSelect(item); });
        item.OnSelectEvent.AddListener((ItemButton) => { HandleEventItemOnClick(item); });
        item.OnSelectEvent.AddListener((ItemButton) => { HandleEventItemOnSubmit(item); });

        return item;
    }

    private void HandleEventItemOnSubmit(ItemsButton item)
    {
        _eventItemOnSubmit.Invoke(item);
    }

    private void HandleEventItemOnClick(ItemsButton item)
    {
        _eventItemClicked.Invoke(item);
    }

    private void HandleEventItemOnSelect(ItemsButton item)
    {
        ScrollViewAutoScroll scrollViewAutoScroll = GetComponent<ScrollViewAutoScroll>();
     //   scrollViewAutoScroll.HandleOnSelectChange(item.gameObject);

        _eventItemOnSelect.Invoke(item);
    }
}
