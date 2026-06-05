using Cysharp.Threading.Tasks;
using System.Collections.Generic;

public class GameObjectManager : BaseManager<GameObjectManager>
{



    protected override void Awake()
    {
        base.Awake();

    }

    private void AwakeSetting()
    {

    }

    private async UniTask LoadEnemyData()
    {
        foreach(KeyValuePair<string, EnemyData> keyValuePair in GameDataManager.Instance.EnemyDataList)
        {
            return;
        }
    }



}
