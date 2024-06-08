using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Utils.Pattern
{
    public interface IAction
    {
        public void Execute();
    }

    public abstract class  Actor
    {
        private IAction action;

        public void SetAction(IAction action)
        {
            this.action = action;
        }

        public virtual void Execute()
        {
            action.Execute();
        }
    }
}
