# Chat Application
A simple browser-based chat application using .NET.
Should allow several users to talk in a chatroom and also to get stock quotes from an API using a specific command.

## Features
- Allow users to register in the application;
- Allow registered users to log in and talk with other users in a chatroom;
- Allow users to post messages as commands into the chatroom with the following format
    - Get stock code: `/{command}={parameter}` where:
        - `{command}` is a command name. Eg.: `stock`;
        - `{parameter}` is a parameter for the commmand. Eg.: `aapl.us`;

### Command: Stock
Allows users to use command `/stock` that will use `stooq.com` API to get stock data, and then post this data back to the chat room as message in the following format: 
> "`stock_code` quote is `value` per share"

The post owner will be the bot.

#### API: stooq.com
Request:
- `HTTP GET` : `https://stooq.com/q/l/?s={stock_code}&f=sd2t2ohlcv&h&e=csv`

Response:

The API result is a CSV file containing the following columns:
`Symbol`, `Date`, `Time`, `Open`, `High`, `Low`, `Close` and `Volume`.


## Architecture
The following [C4 System Context Diagram](https://c4model.com/#SystemContextDiagram) describes the persons and the systems relations:

![C4 System Context Diagram](docs/images/chat-diagram-C4-Context.drawio.svg)


The following [C4 System Container Diagram](https://c4model.com/#ContainerDiagram) the Chat App:
![C4 System Container Diagram](docs/images/chat-diagram-C4-Container.drawio.svg)

## Running the Application

### Docker Compose

### Manually

Start RabbitMQ:
```
docker run -p 15672:15672 -p 5672:5672 masstransit/rabbitmq
```