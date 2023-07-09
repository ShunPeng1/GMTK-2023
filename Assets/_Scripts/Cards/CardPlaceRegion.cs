using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Cards
{
    public class CardPlaceRegion : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPlace;
        [SerializeField] private int _maxCardHold = new();
        [SerializeField] private Vector3 _cardOffset = new Vector3(5f, 0 ,0);
        
        [SerializeField] public bool IsSort = true;
        
        [SerializeField] private List<CardPlaceHolder> _cardPlaceHolders = new();
        private CardPlaceHolder _temporaryCardHolder;
        [SerializeField] private int _cardCount = 0;

        [Header("Smooth Move")] 
        [SerializeField] private float _moveDuration = 0.15f;
        [SerializeField] private Ease _moveEase = Ease.OutCubic;
        
        private void Start()
        {
            if (_cardPlaceHolders.Count != 0)
            {
                _maxCardHold = _cardPlaceHolders.Count;
                return;
            }
            
            for (int i = 0; i < _maxCardHold; i++)
            {
                _cardPlaceHolders.Add( Instantiate(ResourceManager.Instance.CardPlaceHolder, _spawnPlace.position + i * _cardOffset, Quaternion.identity, _spawnPlace));
                
            }
        }

        public List<BaseCardInformation> GetCardsInformation()
        {
            List<BaseCardInformation> result = new();
            for (int i = 0; i < _cardCount; i++)
            {
                result.Add(_cardPlaceHolders[i].BaseCard.CardInformation);
            }

            return result;
        }

        public bool AddCard(BaseCard card, CardPlaceHolder cardPlaceHolder)
        {
            if (_cardCount >= _maxCardHold)
            {
                return false;
            }

            int index = 0;
            if (cardPlaceHolder == null)
            {
                cardPlaceHolder = _cardPlaceHolders[_cardCount];
                index = _cardCount ;
            }
            else
            {
                index = _cardPlaceHolders.IndexOf(cardPlaceHolder);
            }
            
            if (index >= _cardCount)
            {
                index = _cardCount ;
                
                _cardPlaceHolders[index].BaseCard = card;
                card.transform.position = _cardPlaceHolders[index].transform.position;

                _cardCount ++;
                return true;
            }
            
            if(IsSort) ShiftRight(index);
            
            _cardPlaceHolders[index].BaseCard = card;
            card.transform.position = _cardPlaceHolders[index].transform.position;

            _cardCount++;
            return true;
        }

        private void ShiftRight(int startIndex)
        {
            for (int i = _cardPlaceHolders.Count - 1; i > startIndex; i--)
            {
                var card = _cardPlaceHolders[i-1].BaseCard;
                _cardPlaceHolders[i].BaseCard = card;
                
                if (card == null) continue;
                card.transform.position = _cardPlaceHolders[i].transform.position;
            }
        }
        
        
        private void ShiftLeft(int startIndex)
        {
            for (int i = startIndex; i < _cardPlaceHolders.Count - 1; i++)
            {
                var card = _cardPlaceHolders[i+1].BaseCard;
                _cardPlaceHolders[i].BaseCard = card;
                
                if (card == null) continue;
                card.transform.position = _cardPlaceHolders[i].transform.position;
            }

            _cardPlaceHolders[^1].BaseCard = null;
        }
        
        public bool RemoveCard(BaseCard card)
        {
            for (int i = 0; i < _cardPlaceHolders.Count; i++)
            {
                if (_cardPlaceHolders[i].BaseCard == card)
                {
                    _cardPlaceHolders[i].BaseCard = null;
                    ShiftLeft(i);
                    
                    _cardCount--;

                    return true;
                }
            }
            return false;
        }
        
        public bool RemoveCard(BaseCard card,CardPlaceHolder cardPlaceHolder)
        {
            if (cardPlaceHolder.BaseCard != card) return false;
            
            cardPlaceHolder.BaseCard = null;
            if(IsSort) ShiftLeft(_cardPlaceHolders.IndexOf(cardPlaceHolder));
            _cardCount--;

            return true;
        }
        
        /*
        public void SwapCardPlacement(BaseCard card1, BaseCard card2)
        {
            // Swap the placement of two cards in the hand

            int index1 = _cardPlaceHolders.IndexOf(card1);
            int index2 = _cardPlaceHolders.IndexOf(card2);

            if (index1 != -1 && index2 != -1)
            {
                // Swap the positions of the cards
                Vector3 tempPosition = card1.transform.position;
                card1.transform.position = card2.transform.position;
                card2.transform.position = tempPosition;

                // Swap the cards in the list
                _cardPlaceHolders[index1] = card2;
                _cardPlaceHolders[index2] = card1;
            }
        }*/

        public bool TakeOutTemporary(BaseCard card,CardPlaceHolder cardPlaceHolder)
        {
            if (RemoveCard(card, cardPlaceHolder))
            {
                
                _temporaryCardHolder = cardPlaceHolder;
                return true;
            }
            return false;
        }
        
        public void ReAddTemporary(BaseCard baseCard)
        {
            AddCard(baseCard, _temporaryCardHolder);
            
            _temporaryCardHolder = null;
        }

        public void RemoveTemporary(BaseCard baseCard)
        {
            _temporaryCardHolder = null;
        }

        private void SmoothMove(Transform from, Transform to)
        {
            from.DOMove(to.position, _moveDuration).SetEase(_moveEase);
        }
    }
}