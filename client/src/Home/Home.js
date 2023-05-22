import React, { useEffect, useState } from "react";
import Navbar from "./Navbar";
import PipelineFetch from "./PipelineFetch";
import Console from "../Console";
import CreatePipeline from "./CreatePipeline";
import { useNavigate } from "react-router-dom";

function Home() {
  const navigate = useNavigate();
  const [showPipelineModal, setShowPipelineModal] = useState(false);

  useEffect(() => {
    const userToken = localStorage.getItem("userToken");
    if (!userToken) {
      navigate("/"); // Redirect to the "/log" page if "userToken" does not exist
    }
  }, []);

  function logOutHandler() {
    localStorage.removeItem('userToken');
    navigate('/');
  }

  const handleNewPipelineClick = () => {
    setShowPipelineModal(true);
  };
  return (
    <div>
      <Navbar />
      <div className="home-container">
        <div className="home-left">
          <div className="pipelines-container">
          <button onClick={handleNewPipelineClick}>New Pipeline</button>
            <button onClick={logOutHandler}>Log Out</button>
            <div className="pipeline-header">
              <h2>Recently Created Pipelines</h2>
            </div>
            <div className="pipeline-list">
              <PipelineFetch />
            </div>
          </div>
          <div className="pipelines-container">
            <div className="pipeline-header">
              <h2>Recent Successful Pipelines</h2>
            </div>
            <div className="pipeline-list">
              <ul>
                <li>Pipeline A</li>
                <li>Pipeline B</li>
                <li>Pipeline C</li>
              </ul>
            </div>
          </div>
        </div>
        <div className="home-right">
          {/* Right side content goes here */}
          {showPipelineModal && (
            <CreatePipeline/>
          )}
          <Console />
          gdfgdfgdfgdfgfdgfd
        </div>
      </div>
    </div>
  );
}

export default Home;
