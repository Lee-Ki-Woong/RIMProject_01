using UnityEngine;

public class CreateBackGround : MonoBehaviour
{
    private void Start()
    {
        CreateBackGroundThisTransform();
    }

    private void CreateBackGroundThisTransform()
    {
        GameObject backGround = ResourceManager.Instance.LoadAssetSync<GameObject>("UI/BackGround");

        Instantiate(backGround, this.transform);
    }
   

}
