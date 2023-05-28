import React, { useState, useEffect } from "react";
import axios from "axios";
import "./PipelineFetch.css"; // Import the CSS file
import Console from "../Console";

export default function PipelineFetch() {
  const [pipelines, getPipelines] = useState([]);

  useEffect(() => {
    axios
      .get(`http://localhost:5017/pipeline`)
      .then((res) => {
        console.log(res);
        getPipelines(
          Array.isArray(res.data.$values)
            ? res.data.$values.map((pipeline) => ({
                ...pipeline,
                collapsed: true,
                gitUrl: "" // Initialize gitUrl as an empty string
              }))
            : []
        );
      })
      .catch((err) => console.log(err));
  }, []);

  const togglePipeline = (id) => {
    getPipelines((prevPipelines) =>
      prevPipelines.map((pipeline) =>
        pipeline.id === id
          ? { ...pipeline, collapsed: !pipeline.collapsed }
          : { ...pipeline, collapsed: true }
      )
    );
  };
  
  const handleExecutePipelineClick = (pipelineId, gitUrl) => {
    const url = `http://localhost:5017/pipeline/${pipelineId}/execute`;
    console.log(pipelineId,gitUrl);
    const headers = {
      "Content-Type": "application/json", // Specify the content type as JSON
    };
    const payload = {
      gitUrl: gitUrl
    };
  
    axios
      .post(url, payload, { headers })
      .then((res) => {
        console.log("POST request successful");
        console.log(res);
        // Handle the response if needed
      })
      .catch((err) => {
        console.error("Error occurred during POST request:", err);
        // Handle the error if needed
      });
  };
  
  const handleGitUrlChange = (pipelineId, event) => {
    const gitUrl = event.target.value;
    getPipelines((prevPipelines) =>
      prevPipelines.map((pipeline) =>
        pipeline.id === pipelineId ? { ...pipeline, gitUrl } : pipeline
      )
    );
  };
  
  return (
    <div className="pipeline-container">
      {pipelines.map((pipeline) => (
        <div key={pipeline.id} className="pipeline">
          <button onClick={() => togglePipeline(pipeline.id)}>
            {pipeline.name}
          </button>
          {!pipeline.collapsed && (
            <ul>
              {pipeline.steps.$values.map((step) => (
                <li key={step.id}>Command: {step.command}</li>
              ))}
              <form
                onSubmit={(event) => {
                  event.preventDefault();
                  handleExecutePipelineClick(pipeline.id, pipeline.gitUrl);
                }}
              >
                <label>gitUrl:
                <input
                  type="text"
                  value={pipeline.gitUrl}
                  onChange={(event) => handleGitUrlChange(pipeline.id, event)}
                  
                />
                </label>
                <div>
              <Console/>
              </div>
                <button type="submit">Execute</button>
              </form>
            </ul>
          )}
        </div>
      ))}
    </div>
  );
}