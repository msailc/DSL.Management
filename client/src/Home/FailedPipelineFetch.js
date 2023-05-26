import React, { useState, useEffect } from "react";
import axios from "axios";
import Console from "../Console";

export default function FailedPipelineFetch() {
  const [pipelines, setPipelines] = useState([]);
  const [gitUrl, setGitUrl] = useState("");

  const handleSubmit = (event, pipelineId) => {
    event.preventDefault();
    handleExecutePipelineClick(pipelineId, gitUrl);
  };

  useEffect(() => {
    axios
      .get("http://localhost:5017/pipeline/executions?success=false")
      .then((res) => {
        console.log(res);
        const uniquePipelines = removeDuplicatePipelines(res.data.$values);
        console.log("Unique Pipelines:", uniquePipelines);
        setPipelines(uniquePipelines);
      })
      .catch((err) => console.log(err));
  }, []);

  const removeDuplicatePipelines = (pipelineExecutions) => {
    const uniquePipelines = {};
    pipelineExecutions.forEach((pipeline) => {
      if (!uniquePipelines[pipeline.pipelineId]) {
        uniquePipelines[pipeline.pipelineId] = {
          ...pipeline,
          collapsed: true,
        };
      }
    });
    return Object.values(uniquePipelines);
  };

  const togglePipeline = (id) => {
    setPipelines((prevPipelines) =>
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
      "Content-Type": "application/json",
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

  const ErrorMessage = ({ message }) => {
    const [showFullMessage, setShowFullMessage] = useState(false);

    const toggleShowFullMessage = () => {
      setShowFullMessage(!showFullMessage);
    };

    const toggleShowLessMessage = () => {
      setShowFullMessage(false);
    };

    if (message.length > 30 && !showFullMessage) {
      return (
        <div>
          <span>Error Message: {message.substring(0, 30)}...</span>
          <button onClick={toggleShowFullMessage}>Show Full Message</button>
        </div>
      );
    } else if (showFullMessage) {
      return (
        <div>
          <span>Error Message: {message}</span>
          <button onClick={toggleShowLessMessage}>Show Less</button>
        </div>
      );
    } else {
      return <span>Error Message: {message}</span>;
    }
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
                  <br />
                  <ErrorMessage message={step.errorMessage || "none"} />
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
              <div>
                <Console />
              </div>
              <button type="submit">Execute</button>
            </form>
          )}
        </div>
      ))}
    </div>
  );
}