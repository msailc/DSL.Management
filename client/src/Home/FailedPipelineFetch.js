import React, { useState, useEffect } from "react";
import axios from "axios";

export default function FailedPipelineFetch() {
  const [pipelines, getPipelines] = useState([]);

  useEffect(() => {
    axios
      .get("http://localhost:5017/pipeline/executions?success=false")
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
                  {step.errorMessage ? (
                    <span>Error Message: {step.errorMessage}</span>
                  ) : (
                    <span>Error Message: none</span>
                  )}
                </li>
              ))}
            </ul>
          )}
        </div>
      ))}
    </div>
  );
}
