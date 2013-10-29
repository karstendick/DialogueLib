using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialogueLib
{
    public class Query
    {
        private static List<Rule> _ruleDb = null;
        private Dictionary<string, object> _factDict = new Dictionary<string, object>();

        public Query()
        {
            if (_ruleDb == null)
                InitializeRuleDb();
        }

        internal Query(List<Rule> ruleDb)
        {
            _ruleDb = ruleDb;
        }

        private void InitializeRuleDb()
        {
            _ruleDb = new List<Rule>();
            _ruleDb.Add(new Rule("BridgeSecurityOfficerOnHit", new Response("BridgeSecurityOfficerOnHit", null, new TalkThen("Direct hit!")), new Criterion("who", "SecurityOfficer"), new Criterion("concept", "OnHit")));
            _ruleDb.Add(new Rule("ShipSecurityOfficerOnIdle", new Response("ShipSecurityOfficerOnIdle1", null, new TalkThen("Good morning, Captain."), new TalkThen("Nothing to report, Captain.")), new Criterion("who", "SecurityOfficer")));
            _ruleDb.Add(new Rule("BridgeChiefEngineerOnHit", new Response("BridgeChiefEngineerOnHit", null, new TalkThen("We're venting plasma!")), new Criterion("who", "ChiefEngineer"), new Criterion("concept", "OnHit")));
            _ruleDb.Add(new Rule("BridgeSecurityOfficerOnHitShieldsFailing", new Response("BridgeSecurityOfficerOnHitShieldsFailing", null, new TalkThen("Shields are failing!")), new Criterion("who", "SecurityOfficer"), new Criterion("concept", "OnHit"), new Criterion("shields", ComparisonType.LESS_THAN_EQ, 20)));
            _ruleDb.Add(new Rule("BridgeSecurityOfficerOnHitShieldsHolding", new Response("BridgeSecurityOfficerOnHitShieldsHolding", null, new TalkThen("Shields are holding, Captain.")), new Criterion("who", "SecurityOfficer"), new Criterion("concept", "OnHit"), new Criterion("shields", ComparisonType.GREATER_THAN_EQ, 90)));
            _ruleDb.Add(new Rule("AdmiralAdamsIsNotOnMission", new Response("AdmiralAdamsIsNotOnMission", null, new TalkThen("Enjoy some shore leave.")), new Criterion("who", "AdmiralAdams"), new Criterion("IsOnMission", ComparisonType.NOT_EQUALS, true)));
            _ruleDb.Add(new Rule("AdmiralAdamsIsOnMission", new Response("AdmiralAdamsIsOnMission", null, new TalkThen("Get back to your mission, Captain. That's an order.")), new Criterion("who", "AdmiralAdams"), new Criterion("IsOnMission")));
            _ruleDb.Add(new Rule("AmbassadorZorkOnMeetOneTime", new Response("AmbassadorZorkOnMeetFirstTime", null, new TalkThen("It is an honor to meet you, Captain.", new Then("Captain", "OnMeet1"))), 1, new Criterion("who", "AmbassadorZork")));
            _ruleDb.Add(new Rule("CaptainMeetsAmbassadorZork", new Response("CaptainPlayerMeetsAmbassadorZork", null, new TalkThen("Ditto, Ambassador.")), new Criterion("who", "Captain"), new Criterion("OnMeet1")));
            _ruleDb.Add(new Rule("AmbassadorZorkOnMeet", new Response("AmbassadorZorkOnMeet", null, new TalkThen("Good to see you again, Captain.")), new Criterion("who", "AmbassadorZork")));
        }

        public void Add(string key, object value)
        {
            _factDict.Add(key, value);
        }

        public Response Speak()
        {
            Dictionary<Rule, int> acceptDict = new Dictionary<Rule, int>();

            foreach (Rule rule in _ruleDb)
            {
                if (!rule.CanMatch())
                    continue;

                bool match = true;
                foreach (Criterion criterion in rule.Criteria)
                {
                    if (!_factDict.ContainsKey(criterion.Lhs) || !criterion.Compare(_factDict[criterion.Lhs] as IComparable))
                    {
                        match = false;
                        break;
                    }
                }
                if (match)
                    acceptDict.Add(rule, rule.Criteria.Count);
            }
            if (acceptDict.Count == 0)
                return new Response("NullResponse", "NO RESPONSE");

            // Of all the matched rules, find the one(s) that match the most criteria
            int maxScore = acceptDict.Max(kvp => kvp.Value);
            List<Rule> acceptList = acceptDict.Where(kvp => kvp.Value == maxScore).Select(kvp => kvp.Key).ToList();

            // Of all the matched rules of maximum criteria, return a random one
            Rule matchedRule = acceptList.RandomItem();
            matchedRule.Match();
            Response response = matchedRule.Response;
            response.Match();
            return response;
        }
    }
}
