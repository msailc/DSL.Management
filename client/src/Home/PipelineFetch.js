import React, { useState, useEffect } from "react";
import axios from "axios";


export default function PipelineFetch() {
  const [pipelines, getPipelines] = useState([]);

  useEffect(() => {
    axios
      .get("https://jsonplaceholder.typicode.com/users")
      .then((res) => {
        console.log(res);
        getPipelines(res.data);
      })
      .catch((err) => console.log(err));
  },[]);
  return (
    <div>
      <ul>
        {pipelines.map((pipeline) => (
          <li key={pipeline.id}>{pipeline.name}</li>
        ))}
      </ul>
    </div>
  );
}
