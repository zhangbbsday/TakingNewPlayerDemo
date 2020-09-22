using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

namespace Assets.Script
{
    public class UIManager : MonoBehaviour
    {
        public enum StateEnum
        {
            AllDisable,
            AllAble,
            OnGaming,
        }
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _speedUpButton;
        [SerializeField] private Button _speedDownButton;
        [SerializeField] private Button _drillRocketButton;
        [SerializeField] private Button _returnRocketButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Text _speedText;
        [SerializeField] private Text _instructionText;
        [SerializeField] private Text _DrillRocketText;
        [SerializeField] private Text _ReturnRocketText;
        private List<Button> _buttonList;
        private static UIManager Instance;
        public static List<Button> ButtonList {
            get
            {
                if(Instance._buttonList==null)
                    Instance._buttonList = new List<Button>
                    {
                        Instance._startButton,
                        Instance._speedUpButton,
                        Instance._speedDownButton,
                        Instance._drillRocketButton,
                        Instance._returnRocketButton,
                        Instance._exitButton,
                    };
                return Instance._buttonList;
            }
        }
        public static float CustomTimeScale { get; private set; } = 1f;
        public static string Instruction
        {
            set { Instance._instructionText.text = value; }
        }

        public static StateEnum State
        {
            set
            {
                switch (value)
                {
                    case StateEnum.AllAble:
                        foreach (var b in ButtonList)
                        {
                            b.interactable = true;
                        }
                        break;
                    
                    case StateEnum.AllDisable:
                        foreach (var b in ButtonList)
                        {
                            b.interactable = false;
                        }
                        break;
                    
                    case StateEnum.OnGaming:
                        foreach (var b in ButtonList)
                        {
                            b.interactable = true;
                        }
                        Instance._returnRocketButton.interactable = false;
                        Instance._drillRocketButton.interactable = false;
                        Instance._startButton.GetComponentInChildren<Text>().text = "↙";
                        break;
                }
                
            }
        }
        UIManager()
        {
            Instance = this;
        }

        void Start()
        {
            ButtonsAddListener();
            
            string timeStr = CustomTimeScale.ToString("F1");
            _speedText.text = timeStr + "倍速";
        }
        
        void Update()
        {
            ResetRocketText();
        }
        private void ResetRocketText()
        {
            _DrillRocketText.text = "钻头箭 x " + GameManager.DrillRocketUnused;
            _ReturnRocketText.text = "掉头箭 x " + GameManager.ReturnRocketUnused;
        }

        private void ButtonsAddListener()
        {
            _drillRocketButton.onClick.AddListener(() =>
            {
                if (GameManager.DrillRocketUnused == 0) return;
                State = StateEnum.AllDisable;
                var r = Factory.CreatDrillRocket();
                r.SetByMouse();
                GameManager.DrillRocketUnused--;
            });
            _returnRocketButton.onClick.AddListener(() =>
            {
                if (GameManager.ReturnRocketUnused == 0) return;
                State = StateEnum.AllDisable;
                var r = Factory.CreatReturnRocket();
                r.SetByMouse();
                GameManager.ReturnRocketUnused--;
            });
            _startButton.onClick.AddListener(() =>
            {//在不同的游戏阶段，这个按钮有不同的作用
                switch (GameManager.StateMachine.State)
                {
                    case GameManager.StateEnum.SettingRocket:
                        State = StateEnum.OnGaming;
                        GameManager.GameStart();
                        _instructionText.text = "游戏开始！敌人正在赶到战场";
                        break;
                    default:
                        SceneManager.LoadScene("LevelPlayerScene");
                        break;
                }
                
            });
            _speedUpButton.onClick.AddListener(() =>
            {
                CustomTimeScale *= 1.3f;
                if (CustomTimeScale > 20) CustomTimeScale = 20;
                string timeStr = CustomTimeScale.ToString("F1");
                _speedText.text = timeStr + "倍速";

            });
            _speedDownButton.onClick.AddListener(() =>
            {
                CustomTimeScale /= 1.3f;
                string timeStr = CustomTimeScale.ToString("F1");
                _speedText.text = timeStr + "倍速";
            });
            _exitButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("LevelSelectScene");
            });
        }
    }
}
