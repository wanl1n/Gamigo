using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace TestTask.Editable
{
    public class ClientColors : MonoBehaviour
    {
        [field: SerializeField] public List<Image> ReceivedColors { get; private set; } = new List<Image>();
        [field: SerializeField] public GridLayoutGroup ColorPanelGridLayoutGroup { get; private set; }
        [field: SerializeField] public Image ColorSquare { get; private set; }

        public void RequestColors()
        {
            ClientPacketsHandler.SendColorsListRequest();
        }

        public void UpdateColorList(List<Color> colors)
        {
            ColorPanelGridLayoutGroup.constraintCount = colors.Count;

            foreach (Color color in colors)
            {
                Image square = Instantiate(ColorSquare, ColorPanelGridLayoutGroup.gameObject.transform);
                square.color = color;
            }
        }
    }
}
