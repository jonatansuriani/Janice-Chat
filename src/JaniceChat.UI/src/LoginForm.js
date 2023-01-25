import React from 'react';
import { authorize } from './Services';

const LoginForm = ({ onLoginSuccessful }) => {
  const [username, setUsername] = React.useState('');

  const handleSubmit = async (event) => {
    event.preventDefault();

    var result = await authorize(username);
    
    if (result)
      onLoginSuccessful(username);
  };

  return (
    <form onSubmit={handleSubmit}>
        <h2>Login</h2>
      <label>
        Username:
        <input type="text" required value={username} onChange={(event) => setUsername(event.target.value)} />
      </label>
      <input type="submit" value="Login" />
    </form>
  );
};

export default LoginForm;