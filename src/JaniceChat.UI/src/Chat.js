import React from 'react';

const Chat = ({room, messages, onSubmit})=>{
    const [message, setMessage] = React.useState('');

    const handleSubmit = (event) => {
        event.preventDefault();
        onSubmit(message);
        setMessage('');
    };

    return (
        <>
            { room ? (
            <>
                <h2>Room: {room.name}</h2>
                <div>
                    { messages.map((message)=>(
                        <div key={message.id}>
                            {message.userUserName} - {message.date}<br />
                            {message.message}
                            <hr />
                        </div>
                        
                    )) }
                </div>
                <form onSubmit={handleSubmit}>
                    <textarea required value={message} onChange={(event) => setMessage(event.target.value)}></textarea>
                    <input type="submit" value="Register" />
                </form>
            </>) : '' }
        </>
    );
};

export default Chat;