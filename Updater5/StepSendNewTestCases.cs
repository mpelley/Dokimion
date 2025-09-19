using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater5
{
    public class StepSendNewTestCases : StepCode
    {
        public StepSendNewTestCases(Panel panel, Data data, Updater form) : base(panel, data, form)
        {
            StepName = "Send New Test Cases";
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
