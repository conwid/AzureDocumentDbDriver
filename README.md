# Azure DocumentDb LinqPad Driver

This is a LinqPad data context driver for Azure DocumentDb 
(now rebranded as CosmosDb).

You can check out my blogpost on the making of the driver:

[http://www.dotnetfalcon.com/linqpad-documentdb-now-cosmosdb-driver/](http://www.dotnetfalcon.com/linqpad-documentdb-now-cosmosdb-driver/)

## Download the binaries

If you want, you can just go ahead and download the binaries from my blog or directly from these links:

* [Driver package](https://dotnetfalconcontent.blob.core.windows.net/linqpad-documentdb-now-cosmosdb-driver/AzureDocumentDbDriver.lpx)
* [Static driver helper class library](https://dotnetfalconcontent.blob.core.windows.net/linqpad-documentdb-now-cosmosdb-driver/ContextLibrary.dll)


## Build and run

You can fork or clone the repo and modify the source code if you want.
For testing, I suggest you take a look at the [official documentation](http://www.linqpad.net/Extensibility.aspx)
on how to write drivers.

I created a DevDeploy.bat and a ReleaseDeploy.ps1, both of which are included
in the repo, they handle automatic packaging for release mode and copy
the output into the right folders for debug mode. They are added by default as build actions. If you want, you
can disable them or add your own.

**Note**:  LinqPad creates a folder for the drivers and the folder name
includes the public part of the signing key, if the driver is signed. I signed
the driver with a self-signed pfx just for fun, but I didn't upload it.
So in order to build the project, first disable signing the assembly, and then
modify the build scripts so that they copy the unsigned dlls (see the official
documentation on how).

## Contribution

Any ideas are welcome. Also if you discover any issues, feel free to create a ticket.
If you decide to contribute, PRs are welcome.

## Features

### Static driver

A static driver uses a prebuilt dll that you have to load to discover the data source and run the queries. 
To help you get started, I created a class library called ContextLibrary. You can use the base classes and methods in this class library to build your static driver data context, load it into LinqPad and get querying. I included some samples on how to use it
in the StaticDriverDataContext project.

### Dynamic driver

I implemented a dynamic driver too, but since there's no schema or structure
wahtsorever to be discovered automatically by the driver, some sacrificies were
necessary. The most important one being the fact the everything runs on the client side. 
If you create a query against a collection, the contents of the collection are downloaded and then you are essentially making Linq-to-objects queries.

The problem comes from the fact that in order to support a schemaless structure, dynamic have to be used. But Linq and dynamic don't really mix well (expression trees cannot contain dynamic references) so I had to get creative and this is what I came up with. Go ahead and check out the source code; if you have a better idea, suggestions and PRs are welcome.


### Querying collections with Linq

If you load up the driver and connect to a data source, the driver automatically discovers the collections that are available and you can run queries against them:

![](https://dotnetfalconcontent.blob.core.windows.net/linqpad-documentdb-now-cosmosdb-driver/collections.png)

If you use the dynamic driver, as mentioned before, you are basically running Linq-to-objects queries against the downloaded contents of the collection. If you use the static driver, then the queries you create are executed on the server-side.

![](https://dotnetfalconcontent.blob.core.windows.net/linqpad-documentdb-now-cosmosdb-driver/collection_query.png)

As a bonus, if you right-click on a collection, you have all sorts of shortcuts to execute queries:

![](https://dotnetfalconcontent.blob.core.windows.net/linqpad-documentdb-now-cosmosdb-driver/collection_context.png)

### Stored procedures and user-defined functions
Both the static and the dynamic drivers support stored procedures and user defined functions (though a little differently).

First off, they are listed in the browser window:

![](https://dotnetfalconcontent.blob.core.windows.net/linqpad-documentdb-now-cosmosdb-driver/splist.png)

Second, you can run an sp if you want either by typing the call manually, or right-clicking the sp name in the browser and importing the call into the editor window. The results are then displayed in a table form:

![](https://dotnetfalconcontent.blob.core.windows.net/linqpad-documentdb-now-cosmosdb-driver/sp_run.png)

Parameters are also supported.

Since the results of the sp are loaded into memory, you can go ahead and use Linq-to-objects to query your data.

![](https://dotnetfalconcontent.blob.core.windows.net/linqpad-documentdb-now-cosmosdb-driver/sp_run_filter.png)

If you use the dynamic driver, you don't really need to do anything. If you use the static driver, you have to create a method with the name of the SP that returns IQueryable<T> (T is your type that you are querying) and inside the method call the CreateStoredProcedure() method (you can check out the Github sample for more).
UDFs are also supported &mdash; you can run them as an SQL query (see below).

And the nice addition: if you hover over an sp or udf, you can check out their code:

![](https://dotnetfalconcontent.blob.core.windows.net/linqpad-documentdb-now-cosmosdb-driver/sp_hover.png)

### SQL support

Here's the coolest of all: if you change your language to SQL, you can run DocumentDB sql queries directly from LinqPad and see the results in the table. 

![](https://dotnetfalconcontent.blob.core.windows.net/linqpad-documentdb-now-cosmosdb-driver/sql.png)

The syntax is almost entirely coherent with that of DocumentDb. The only difference is that you have to first specify the collection, against which you'd like to run the query (note that cross-collection queries are not supported, so this is not really a problem). 

## Roadmap
Here's a couple of features that I would like to add in the future (if you want to help, feel free to submit a PR on Github):

* Nicer connect dialog: It would be nice if you could just connect to your Azure subscription and then select the data source from a treeview or something.
* Populating the SQL window: DocumentDb Linq queries are compiled to data source specific queries, just like in the case of Linq-to-Entities or every other Linq-based query API. LinqPad has a tab to display the generated data source specific query from the Linq query &mdash; hooking this up would be a nice addition.
* Trying to find ways the push even more things to the server side. I'm not sure how much of this is possible, but this is part of a continuous improvement process (not that critical though &mdash; my experience is people usually only use LinqPad for fiddling, and for that convenience is a more important factor than performance).
* Maybe the ADO.NET provider could be extended to support more features, not just the ones that LinqPad currently uses. 

## Disclaimer and license notes

The driver is released under MIT license (check out the License file here in the repo).

Icons made by <a href="http://www.freepik.com" title="Freepik">Freepik</a> from <a href="http://www.flaticon.com" title="Flaticon">www.flaticon.com</a> is licensed by <a href="http://creativecommons.org/licenses/by/3.0/" title="Creative Commons BY 3.0" target="_blank">CC 3.0 BY</a>

Also see the source code for every indication of reasonable attribution.

I would like to thank all the people that helped me when I got stuck with the driver. Check out the discussions at StackOverflow:

* [Grouping multiple LinqPad data context connections](http://stackoverflow.com/questions/43030475/grouping-multiple-linqpad-data-context-connections)
* [LinqPad driver create custom IDbConnection](http://stackoverflow.com/questions/43076219/linqpad-driver-create-custom-idbconnection)
* [Converting arbitrary json response to list of “things”](http://stackoverflow.com/questions/43214424/converting-arbitrary-json-response-to-list-of-things)


