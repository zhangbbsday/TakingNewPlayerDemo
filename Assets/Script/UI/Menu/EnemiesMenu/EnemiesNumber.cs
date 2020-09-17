using UnityEngine.UI;

public class EnemiesNumber : UIElementBase
{
    private Text Text { get; set; }

    protected override void Start()
    {
        base.Start();
        Text = GetComponent<Text>();
    }

    protected override void Update()
    {
        base.Update();
        Text.text = GameManager.Instance.EnemyContainerManager.EnemyContainerNumber.ToString();
    }
}
