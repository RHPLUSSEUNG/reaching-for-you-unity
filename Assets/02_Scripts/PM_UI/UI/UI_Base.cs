using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Base : MonoBehaviour
{
    protected Dictionary<Type, UnityEngine.Object[]> object_type = new Dictionary<Type, UnityEngine.Object[]>();
    public abstract void Init();

    private void Awake()
    {
        Init();
    }

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);

        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        object_type.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
            {
                objects[i] = Util.FindChild(gameObject, names[i], true);
            }
            else
            {
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);
            }

            if (objects[i] == null)
            {
                Debug.Log($"Bind Failed : {names[i]}");
            }
        }
    }

    protected T Get<T>(int index) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (object_type.TryGetValue(typeof(T), out objects) == false)
        {
            return null;
        }

        return objects[index] as T;
    }

    protected GameObject GetObject(int index)
    {
        return Get<GameObject>(index);
    }
    protected Text GetText(int index)
    {
        return Get<Text>(index);
    }
    protected Button GetButton(int index)
    {
        return Get<Button>(index);
    }
    protected Image GetImage(int index)
    {
        return Get<Image>(index);
    }

    public static void BindEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Enter)
    {
        UI_Event evt = Util.GetOrAddComponent<UI_Event>(go);
        switch (type)
        {
            case Define.UIEvent.Enter:
                evt.OnEnterHandler -= action;
                evt.OnEnterHandler += action;
                break;
            case Define.UIEvent.Exit:
                evt.OnExitHandler -= action;
                evt.OnExitHandler += action;
                break;
            case Define.UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
        }
    }
}
