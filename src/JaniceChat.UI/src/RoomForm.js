import React from 'react';
import { createRoom } from './Services';

const RoomForm = ({ onRoomCreated }) => {
  const [name, setName] = React.useState('');

  const handleSubmit = async (event) => {
    event.preventDefault();
    await createRoom(name);
    setName('');
    onRoomCreated();
  };

  return (
    <form onSubmit={handleSubmit}>
        <h2>Create Room</h2>
      <label>
        Room Name:
        <input type="text" required value={name} onChange={(event) => setName(event.target.value)} />
      </label>
      <input type="submit" value="Create Room" />
    </form>
  );
};

export default RoomForm;