import React from 'react';
import { registerUser } from './Services';

const RegisterForm = ({ onSubmit }) => {
  const [username, setUsername] = React.useState('');

  const handleSubmit = async (event) => {
    event.preventDefault();

    if ((await registerUser(username)))
      alert('Successfully registered');

    setUsername('');
  };

  return (
    <form onSubmit={handleSubmit}>
        <h2>Register</h2>
      <label>
        Username:
        <input type="text" required value={username} onChange={(event) => setUsername(event.target.value)} />
      </label>
      <input type="submit" value="Register" />
    </form>
  );
};

export default RegisterForm;