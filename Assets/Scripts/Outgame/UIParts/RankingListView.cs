using MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Outgame.QuestListView;

namespace Outgame
{
    /// <summary>
    /// �N�G�X�g���X�g��\������r���[
    /// </summary>
    public class RankingListView : ListView
    {
        [SerializeField] GameObject _RankingPrefab;

        List<ListItemRanking> _rankingList = new List<ListItemRanking>();
        APIResponceEventGetRanking _ranking = null;
        int _selectedRank = -1;

        

        /// <summary>
        /// �r���[�����
        /// </summary>
        public override void Setup()
        {
            _lineList.ForEach(l => GameObject.Destroy(l));
            _itemList.Clear();
            _scrollPos = 0;

            //�`���v�^�[�Ƃ��̎q���ɂȂ�N�G�X�g�����X�g�ɓ����
            for (int i = 0; i < _ranking.rewards.Length; ++i)
            {
                var chapter = GameObject.Instantiate(_RankingPrefab, _content.RectTransform);
                var listItem = ListItemBase.ListItemSetup<ListItemRanking>(i, chapter, null);

                listItem.SetupRankingData(i, _ranking.rewards[i].userName, _ranking.rewards[i].point);

                _itemList.Add(listItem);
                _lineList.Add(listItem.gameObject);
            }

            //�T�C�Y�v�Z���čő�X�N���[���l�����߂�
            //�N�G�X�g�̓T�C�Y�ς���̂Ŗ���Čv�Z����
            _content.RectTransform.sizeDelta = new Vector2(800, (_lineList.Where(go => go.activeSelf).Count() + 1) * CardUIHeight);

            //�C�x���g�o�^
            _rect.onValueChanged.AddListener(ScrollUpdate);
        }
    }
}
