using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Outgame
{
    public class UIEventQuestView : UIStackableView
    {
        [SerializeField] QuestListView _listView;

        protected override void AwakeCall()
        {
            ViewId = ViewID.EventQuest;
            _hasPopUI = false;
        }

        private async void Start()
        {
            await QuestListModel.LoadAsync();

            _listView.Setup();
            _listView.SetReadyCallback(Ready);
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


                //パッケージ
                var package = SequenceBridge.GetSequencePackage<QuestPackage>("EventQuest");
                package.QuestResult = result;

                //リザルトへ
                UniTask.Post(GoEventResult);
            })));
        }

        void GoEventResult()
        {
            UIManager.NextView(ViewID.EventQuestResult);
        }

        public void Back()
        {
            UIManager.Back();
        }
    }
}
