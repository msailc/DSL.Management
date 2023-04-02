import UserData from "./UserTest.json";

export default function UserModal(props) {
  return (
    <div>
      <header>
        <h2>Welcome {UserData[0].username}</h2>
      </header>
      <body>
        <h3>Name: {UserData[0].name}</h3>
        <h3>Surname:{UserData[0].surname}</h3>
      </body>
      <footer>
        <button>Log Out not implemented</button>
        <button onClick={props.CloseModal}>Close Modal!</button>
      </footer>
    </div>
  );
}
