using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] GameObject LVLCompleted, Background, LVLSuccess, Star1, Star2, Star3, Coin, GoButton;
    private Vector3 backgroundTargetPosition;

    public void AnimateWin()
    {
        Vector3 currentPosition = LVLSuccess.transform.position;
        Vector3 targetPosition = new Vector3(currentPosition.x, currentPosition.y + 3.7f, currentPosition.z);
        backgroundTargetPosition = new Vector3(targetPosition.x, targetPosition.y - 85f, targetPosition.z);

        LeanTween.scale(LVLSuccess, new Vector3(1f, 1f, 1f), 2f).setDelay(0.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.move(LVLSuccess, targetPosition, 0.5f).setDelay(0.8f).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.scale(LVLSuccess, new Vector3(1f, 1f, 1f), 2f).setDelay(0.1f).setEase(LeanTweenType.easeInOutCubic);

        LeanTween.moveLocal(Background, backgroundTargetPosition, 0.7f).setDelay(0.5f).setEase(LeanTweenType.easeOutCirc);

        // Анимация для LVLCompleted
        LeanTween.scale(LVLCompleted, new Vector3(1.2f, 1.2f, 1.2f), 0.5f).setDelay(1f).setEase(LeanTweenType.easeOutBack);
        LeanTween.moveLocalY(LVLCompleted, LVLCompleted.transform.localPosition.y + 50f, 0.5f).setDelay(1f).setEase(LeanTweenType.easeOutBack);

        // Анимация для Coin
        LeanTween.scale(Coin, new Vector3(1.2f, 1.2f, 1.2f), 0.5f).setDelay(3f).setEase(LeanTweenType.easeOutBack);
        LeanTween.moveLocalY(Coin, Coin.transform.localPosition.y + 50f, 0.5f).setDelay(3f).setEase(LeanTweenType.easeOutBack);

        // Анимация для GoButton
        LeanTween.scale(GoButton, new Vector3(1.2f, 1.2f, 1.2f), 0.5f).setDelay(3.5f).setEase(LeanTweenType.easeOutBack);
        LeanTween.moveLocalY(GoButton, GoButton.transform.localPosition.y + 50f, 0.5f).setDelay(3.5f).setEase(LeanTweenType.easeOutBack);
    }

    public void AnimateStars()
    {
        // Анимация для Star1
        LeanTween.scale(Star1, new Vector3(1.5f, 1.5f, 1.5f), 0.5f).setDelay(1.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.moveLocalY(Star1, Star1.transform.localPosition.y + 30f, 0.5f).setDelay(1.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(Star1, new Vector3(1f, 1f, 1f), 0.5f).setDelay(2f).setEase(LeanTweenType.easeOutElastic);

        // Анимация для Star2
        LeanTween.scale(Star2, new Vector3(1.5f, 1.5f, 1.5f), 0.5f).setDelay(2f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.moveLocalY(Star2, Star2.transform.localPosition.y + 30f, 0.5f).setDelay(2f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(Star2, new Vector3(1f, 1f, 1f), 0.5f).setDelay(2f).setEase(LeanTweenType.easeOutElastic);

        // Анимация для Star3
        LeanTween.scale(Star3, new Vector3(1.5f, 1.5f, 1.5f), 0.5f).setDelay(2.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.moveLocalY(Star3, Star3.transform.localPosition.y + 30f, 0.5f).setDelay(2.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(Star3, new Vector3(1f, 1f, 1f), 0.5f).setDelay(2.5f).setEase(LeanTweenType.easeOutElastic);
    }

    void Start()
    {

    }
}
