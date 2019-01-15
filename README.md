# Azure CosmosDb LinqPad Driver

This is a LinqPad data context driver for Azure CosmosDb 

Check out my blogposts on the making of the driver:

[http://dotnetfalcon.com/tag/linqpad/](http://dotnetfalcon.com/tag/linqpad/)

## Download the binaries

Head over to the [releases](https://github.com/conwid/AzureDocumentDbDriver/releases) to download the binaries.

## Features

For a detailed description of the currently supported features, please refer to the project [Wiki](https://github.com/conwid/AzureDocumentDbDriver/wiki).

* Static datacontext driver that you can use if you have a prebuilt context using my context library
* Dynamic datacontext driver if you want to et started fast
* Querying CosmosDb document collections using LInQ with all the LinqPad integration that you like
* Running CosmosDb stored procedures
* Running CosmosDb UDFs
* Support for executing raw SQL queries in the raw SQL LinqPad window
* Support for executing raw SQL queries from the C# windows
* Support for setting the FeedOptions for the underlying queries
* Populating the SQL translation tab

## Contribution

Every idea, issue or improvement is welcome. If you want to contribute, join the discussions under the issues or read the [Wiki](https://github.com/conwid/AzureDocumentDbDriver/wiki) on how to contribute and issue a PR.

## Disclaimer and license notes

The driver is released under MIT license (check out the License file here in the repo).

Icons made by <a href="http://www.freepik.com" title="Freepik">Freepik</a> from <a href="http://www.flaticon.com" title="Flaticon">www.flaticon.com</a> is licensed by <a href="http://creativecommons.org/licenses/by/3.0/" title="Creative Commons BY 3.0" target="_blank">CC 3.0 BY</a>

Also see the source code for every indication of reasonable attribution.

I would like to thank all the people that helped me when I got stuck with the driver. Check out the discussions at StackOverflow:

* [Grouping multiple LinqPad data context connections](http://stackoverflow.com/questions/43030475/grouping-multiple-linqpad-data-context-connections)
* [LinqPad driver create custom IDbConnection](http://stackoverflow.com/questions/43076219/linqpad-driver-create-custom-idbconnection)
* [Converting arbitrary json response to list of “things”](http://stackoverflow.com/questions/43214424/converting-arbitrary-json-response-to-list-of-things)


