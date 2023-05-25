import React, { useState, useEffect } from "react";
import axios from "axios";

export default function FailedPipelineFetch() {
  const [pipelines, getPipelines] = useState([]);
  const [gitUrl, setGitUrl] = useState("");

  const handleSubmit = (event, pipelineId) => {
    event.preventDefault();
    handleExecutePipelineClick(pipelineId, gitUrl);
  };

  useEffect(() => {
    axios
      .get("http://localhost:5017/pipeline/executions?success=true")
      .then((res) => {
        console.log(res);
        getPipelines(
          Array.isArray(res.data.$values)
            ? res.data.$values.map((pipeline) => ({
                ...pipeline,
                collapsed: true,
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
    console.log(pipelineId, gitUrl);
    const headers = {
      "Content-Type": "application/json", // Specify the content type as JSON
    };
    const payload = {
      gitUrl: gitUrl,
    };

    axios
      .post(url, payload, { headers })
      .then((res) => {
        console.log("POST request successful");
        // Handle the response if needed
      })
      .catch((err) => {
        console.error("Error occurred during POST request:", err);
        // Handle the error if needed
      });
  };

  return (
    <div className="pipeline-container">
      {pipelines.map((pipeline) => (
        <div key={pipeline.id} className="pipeline">
          <button onClick={() => togglePipeline(pipeline.id)}>
            {pipeline.pipelineName}
          </button>
          {!pipeline.collapsed && (
            <ul>
              {pipeline.stepExecutions.$values.map((step) => (
                <li key={step.id}>
                  Command: {step.pipelineStepCommand}
                </li>
              ))}
            </ul>
          )}
          {!pipeline.collapsed && (
            <form
              onSubmit={(event) => handleSubmit(event, pipeline.pipelineId)}
            >
              <label>
                GitUrl:
                <input
                  type="text"
                  value={gitUrl}
                  onChange={(e) => setGitUrl(e.target.value)}
                  required
                />
              </label>
              <button type="submit">Execute</button>
            </form>
          )}
        </div>
      ))}
    </div>
  );
}