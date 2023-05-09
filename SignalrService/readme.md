# Demo SignalR .NET Service
This is ASP.NET Core SignalR  service, that allows clients to subscribe for notification & commands.Provides a REST API
to notify & send commands to connected clients. 
## Overview
A ASP.NET Core server application that uses Signalr feature enable connected client to receive notification & commands

### Building Docker images
Make sure Docker is installed on your system.
- Change to **SignalrService** project directory
	```shell
		cd SignalrService
	```
- Build docker images,
	```shell
	docker build -t demos/signalrservice:1.0 -f Dockerfile ..\..\SignalrConfPush
	```
### Running in Kubernetes
Make sure to have Kubernetes with [nginx ingress](https://kubernetes.github.io/ingress-nginx/deploy/).
- Change to **Deployment** directory
	```shell
		cd SignalrService
	```
- Execute kubectl command to deploy app in **demo** namespace
	```shell
		kubectl apply -f signalr-service-deployment.yaml	
	```
- Point your Web Browser to http://kubernetes.docker.internal/swagger to see the API documentation 