using System;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(RectTransform))]
    public class Square : MonoBehaviour
    {
        public Texture x;
        public Texture o;
        public RawImage teamImage;
    
        private int _row;
        private int _column;
        private Button _button;
        private Action<int, int> _onClick;
    
        public void Setup(int row, int column)
        {
            _row = row;
            _column = column;
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
            _onClick = (_, _) => { };
        }

        public void RegisterOnClick(Action<int,int> selectSquare)
        {
            _onClick = selectSquare;
        }

        public void Reset()
        {
            _onClick = (_, _) => { };
            SetTeam(Team.None);
        }

        private void OnClick()
        {
            _onClick.Invoke(_row,_column);
        }

        public void SetTeam(Team team)
        {
            switch (team)
            {
                case Team.X:
                    teamImage.texture = x;
                    teamImage.gameObject.SetActive(true);
                    break;
                case Team.O:
                    teamImage.texture = o;
                    teamImage.gameObject.SetActive(true);
                    break;
                case Team.None:
                default:
                    teamImage.texture = null; // TODO: use blank texture
                    teamImage.gameObject.SetActive(false);
                    break;
            }
        }
    }
}