# Building .NET Microservices using the REST API pattern 

## Platform Service
Tracks all the platforms in the company. 
Build by the Infrastructure Team. 
Used by Infrastructure Team, Technical Support Team, Engineering, Accounting and Procurement.
Uses SQL database.

## Commands Service
Function as a repository of command line augments for given Plaforms. 
Aid in the automation of support process. 
Built by the Technical Support Team. 
Used by Technical Support Team, Infrastructure Team and Engineering.
Uses In Memory database.

## RabbitMQ Message Bus
Used for data sharing between publisher (Platform Service) and subscriber (Command Service).
