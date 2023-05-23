import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import Navbar from "./Navbar";
import PipelineFetch from "./PipelineFetch";
import Console from "../Console";
import CreatePipeline from "./CreatePipeline";
import Sidebar from "./Sidebar";
import "./Home.css";
import "./Sidebar.css";

function Home() {
  const navigate = useNavigate();
  const [showPipelineModal, setShowPipelineModal] = useState(false);
  const [showPipelines, setShowPipelines] = useState(false);

  useEffect(() => {
    const userToken = localStorage.getItem("userToken");
    if (!userToken) {
      navigate("/"); // Redirect to the "/log" page if "userToken" does not exist
    }
  }, []);

  function logOutHandler() {
    localStorage.removeItem("userToken");
    navigate("/");
  }

  const handleNewPipelineClick = () => {
    setShowPipelineModal(true);
    setShowPipelines(false)
  };

  const handleSuccessfulPipelinesClick = () => {
    setShowPipelines(true);
    setShowPipelineModal(false)
  };

  return (
      <div>
        <Navbar />
        <div className="home-container">
          <Sidebar
              handleSuccessfulPipelinesClick={handleSuccessfulPipelinesClick}
              handleNewPipelineClick={handleNewPipelineClick}
              logOutHandler={logOutHandler}
          />
          <div className="home-right">
            {/* Right side content goes here */}
            {showPipelineModal && <CreatePipeline />}
            {showPipelines && <PipelineFetch />}
          </div>
        </div>
      </div>
  );
}

export default Home;