using UnityEngine;

public class RandomPlane : MonoBehaviour
{
    public GameObject BasicPlane;
    public GameObject CurePlane;
    public GameObject SavePlane;

    public void SpawnBasic() 
    {
        BasicPlane.SetActive(true);
        CurePlane.SetActive(false);
        SavePlane.SetActive(false);
    } 

    public void SpawnCure()
    {
        BasicPlane.SetActive(false);
        CurePlane.SetActive(true);
        SavePlane.SetActive(false);
    }

    public void SpawnSave()
    {
        BasicPlane.SetActive(false);
        CurePlane.SetActive(false);
        SavePlane.SetActive(true);
    }
}
