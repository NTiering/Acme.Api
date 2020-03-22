========================
	Acme.Data
========================

This project contains application logic to control the access and actions upon persistent state of the application.

-----------------------
Acme.Changehandlers 
-----------------------
These are business logic actions to be ran AFTER data has been changed

-----------------------
Acme.DataModels
-----------------------
These are the classes that represent the state of the application 

-----------------------
Acme.Mutators  
-----------------------
These contain actions that act on the data BEFORE it is committed to the data store

-----------------------
Acme.Search
-----------------------
These are dedicated search routines implemented as extension methods. 
For small numbers of servers (1-3) you can implement caching locally using asp.net cache.
For larger cluters , try an external cache, E.G. Redis 



