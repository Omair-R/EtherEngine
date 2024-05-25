using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Utils.Validate
{
    public class FlagEventArgs : EventArgs
    {
        public bool State { get; set; }
        public FlagEventArgs(bool state) {
            State = state;
        }
    }

    public class Flag
    {
        private bool _flag;

        public event EventHandler<FlagEventArgs> FlagChanged;

        public Flag(bool initialState = false) {
            _flag = initialState;
        }

        public bool GetState() => _flag;
        public void Up() {
            if(!_flag) EventUtils.Invoke< FlagEventArgs>(FlagChanged, this, new FlagEventArgs(true));
            _flag = true;
        }
        public void Down() {
            if (_flag) EventUtils.Invoke<FlagEventArgs>(FlagChanged, this, new FlagEventArgs(false));
            _flag = false; 
        }
        public void Toggle() {
            _flag = !_flag;
            EventUtils.Invoke<FlagEventArgs>(FlagChanged, this, new FlagEventArgs(_flag));
        }
    }
}
