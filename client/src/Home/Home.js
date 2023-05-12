import React from 'react';
import Navbar from './Navbar';
import PipelineFetch from './PipelineFetch';


function Home() {
  return (
    <div>
      <Navbar />
      <div className="home-container">
        <div className="home-left">
          <div className="pipelines-container">
            <div className="pipeline-header">
              <h2>Recently Created Pipelines</h2>
            </div>
            <div className="pipeline-list">
            <PipelineFetch/>
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
        </div>
      </div>
    </div>
  );
}

export default Home;
