using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics.CodeAnalysis;

namespace ShortCuts.Desktop
{
    public class WindowModel: IComparable<WindowModel>
    {
        public WindowModel()
        {
            this.WindowRect = new Rectangle(-333, -333, -333, -333);
        }

        #region properties
        public int Id { get; set; }
        public string ProcessName { get; set; }
        public string MainWindowTitle { get; set; }
        public IntPtr MainWindowHandle { get; set; }
        public Rectangle WindowRect { get; set; }
        public string Summary 
        {
            get
            {
                return $"{this.ProcessName} - {this.MainWindowTitle} - X: {this.WindowRect.X} - Y: {this.WindowRect.Y} - Width: {this.WindowRect.Width - this.WindowRect.X} - Height: {this.WindowRect.Height - this.WindowRect.Y}";
            }
        }
        #endregion

        public void ApplyProperties(WindowModel other)
        {
            this.Id = other.Id;
            this.MainWindowHandle = other.MainWindowHandle;
            this.MainWindowTitle = other.MainWindowTitle;
            this.ProcessName = other.ProcessName;
            this.WindowRect = other.WindowRect;
        }
        public int CompareTo([AllowNull] WindowModel other)
        {
            if(other == null)
            {
                return 1;
            }

            return this.ToString().CompareTo(other.ToString());
        }

        public override string ToString()
        {
            return this.Summary;
        }
    }
}
