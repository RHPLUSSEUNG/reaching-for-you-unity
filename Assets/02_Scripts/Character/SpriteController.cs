using System.Collections;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    private Transform sprite;
    [SerializeField]
    float rotateSpeed = 5.0f;

    GameObject mainCamera;

    Animator anim;
    float targetAngleX;
    float targetAngleY;
    float angleX;
    float angleY;
    bool isIdle;
    Direction direction;
    void Awake()
    {
        sprite = this.transform.GetChild(0);
        anim = sprite.GetComponent<Animator>();
        isIdle = true;
        direction = Direction.Right;
        mainCamera = GameObject.Find("Main Camera");
        SetAnimState(AnimState.Idle);
    }
    private void LateUpdate()
    {
        SetRotation(mainCamera.transform.eulerAngles.x, mainCamera.transform.eulerAngles.y);

        if (direction == Direction.Left)
        {
            StartCoroutine(RotateSprite(Direction.Left));
        }
        else
        {
            StartCoroutine(RotateSprite(Direction.Right));
        }
    }
    public void SetAnimState(AnimState state)
    {
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
            StartCoroutine(RotateSprite(Direction.Left));
        }
        else if (direction == Direction.Right)
        {
            StartCoroutine(RotateSprite(Direction.Right));
        }
    }
    public void SetRotation(float _angleX, float _angleY)   //main카메라에 맞게 스프라이트를 회전
    {
        angleX = _angleX;
        angleY = _angleY;
    }
    IEnumerator RotateSprite(Direction direction)
    {
        isIdle = false;
        float startAngleX = sprite.transform.eulerAngles.x;
        float startAngleY = sprite.transform.eulerAngles.y;
        float t = 0f;
        float speed = 0f;

        //if (mainCamera.transform.eulerAngles.y)
        if (direction == Direction.Left)
        {
            targetAngleX = Mathf.Abs(angleX);
            targetAngleY = Mathf.Abs(angleY) %360;
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
            sprite.transform.eulerAngles = new Vector3(xRotation, yRotation, sprite.transform.eulerAngles.z);

            yield return null;
        }
        isIdle = true;
    }
}
