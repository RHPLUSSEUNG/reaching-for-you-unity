using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldUI : MonoBehaviour
{
    private void Update()
    {
        Transform parent = transform.parent;

        transform.position = parent.position;
    }

    // TODO : ���ȭ �� �ڵ����
}
