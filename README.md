# Unity xAPI Wrapper
Library for interacting with an LRS from within a Unity project.

At the time of writing, the library works primarily with the `/statements` API.  Additional API interactions are in development, but,
if you are solely interested in having your Unity project send and receive xAPI statements, then this library will work for you.

Using the functions and classes in this library might be a bit difficult if you are unfamiliar with xAPI, so it's
**strongly recommended** that you take the time and learn how xAPI works and its basic terminology prior to using this.  The
subsequent sections will assume that you have a basic understanding of xAPI and are ready to incorporate it into your Unity project.

## Setup
This will eventually be available in the Unity Asset Store, but for now it will need to be copied into your project manually.

### TL;DR
- Get the project (download as zip or clone with `git clone https://github.com/vbhayden/unity-xapi-wrapper`)
- Drop into your Unity project's Assets folder
- Reference the namespace (make sure `using XAPI;` is at the top of any script that intends to use it)

### How it Works
This library works by keeping a persistent GameObject across your scenes with an `XAPIMessenger` script attached.  All xAPI-related
functionality is performed through a sealed class called `XAPIWrapper` that uses this GameObject to execute coroutines for sending
and receiving xAPI statements.  

As the entire point of using UnityWebRequest objects with coroutines is to ensure asynchronous behaviour, responses are handled with 
*optional* callbacks for sending statements and *mandatory* callbacks for retrieving statements.

**Please Note:**  The object containing your `XAPIMessenger` is intended to be a "singleton" -- meaning only one will exist at a time.  If you
load a new scene with a different `XAPIMessenger` in it, **only the previous one will persist**.  This also means that this object
should probably only contain the `XAPIMessenger` script, as this object will remain alive through the project's execution.

### How-to Use
In order to send xAPI statements with this library, there are a few required steps.  These are already done in both of the example
scenes, but I'll outline them here:
- Create a new LRS Configuration
- Assign that LRS Configuration
- Use the `XAPIWrapper` functions and XAPI.X____ objects to send statements to that LRS.

Using the functions and classes in this library might be a bit difficult if you are unfamiliar with xAPI, so it's
**strongly recommended** that you take the time and learn how xAPI works and its basic terminology prior to using this.

### Creating an LRS Configuration
To communicate with an LRS, the wrapper needs to know which LRS you intend to use.  To create a new
LRS Configuration:
- Right-click inside of Unity's `Project` tab
- `Create > XAPI > New LRS Configuration`
- Assign your endpoint
- Assign your credentials

### Using an LRS Configuration
To use the configured LRS, your scene must have an object with an `XAPIMessenger` script attached.  Once this exists, you can assign
your configuration through the `XAPIMessenger` script's inspector by either drag-and-drop or searching the project.

Once that's assigned, save the project and the wrapper will send all of its statements to that LRS.

### Using the XAPIWrapper class and XAPI Objects
The `XAPIWrapper` class is intended to be your interface into sending and receiving xAPI statements with the configured LRS,
but the `XAPI` namespace also contains classes for contructing those statements.  As of writing, these objects use the following
convention:
- xAPI statement components begin with `X___` (Actor, Verb, Activity, etc)
- All other xAPI-relevant objects do not.  This includes:
  - Statement
  - StatementQuery
  - StatementResult

To build an xAPI statement, you will need an actor, a verb, and an object.  For verbs, there's a sealed helper class `Verbs` 
that has a collection of predefined verbs for use, but actors and objects need their own declaration.  The following snippet
is from the Sample scene:
```
// Set our domain
var domain = "http://example.com";

// Create our objects
this.actor = Actor.FromAccount(domain, "some-key-relevant-to-homepage", false, "Example Actor Name");
this.activity = new Activity(domain + "/simple-example", "Simple Unity Example");

// Send the statement
XAPIWrapper.SendStatement(this.actor, Verbs.Interacted, this.activity);
```
Additionally, a callback can be provided to receive information about the LRS response, including the stored statement ID:
```
// Simple xAPI Callback
void SendStatementCallback(Statement statement, string statementKey, UnityWebRequest request)
{
    if (request.responseCode == 200)
        Debug.LogFormat("Sent Statement: {0}, stored with key: {1}", statement.SimpleTriple, statementKey);
    else
    {
        Debug.LogErrorFormat("ERROR (part 1 of 3): Check your LRS configuration and statement syntax!");
        Debug.LogErrorFormat("ERROR (part 2 of 3): Statement: {0}", statement.SimpleTriple);
        Debug.LogErrorFormat("ERROR (part 2 of 3): Response: {0}", request.downloadHandler.text);
    }
}
```
Which would then be supplied with:
```
XAPIWrapper.SendStatement(this.actor, Verbs.Interacted, this.activity, callback: this.SendStatementCallback);
```

### Example Scenes
Example scenes have been removed from this project and will be included later.
