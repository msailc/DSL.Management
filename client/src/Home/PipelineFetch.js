import React, { useState, useEffect } from "react";
import axios from "axios";
import "./PipelineFetch.css"; // Import the CSS file
import Console from "../Console";

export default function PipelineFetch() {
    const [pipelines, getPipelines] = useState([]);
    const [selectedPipelineId, setSelectedPipelineId] = useState(null);
    const [showConsole, setShowConsole] = useState(false);
    const [executionData, setExecutionData] = useState(null);

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
        setSelectedPipelineId((prevId) => (prevId === id ? null : id));
        getPipelines((prevPipelines) =>
            prevPipelines.map((pipeline) =>
                pipeline.id === id
                    ? { ...pipeline, collapsed: !pipeline.collapsed }
                    : { ...pipeline, collapsed: true }
            )
        );
    };

    const handleExecutePipelineClick = (pipelineId, gitUrl, deleteRepository) => {
        const deleteRepositoryQuery = deleteRepository ? 'true' : 'false';
        const url = `http://localhost:5017/pipeline/${pipelineId}/execute?deleteRepositoryAfterExecution=${deleteRepositoryQuery}`;

        const headers = {
            'Content-Type': 'application/json',
        };

        const payload = {
            gitUrl,
            userId: localStorage.getItem("userID")
        };

        setShowConsole(true);
        axios
            .post(url, payload, { headers })
            .then((res) => {
                console.log('POST request successful');
                console.log(res);
                setExecutionData(res.data); // Store the execution data
            })
            .catch((err) => {
                console.error('Error occurred during POST request:', err);
            });
    };

    const handleDeleteRepositoryChange = (pipelineId, event) => {
        const deleteRepository = event.target.checked;
        getPipelines((prevPipelines) =>
            prevPipelines.map((pipeline) =>
                pipeline.id === pipelineId ? { ...pipeline, deleteRepository } : pipeline
            )
        );
    };


    const handleGitUrlChange = (pipelineId, event) => {
        const gitUrl = event.target.value;
        getPipelines((prevPipelines) =>
            prevPipelines.map((pipeline) =>
                pipeline.id === pipelineId ? { ...pipeline, gitUrl } : pipeline
            )
        );
    };

    const fetchPipelineData = (pipelineId) => {
        const url = `http://localhost:5017/pipeline/${pipelineId}`;
        axios
            .get(url)
            .then((res) => {
                const expandedPipeline = res.data;
                const updatedPipelines = pipelines.map((pipeline) =>
                    pipeline.id === pipelineId ? { ...pipeline, ...expandedPipeline } : pipeline
                );
                getPipelines(updatedPipelines);
                console.log(res);
            })
            .catch((err) => console.log(err));
    };

    useEffect(() => {
        if (selectedPipelineId) {
            fetchPipelineData(selectedPipelineId);
        }
    }, [selectedPipelineId]);

    useEffect(() => {
        const handleResize = () => {
            const windowHeight = window.innerHeight;
            const containerElement = document.querySelector(".pipeline-container");
            const containerRect = containerElement.getBoundingClientRect();
            const containerTop = containerRect.top;
            const containerBottom = windowHeight - containerTop;
            const containerMaxHeight = containerBottom - 20; // Subtract additional spacing if needed
            containerElement.style.maxHeight = `${containerMaxHeight}px`;
        };

        handleResize(); // Initial resize

        window.addEventListener("resize", handleResize);
        return () => {
            window.removeEventListener("resize", handleResize);
        };
    }, []);

    return (
        <div className="pipeline-container-wrapper">
            <div className="pipeline-container">
                {pipelines.map((pipeline) => (
                    <div key={pipeline.id} className="pipeline">
                        <button
                            onClick={() => togglePipeline(pipeline.id)}
                            className={selectedPipelineId === pipeline.id ? "selected" : ""}
                        >
                            {pipeline.name}
                        </button>
                        {!pipeline.collapsed && (
                            <div className="pipeline-details">
                                <div className="left-side">
                                    <div>Author: {pipeline.userId}</div>

                                    Pipeline execution steps
                                    <ul>
                                        {pipeline.steps.$values.map((step) => (
                                            <li key={step.id}>{step.command}</li>
                                        ))}
                                    </ul>
                                    <form
                                        onSubmit={(event) => {
                                            event.preventDefault();
                                            handleExecutePipelineClick(pipeline.id, pipeline.gitUrl, pipeline.deleteRepository);
                                        }}
                                    >
                                        <div className="form-row">
                                            <label>
                                                Git URL:
                                                <input
                                                    type="text"
                                                    value={pipeline.gitUrl}
                                                    onChange={(event) => handleGitUrlChange(pipeline.id, event)}
                                                />
                                            </label>
                                            <button className="execute-button">Execute</button>
                                        </div>
                                        <label>
                                            Delete repository from local disk after execution:
                                            <input
                                                type="checkbox"
                                                checked={pipeline.deleteRepository}
                                                onChange={(event) => handleDeleteRepositoryChange(pipeline.id, event)}
                                            />
                                        </label>
                                    </form>
                                </div>
                                <div className="right-side">
                                    {pipeline.lastExecutions &&
                                    pipeline.lastExecutions.$values.length > 0 ? (
                                        <div>
                                            <div className="execution-results">
                                                Recent executions of this pipeline:{" "}
                                                {pipeline.lastExecutions.$values.map((execution) =>
                                                    execution.success ? "T" : "F"
                                                ).join(" ")}
                                            </div>
                                            <div className="last-commits">
                                                Last 5 commits on last execution state of repository:
                                                <ul>
                                                    {pipeline.lastExecutions.$values[0].commitTitles.$values
                                                        .slice(0, 5)
                                                        .map((title, index) => (
                                                            <li key={index}>{title}</li>
                                                        ))}
                                                </ul>
                                            </div>
                                            <div className="execution-timestamps">
                                                Start Time:{" "}
                                                {new Date(
                                                    pipeline.lastExecutions.$values[0].startTime
                                                ).toLocaleString()},{" "}
                                                End Time:{" "}
                                                {new Date(
                                                    pipeline.lastExecutions.$values[0].endTime
                                                ).toLocaleString()}
                                            </div>
                                        </div>
                                    ) : (
                                        <div>This pipeline has not been executed yet</div>
                                    )}
                                </div>
                            </div>
                        )}
                        {selectedPipelineId === pipeline.id && showConsole && (
                            <div className="console-container">
                                <div className="console-left-side">
                                    <Console />
                                </div>
                                <div className="console-right-side">
                                    Pipeline execution result
                                </div>
                            </div>
                        )}
                    </div>
                ))}
            </div>
        </div>
    );
}
