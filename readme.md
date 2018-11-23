# SmartHome Commander
The SmartHomeCommander sends a given command to an defined MQTT broker with the topic `nexhome/event/{{thingId}}/{{event.name}}`. 

## Getting Started
If you want to start the app you can just clone the repository and run the following command.  
```
docker-compose up
``` 
With the API there is also an local MQTT Server. The API connects by default to this local MQTT Server.

## Using the API
There is an example request in `test.http` to test the API with the required format. 

## Deployment
You can change the given connection and other paramters in two ways: Either you set the environment variable in the `docker-compose.yml` or you can set the same properties in the `appsettings.json`. 
If you define the parameters in both files, the params from the `docker-compose.yml` will be used. 
