using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> boxNoteList = new List<GameObject>();

    [SerializeField] Transform center = null; // perfect 판정
    [SerializeField] RectTransform[] timingRect = null; // 판정 범위
    Vector2[] timingBoxs = null; // 판정 범위 최솟값, 최댓값

    JudgementEffect judgementEffect = null;
    
    // Start is called before the first frame update
    void Start()
    {
        judgementEffect = GetComponent<JudgementEffect>();

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
                    
                    judgementEffect.JudgementAnim(j);

                    // 점수 증가
                    MagicBasicsScore.Instance.IncreaseScore(j);
                    return;
                }
            }
        }
        MagicBasicsScore.Instance.IncreaseScore(timingRect.Length - 1);
        judgementEffect.JudgementAnim(timingRect.Length - 1);
    }
}
