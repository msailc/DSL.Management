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
                        ? res.data.$values.map((pipeline) => ({ ...pipeline, collapsed: true }))
                        : []
                );
            })
            .catch((err) => console.log(err));
    }, []);

    const togglePipeline = (id) => {
        getPipelines((prevPipelines) =>
            prevPipelines.map((pipeline) =>
                pipeline.id === id ? { ...pipeline, collapsed: !pipeline.collapsed } : { ...pipeline, collapsed: true }
            )
        );
    };

    return (
        <div className="pipeline-container">
            {pipelines.map((pipeline) => (
                <div key={pipeline.id} className="pipeline">
                    <button onClick={() => togglePipeline(pipeline.id)}>{pipeline.name}</button>
                    {!pipeline.collapsed && (
                        <ul>
                            <li>{pipeline.name}</li>
                            <li>Sample text</li>
                            <li>More sample text</li>
                        </ul>
                    )}
                </div>
            ))}
        </div>
    );
}
