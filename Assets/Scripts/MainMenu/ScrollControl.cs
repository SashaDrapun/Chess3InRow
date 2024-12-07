using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnhancedScrollManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ScrollRect horizontalScrollRect; // �������������� ScrollRect (������������)
    public ScrollRect verticalScrollRect;   // ������������ ScrollRect (� ��������)

    private bool isDraggingHorizontally;    // ���� ��� ����������� ����������� �����
    private bool isDraggingVertically;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // ���������� ����� ����� �������� �����������
        isDraggingHorizontally = false;
        isDraggingVertically = false;

        // ����������� ����������� ��������
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
        // �������������� ������� � �������� ScrollRect
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
        // ��������� �������������� � �������� ScrollRect
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
