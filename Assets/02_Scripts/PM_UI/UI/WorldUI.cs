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
