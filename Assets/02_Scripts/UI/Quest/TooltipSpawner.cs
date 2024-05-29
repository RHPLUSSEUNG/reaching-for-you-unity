using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class TooltipSpawner : MonoBehaviour, IPointerDownHandler, IPointerExitHandler
{
    [Tooltip("The prefab of the tooltip to spawn.")]
    [SerializeField] GameObject tooltipPrefab = null;

    GameObject tooltip = null;

    public abstract void UpdateTooltip(GameObject tooltip);

    public abstract bool CanCreateTooltip();

    private void OnDestroy()
    {
        ClearTooltip();
    }

    private void OnDisable()
    {
        ClearTooltip();
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        var parentCanvas = GetComponentInParent<Canvas>();

        if (tooltip && !CanCreateTooltip())
        {
            ClearTooltip();
        }

        if (!tooltip && CanCreateTooltip())
        {
            tooltip = Instantiate(tooltipPrefab, parentCanvas.transform);
        }

        if (tooltip)
        {
            UpdateTooltip(tooltip);
            PositionTooltip();
        }
    }

    private void PositionTooltip()
    {
        Canvas.ForceUpdateCanvases();

        var tooltipCorners = new Vector3[4];
        tooltip.GetComponent<RectTransform>().GetWorldCorners(tooltipCorners);
        var slotCorners = new Vector3[4];
        GetComponent<RectTransform>().GetWorldCorners(slotCorners);

        bool below = transform.position.y > Screen.height / 2;
        bool right = transform.position.x < Screen.width / 2;

        int slotCorner = GetCornerIndex(below, right);
        int tooltipCorner = GetCornerIndex(!below, !right);

        tooltip.transform.position = slotCorners[slotCorner] - tooltipCorners[tooltipCorner] + tooltip.transform.position;
    }

    private int GetCornerIndex(bool below, bool right)
    {
        if (below && !right) return 0;
        else if (!below && !right) return 1;
        else if (!below && right) return 2;
        else return 3;

    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        //ClearTooltip();
    }

    private void ClearTooltip()
    {
        if (tooltip)
        {
            Destroy(tooltip.gameObject);
        }
    }
}