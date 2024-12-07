using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnhancedScrollManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ScrollRect horizontalScrollRect; // Горизонтальный ScrollRect (родительский)
    public ScrollRect verticalScrollRect;   // Вертикальный ScrollRect (в магазине)

    private bool isDraggingHorizontally;    // Флаг для определения направления ввода
    private bool isDraggingVertically;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Сбрасываем флаги перед анализом направления
        isDraggingHorizontally = false;
        isDraggingVertically = false;

        // Анализируем направление движения
        if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
        {
            isDraggingHorizontally = true;
            horizontalScrollRect.OnBeginDrag(eventData);
        }
        else
        {
            isDraggingVertically = true;
            verticalScrollRect.OnBeginDrag(eventData);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Перенаправляем событие в активный ScrollRect
        if (isDraggingHorizontally)
        {
            horizontalScrollRect.OnDrag(eventData);
        }
        else if (isDraggingVertically)
        {
            verticalScrollRect.OnDrag(eventData);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Завершаем перетаскивание в активном ScrollRect
        if (isDraggingHorizontally)
        {
            horizontalScrollRect.OnEndDrag(eventData);
        }
        else if (isDraggingVertically)
        {
            verticalScrollRect.OnEndDrag(eventData);
        }
    }
}
