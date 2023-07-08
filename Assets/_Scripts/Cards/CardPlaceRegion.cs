using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Cards
{
    public class CardPlaceRegion : MonoBehaviour
    {
        private Vector3 _cardOffset = new Vector3(0.5f, 0 ,0);
        
        private readonly List<CardPlaceHolder> _cardPlaceHolders = new();
        [SerializeField] private int _maxCardHold = new();
        private CardPlaceHolder _temporaryCardHolder;

        private void Start()
        {
            for (int i = 0; i < _maxCardHold; i++)
            {
                _cardPlaceHolders[i] = Instantiate(ResourceManager.Instance.CardPlaceHolder, transform.position + i * _cardOffset, Quaternion.identity, transform);
            }
        }

        public bool AddCard(BaseCard card, CardPlaceHolder cardPlaceHolder)
        {
            int index = _cardPlaceHolders.IndexOf(cardPlaceHolder);
            if (index >= _maxCardHold)
            {
                return false;
            }

            if (index >= _cardPlaceHolders.Count)
            {
                _cardPlaceHolders[index].BaseCard = card;
                return true;
            }
            
            ShiftRight(index);
            
            _cardPlaceHolders[index].BaseCard = card;
            return true;
        }

        private void ShiftRight(int startIndex)
        {
            for (int i = _cardPlaceHolders.Count; i >= startIndex; i--)
            {
                var card = _cardPlaceHolders[i].BaseCard;
                _cardPlaceHolders[i+1].BaseCard = card;
                card.transform.position = _cardPlaceHolders[i + 1].transform.position;
            }
        }
        
        
        private void ShiftLeft(int startIndex)
        {
            for (int i = startIndex; i < _cardPlaceHolders.Count; i++)
            {
                var card = _cardPlaceHolders[i+1].BaseCard;
                _cardPlaceHolders[i].BaseCard = card;
                card.transform.position = _cardPlaceHolders[i].transform.position;
            }
        }
        
        public void RemoveCard(BaseCard card)
        {
            for (int i = 0; i < _cardPlaceHolders.Count; i++)
            {
                if (_cardPlaceHolders[i].BaseCard == card)
                {
                    _cardPlaceHolders[i].BaseCard = null;
                    ShiftLeft(i);
                    break;
                }
            }
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

        public int TakeOutTemporary(CardPlaceHolder cardPlaceHolder)
        {
            _temporaryCardHolder = cardPlaceHolder;
            return _cardPlaceHolders.IndexOf(cardPlaceHolder);
        }
        
        public void ReAddTemporary(BaseCard baseCard)
        {
            _temporaryCardHolder = null;
            baseCard.transform.position = transform.position + _cardOffset;
        }

        public void RemoveTemporary(BaseCard baseCard)
        {
            RemoveCard(baseCard);
            
        }
    }
}