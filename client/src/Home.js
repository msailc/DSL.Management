import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import Navbar from "./Navbar";
import PipelineFetch from "./PipelineFetch";
import FailedPipelineFetch from "./FailedPipelineFetch";
import SuccessfulPipelineFetch from "./SuccessfulPipelineFetch";
import Console from "../Console";
import CreatePipeline from "./CreatePipeline";
import Sidebar from "./Sidebar";
import "./Home.css";
import "./Sidebar.css";
import axios from "axios";

function Home() {
  const navigate = useNavigate();
  const [showPipelineModal, setShowPipelineModal] = useState(false);
  const [showHomePipelines, setShowHomePipelines] = useState(false);
  const [username, setUsername] = useState("");
  const [showSuccessfulPipelines, setShowSuccessfulPipelines] = useState(false);
  const [showFailedPipelines, setShowFailedPipelines] = useState(false);

  gittuseEffect(() => {
    const userToken = localStorage.getItem("userToken");
    if (!userToken) {
      navigate("/"); // Redirect to the "/log" page if "userToken" does not exist
    }
  }, []); 

  const decodeToken = (token) => {
    if (!token) return null;

    try {
      const base64Url = token.split(".")[1];
      const base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
      const jsonPayload = decodeURIComponent(
        Array.from(atob(base64))
          .map(
            (char) => "%" + ("00" + char.charCodeAt(0).toString(16)).slice(-2)
          )
          .join("")
      );

      return JSON.parse(jsonPayload);
    } catch (error) {
      console.error("Error decoding token:", error);
      return null;
    }
  };

  useEffect(() => {
    const token = localStorage.getItem("userToken");
    const decodedToken = decodeToken(token);

    if (decodedToken) {
      const base64Url = token.split(".")[1];
      const base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
      const jsonPayload = decodeURIComponent(
        Array.from(atob(base64))
          .map(
            (char) => "%" + ("00" + char.charCodeAt(0).toString(16)).slice(-2)
          )
          .join("")
      );

      const name =
        decodedToken[
          "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
        ];
      setUsername(name);
      console.log(name);
      localStorage.setItem("username", name);
    } else {
      // Handle the case when the token is invalid or not present
    }
  }, []);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const username = localStorage.getItem("username");
        const response = await axios.get(
          `http://localhost:5017/user/username/${username}`
        );
        const { $id } = response.data;

        localStorage.setItem("userID", $id);
        console.log(localStorage.getItem("userID"));
      } catch (error) {
        // Handle error
        console.error("Error fetching user data:", error);
      }
    };

    fetchData();
  }, []);

  function logOutHandler() {
    localStorage.removeItem("userToken");
    localStorage.removeItem("username");
    localStorage.removeItem("userID");
    navigate("/");
  }

  const handleNewPipelineClick = () => {
    setShowPipelineModal(true);
    setShowFailedPipelines(false);
    setShowHomePipelines(false);
    setShowSuccessfulPipelines(false);
  };

  const handleSuccessfulPipelinesClick = () => {
    setShowSuccessfulPipelines(true);
    setShowPipelineModal(false);
    setShowFailedPipelines(false);
    setShowHomePipelines(false);
  };
  const handleFailedPipelinesClick = () => {
    setShowSuccessfulPipelines(false);
    setShowPipelineModal(false);
    setShowFailedPipelines(true);
    setShowHomePipelines(false);
  };
  const handleHomeClick = () => {
    setShowHomePipelines(true);
    setShowSuccessfulPipelines(false);
    setShowPipelineModal(false);
    setShowFailedPipelines(false);
  };

  return (
    <div>
      <Navbar username={username} />
      <div className="home-container">
        <Sidebar
          handleSuccessfulPipelinesClick={handleSuccessfulPipelinesClick}
          handleNewPipelineClick={handleNewPipelineClick}
          logOutHandler={logOutHandler}
          handleFailedPipelinesClick={handleFailedPipelinesClick}
          handleHomeClick={handleHomeClick}
        />
        <div className="home-right">
          {/* Right side content goes here */}
          {showPipelineModal && <CreatePipeline />}
          {showHomePipelines && <PipelineFetch />}
          {showSuccessfulPipelines && <SuccessfulPipelineFetch />}
          {showFailedPipelines && <FailedPipelineFetch />}
        </div>
      </div>
    </div>
  );
}

export default Home;