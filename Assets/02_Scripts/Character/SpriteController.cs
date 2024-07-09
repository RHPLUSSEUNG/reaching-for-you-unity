using System.Collections;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class SpriteController : MonoBehaviour
{
    private Transform sprite;
    private Transform character;
    private Transform shadow;
    [SerializeField]
    float rotateSpeed = 5.0f;
    [SerializeField]
    Direction direction = Direction.Right;
    GameObject mainCamera;

    Animator anim;
    float targetAngleX;
    float targetAngleY;
    float targetAngleZ;
    float angleX;
    float angleY;
    bool isIdle;
    public bool cameraChanged;

    void Awake()
    {
        sprite = this.transform.GetChild(0);
        character = sprite.GetChild(0);
        shadow = sprite.GetChild(1);
        anim = character.GetComponent<Animator>();
        isIdle = true;
        mainCamera = GameObject.Find("Main Camera");
        SetAnimState(AnimState.Idle);
    }
    private void LateUpdate()
    {
        SetRotation(mainCamera.transform.eulerAngles.x, mainCamera.transform.eulerAngles.y);
        
        if (direction == Direction.Left)
        {
            StartCoroutine(RotateCharacter(Direction.Left));
        }
        else
        {
            StartCoroutine(RotateCharacter(Direction.Right));
        }
        StartCoroutine(RotateSprite(mainCamera.transform.eulerAngles.y));
    }
    public void SetAnimState(AnimState state)
    {
        if (!anim)
        {
            return;
        }
        switch(state)
        {
            case (AnimState.Idle):
                anim.SetInteger("State", (int)AnimState.Idle);
                break;
            case (AnimState.Move):
                anim.SetInteger("State", (int)AnimState.Move);
                break;
            case (AnimState.Hit):
                anim.SetInteger("State", (int)AnimState.Hit);
                break;
            case (AnimState.Attack):
                anim.SetInteger("State", (int)AnimState.Attack);
                break;
            case (AnimState.Potion):
                anim.SetInteger("State", (int)AnimState.Potion);
                break;
            //case (AnimState.Roll):
            //    anim.SetInteger("State", (int)AnimState.Roll);
            //    break;
        }
    }
    public void Flip(Direction _direction)  //입력 방향에 맞게 스프라이트를 회전
    {
        direction = _direction;

        if (direction == Direction.Left)
        {
            StartCoroutine(RotateCharacter(Direction.Left));
        }
        else if (direction == Direction.Right)
        {
            StartCoroutine(RotateCharacter(Direction.Right));
        }
    }
    public void SetRotation(float _angleX, float _angleY)   //main카메라에 맞게 스프라이트를 회전
    {
        angleX = _angleX;
        angleY = _angleY;
    }
    IEnumerator RotateCharacter(Direction direction)
    {
        isIdle = false;
        float startAngleX = character.eulerAngles.x;
        float startAngleY = character.eulerAngles.y;
        float t = 0f;
        float speed = 0f;

        if (direction == Direction.Left)
        {
            targetAngleX = Mathf.Abs(angleX);
            targetAngleY = Mathf.Abs(angleY) % 360;
        }
        else
        {
            targetAngleX = -Mathf.Abs(angleX);
            targetAngleY = 180 + Mathf.Abs(angleY);
        }

        while (speed < 1.0f)
        {
            t += Time.deltaTime;
            speed = rotateSpeed * t;

            float xRotation = Mathf.LerpAngle(startAngleX, targetAngleX, speed);
            float yRotation = Mathf.LerpAngle(startAngleY, targetAngleY, speed);
            character.eulerAngles = new Vector3(xRotation, yRotation, character.eulerAngles.z);
            yield return null;
        }
        isIdle = true;
    }
    IEnumerator RotateSprite(float cameraAngleY)
    {
        isIdle = false;
        float startAngleY = sprite.eulerAngles.y;
        float t = 0f;
        float speed = 0f;

        while (speed < 1.0f)
        {
            t += Time.deltaTime;
            speed = rotateSpeed * t;

            float yRotation = Mathf.LerpAngle(startAngleY, cameraAngleY, speed);
            sprite.eulerAngles = new Vector3(sprite.eulerAngles.x, yRotation, sprite.eulerAngles.z);

            yield return null;
        }
    }

}
