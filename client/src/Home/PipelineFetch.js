import React, { useState, useEffect } from "react";
import axios from "axios";

export default function PipelineFetch() {
    const [pipelines, getPipelines] = useState([]);

    useEffect(() => {
        axios
            .get(`http://localhost:5017/pipeline`)
            .then((res) => {
                console.log(res);
                getPipelines(Array.isArray(res.data.$values) ? res.data.$values : []);
            })
            .catch((err) => console.log(err));
    }, []);

    return (
        <div>
            {pipelines.map((pipeline) => (
                <div key={pipeline.id}>
                    <ul>
                        <li>{pipeline.name}</li>
                    </ul>
                </div>
            ))}
        </div>
    );
}

