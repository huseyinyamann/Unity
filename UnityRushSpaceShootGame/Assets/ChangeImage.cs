using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    public Sprite[] imageList;
    private int currentImageIndex = 0;
    private Image imageComponent;
    public Image imageComponent2;
    private Sprite mainPlayer;
    public Button[] buttons;

    void Start()
    {
        imageComponent = GetComponent<Image>();
        mainPlayer = imageComponent2.sprite;
    }

    public async void Change()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        currentImageIndex = 0;
        while (currentImageIndex<imageList.Length)
        {
            imageComponent.sprite = imageList[currentImageIndex];
            imageComponent2.sprite = imageList[currentImageIndex];
            currentImageIndex++;
            await Task.Delay(100);

            print("devam");
           
        }
        while (currentImageIndex < imageList.Length)
        {
            imageComponent.sprite = imageList[currentImageIndex];
            imageComponent2.sprite = imageList[currentImageIndex];
            currentImageIndex++;
            await Task.Delay(100);

            print("devam");

        }
        while (currentImageIndex < imageList.Length)
        {
            imageComponent.sprite = imageList[currentImageIndex];
            imageComponent2.sprite = imageList[currentImageIndex];
            currentImageIndex++;
            await Task.Delay(100);

            print("devam");

        }

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
        }

    }

    public void Back()
    {
        imageComponent2.sprite = mainPlayer;
    }

}
