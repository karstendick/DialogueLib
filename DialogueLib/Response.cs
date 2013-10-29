using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialogueLib
{
    public class Response
    {
        private string _responseId;
        private string _applyFacts;
        private List<TalkThen> _talkThens;
        private TalkThen _talkThen;
        private bool _matched;

        protected void Initialize(string responseId)
        {
            _responseId = responseId;
            _talkThen = null;
            _matched = false;
            _applyFacts = String.Empty;
        }

        internal Response(string responseId, string applyFacts, params TalkThen[] talks)
        {
            Initialize(responseId);
            _talkThens = talks.ToList();
            _applyFacts = applyFacts;
        }
        

        public string ResponseId
        {
            get { return _responseId; }
        }

        public string ApplyFacts
        {
            get { return _applyFacts; }
        }

        internal TalkThen TalkThen
        {
            get { return _talkThen; }
        }

        public string Talk
        {
            get { return _talkThen.Talk; }
        }
        internal bool Matched
        {
            get { return _matched; }
        }

        internal void Match()
        {
            _matched = true;
            _talkThen = _talkThens.RandomItem();
        }

        public Query FollowUpQuery()
        {
            if (_talkThen.Then == null)
                return null;
            return _talkThen.Then.GetQuery();
        }
    }

    internal class Then
    {
        private KeyValuePair<string, string> _kvp;

        public Then(string who, string criterionLhs)
        {
            _kvp = new KeyValuePair<string, string>(who, criterionLhs);
        }

        public string Who
        {
            get { return _kvp.Key; }
        }

        public string CriterionLhs
        {
            get { return _kvp.Value; }
        }

        public Query GetQuery()
        {
            Query query = new Query();
            query.Add("who", Who);
            query.Add(CriterionLhs, true);
            return query;
        }
    }

    internal class TalkThen
    {
        private KeyValuePair<string, Then> _kvp;

        public TalkThen(string talk)
        {
            _kvp = new KeyValuePair<string, Then>(talk, null);
        }

        public TalkThen(string talk, Then then)
        {
            _kvp = new KeyValuePair<string, Then>(talk, then);
        }

        public string Talk
        {
            get { return _kvp.Key; }
        }

        public Then Then
        {
            get { return _kvp.Value; }
        }
    }
}
