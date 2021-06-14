using UnityEngine;

namespace General.UVCalculator
{
    public static class UVCalculator
    {
        private static int textureAlignment = 4; //middle center

        public static Rect CalculateNewUV(Vector2 texSize, Vector2 buttonSize)
        {
            float ratio = texSize.x / buttonSize.x;
            buttonSize *= ratio;
            if (buttonSize.y > texSize.y)
            {
                ratio = texSize.y / buttonSize.y;
                buttonSize *= ratio;
            }

            var rectUV = new Rect(0, 0, 1, 1);
            var newRectUV = rectUV;

            newRectUV.width = (texSize.x < buttonSize.x ? texSize.x / buttonSize.x : buttonSize.x / texSize.x) * rectUV.width;
            newRectUV.height = (texSize.y < buttonSize.y ? texSize.y / buttonSize.y : buttonSize.y / texSize.y) * rectUV.height;

            var atlasVerticalIndex = textureAlignment / 3;
            var atlasHorizontalIndex = textureAlignment % 3;

            var halfVerticalUV = (rectUV.height - newRectUV.height) / 2;
            var halfHorizontalUV = (rectUV.width - newRectUV.width) / 2;

            newRectUV.y += (2 - atlasVerticalIndex) * halfVerticalUV;
            newRectUV.x += atlasHorizontalIndex * halfHorizontalUV;

            return newRectUV;
        }
    }
}
