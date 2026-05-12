using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // [SerializeField]
    [SerializeField] private GameObject MyCanvas;

    // [Field]
    public static UIManager Instance {  get; private set; }
    private Transform UIFolder;

    private Dictionary<UIType, GameObject> m_uiListDic = new();
    private Dictionary<UIType, UniTask<GameObject>> m_checkUniTaskDic = new(); 
    private HashSet<UIType> m_checkUIHash = new();

    // [Life Cycle]
    private void Awake()
    {
        if(Instance == null) Instance = this;

        InstantiateCanvas().Forget();
    }


    // [Instantiate]
    private async UniTaskVoid InstantiateCanvas()
    {
        if (MyCanvas == null && UIFolder == null)
        {
            MyCanvas = await ResourceManager.Instance.LoadAsset<GameObject>("Base/Canvas", destroyCancellationToken);
            GameObject canvas = Instantiate(MyCanvas);
            canvas.name = "UIFolder";
            UIFolder = MyCanvas.transform;
        }
    }


    // [Create UI]
    private async UniTask<GameObject> CreateUIAsync(UIType uiType, CancellationToken token)
    {
        GameObject ui = await ResourceManager.Instance.LoadAsset<GameObject>(uiType.ToString(), token);
        if (ui == null) return null;

        GameObject uiInstance = Instantiate(ui, UIFolder);
        uiInstance.name = uiType.ToString();

        return uiInstance;
    }


    // [Open UI]
    public async UniTask OepnUIAsync(UIType uiType, CancellationToken token)
    {
        if (m_checkUIHash.Contains(uiType)) return;

        if (m_uiListDic.TryGetValue(uiType, out GameObject ui))
        {
            ui.SetActive(true);
            return;
        }

        if (m_checkUniTaskDic.TryGetValue(uiType, out UniTask<GameObject> uniTask))
        {
            await uniTask;
            return;
        }

        UniTask<GameObject> newUniTask = CreateUIAsync(uiType, token);
        m_checkUniTaskDic.Add(uiType, newUniTask);

        try
        {
            GameObject newUI = await newUniTask;

            if (newUI != null)
            {
                m_uiListDic.Add(uiType, newUI);
                m_checkUIHash.Add(uiType);
                newUI.SetActive(true);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
            m_uiListDic.Remove(uiType);
        }
        finally
        {
            m_checkUniTaskDic.Remove(uiType);
        }
    }

    public void CloseUI(UIType uiType)
    {
        if (m_checkUIHash.Contains(uiType) == false) return;

        if (m_uiListDic.TryGetValue(uiType, out GameObject ui))
        {
            m_checkUIHash.Remove(uiType);
            ui.SetActive(false);
        }
    }
}
