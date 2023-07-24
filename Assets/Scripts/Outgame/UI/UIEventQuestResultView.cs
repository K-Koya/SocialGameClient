using Cysharp.Threading.Tasks;
using MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Outgame
{
    public class UIEventQuestResultView : UIStackableView
    {
        [SerializeField] GameObject _root;
        [SerializeField] GameObject _rewardPrefab;

        int _questId = 0;

        protected override void AwakeCall()
        {
            ViewId = ViewID.QuestResult;
            _hasPopUI = false;

            CreateView();
        }

        string GetRewardObjectString(APIResponceQuestReward reward)
        {
            string ret = "";
            var type = (RewardItemType)reward.type;
            switch (type)
            {
                case RewardItemType.EventPoint: ret = string.Format("{0}ポイント", int.Parse(reward.param[0])); break;
                default: Debug.LogError($"規定と異なる種類の報酬が検出されました。type : {type}"); break;
            }
            return ret;
        }

        void CreateView()
        {
            var package = SequenceBridge.GetSequencePackage<QuestPackage>("Quest");

            foreach (var reward in package?.QuestResult?.rewards)
            {
                Debug.Log(reward);
                if (reward.type == 0) continue;

                var rewardObj = GameObject.Instantiate(_rewardPrefab, _root.transform);
                var text = rewardObj.GetComponent<TextMeshProUGUI>();

                text.text = string.Format("{0}を手に入れた", GetRewardObjectString(reward));
            }

            SequenceBridge.DeleteSequence("Quest");
        }

        public void GoHome()
        {
            UIManager.NextView(ViewID.Home);
        }
    }
}
