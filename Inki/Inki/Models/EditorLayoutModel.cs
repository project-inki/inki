using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inki.Models
{
    public class EditorLayoutModel
    {
        public enum EditorSplitStyle { None = 0, Horizontal = 1, Vertical = 2 };

        public EditorSplitStyle splitStyle;

        public double splitRatio;

        public EditorLayoutModel(EditorSplitStyle splitStyle, double splitRatio)
        {
            this.splitStyle = splitStyle;
            this.splitRatio = splitRatio;
        }

        public EditorLayoutModel()
        {
            this.splitStyle = EditorSplitStyle.None;
            this.splitRatio = .5;
        }
    }
}
