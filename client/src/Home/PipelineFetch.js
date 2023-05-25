import React, { useState, useEffect } from "react";
import axios from "axios";
import "./PipelineFetch.css"; // Import the CSS file

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
                collapsed: true
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
  
  const handleExecutePipelineClick = (pipelineId) => {
    const url = `http://localhost:5017/pipeline/${pipelineId}/execute`;
    const headers = {
    "Content-Type": "application/json", // Specify the content type as JSON
  };
  const payload = {
    gitUrl: "https://github.com/msailc/DSLManagement.git" // Replace with the actual Git URL
  };
    console.log(pipelineId);
  
    axios
      .post(url,payload,{headers})
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
            {pipeline.name}
          </button>
          {!pipeline.collapsed && (
             <ul>
             {pipeline.steps.$values.map((step) => (
               <li key={step.id}>Command: {step.command}</li>
             ))}
             <button onClick={() => handleExecutePipelineClick(pipeline.id)}>button</button>
           </ul>
          )}
        </div>
      ))}
    </div>
  );
}