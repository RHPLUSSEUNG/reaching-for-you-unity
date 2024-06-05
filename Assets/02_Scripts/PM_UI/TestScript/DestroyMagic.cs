using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMagic : Magic
{
    [SerializeField]
    GameObject range;
    Transform rangePos;
    Vector3 pos;
    bool isUsing = false;
    // Init 만들어서 Range 생성 후 UseMagic에서는 SetActive만?
    // Range 위치 조정
    private void Start()
    {
        // range = Managers.Resource.Instantiate("Range");
        range.SetActive(false);
        rangePos = range.GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        if (isUsing)
        {
            pos = Input.mousePosition;

            pos.z = Camera.main.WorldToScreenPoint(rangePos.position).z;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(pos);

            worldPosition.y = 0;
            rangePos.position = worldPosition;
        }
    }
    public override void UseMagic()
    {
        Debug.Log("파괴 마법 사용");
        isUsing = !isUsing;
        range.SetActive(isUsing);
    }
}
