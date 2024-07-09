using UnityEngine;

public class PrefabManager
{
    Vector3 instantiatepos = new (99, 0.8f, 99);
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if( prefab == null)
        {
            //Debug.Log($"Prefab Not Found : {path}");
            return null;
        }
        prefab.transform.position = instantiatepos;
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
