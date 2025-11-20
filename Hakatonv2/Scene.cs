using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace Hakatonv2
{
    public class Scene
    {
        public string Name { get; set; }
        public string Duration { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public Image PreviewImage { get; set; }
        public bool IsAvailableOnQuest { get; set; }
    }
}
