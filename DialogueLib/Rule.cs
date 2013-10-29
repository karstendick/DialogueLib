using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialogueLib
{
    internal class Rule
    {
        string _ruleId;
        Response _response;
        List<Criterion> _criteria;
        int? _disableAfter;
        int _timesMatched;

        private void Initialize(string ruleId, Response response, params Criterion[] criteria)
        {
            _ruleId = ruleId;
            _response = response;
            _criteria = criteria.ToList();

            _timesMatched = 0;
        }

        internal Rule(string ruleId, Response response, params Criterion[] criteria)
        {
            Initialize(ruleId, response, criteria);

            _disableAfter = null;
            _timesMatched = 0;
        }

        internal Rule(string ruleId, Response response, int disableAfter, params Criterion[] criteria)
        {
            Initialize(ruleId, response, criteria);

            _disableAfter = disableAfter;
            _timesMatched = 0;
        }

        internal void Match()
        {
            _timesMatched++;
        }

        internal bool CanMatch()
        {
            return !_disableAfter.HasValue || _timesMatched < _disableAfter.Value;
        }

        internal string RuleId
        {
            get { return _ruleId; }
        }

        internal List<Criterion> Criteria
        {
            get { return _criteria; }
        }

        internal Response Response
        {
            get { return _response; }
        }
    }
}
