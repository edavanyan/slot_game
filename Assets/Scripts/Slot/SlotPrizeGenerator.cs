using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Slot
{
    public class SlotPrizeGenerator : MonoBehaviour
    {
        private readonly Dictionary<PrizePackage, SlotPrize[]> prizes = new ();
        private readonly SlotPrize[] randomPrizes =
        {
            SlotPrize.Star,
            SlotPrize.Jackpot,
            SlotPrize.Ticket
        };

        [Range(0, 1)]
        public float gold1;
        [Range(0, 1)]
        public float gold2;
        [Range(0, 1)]
        public float gold3;
        [Range(0, 1)]
        public float pouchOfGold1;
        [Range(0, 1)]
        public float pouchOfGold2;
        [Range(0, 1)]
        public float pouchOfGold3;
        [Range(0, 1)]
        public float star;
        [Range(0, 1)]
        public float spin1;
        [Range(0, 1)]
        public float spin2;
        [Range(0, 1)]
        public float spin3;
        [Range(0, 1)]
        public float ticket;

        private float sumProbability;
        private void Start()
        {
            sumProbability = gold1 +
                             gold2 +
                             gold3 +
                             pouchOfGold1 +
                             pouchOfGold2 +
                             pouchOfGold3 +
                             star +
                             spin1 +
                             spin2 +
                             spin3 + 
                             ticket;
        
            prizes.Add(new PrizePackage(Prize.Gold, 50), new []{SlotPrize.Gold});//0.9
            prizes.Add(new PrizePackage(Prize.Gold, 100), new []{SlotPrize.Gold, SlotPrize.Gold});//0.6
            prizes.Add(new PrizePackage(Prize.Gold, 500), new []{SlotPrize.Gold, SlotPrize.Gold, SlotPrize.Gold});//0.5
            prizes.Add(new PrizePackage(Prize.Gold, 1000), new []{SlotPrize.PouchOfGold});//0.3
            prizes.Add(new PrizePackage(Prize.Gold, 2000), new []{SlotPrize.PouchOfGold, SlotPrize.PouchOfGold});//0.25
            prizes.Add(new PrizePackage(Prize.Gold, 10000), new []{SlotPrize.PouchOfGold, SlotPrize.PouchOfGold, SlotPrize.PouchOfGold});//0.2
            prizes.Add(new PrizePackage(Prize.Star, 1), new []{SlotPrize.Star, SlotPrize.Star, SlotPrize.Star});//0.025
            prizes.Add(new PrizePackage(Prize.Spin, 5), new []{SlotPrize.Spin});//0.4
            prizes.Add(new PrizePackage(Prize.Spin, 10), new []{SlotPrize.Spin, SlotPrize.Spin});//0.3
            prizes.Add(new PrizePackage(Prize.Spin, 50), new []{SlotPrize.Spin, SlotPrize.Spin, SlotPrize.Spin});//0.2
            prizes.Add(new PrizePackage(Prize.Ticket, 1), new []{SlotPrize.Ticket, SlotPrize.Ticket, SlotPrize.Ticket});//0.1
        }

        private readonly List<int> indices = new ();
        public void PreDefinePrize(SlotPrize[] preSetPrizes)
        {
            indices.Clear();
            indices.Add(0);
            indices.Add(1);
            indices.Add(2);
            var sum = sumProbability - gold1;
            var prob = Random.Range(0, sumProbability);
            if (prob > sum)
            {
                var index = indices[Random.Range(0, indices.Count)];
                indices.Remove(index);
                preSetPrizes[index] = SlotPrize.Gold;
                index = indices[Random.Range(0, indices.Count)];
                indices.Remove(index);
                preSetPrizes[index] = randomPrizes[Random.Range(0, randomPrizes.Length)];
                index = indices[Random.Range(0, indices.Count)];
                indices.Remove(index);
                preSetPrizes[index] = randomPrizes[Random.Range(0, randomPrizes.Length)];
                return;
            }

            sum -= gold2;
            if (prob > sum)
            {
                var index = indices[Random.Range(0, indices.Count)];
                indices.Remove(index);
                preSetPrizes[index] = SlotPrize.Gold;
                index = indices[Random.Range(0, indices.Count)];
                indices.Remove(index);
                preSetPrizes[index] = SlotPrize.Gold;
                index = indices[Random.Range(0, indices.Count)];
                indices.Remove(index);
                preSetPrizes[index] = randomPrizes[Random.Range(0, randomPrizes.Length)];
                return;
            }

            sum -= gold3;
            if (prob > sum)
            {
                preSetPrizes[0] = SlotPrize.Gold;
                preSetPrizes[1] = SlotPrize.Gold;
                preSetPrizes[2] = SlotPrize.Gold;
                return;
            }

            sum -= pouchOfGold1;
            if (prob > sum)
            {
                var index = indices[Random.Range(0, indices.Count)];
                indices.Remove(index);
                preSetPrizes[index] = SlotPrize.PouchOfGold;
                index = indices[Random.Range(0, indices.Count)];
                indices.Remove(index);
                preSetPrizes[index] = randomPrizes[Random.Range(0, randomPrizes.Length)];
                index = indices[Random.Range(0, indices.Count)];
                indices.Remove(index);
                preSetPrizes[index] = randomPrizes[Random.Range(0, randomPrizes.Length)];
                return;
            }

            sum -= pouchOfGold2;
            if (prob > sum)
            {
                var index = indices[Random.Range(0, indices.Count)];
                indices.Remove(index);
                preSetPrizes[index] = SlotPrize.PouchOfGold;
                index = indices[Random.Range(0, indices.Count)];
                indices.Remove(index);
                preSetPrizes[index] = SlotPrize.PouchOfGold;
                index = indices[Random.Range(0, indices.Count)];
                indices.Remove(index);
                preSetPrizes[index] = randomPrizes[Random.Range(0, randomPrizes.Length)];
                return;
            }

            sum -= pouchOfGold3;
            if (prob > sum)
            {
                preSetPrizes[0] = SlotPrize.PouchOfGold;
                preSetPrizes[1] = SlotPrize.PouchOfGold;
                preSetPrizes[2] = SlotPrize.PouchOfGold;
                return;
            }

            sum -= star;
            if (prob > sum)
            {
                preSetPrizes[0] = SlotPrize.Star;
                preSetPrizes[1] = SlotPrize.Star;
                preSetPrizes[2] = SlotPrize.Star;
                return;
            }

            sum -= spin1;
            if (prob > sum)
            {
                var index = indices[Random.Range(0, indices.Count)];
                indices.Remove(index);
                preSetPrizes[index] = SlotPrize.Spin;
                index = indices[Random.Range(0, indices.Count)];
                indices.Remove(index);
                preSetPrizes[index] = randomPrizes[Random.Range(0, randomPrizes.Length)];
                index = indices[Random.Range(0, indices.Count)];
                indices.Remove(index);
                preSetPrizes[index] = randomPrizes[Random.Range(0, randomPrizes.Length)];
                return;
            }

            sum -= spin2;
            if (prob > sum)
            {
                var index = indices[Random.Range(0, indices.Count)];
                indices.Remove(index);
                preSetPrizes[index] = SlotPrize.Spin;
                index = indices[Random.Range(0, indices.Count)];
                indices.Remove(index);
                preSetPrizes[index] = SlotPrize.Spin;
                index = indices[Random.Range(0, indices.Count)];
                indices.Remove(index);
                preSetPrizes[index] = randomPrizes[Random.Range(0, randomPrizes.Length)];
                return;
            }

            sum -= spin3;
            if (prob > sum)
            {
                preSetPrizes[0] = SlotPrize.Spin;
                preSetPrizes[1] = SlotPrize.Spin;
                preSetPrizes[2] = SlotPrize.Spin;
                return;
            }

            sum -= ticket;
            if (prob > sum)
            {
                preSetPrizes[0] = SlotPrize.Ticket;
                preSetPrizes[1] = SlotPrize.Ticket;
                preSetPrizes[2] = SlotPrize.Ticket;
                return;
            }
        }

        public struct PrizePackage
        {
            public Prize prize;
            public int amount;

            public PrizePackage(Prize prize, int amount)
            {
                this.prize = prize;
                this.amount = amount;
            }
        }

        public enum Prize
        {
            Gold,
            Star, 
            Spin,
            Ticket
        }
    }
}