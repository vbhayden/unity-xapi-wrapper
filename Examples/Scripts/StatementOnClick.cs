using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XAPI.Examples
{
    public class StatementOnClick : MonoBehaviour
    {
        // Start is called before the first frame update
        public void SendSimpleStatement()
        {
            var actor = Actor.FromAccount("https://auth.example.com", "some-long-user-id", name: "some-user-name");
            var verb = Verbs.Interacted;
            var activity = new Activity("https://lms.example.com", "Unity Button Example");

            var statement = new Statement(actor, verb, activity);

            XAPIWrapper.SendStatement(statement, res => {
                Debug.Log("Sent simple statement!  LRS stored with ID: " + res.StatementID); 
            });
        }

        public void SendMultipleStatements()
        {
            var actor = XAPI.Actor.FromAccount(new ActorAccount("https://auth.example.com", "some-long-user-id"), name: "some-user-name");
            var verb = XAPI.Verbs.Interacted;
            var activity = new XAPI.Activity("https://lms.example.com", "Unity Button Example");

            var statement = new Statement(actor, verb, activity);
            var statements = new Statement[] { statement, statement };

            XAPIWrapper.SendStatements(statements, res => {
                Debug.Log("Sent multiple statements!  LRS stored with IDs: " + res.StatementIDs[0] + " : " + res.StatementIDs[1]); 
            });
        }
        
        public void SendComplexStatement()
        {
            var actor = XAPI.Actor.FromAccount(new ActorAccount("https://auth.example.com", "some-long-user-id"), name: "some-user-name");
            var verb = XAPI.Verbs.Interacted;
            var activity = new XAPI.Activity("https://lms.example.com", "Unity Button Example");
            var result = new XAPI.Result(true, true);

            result.Score = new XAPI.Score(163, 0, 200);
            result.Duration = new System.TimeSpan(5, 20, 12);

            var statement = new Statement(actor, verb, activity);

            statement.Result = result;
            statement.Context = new Context();

            statement.Context.Instructor = Actor.FromMailbox("mailto:someone@example.com");

            statement.Context.Extensions = new Dictionary<string, string>();
            statement.Context.Extensions.Add("https://schema.org/FakeExtension", "fake-value");

            XAPIWrapper.SendStatement(statement, res => {
                Debug.Log("Sent complex statement!  LRS stored with ID: " + res.StatementID); 
            });
        }
    }
}
