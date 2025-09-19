using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater5
{
    public class StepDownloadMetadata : StepCode
    {
        public StepDownloadMetadata(Panel panel, Data data, Updater form) : base(panel, data, form)
        {
            StepName = "Download Metadata";
        }

        public override void Init()
		{
		}

        public override StepCode? Prev()
        {
            return PrevStepCode;
        }

        public override StepCode? Next()
        {
            return NextStepCode;
        }

        public override void Activate()
        {
            Form.PrevButton.Enabled = true;
            Form.NextButton.Enabled = true;
        }
    }
}
