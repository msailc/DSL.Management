import React, { useState } from 'react';
import { FaHome, FaUser, FaCheckCircle, FaTimesCircle, FaPlus, FaSignOutAlt } from 'react-icons/fa';

function Sidebar({ handleSuccessfulPipelinesClick, handleNewPipelineClick, logOutHandler, handleHomeClick, handleProfileClick, handleFailedPipelinesClick }) {
  const [selectedButton, setSelectedButton] = useState('home');

  const handleButtonClick = (button) => {
    setSelectedButton(button);
  };

  return (
    <div className="home-left">
      <div className="pipelines-container">
        <ul>
          <li className={selectedButton === 'home' ? 'selected' : ''}>
            <a href="#" onClick={() => { handleButtonClick('home'); handleHomeClick(); }}>
              <FaHome size={20} /> <span className="sidebar-text">Home</span>
            </a>
          </li>
          <li className={selectedButton === 'profile' ? 'selected' : ''}>
            <a href="#" onClick={() => { handleButtonClick('profile'); handleProfileClick(); }}>
              <FaUser size={20} /> <span className="sidebar-text">Profile</span>
            </a>
          </li>
          <li className={selectedButton === 'successfulPipelines' ? 'selected' : ''}>
            <a href="#" onClick={() => { handleButtonClick('successfulPipelines'); handleSuccessfulPipelinesClick(); }}>
              <FaCheckCircle size={20} /> <span className="sidebar-text">Successful Pipelines</span>
            </a>
          </li>
          <li className={selectedButton === 'failedPipelines' ? 'selected' : ''}>
            <a href="#" onClick={() => { handleButtonClick('failedPipelines'); handleFailedPipelinesClick(); }}>
              <FaTimesCircle size={20} /> <span className="sidebar-text">Failed Pipelines</span>
            </a>
          </li>
          <li className={selectedButton === 'createPipeline' ? 'selected' : ''}>
            <a href="#" onClick={() => { handleButtonClick('createPipeline'); handleNewPipelineClick(); }}>
              <FaPlus size={20} /> <span className="sidebar-text">Create Pipeline</span>
            </a>
          </li>
        </ul>
        <button className="logout-button" onClick={logOutHandler}>
          <FaSignOutAlt size={20} /> <span className="sidebar-text">Log Out</span>
        </button>
      </div>
    </div>
  );
}

export default Sidebar;
