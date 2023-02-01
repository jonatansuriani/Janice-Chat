import React from 'react';
import { getChatRooms, getMessages } from './Services';
import { sendMessage } from "./Services";
import { getHubConnection } from './Services';
import RoomForm from './RoomForm'

class ChatRoom extends React.Component {
    constructor(props) {
      super(props);
      this.state = {
        roomList: [],
        roomSelected: null,
        messages : [],
        message: ''
      };

      this.onMessageSubmit = this.onMessageSubmit.bind(this);
      this.onMessageCreatedEvent = this.onMessageCreatedEvent.bind(this);
      this.refreshChatRooms = this.refreshChatRooms.bind(this);
      this.connection = null;
    }
  
    componentDidMount() {
        this.refreshChatRooms();
    }

    async refreshChatRooms() {
        const data = await getChatRooms();
        this.setState({ roomList: data });
    };
    
    async onRoomSelected(room) {
        this.setState({roomSelected: room});
        this.setState({ messages: await getMessages(room.id)});

        this.connection = getHubConnection(room.id, this.onMessageCreatedEvent);
    };

    async onMessageCreatedEvent (message) {
        this.setState({ messages: [...this.state.messages, message]});
    };

    async onMessageSubmit(event){
        event.preventDefault();
        await sendMessage(this.state.roomSelected.id, this.state.message);
        this.setState({message: ''});
    }    

    render() {
   
        return (
            <>
                <RoomForm onRoomCreated={this.refreshChatRooms} />
                <hr />
                Rooms Available:
                <button onClick={this.refreshChatRooms}>Refresh Rooms</button>

                
                <ul>
                    {this.state.roomList.map((room) => (
                        <button key={room.id} onClick={()=>this.onRoomSelected(room)}>{room.name}</button>
                    ))}
                </ul>
                <hr />
                { this.state.roomSelected ? (
                    <>
                        <h2>Room: {this.state.roomSelected.name}</h2>
                        <div>
                            { this.state.messages.map((message)=>(
                                <div key={message.id}>
                                    {message.userUserName} - {message.date}<br />
                                    {message.message}
                                    <hr />
                                </div>
                                
                            )) }
                        </div>
                        <form onSubmit={this.onMessageSubmit}>
                            <textarea required value={this.state.message} onChange={(event) => this.setState({message: event.target.value})}></textarea>
                            <input type="submit" value="Send" />
                        </form>
                </>) : '' }
            </>
        );
      }
}

export default ChatRoom;