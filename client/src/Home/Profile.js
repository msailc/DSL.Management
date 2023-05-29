
import React, {useEffect, useState} from 'react';
import './Profile.css';
import axios from "axios";
import UserPipelineFetch from "./UserPipelineFetch";

export default function Profile() {
    const user = {
        avatar: 'https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_1280.png',
    };

    const [pipelinesCount, setPipelinesCount] = useState(0);
    const [pipelineStepsCount, setPipelineStepsCount] = useState(0);
    const [successfulPipelinesCount, setSuccessfulPipelinesCount] = useState(0);
    const [unsuccessfulPipelinesCount, setUnsuccessfulPipelinesCount] = useState(0);

    const fetchData = async () => {
        try {
            const username = localStorage.getItem("username");
            const response = await axios.get(
                `http://localhost:5017/user/username/${username}`
            );
            const {
                pipelinesCount,
                pipelineStepsCount,
                successPipelinesCount,
                failedPipelinesCount
            } = response.data;
            console.log(response.data);

            setPipelinesCount(pipelinesCount);
            setPipelineStepsCount(pipelineStepsCount);
            setSuccessfulPipelinesCount(successPipelinesCount);
            setUnsuccessfulPipelinesCount(failedPipelinesCount);
        } catch (error) {
            // Handle error
            console.error("Error fetching user data:", error);
        }
    };

    useEffect(() => {
        fetchData();
    }, []);

    var username = localStorage.getItem("username")?.split("@")[0]
    var email = localStorage.getItem("username");

    return (
        <div className="profile-container">
            <div className="profile-info">
                <img className="avatar" src={user.avatar} alt="Profile Avatar" />
                <div className="user-info">
                    <div className="username">{username}</div>
                    <div className="email">{email}</div>
                </div>
            </div>
            <div className="divider"></div>
            <div className="title-container">
                <div className="title">Pipelines Created: {pipelinesCount}</div>
                <div className="title">Number of pipeline steps:{pipelineStepsCount}</div>
            </div>
            <div className="title-container">
                <div className="title">successful pipelines:{successfulPipelinesCount}</div>
                <div className="title">unsuccessful pipelines:{unsuccessfulPipelinesCount}</div>
            </div>
            <div className="divider"></div>
            <div>
            <h2>My pipelines:</h2>
            <UserPipelineFetch/>
            </div>
        </div>
    );
}