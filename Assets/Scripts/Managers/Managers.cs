using Unity.VisualScripting;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static bool DontDestroyOnLoaded = false;
    private static Managers _manager = null;

    #region Managers Fields
    [SerializeField]
    private int ManagerID ;
    [SerializeField]
    private DataManager _dataManager = null;
    [SerializeField]
    private InputManager _inputManager = null;
    [SerializeField]
    private ResourceManager _resourceManager = null;
    [SerializeField]
    private SoundManager _soundManager = null;
    #endregion

    #region Managers Properties
    private static Managers Manager
    {
        get
        {
            Init();
            return _manager;
        }
    }

    public static DataManager DataManager { get { return Manager._dataManager; } }
    public static InputManager InputManager { get { return Manager._inputManager; } }
    public static ResourceManager ResourceManager { get { return Manager._resourceManager; } }
    public static SoundManager SoundManager { get { return Manager._soundManager; } }
    #endregion

    #region Managers Methods
    //매니저 초기화
    private static void Init()
    {
        if (!_manager)
        {
            GameObject manager = GameObject.FindWithTag("Manager");
            if (manager == null)
            {
                manager = new GameObject($"{ObjectName.Manager}");
                manager.tag = TagName.Manager;
                manager.SetActive(false);
            }
            _manager = manager.GetOrAddComponent<Managers>();
            _manager.ManagerID = manager.GetInstanceID();
            _manager._inputManager = new InputManager(_manager.ManagerID);
            _manager._dataManager = new DataManager(_manager.ManagerID);
            _manager._soundManager = new SoundManager(_manager.ManagerID);
            _manager._resourceManager = new ResourceManager(_manager.ManagerID);
            if (!DontDestroyOnLoaded)
            {
                DontDestroyOnLoad(manager);
                DontDestroyOnLoaded = true;
            }
            _manager._inputManager.Init();
            manager.SetActive(true);
        }
    }
    #endregion
    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
       InputManager.InputActionEnable();
    }

    void Start()
    {

    }

    private void OnDisable()
    {
       InputManager.InputActionDisable();
    }

}