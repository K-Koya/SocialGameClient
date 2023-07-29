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
        void Ready(int questId)
        {
            SequenceBridge.RegisterSequence("EventQuest", SequencePackage.Create<QuestPackage>(UniTask.RunOnThreadPool(async () =>
            {
                var start = await GameAPI.API.QuestStart(questId);
                //本来はインゲームに行く
                //成功ってことにする
                var result = await GameAPI.API.EventQuestResult(1);

                //アイテム付与
                Debug.Log($"クエスト : {questId}");

                //パッケージ
                var package = SequenceBridge.GetSequencePackage<QuestPackage>("EventQuest");
                package.QuestResult = result;

                //リザルトへ
                UniTask.Post(GoEventHome);
            })));
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
