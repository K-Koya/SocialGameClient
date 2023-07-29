using MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static Outgame.QuestListView;

namespace Outgame
{
    /// <summary>
    /// クエストリストを表示するビュー
    /// </summary>
    public class RankingListView : ListView
    {
        List<ListItemRanking> _rankingList = new List<ListItemRanking>();
        APIResponceEventGetRanking _ranking = null;
        int _selectedRank = -1;

        

        /// <summary>
        /// ビューを作る
        /// </summary>
        public override void Setup()
        {
            _lineList.ForEach(l => GameObject.Destroy(l));
            _itemList.Clear();
            _scrollPos = 0;

            GameObject sceneOrigin = Addressables.LoadAssetAsync<GameObject>(string.Format("Assets/Prefabs/Event/EventRanking.prefab")).WaitForCompletion();
            if (sceneOrigin == default)
            {
                Debug.LogError($"EventRanking : ランキングビューの読み込みに失敗");
                return;
            }

            //_ranking = 

            //ランキング情報をリストに入れる
            for (int i = 0; i < _ranking.ranking.Length; ++i)
            {
                var ins = GameObject.Instantiate(sceneOrigin, _content.RectTransform);
                var listItem = ListItemBase.ListItemSetup<ListItemRanking>(i, ins, null);

                listItem.SetupRankingData(i + 1, _ranking.ranking[i].userName, _ranking.ranking[i].point);

                _itemList.Add(listItem);
                _lineList.Add(listItem.gameObject);
            }

            //サイズ計算して最大スクロール値を決める
            //クエストはサイズ可変するので毎回再計算する
            _content.RectTransform.sizeDelta = new Vector2(800, (_lineList.Where(go => go.activeSelf).Count() + 1) * CardUIHeight);

            //イベント登録
            _rect.onValueChanged.AddListener(ScrollUpdate);
        }
    }
}
