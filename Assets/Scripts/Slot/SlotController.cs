using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Slot
{
    public class SlotController : MonoBehaviour
    {
        [FormerlySerializedAs("slots")] public List<Reel> reels;

        public GameObject slotRichag;

        private bool isSlotPulled;
        private Vector3 slotDownScale;

        private readonly List<SlotPrize> prizes = new();
        private readonly SlotPrize[] preSetPrizes = {SlotPrize.Gold, SlotPrize.Jackpot, SlotPrize.PouchOfGold};
        public SlotPrizeGenerator slotPrizeGenerator;
        private void Start()
        {
            var localScale = slotRichag.transform.localScale;
            slotDownScale = Vector3.down * localScale.y + Vector3.right + Vector3.forward;

            foreach (var reel in reels)
            {
                reel.OnReelStop += prize =>
                {
                    prizes.Add(prize);
                    if (prizes.Count == 3)
                    {
                        stopIndex = 0;
                        DeterminePrize();
                    }
                    else
                    {
                        stopIndex++;
                        StartCoroutine(StopSlots());
                    }
                };
            }
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                PullSlot();
            }
        }

        private void DeterminePrize()
        {
            var log = "";

            var spinCount = 0;
            var goldCount = 0;
            var pouchCount = 0;
            var starCount = 0;
            var ticketCount = 0;
        
            foreach (var slotPrize in prizes)
            {
                if (slotPrize == SlotPrize.Gold)
                {
                    goldCount++;
                }
                if (slotPrize == SlotPrize.PouchOfGold)
                {
                    pouchCount++;
                }
                if (slotPrize == SlotPrize.Spin)
                {
                    spinCount++;
                }
                if (slotPrize == SlotPrize.Star)
                {
                    starCount++;
                }
                if (slotPrize == SlotPrize.Ticket)
                {
                    ticketCount++;
                }
                log += slotPrize + " ";
            }

            var userData = GameManager.instance.userDataController.userData;
            if (spinCount == 3)
            {
                userData.spinAmount += 10;
            }
            else if (spinCount == 2)
            {
                userData.spinAmount += 5;
            }
            else if (goldCount == 3)
            {
                userData.goldAmount += 500;
            }
            else if (pouchCount == 3)
            {
                userData.goldAmount += 10000;
            }
            else if (pouchCount == 2)
            {
                userData.goldAmount += 2000;
            }
            else if (pouchCount == 1)
            {
                userData.goldAmount += 1000;
            }
            else if (goldCount == 2)
            {
                userData.goldAmount += 200;
            }
            else if (spinCount == 1)
            {
                userData.spinAmount += 1;
            }
            else if (goldCount == 1)
            {
                userData.goldAmount += 100;
            }
            else if (starCount == 3)
            {
                userData.starAmount += 1;
            }
            else if (ticketCount == 3)
            {
            
            }
            prizes.Clear();
        
            isSlotPulled = false;
            slotRichag.transform.localScale = Vector3.one;
        
            // StartCoroutine(AutoSpinSlot());
        }

        private IEnumerator AutoSpinSlot()
        {
            yield return new WaitForSeconds(0.2f);
            PullSlot();
        }

        private int spinCount = 0;
        private void PullSlot()
        {
            if (isSlotPulled)
            {
                return;
            }

            if (GameManager.instance.userDataController.userData.spinAmount <= 0)
            {
                print("stopped: " + spinCount);
                return;
            }

            spinCount++;
            GameManager.instance.userDataController.userData.spinAmount--;
            isSlotPulled = true;
            spinIndex = 0;
            slotRichag.transform.localScale = slotDownScale;
            slotPrizeGenerator.PreDefinePrize(preSetPrizes);
            var log = "";
            foreach (var slotPrize in preSetPrizes)
            {
                log += slotPrize + " ";
            }
        
            StartCoroutine(SpinSlots());
        }

        private int spinIndex;

        private IEnumerator SpinSlots()
        {
            reels[spinIndex].StartSpin();
            yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));
            spinIndex++;
            if (spinIndex < reels.Count)
            {
                StartCoroutine(SpinSlots());
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(0.5f, 0.75f));
                ReleaseSlot();
            }
        }

        private void ReleaseSlot()
        {
            stopIndex = 0;
            StartCoroutine(StopSlots());
        }

        private int stopIndex;

        private IEnumerator StopSlots()
        {
            yield return new WaitForSeconds(Random.Range(0.1f, 0.15f));
            reels[stopIndex].Stop(stopIndex, preSetPrizes[stopIndex]);
        }
    }
}
