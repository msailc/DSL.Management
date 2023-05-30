
import React, { useState, useEffect } from "react";
import axios from "axios";
import Console from "../Console";

export default function UserPipelines() {
  const [pipelines, getPipelines] = useState([]);
  const userId = localStorage.getItem("userID");

  useEffect(() => {
    axios
      .get(`http://localhost:5017/user/${userId}`)
      .then((res) => {
        console.log(res);
        getPipelines(
          Array.isArray(res.data.pipelines.$values)
            ? res.data.pipelines.$values.map((pipeline) => ({
                ...pipeline,
                collapsed: true,
                gitUrl: ""
              }))
            : []
        );
      })
      .catch((err) => console.log(err));
  }, [userId]);

  const togglePipeline = (id) => {
    getPipelines((prevPipelines) =>
      prevPipelines.map((pipeline) =>
        pipeline.pipelineId === id
          ? { ...pipeline, collapsed: !pipeline.collapsed }
          : { ...pipeline, collapsed: true }
      )
    );
  };

  const handleExecutePipelineClick = (pipelineId, gitUrl) => {
    const url = `http://localhost:5017/pipeline/${pipelineId}/execute`;
    const headers = {
      "Content-Type": "application/json",
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
        pipeline.pipelineId === pipelineId ? { ...pipeline, gitUrl } : pipeline
      )
    );
  };

  return (
    <div className="pipeline-container">
      {pipelines.map((pipeline) => (
        <div key={pipeline.pipelineId} className="pipeline">
          <button onClick={() => togglePipeline(pipeline.pipelineId)}>
            {pipeline.name}
          </button>
          {!pipeline.collapsed && (
            <ul>
              {pipeline.steps.$values.map((step) => (
                <li key={step.stepId}>Command: {step.command}</li>
              ))}
            </ul>
          )}
        </div>
      ))}
    </div>
  );
}