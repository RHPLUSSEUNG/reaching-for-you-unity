using UnityEngine;

public class RaycastManager
{
    public void OnUpdate()
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
                    Managers.PlayerButton.Bind();
                    //TODO UI
                    Managers.PlayerButton.UpdateButton(hit.collider.gameObject);
                }
                else if (hit.transform.gameObject.CompareTag("Monster"))
                {
                    Debug.Log("Monster Selected");
                    //TODO UI
                }
            }
            else if (Managers.PlayerButton.state == ButtonState.Skill)
            {
               Managers.PlayerButton.SetSkillPos();
            }
        }
    }



}
