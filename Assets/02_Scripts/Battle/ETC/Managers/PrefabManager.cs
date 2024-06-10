using UnityEngine;

public class PrefabManager
{
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if( prefab == null)
        {
            Debug.Log($"Prefab Not Found : {path}");
            return null;
        }
        return Object.Instantiate(prefab, parent);
    }

    public void Destroy(GameObject go)
    {
        if(go != null )
        {
            Object.Destroy(go);
        }
    }
}
