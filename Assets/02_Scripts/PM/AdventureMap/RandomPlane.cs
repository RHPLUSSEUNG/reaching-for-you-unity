using UnityEngine;

public class RandomPlane : MonoBehaviour
{
    public GameObject desertBasicPlane;
    public GameObject waterBasicPlane;

    public GameObject CurePlane;
    public GameObject SavePlane;

    public void SpawnBasic() 
    {
        if(AdventureManager.StageNumber == 0)
            desertBasicPlane.SetActive(true);
        else if(AdventureManager.StageNumber == 1)
            waterBasicPlane.SetActive(true);
        CurePlane.SetActive(false);
        SavePlane.SetActive(false);
    } 

    public void SpawnCure()
    {
        desertBasicPlane.SetActive(false);
        waterBasicPlane.SetActive(false);
        CurePlane.SetActive(true);
        SavePlane.SetActive(false);
    }

    public void SpawnSave()
    {
        desertBasicPlane.SetActive(false);
        waterBasicPlane.SetActive(false);
        CurePlane.SetActive(false);
        SavePlane.SetActive(true);
    }
}
