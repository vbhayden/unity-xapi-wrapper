# Unity xAPI Wrapper
Library for interacting with an LRS from within a Unity project.

At the time of writing, the library works primarily with the `/statements` API.  Additional API interactions are in development, but,
if you are solely interested in having your Unity project send and receive xAPI statements, then this library should work for you.

Using the functions and classes in this library might be a bit difficult if you are unfamiliar with xAPI.

**Note: While this library has been used for a few projects without issue, it's still in development.**

Issus and pull requests are welcome! 🎈👏

## Setup
The setup will seem a bit involved, but most of this will just be typical Unity pageantry.  To get the project:
- Get the project (download as zip or clone)
- Drop into your Unity project's Assets folder

To ensure the wrapper is working within your project:
- Open Unity
- Extract the required JSON.Net version for your system (see below)
- Open the example scene (`/Examples/Buttons`)
- (Optional) Create an LRS configuration (see below)
- (Optional) Assign your LRS configuration
- Run the example scene to check if everything's behaving ☕

If you didn't create an LRS configuration, the example scene will send statements to the public ADL LRS using the known `tom:1234` credentials.

## Additional Steps
While the example scene should work without issue, there are a few additional steps before xAPI will appear
in a specified LRS -- namely: importing the required JSON.Net library and creating LRS configurations.

### Extracting JSON.Net
Within Unity, double click the `Json.Net` Unity Package and extract the desired version (see below if confused).  This should correct the sea of compiler
errors and allow the example scene to run.

When you double click the Json.NET Unity Package, there will be 3 options to choose from:
- .NET Standard 2.0 (most common)
- .NET 4.0 (available for newer versions of Unity)
- .NET 4.5

![Json.NET Unity Package Import UI](https://i.imgur.com/yzcpGec.png)

Import the one matching your project's build settings, which can be checked at
```
Edit > Project Settings > Player > Api Compatibility Level
```
![Unity Project Settings](https://i.imgur.com/vEfGztT.png)

There may be subtle differences between their `.NET 4.0` and `.NET 4.5` libraries, so mileage may vary between them.  


### Creating an LRS Configuration
To communicate with an LRS, the wrapper needs to know which LRS you intend to use.  To create a new
LRS Configuration:
- Right-click inside of Unity's `Project` tab
- `Create > XAPI > LRS Configuration`
- Assign your endpoint
- Assign your credentials

Once that's created, you'll need to assign that to the xAPI Messenger instance in your scene(s).  

**Note:** This is a bit clunky, so we're open to different ways of assigning these.



### Using the XAPIWrapper class and XAPI Objects
The `XAPIWrapper` class is intended to be your interface into sending and receiving xAPI statements with the configured LRS.  The
`XAPI` namespace itself contains class definitions for serializable xAPi objects as well as helper classes for handling timestamps
and conversions.

From the included "Button" example:
```csharp
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
```

### Example Scene(s)
There is a simple example scene included that demonstrates how to send three different statement payloads to an LRS:
- A single, simple statement
- A pair of simple statements
- A more complex statement using `result` and `context` with extensions

As this example is a bit dull, additional scenes may be added in the future or by request.

