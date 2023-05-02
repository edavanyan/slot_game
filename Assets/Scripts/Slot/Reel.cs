using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Slot
{
    public class Reel : MonoBehaviour
    {
        public event Action<SlotPrize> OnReelStop;

        private Slot currentActive;
        private readonly float[] timeIntervals = {
        
            Mathf.Cos(1.3f),
            Mathf.Cos(1.31f),
            Mathf.Cos(1.32f),
            Mathf.Cos(1.33f),
            Mathf.Cos(1.34f),
            Mathf.Cos(1.35f),
            Mathf.Cos(1.36f),
            Mathf.Cos(1.37f),
            Mathf.Cos(1.38f),
            Mathf.Cos(1.39f),
        
            Mathf.Cos(1.4f),
            Mathf.Cos(1.41f),
            Mathf.Cos(1.42f),
            Mathf.Cos(1.43f),
            Mathf.Cos(1.44f),
            Mathf.Cos(1.45f),
            Mathf.Cos(1.46f),
            Mathf.Cos(1.47f),
            Mathf.Cos(1.48f),
            Mathf.Cos(1.49f),
            Mathf.Cos(1.51f),
            Mathf.Cos(1.52f),
            Mathf.Cos(1.53f),
            Mathf.Cos(1.53f),
            Mathf.Cos(1.53f),
            Mathf.Cos(1.53f),
            Mathf.Cos(1.53f),
            Mathf.Cos(1.53f),
            Mathf.Cos(1.53f),
            // Mathf.Cos(1.54f),
            // Mathf.Cos(1.55f),
            // Mathf.Cos(1.56f)
        };
        private List<Slot> slots;
        private bool spinning;
        private int stopIndex;

        private readonly List<TweenerCore<Vector2, Vector2, VectorOptions>> tweeners = new();
        private void Start()
        {
            slots = gameObject.GetComponentsInChildren<Slot>().ToList();
            currentActive = slots[slots.Count / 2];
            for (var i = 0; i < slots.Count; i++)
            {
                tweeners.Add(null);
            }
        }

        public void StartSpin()
        {
            index = timeIntervals.Length - 3;
            spinning = true;
            StartCoroutine(Spin());
        }

        private int index;
        IEnumerator Spin()
        {
            yield return new WaitForSeconds(timeIntervals[index]);
            if (spinning)
            {
                index++;
            } else
            {
                index--;
            }
            if (index >= timeIntervals.Length)
            {
                index = timeIntervals.Length - 1;
            }
            else if (index < 0)
            {
                index = 0;
            }

            for (var i = 0; i < slots.Count; i++)
            {
                var item = slots[i];
                tweeners[i] = item.MoveY(timeIntervals[index]);
                var tweener = tweeners[i];
                if (tweener.endValue.y == 0)
                {
                    currentActive = item;
                }

                if (stopIndex >= index && prize == currentActive.type)
                {
                    foreach (var tweenerCore in tweeners)
                    {
                        tweenerCore.SetEase(Ease.OutBack);
                    }
                }
            }
            var spinReel = stopIndex < index || prize != currentActive.type;
            if (spinReel)
            {
                if (stopIndex < 0)
                {
                    stopIndex++;
                }
                StartCoroutine(Spin());
            }
            if (!spinReel) {
                yield return new WaitForSeconds(timeIntervals[index]);
                stopIndex = 0;
                OnReelStop?.Invoke(currentActive.type);
            }
        }

        private SlotPrize prize; 
        public void Stop(int slotIndex, SlotPrize preset)
        {
            prize = preset;
            spinning = false;
            stopIndex = index - slotIndex;
        }
    }
}
