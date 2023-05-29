import React, { useState, useEffect, useRef } from "react";
import * as signalR from "@microsoft/signalr";
import Socket from "./Socket";
import "./Home/PipelineFetch.css";

const Console = () => {
    const [output, setOutput] = useState("");
    const consoleRef = useRef(null);

    useEffect(() => {
        const handleMessageReceived = (data) => {
            setOutput((prevOutput) => prevOutput + data);
            consoleRef.current.scrollTop = consoleRef.current.scrollHeight;
        };

        Socket.instance.on("ReceiveMessage", handleMessageReceived);

        return () => {
            Socket.instance.off("ReceiveMessage", handleMessageReceived);
        };
    }, []);

    return (
        <div className="console-box">
            <div className="console-content" ref={consoleRef}>
                {output}
            </div>
        </div>
    );
};

export default Console;
