export default function NavBar(props) {
  return (
    <>
      <nav>
        <h1>DSL Management</h1>
        <button onClick={props.UserButtonHandler}>USER</button>
      </nav>
    </>
  );
}
