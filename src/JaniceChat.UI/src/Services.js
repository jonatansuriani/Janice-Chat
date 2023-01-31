import { HubConnectionBuilder } from "@microsoft/signalr";

const baseUrl = "http://localhost:5071/";

const apiCall = async (url, options) => {

  const token = localStorage.getItem("token");
  const headers = { ...options.headers, 'Content-Type': 'application/json', Authorization: `Bearer ${token}` };
  const requestOptions = { ...options, headers };

  return await fetch(baseUrl + url, requestOptions);
};

export async function registerUser (userName) {
  try {
    const response = await apiCall('api/users/', {
      method: 'POST',
      body: JSON.stringify({ userName }),
    });

    if (!response.ok)
      throw Error((await response.json()).message);
  } 
  catch (error) {
    alert(error.message);
    return false;
  }
  return true;
};

export async function createRoom (name) {
  try {
    const response = await apiCall('api/chat/room', {
      method: 'POST',
      body: JSON.stringify({ name }),
    });

    if (!response.ok)
      throw Error((await response.json()).message);
  } 
  catch (error) {
    alert(error.message);
    return false;
  }
  return true;
};

export async function authorize (userName) {
  try {
    const response = await apiCall('api/users/authorize', {
      method: 'POST',
      body: JSON.stringify({ userName }),
    });
    if (!response.ok)
      if (response.status == 401)
        throw Error("Unauthorized");
      else
        throw Error((await response.json()).message);

    const { token } = await response.json();
    localStorage.setItem("token", token);
  } 
  catch (error) {
    alert(error.message);
    return false;
  }
  return true;
};

export async function getChatRooms() {
  try {
    const response = await apiCall('api/chat/rooms', {
      method: 'GET'
    });

    if (!response.ok) {
      throw new Error(response.statusText);
    }
    return await response.json();
  } 
  catch (error) {
    alert(error.message);
  }
};



export async function getMessages(roomId) {
  try {
    const response = await apiCall(`api/chat/room/${roomId}/messages?skip=0&take=50`, {
      method: 'GET'
    });

    if (!response.ok) {
      throw new Error(response.statusText);
    }
    return (await response.json()).sort((a,b)=> a.date > b.date ? 1 : -1);
  } 
  catch (error) {
    alert(error.message);
  }
};

export async function sendMessage(roomId, message) {
  try {
    const response = await apiCall(`api/chat/room/${roomId}/message`, {
      method: 'POST',
      body: JSON.stringify({ message })
    });

    if (!response.ok) {
      throw new Error(response.statusText);
    }
    return true;
  } 
  catch (error) {
    alert(error.message);
  }
};

export async function getHubConnection(roomId, onMessageCreatedEvent) {
  const connection = new HubConnectionBuilder()
    .withUrl(baseUrl + "Chat", { 
      accessTokenFactory: () => localStorage.getItem("token"),
      withCredentials: true
    })
    .build();

  await connection.start()
    .catch(err => console.error(err));

  await connection.invoke("JoinChatRoom", roomId)
    .catch(console.error);

  connection.on("MessageCreatedEvent", onMessageCreatedEvent);    
  return connection;
}