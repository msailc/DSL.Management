import "./Home.css";
import "./Sidebar.css";
function Sidebar({ handleSuccessfulPipelinesClick, handleNewPipelineClick, logOutHandler }) {

    return (
        <div className="home-left">
            <div className="pipelines-container">
                <ul>
                    <li>
                        <a href="#">Home</a>
                    </li>
                    <li>
                        <a href="#">Profile</a>
                    </li>
                    <li>
                        <a href="#" onClick={handleSuccessfulPipelinesClick}>
                            Successful Pipelines
                        </a>
                    </li>
                    <li>
                        <a href="#" onClick={handleSuccessfulPipelinesClick}>
                            Failed Pipelines
                        </a>
                    </li>
                    <li>
                        <a href="#" onClick={handleNewPipelineClick}>
                            Create Pipeline
                        </a>

                    </li>
                </ul>
                <button onClick={logOutHandler}>Log Out</button>
            </div>
        </div>
    );
}

export default Sidebar