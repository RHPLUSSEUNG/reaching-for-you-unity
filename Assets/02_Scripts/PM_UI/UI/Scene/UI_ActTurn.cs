using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ActTurn : UI_Scene
{
    [SerializeField] GameObject turnUI;

    //Test
    public List<Sprite> testList = new List<Sprite>();
    public Sprite[] tempList = new Sprite[8];
    public Sprite newTest;
    public override void Init()
    {
        Test();     // Test
        int child = transform.childCount;
        int objectCount = testList.Count/*Managers.Battle.ObjectList.Count*/;
        objectCount = 6;        //Test
        if (child > objectCount)
        {
            for(int i = 0; i < child - objectCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
        else
        {
            for(int i = 0; i < objectCount - child; i++)
            {
                Instantiate(turnUI, transform);
            }
        }
    }

    //public void Update()            // Test
    //{
    //    if(Input.GetMouseButtonDown(0))
    //    {
    //        TurnUpdate();
    //    }
    //    if(Input.GetMouseButtonDown(1))
    //    {
    //        DestroyTurnUI();
    //    }
    //    if(Input.GetMouseButtonDown(2))
    //    {
    //        MakeTurnUI(newTest);
    //    }
    //}

    public void Test()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Image img = transform.GetChild(i).gameObject.GetComponent<Image>();
            testList.Add(tempList[i]);
            img.sprite = tempList[i];
        }
    }

    public void SetTurnUI()
    {
        for(int i = 0; i < testList.Count/*Managers.Battle.ObjectList.Count*/; i++)
        {
            Image turnImage = transform.GetChild((testList.Count/*Managers.Battle.ObjectList.Count*/-1)-i).gameObject.GetComponent<Image>();
            Sprite charImage = Managers.Battle.ObjectList[i].GetComponent<Sprite>();            // 임시, 이미지를 가져온다

            charImage = testList[i];        // Test
            turnImage.sprite = charImage;
        }
    }

    public void MakeTurnUI(Sprite newObj)
    {
        Image newUI = Instantiate(turnUI, transform).GetComponent<Image>();
        newUI.sprite = newObj;
        SetTurnUI();
    }

    public void DestroyTurnUI()
    {
        Destroy(transform.GetChild(0).gameObject);
        SetTurnUI();
    }

    public void TurnUpdate()
    {
        Image curChar = transform.GetChild(transform.childCount - 1).gameObject.GetComponent<Image>();
        Sprite curSprite = curChar.sprite;
        for (int i = transform.childCount - 2; i >= 0; i--)
        {
            Image turnImg = transform.GetChild(i + 1).gameObject.GetComponent<Image>();
            Image moveImg = transform.GetChild(i).gameObject.GetComponent<Image>();

            turnImg.sprite = moveImg.sprite;
        }
        Image lastImg = transform.GetChild(0).gameObject.GetComponent<Image>();
        lastImg.sprite = curSprite;
        // TODO : Color 변경
    }
}
