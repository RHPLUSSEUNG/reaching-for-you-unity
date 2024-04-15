using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerButton : MonoBehaviour
{
    #region singleton
    static PlayerButton _instance;
    public static PlayerButton playerButton { get { return _instance; } }
    #endregion
    public ButtonState state = ButtonState.Idle;
    public GameObject player;
    public PlayerSpec spec;

    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;


    public Vector3 skillPos = Vector3.zero;
    Active skill = null;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.CompareTag("Character"))
                {
                    Debug.Log("Character Selected : " + hit.collider.gameObject.name);

                    //TODO UI
                    UpdateButton(hit.collider.gameObject);
                }
                else if (hit.transform.gameObject.CompareTag("Monster"))
                {
                    Debug.Log("Monster Selected");
                    //TODO UI
                }
            }else if (state == ButtonState.Skill)
            {
                skillPos = Input.mousePosition;
                skill.Activate(skillPos);
                state = ButtonState.Idle;
            }
        }
    }

    public void UpdateButton(GameObject player)
    {
        Debug.Log("clear");
        this.player = player;
        this.spec = this.player.GetComponent<PlayerSpec>();
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button3.onClick.RemoveAllListeners();

        button1.onClick.AddListener(Skill1);
        button2.onClick.AddListener(Skill2);
        button3.onClick.AddListener(Skill3);
    }

    public void TestClick()
    {
        PlayerSpec spec = player.GetComponent<PlayerSpec>();
        Debug.Log(spec.name);
    }


    public void Skill1()
    {
        skill = spec._equipSkills[0] as Active;
        
        state = ButtonState.Skill;
    }

    public void Skill2()
    {
        skill = spec._equipSkills[1] as Active;
        
        state = ButtonState.Skill;
    }

    public void Skill3()
    {
        skill = spec._equipSkills[2] as Active;
        
        state = ButtonState.Skill;
    }

    public void Cancle()
    {
        state = ButtonState.Idle;
    }
}
