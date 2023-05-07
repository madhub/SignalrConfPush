# Demo SignalR .NET Client
This is .NET SignalR  client , that subscribe notification & commands from SignalR service.
It also implements logic to **increase/decrease the logging level** based on the command from server without
**restarting** the application
## Overview
A persistant bidirectional connection (**Websocket**) created  to signalr service and receives commands & notfication.
A background service receives commands and acts on it.
