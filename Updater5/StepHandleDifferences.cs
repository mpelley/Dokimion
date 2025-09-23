using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater5
{
    public class StepHandleDifferences : StepCode
    {
        public StepHandleDifferences(Panel panel, Data data, Updater form) : base(panel, data, form)
        {
            StepName = "Handle Differences";
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
            Form.FeedbackTextBox.Text = "";
            Form.PrevButton.Enabled = true;
            Form.NextButton.Enabled = true;
        }
    }
}
