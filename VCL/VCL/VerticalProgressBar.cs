using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VerticalProgressBar
{
    public class VerticalProgressBar : ProgressBar
    {
        get
        {
            CreateParams cp = base.CreateParams;
            cp.Style |= 0x04;
            return cp;
        }
    }
}
