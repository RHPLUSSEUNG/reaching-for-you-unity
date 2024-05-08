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

    // TODO : 모듈화 및 코드수정
}
