using System.Drawing;
using System.Windows.Forms;

namespace Aga.Controls
{
    public static class TextHelper
    {
        public static StringAlignment TranslateAligment(HorizontalAlignment alignment)
        {
            if (alignment == HorizontalAlignment.Left)
                return StringAlignment.Near;
            else if (alignment == HorizontalAlignment.Right)
                return StringAlignment.Far;
            else
                return StringAlignment.Center;
        }

        public static TextFormatFlags TranslateAligmentToFlag(HorizontalAlignment alignment)
        {
            if (alignment == HorizontalAlignment.Left)
                return TextFormatFlags.Left;
            else if (alignment == HorizontalAlignment.Right)
                return TextFormatFlags.Right;
            else
                return TextFormatFlags.HorizontalCenter;
        }

        public static TextFormatFlags TranslateTrimmingToFlag(StringTrimming trimming)
        {
            if (trimming == StringTrimming.EllipsisCharacter)
                return TextFormatFlags.EndEllipsis;
            else if (trimming == StringTrimming.EllipsisPath)
                return TextFormatFlags.PathEllipsis;
            if (trimming == StringTrimming.EllipsisWord)
                return TextFormatFlags.WordEllipsis;
            if (trimming == StringTrimming.Word)
                return TextFormatFlags.WordBreak;
            else
                return TextFormatFlags.Default;
        }
    }
}
