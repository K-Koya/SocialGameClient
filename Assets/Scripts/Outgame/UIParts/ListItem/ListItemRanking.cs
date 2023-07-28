using MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

namespace Outgame
{
    /// <summary>
    /// ランキング表示用のボード
    /// NOTE: 移譲すべきオブジェクトは無いので、このクラスで表示管理する
    /// </summary>
    internal class ListItemRanking : ListItemBase
    {
        [SerializeField] TMPro.TextMeshProUGUI _tmRank;
        [SerializeField] TMPro.TextMeshProUGUI _tmUserName;
        [SerializeField] TMPro.TextMeshProUGUI _tmPoint;

        int _rank = 0;
        string _userName = default;
        int _point = 0;


        public void SetupRankingData(int rank, string userName, int point)
        {
            _rank = rank;
            _userName = userName;
            _point = point;

            _tmRank.text = rank < 1 ? "-" : rank.ToString();
            _tmUserName.text = userName;
            _tmPoint.text = point.ToString();
        }

        public override void Bind(GameObject target)
        {
        }

        public override void Load()
        {
        }

        public override void Release()
        {
        }

        protected override void OnClick()
        {
        }

        void OnItemClick(int evtId, int index)
        {
        }
    }
}
