using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Utils.Validate
{
    public class FlagValidator
    {
        private Exception _exception;
        private bool _flag = false;
        private readonly bool _defaultDown = false;

        public FlagValidator(Exception exception, bool defaultDown = false) {
            _exception = exception ?? new Exception();
            _flag = defaultDown;
            _defaultDown = defaultDown;
        }

        public FlagValidator(string message, bool defaultDown = false)
        {
            _exception = new Exception("message");
            _flag = defaultDown;
            _defaultDown = defaultDown;
        }

        public void Up() => _flag = !_defaultDown;
        public void Down() => _flag = _defaultDown;
        public void Toggle() => _flag = !_flag;
        public void Throw() =>  throw _exception;
        public bool GetState() => _flag;

        public void Check()
        {
            if (_flag == _defaultDown) throw _exception;
        }

        public void CheckOpposite()
        {
            if (_flag != _defaultDown) throw _exception;
        }
    }
}
