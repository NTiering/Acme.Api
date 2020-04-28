# Acme.Api
Example Api Application

Uses dotnet core & web api

Contains examples of caching, encryption, data mutation and Change handlers 


## Design considerations 

Splitting read write and readonly controllers 

This allows searches to be slightly faster and the IoC container doenst need to build CRUD (create, update , delete) functions.

Search functions as extension methods 
This allows search functions to be plugged in as cross cutting concerns. This MAY be a  problem in the future as extension methods are particularly difficult to mock 

