using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] GameObject LVLCompleted, Background, LVLSuccess, Star1, Star2, Star3, Coin, GoButton, BackgroundFailed, againButton, MenuButton, EarnedMoney1;

    private Vector3 backgroundTargetPosition;
    private Vector3 backgroundFailedTargetPosition;

    public void AnimateWin()
    {
        Vector3 currentPosition = LVLSuccess.transform.position;
        Vector3 targetPosition = new Vector3(currentPosition.x, currentPosition.y + 3.7f, currentPosition.z);
        backgroundTargetPosition = new Vector3(targetPosition.x, targetPosition.y - 85f, targetPosition.z);
        EarnedMoney1.transform.localScale = Vector3.one;
        EarnedMoney1.transform.localPosition = new Vector3(EarnedMoney1.transform.localPosition.x, EarnedMoney1.transform.localPosition.y, EarnedMoney1.transform.localPosition.z);


        LeanTween.scale(LVLSuccess, new Vector3(1f, 1f, 1f), 2f).setDelay(0.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.move(LVLSuccess, targetPosition, 0.5f).setDelay(0.8f).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.scale(LVLSuccess, new Vector3(1f, 1f, 1f), 2f).setDelay(0.1f).setEase(LeanTweenType.easeInOutCubic);

        LeanTween.moveLocal(Background, backgroundTargetPosition, 0.7f).setDelay(0.5f).setEase(LeanTweenType.easeOutCirc);

        // �������� ��� LVLCompleted
        LeanTween.scale(LVLCompleted, new Vector3(1.2f, 1.2f, 1.2f), 0.5f).setDelay(1f).setEase(LeanTweenType.easeOutBack);
        LeanTween.moveLocalY(LVLCompleted, LVLCompleted.transform.localPosition.y + 50f, 0.5f).setDelay(1f).setEase(LeanTweenType.easeOutBack);

        // �������� ��� Coin
        LeanTween.scale(Coin, new Vector3(1.2f, 1.2f, 1.2f), 0.5f).setDelay(3f).setEase(LeanTweenType.easeOutBack);
        LeanTween.moveLocalY(Coin, Coin.transform.localPosition.y + 50f, 0.5f).setDelay(3f).setEase(LeanTweenType.easeOutBack);


        LeanTween.scale(EarnedMoney1, new Vector3(1.2f, 1.2f, 1.2f), 0.5f).setDelay(3.5f).setEase(LeanTweenType.easeOutBack);
        LeanTween.moveLocalY(EarnedMoney1, EarnedMoney1.transform.localPosition.y + 50f, 0.5f).setDelay(3.5f).setEase(LeanTweenType.easeOutBack);

        // �������� ��� GoButton
        LeanTween.scale(GoButton, new Vector3(1.2f, 1.2f, 1.2f), 0.5f).setDelay(4f).setEase(LeanTweenType.easeOutBack);
        LeanTween.moveLocalY(GoButton, GoButton.transform.localPosition.y + 50f, 0.5f).setDelay(4f).setEase(LeanTweenType.easeOutBack);
    }

    public void AnimateStars()
    {
        LeanTween.scale(Star1, new Vector3(1.5f, 1.5f, 1.5f), 0.5f).setDelay(1.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.moveLocalY(Star1, Star1.transform.localPosition.y + 30f, 0.5f).setDelay(1.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(Star1, new Vector3(1f, 1f, 1f), 0.5f).setDelay(2f).setEase(LeanTweenType.easeOutElastic);

        LeanTween.scale(Star2, new Vector3(1.5f, 1.5f, 1.5f), 0.5f).setDelay(2f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.moveLocalY(Star2, Star2.transform.localPosition.y + 30f, 0.5f).setDelay(2f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(Star2, new Vector3(1f, 1f, 1f), 0.5f).setDelay(2f).setEase(LeanTweenType.easeOutElastic);

        LeanTween.scale(Star3, new Vector3(1.5f, 1.5f, 1.5f), 0.5f).setDelay(2.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.moveLocalY(Star3, Star3.transform.localPosition.y + 30f, 0.5f).setDelay(2.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(Star3, new Vector3(1f, 1f, 1f), 0.5f).setDelay(2.5f).setEase(LeanTweenType.easeOutElastic);
    }

    public void AnimateLose()
    {
        Vector3 currentPosition = LVLSuccess.transform.position;
        Vector3 targetPosition = new Vector3(currentPosition.x, currentPosition.y + 3.7f, currentPosition.z);
        backgroundFailedTargetPosition = new Vector3(targetPosition.x, targetPosition.y - 1f, targetPosition.z);


        LeanTween.moveLocal(BackgroundFailed, backgroundFailedTargetPosition, 0.7f).setDelay(0.5f).setEase(LeanTweenType.easeOutCirc);

        LeanTween.scale(againButton, new Vector3(1.2f, 1.2f, 1.2f), 0.5f).setDelay(1f).setEase(LeanTweenType.easeOutBack);
        LeanTween.moveLocalY(againButton, againButton.transform.localPosition.y + 50f, 0.5f).setDelay(1f).setEase(LeanTweenType.easeOutBack);

        LeanTween.scale(MenuButton, new Vector3(1.2f, 1.2f, 1.2f), 0.5f).setDelay(1f).setEase(LeanTweenType.easeOutBack);
        LeanTween.moveLocalY(MenuButton, MenuButton.transform.localPosition.y + 50f, 0.5f).setDelay(1f).setEase(LeanTweenType.easeOutBack);
    }

    void Start()
    {

        
    }
}
