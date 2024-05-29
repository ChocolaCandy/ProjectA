
public class PlayerSceneScript : BaseSceneScript
{
    /// <summary>
    /// �� �ε�� ����� �޼���
    /// </summary>
    protected override void OnSceneLoad()
    {
        if (gameObject.activeSelf == false)
            gameObject.SetActive(true);
    }

    /// <summary>
    /// �� ��ε�� ����� �޼���
    /// </summary>
    protected override void OnSceneUnLoad()
    {
        if (gameObject.activeSelf == true)
            gameObject.SetActive(false);
    }
}
