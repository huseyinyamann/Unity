using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DotweenShake : MonoBehaviour
{
    public Image image;
    public float shakeStrength = 25f;
    public float shakeDuration = 0.5f;

    public bool isShake = false;

    public float destroyDamage;
    public PlayerController playerController;

    private void Start()
    {
       image = GetComponent<Image>();
    }

    private void Update()
    {
        if (playerController.health<=destroyDamage && !isShake)
        {
            // Shake işlemi için varsayılan konum ve boyut değerlerini kaydet
            Vector3 defaultPosition = image.rectTransform.localPosition;
            Vector3 defaultScale = image.rectTransform.localScale;

            // DOTween kullanarak shake işlemi gerçekleştir
            image.rectTransform.DOShakePosition(shakeDuration, shakeStrength)
                .OnComplete(() =>
                {
                    // Shake işlemi tamamlandığında varsayılan konum ve boyut değerlerine geri dön
                    image.rectTransform.localPosition = defaultPosition;
                    image.rectTransform.localScale = defaultScale;
                });

            StartCoroutine(Shaker());
            isShake= true;
        }
    }

    IEnumerator Shaker()
    {
        yield return new WaitForSeconds(shakeDuration);
        Destroy(gameObject);
    }
}
