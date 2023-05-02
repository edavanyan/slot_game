using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Plugins.Demigiant.DOTween.Modules;
using UnityEngine;

namespace Slot
{
    public class Slot : MonoBehaviour
    {
        private float time = 1;
        public Vector3 InitialPosition { private get; set; }
        public Vector3 DestinationPosition { private get; set; }
        private RectTransform rectTransform;

        public SlotPrize type;
        // Start is called before the first frame update
        void Start()
        {
            rectTransform = (RectTransform)transform;
            DestinationPosition = rectTransform.anchoredPosition3D;
        }

        // private float progress = 0;
        // private void Update()
        // {
        //     if (rectTransform.anchoredPosition3D != DestinationPosition)
        //     {
        //         if (progress <= time)
        //         {
        //             var rectPosition = rectTransform.anchoredPosition3D;
        //             progress += Time.deltaTime;
        //             float p = progress / time;
        //             rectPosition.x = Mathf.Lerp(InitialPosition.x, DestinationPosition.x, p);
        //             rectPosition.y = Mathf.Lerp(InitialPosition.y, DestinationPosition.y, p);
        //             rectPosition.z = Mathf.Lerp(InitialPosition.z, DestinationPosition.z, p);
        //             rectTransform.anchoredPosition3D = rectPosition;
        //         }
        //     }
        // }

        public TweenerCore<Vector2, Vector2, VectorOptions> MoveY(float timeInterval, Ease ease = Ease.Linear)
        {
            var upperLimit = 4 * rectTransform.rect.height;
            var lowerLimit = -upperLimit;
            var realPosition = InitialPosition = rectTransform.anchoredPosition3D;
            time = timeInterval;
            var position = DestinationPosition;
            position.y -= rectTransform.rect.height;
            if (position.y > upperLimit)
            {
                realPosition.y = lowerLimit;
                rectTransform.anchoredPosition3D = realPosition;
                position.y = lowerLimit;
            }
            if (position.y < lowerLimit)
            {
                realPosition.y = upperLimit;
                rectTransform.anchoredPosition3D = realPosition;
                position.y = upperLimit;
            }
            // else
            // {
            //     return rectTransform.DOAnchorPos(position, timeInterval).SetEase(ease);
            // }
            DestinationPosition = position;
            // progress = 0;
            return rectTransform.DOAnchorPos(DestinationPosition, timeInterval).SetEase(ease);
        }
    }
}
