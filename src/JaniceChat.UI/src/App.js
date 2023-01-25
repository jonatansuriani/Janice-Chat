import React from 'react';
import RegisterForm from './RegisterForm';
import LoginForm from './LoginForm';
import ChatRoom from './ChatRoom';

const App = () => {
  const [loggedIn, setLoggedIn] = React.useState(localStorage.getItem("token") != null);

  const handleOnLoginSuccessful = (userName)=> {
    setLoggedIn(userName != null);
  };

  return  (
    <>
      <h1>Chat</h1>

      { !loggedIn ? (
        <div>
          <RegisterForm />
          <hr />
          <LoginForm onLoginSuccessful={handleOnLoginSuccessful} />
        </div>)
      : (
        <div>
          <button onClick={()=>{localStorage.setItem("token", null); handleOnLoginSuccessful(null)}}>Logout</button>
          <ChatRoom />
        </div>
      )}
    </>
  );
}

export default App;
