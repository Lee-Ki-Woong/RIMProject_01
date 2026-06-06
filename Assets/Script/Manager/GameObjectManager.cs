using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectManager : BaseManager<GameObjectManager>
{
    private Party m_party;

    protected override void Awake()
    {
        base.Awake();

    }

    public void SetParty(List<CharacterData> partyData)
    {
        if (m_party == null)
        {
            CreateParty();
        }

        m_party.InitPlayerData(partyData);
    }

    public void DestroyParty()
    {
        if (m_party != null)
        {
            Destroy(m_party.gameObject);
        }
    }

    private void CreateParty()
    {
        if (this.TryInstantiate(LoadUtil.Sync.LoadPrefab(AddressUtil.Sync.Prefab.Party), this.transform, out GameObject partyInstance) == false)
        {
            this.LogError("Party 프리팹을 생성할 수 없습니다!!");
            return;
        }

        if (partyInstance.TryGetComponent(out Party partyScript) == false)
        {
            this.LogError("Party 프리팹에서 Party 스크립트를 찾을 수 없었습니다!!");
            return;
        }

        m_party = partyScript;
    }
}
