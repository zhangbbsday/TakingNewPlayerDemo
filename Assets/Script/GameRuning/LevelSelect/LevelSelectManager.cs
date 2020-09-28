using System.IO;
using System.Collections;
using System.Collections.Generic;
using Assets.Script;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour
{
    [SerializeField] private RectTransform _fileListTrans;
    private List<LevelText> ScriptList { get;set; } = new List<LevelText>();
    private GameObject _origin;
    private int SelectedIndex { get; set; } = 0;
    private float TextHeight { get; set; } = 39f;//好像因为有layout组件挂在上面，所以不能获取到这个值。请手动输入
    GameObject Origin {
        get
        {
            if(_origin==null) _origin = _fileListTrans.GetChild(0).gameObject;
            return _origin;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //创造文件并加入列表
        DirectoryInfo di = new DirectoryInfo( Application.dataPath+@"/Levels");
        if (!di.Exists)
            di.Create();

        var i = di.GetFiles();
        foreach (var fi in i)
        {
            string name = fi.Name;
            if(  !(name.EndsWith(".xml")||name.EndsWith(".XML") )  ) continue;
            CreatFileText(fi.Name);
        }
        Destroy(_origin);
    }

    // Update is called once per frame
    void Update()
    {
        //根据输入设置SelectedIndex
        if (Input.mouseScrollDelta.y < 0) SelectedIndex++;
        if (Input.mouseScrollDelta.y > 0) SelectedIndex--;
        if (SelectedIndex < 0) SelectedIndex = ScriptList.Count - 1;
        if (SelectedIndex > ScriptList.Count - 1) SelectedIndex = 0;
        
        //根据SelectIndex设置选中文件
        //var file = ScriptList[SelectedIndex];
        foreach (var s in ScriptList)
        {
            s.Text.alignment = TextAnchor.MiddleRight;
            s.Text.color = new Color(0.5f,0.5f,0.5f);
            (s.transform as RectTransform).offsetMax *= Vector2.up;
            (s.transform as RectTransform).offsetMax += Vector2.right*_fileListTrans.rect.width;
        }

        if (SelectedIndex >= ScriptList.Count)
            return;

        ScriptList[SelectedIndex].Text.alignment = TextAnchor.MiddleLeft;
        ScriptList[SelectedIndex].Text.color = new Color(0.1725f,0.1725f,0.1725f);
        
        //设置列表位置
        float distance = GetPositionOfFile(SelectedIndex) - _fileListTrans.anchoredPosition.y;
        float p = distance;
        i += distance;
        float d = distance - previous;
        float speed = p*0.1f+i*0.03f+d*0.2f;
        _fileListTrans.anchoredPosition += speed * Vector2.up;
        previous = distance;
    }
    private float i = 0;
    private float previous = 0;

    float GetPositionOfFile(int index)
    {
        float count = ScriptList.Count;
        return (index - (count-1) / 2) * TextHeight;
    }

    /// <summary>
    /// 根据文件名制造一个文本框，并纳入字典管理
    /// </summary>
    void CreatFileText(string name)
    {
        var newObj = Instantiate(Origin, _fileListTrans);
        var script = newObj.GetComponent<LevelText>();
        script.Init(name);
        ScriptList.Add(script);
    }
    public void OnButtonClick()
    {
        if (SelectedIndex >= ScriptList.Count)
            return;

        GameManager.LevelPath = Application.dataPath+@"/Levels/"+ScriptList[SelectedIndex].Name;
        GameEditor.SceneUtils.ChangeScene("LevelPlayerScene");
    }
}
