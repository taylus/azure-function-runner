# Azure Function Runner
A simple command line-based Azure Function simulator/runner.

I made this because I wanted the IntelliSense and debugging experience that the [Azure Functions Portal](https://functions.azure.com/), as neat as it is, doesn't have.

This isn't a replacement for [proper tooling from Microsoft](https://docs.microsoft.com/en-us/azure/azure-functions/functions-develop-vs) that handles the other aspects of an Azure Functions project, it's just for rapid prototyping.

This is also written only around the "HttpTrigger" kind of Azure Function. Other kinds should be relatively simple to adapt.