using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> boxNoteList = new List<GameObject>();

    [SerializeField] Transform center = null; // perfect 판정
    [SerializeField] RectTransform[] timingRect = null; // 판정 범위
    Vector2[] timingBoxs = null; // 판정 범위 최솟값, 최댓값

    // Start is called before the first frame update
    void Start()
    {
        // 타이밍 박스 설정
        timingBoxs = new Vector2[timingRect.Length];

        for (int i = 0; i < timingRect.Length; i++)
        {
            timingBoxs[i].Set(center.localPosition.x - timingRect[i].rect.width / 2,
                              center.localPosition.x + timingRect[i].rect.width / 2);
        }
    }

    public void CheckTiming()
    {
        for (int i = 0; i < boxNoteList.Count; i++)
        {
            float notePosX = boxNoteList[i].transform.localPosition.x;

            for(int j = 0; j < timingBoxs.Length; j++)
            {
                if(timingBoxs[j].x <= notePosX && notePosX <= timingBoxs[j].y)
                {
                    Destroy(boxNoteList[i]);
                    boxNoteList.RemoveAt(i);
                    Debug.Log("Hit: "+ timingRect[j].name);
                    return;
                }
            }
        }

        // Debug.Log("Miss");
    }
}
