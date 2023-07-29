using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Outgame
{
    public class UIEventRankingView : UIStackableView
    {
        [SerializeField] RankingListView _listView;

        protected override void AwakeCall()
        {
            ViewId = ViewID.EventQuest;
            _hasPopUI = false;
        }

        private void Start()
        {
            _listView.Setup();
            Active();
        }

        public void GoEventHome()
        {
            UIManager.NextView(ViewID.Event);
        }

        public void Back()
        {
            UIManager.Back();
        }
    }
}
