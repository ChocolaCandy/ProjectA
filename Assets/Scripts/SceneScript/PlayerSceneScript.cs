
public class PlayerSceneScript : BaseSceneScript
{
    /// <summary>
    /// 씬 로드시 실행될 메서드
    /// </summary>
    protected override void OnSceneLoad()
    {
        if (gameObject.activeSelf == false)
            gameObject.SetActive(true);
    }

    /// <summary>
    /// 씬 언로드시 실행될 메서드
    /// </summary>
    protected override void OnSceneUnLoad()
    {
        if (gameObject.activeSelf == true)
            gameObject.SetActive(false);
    }
}
