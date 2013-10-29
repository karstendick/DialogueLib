using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialogueLib
{
    internal class Criterion
    {
        /// <summary>
        /// Set all params
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="ctype"></param>
        /// <param name="rhs"></param>
        internal Criterion(string lhs, ComparisonType ctype, IComparable rhs)
        {
            _lhs = lhs;
            _ctype = ctype;
            _rhs = rhs;
        }

        /// <summary>
        /// Hard-code check for equality
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        internal Criterion(string lhs, IComparable rhs)
        {
            _lhs = lhs;
            _rhs = rhs;
            _ctype = ComparisonType.EQUALS;
        }

        /// <summary>
        /// Hard-code a boolean comparison to true
        /// </summary>
        /// <param name="lhs"></param>
        internal Criterion(string lhs)
        {
            _lhs = lhs;
            _rhs = true;
            _ctype = ComparisonType.EQUALS;
        }

        private string _lhs;
        private IComparable _rhs;
        private ComparisonType _ctype;

        internal string Lhs
        {
            get { return _lhs; }
        }
        internal IComparable Rhs
        {
            get { return _rhs; }
        }

        internal bool Compare(IComparable rhs)
        {
            if (_ctype == ComparisonType.EQUALS)
                return rhs.CompareTo(_rhs) == 0;
            else if (_ctype == ComparisonType.GREATER_THAN)
                return rhs.CompareTo(_rhs) > 0;
            else if (_ctype == ComparisonType.GREATER_THAN_EQ)
                return rhs.CompareTo(_rhs) >= 0;
            else if (_ctype == ComparisonType.LESS_THAN)
                return rhs.CompareTo(_rhs) < 0;
            else if (_ctype == ComparisonType.LESS_THAN_EQ)
                return rhs.CompareTo(_rhs) <= 0;
            else if (_ctype == ComparisonType.NOT_EQUALS)
                return rhs.CompareTo(_rhs) != 0;

            throw new ApplicationException("I never expected to reach this code.");
        }
    }

    internal enum ComparisonType
    {
        EQUALS,
        GREATER_THAN,
        GREATER_THAN_EQ,
        LESS_THAN,
        LESS_THAN_EQ,
        NOT_EQUALS,
    }
}
