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
        List<GameObject> boxes = new List<GameObject>(boxNoteList);

        for (int i = 0; i < boxes.Count; i++)
        {
            float notePosX = boxes[i].transform.localPosition.x;

            for(int j = 0; j < timingBoxs.Length; j++)
            {
                if(timingBoxs[j].x <= notePosX && notePosX <= timingBoxs[j].y)
                {
                    Destroy(boxes[i]);
                    // boxes[i].GetComponent<Note>().gameObject.SetActive(false);
                    boxes.RemoveAt(i);
    
                    judgementEffect.JudgementAnim(j);

                    // 점수 증가
                    MagicBasicsScore.Instance.IncreaseScore(j);

                    boxNoteList = boxes;
                    return;
                }
            }
        }
        MagicBasicsScore.Instance.IncreaseScore(timingRect.Length - 1);
        judgementEffect.JudgementAnim(timingRect.Length - 1);
    }
}
