using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Updater5
{
    public abstract class StepCode
    {
        private StepCode()
        {
            Panel = new();
            Data = new();
            StepName = "";
            Form = new();
        }

        protected StepCode(Panel panel, Data data, Updater form)
        {
            Panel = panel;
            Data = data;
            StepName = "";
            Form = form;
        }

        public string StepName;
        public Panel Panel;
        public StepCode? PrevStepCode;
        public StepCode? NextStepCode;
        public Data Data;
        public Updater Form;

        public abstract void Init();

        public abstract StepCode? Prev();

        public abstract StepCode? Next();

        public abstract void Activate();
    }
}
