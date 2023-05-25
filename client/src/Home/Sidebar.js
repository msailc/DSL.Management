import "./Home.css";
import "./Sidebar.css";
function Sidebar(props) {
  return (
    <div className="home-left">
      <div className="pipelines-container">
        <ul>
          <li>
            <a href="#" onClick={props.handleHomeClick}>
              Home
            </a>
          </li>
          <li>
            <a href="#">Profile</a>
          </li>
          <li>
            <a href="#" onClick={props.handleSuccessfulPipelinesClick}>
              Successful Pipelines
            </a>
          </li>
          <li>
            <a href="#" onClick={props.handleFailedPipelinesClick}>
              Failed Pipelines
            </a>
          </li>
          <li>
            <a href="#" onClick={props.handleNewPipelineClick}>
              Create Pipeline
            </a>
          </li>
        </ul>
        <button onClick={props.logOutHandler}>Log Out</button>
      </div>
    </div>
  );
}

export default Sidebar;
