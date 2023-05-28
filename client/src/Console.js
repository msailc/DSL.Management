import React, { useState, useEffect, useRef } from "react";
import * as signalR from "@microsoft/signalr";
import Socket from "./Socket";

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
            // Clean up the event subscription when the component unmounts
            Socket.instance.off("ReceiveMessage", handleMessageReceived);
        };
    }, []);


    return (
        <div
            style={{
                display: "flex",
                alignItems: "center",
                justifyContent: "center",
                minHeight: "500px"
            }}
        >
            <div
                style={{
                    border: "1px solid black",
                    padding: "10px",
                    width: "80%",
                    height: "80%",
                    overflow: "auto",
                    backgroundColor: "black",
                    color: "white",
                    fontFamily: "monospace"
                }}
                ref={consoleRef}
            >
                {output}
            </div>
        </div>
    );
};

export default Console;
