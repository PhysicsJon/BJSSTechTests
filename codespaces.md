# Codespaces

Codespaces are fully cloud based development environments utilising Visual Studio Code in a browser window.

## Setup
The following steps will get you up and running with a codespace. This must all be done in advance of the interview (should take no more than 15 mins, please ensure this is completed in the 30 mins before the candidate is due).

1. Create a new branch with a sensible name that relates to the candidate and the date
2. Click the green `Code` button and select the `Codespaces` tab.
3. Click `Create codespace on {branch-name}` button
4. When the codespace has loaded (you will see a window in the browser that looks like VS Code, run the commands from [here](installChromeOnCodespaces.md) in the integrated terminal
5. Install the Live Share extension ([instructions](https://docs.github.com/en/codespaces/developing-in-codespaces/working-collaboratively-in-a-codespace))
6. Click the new live share button in the left aligned taskbar click the green Share button (this should copy a link to the live share to your clipboard)
7. When the candidate is ready, share the link copied above and approve the candidate as a read and write user.

## Running dotnet based tests
The dotnet tests do not need anything other than the above steps. Just right click on the `dotnet/BJSSTechTestDotNet/BJSSTechTestDotNet.CandidateTests` folder and select `Open in integrated terminal`. When the terminal has opened, enter `dotnet test` and the tests will run with a dynamically created environment.

## Running other language tests
There is a short setup for other languages.

1. Right click on `dotnet/BJSSTechTestDotNet/BJSSTechTestDotNet.WebApp` and select `Open in integrated terminal`
2. In the terminal enter `dotnet run watch`
3. Create a new terminal (you can right click on the folder of the relevant language and select `Open in integrated terminal`) and navigate to the part of the codebase that is the base for running tests
